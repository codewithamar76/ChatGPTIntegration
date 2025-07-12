using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatGPTIntegration.Helper
{
    public class SQLHelper
    {
        public string GenerateTableScript(string connectionString, string schemaName, string tableName)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            var sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE [{schemaName}].[{tableName}] (");
            var columnLines = new List<string>();

            // ——— Column + Defaults
            string columnQuery = @"SELECT 
    col.COLUMN_NAME,
    col.DATA_TYPE,
    col.CHARACTER_MAXIMUM_LENGTH,
    col.IS_NULLABLE,
    dc.definition AS DEFAULT_VALUE
FROM INFORMATION_SCHEMA.COLUMNS col
LEFT JOIN sys.columns sc ON sc.name = col.COLUMN_NAME
    AND sc.object_id = OBJECT_ID(@SchemaName + '.' + @TableName)
LEFT JOIN sys.default_constraints dc ON dc.parent_column_id = sc.column_id
    AND dc.parent_object_id = sc.object_id
WHERE col.TABLE_NAME = @TableName AND col.TABLE_SCHEMA = @SchemaName
ORDER BY col.ORDINAL_POSITION"; // Corrected column name



            using (SqlCommand cmd = new SqlCommand(columnQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@SchemaName", schemaName);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader["COLUMN_NAME"].ToString();
                    string type = reader["DATA_TYPE"].ToString();
                    string nullable = (reader["IS_NULLABLE"].ToString() == "NO") ? "NOT NULL" : "NULL";
                    string dataType = type;

                    if (reader["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value &&
                        int.TryParse(reader["CHARACTER_MAXIMUM_LENGTH"].ToString(), out int len))
                    {
                        dataType += len == -1 ? "(MAX)" : $"({len})";
                    }

                    string defaultVal = reader["DEFAULT_VALUE"] != DBNull.Value
                        ? $" DEFAULT {reader["DEFAULT_VALUE"]}"
                        : "";

                    columnLines.Add($"    [{name}] {dataType} {nullable}{defaultVal}");
                }
            }

            // Primary keys, foreign keys, constraints — reuse the rest of the logic from the previous answer here
            // PRIMARY KEY
            var pkQuery = @"
            SELECT k.COLUMN_NAME
            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t
            JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k ON t.CONSTRAINT_NAME = k.CONSTRAINT_NAME
            WHERE t.TABLE_NAME = @TableName AND t.TABLE_SCHEMA = @SchemaName AND t.CONSTRAINT_TYPE = 'PRIMARY KEY'
            ORDER BY k.ORDINAL_POSITION";

            using (SqlCommand cmd = new SqlCommand(pkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@SchemaName", schemaName);

                var pkColumns = new List<string>();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    pkColumns.Add($"[{reader["COLUMN_NAME"]}]");

                if (pkColumns.Count > 0)
                    columnLines.Add($"    CONSTRAINT [PK_{tableName}] PRIMARY KEY ({string.Join(", ", pkColumns)})");
            }

            // UNIQUE CONSTRAINTS
            var uqQuery = @"
            SELECT kc.COLUMN_NAME, tc.CONSTRAINT_NAME
            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
            JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kc ON kc.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
            WHERE tc.CONSTRAINT_TYPE = 'UNIQUE' AND tc.TABLE_NAME = @TableName AND tc.TABLE_SCHEMA = @SchemaName";

            using (SqlCommand cmd = new SqlCommand(uqQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@SchemaName", schemaName);
                using SqlDataReader reader = cmd.ExecuteReader();

                var uniqueConstraints = new Dictionary<string, List<string>>();
                while (reader.Read())
                {
                    string name = reader["CONSTRAINT_NAME"].ToString();
                    string column = reader["COLUMN_NAME"].ToString();

                    if (!uniqueConstraints.ContainsKey(name))
                        uniqueConstraints[name] = new List<string>();

                    uniqueConstraints[name].Add($"[{column}]");
                }

                foreach (var uc in uniqueConstraints)
                    columnLines.Add($"    CONSTRAINT [{uc.Key}] UNIQUE ({string.Join(", ", uc.Value)})");
            }

            // CHECK CONSTRAINTS
            var checkQuery = @"
            SELECT cc.name AS ConstraintName, cc.definition
            FROM sys.check_constraints cc
            INNER JOIN sys.tables t ON cc.parent_object_id = t.object_id
            WHERE t.name = @TableName";

            using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["ConstraintName"].ToString();
                    string def = reader["definition"].ToString();
                    columnLines.Add($"   CONSTRAINT [{name}] CHECK {def}");
                }
            }

            // FOREIGN KEYS
            var fkQuery = @"
            SELECT 
                fk.name AS FK_Name,
                c.name AS ColumnName,
                rt.name AS RefTableName,
                rc.name AS RefColumnName
            FROM sys.foreign_keys fk
            JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
            JOIN sys.columns c ON fkc.parent_column_id = c.column_id AND c.object_id = fkc.parent_object_id
            JOIN sys.columns rc ON fkc.referenced_column_id = rc.column_id AND rc.object_id = fkc.referenced_object_id
            JOIN sys.tables t ON t.object_id = fkc.parent_object_id
            JOIN sys.tables rt ON rt.object_id = fkc.referenced_object_id
            WHERE t.name = @TableName";

            using (SqlCommand cmd = new SqlCommand(fkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string fkName = reader["FK_Name"].ToString();
                    string columnName = reader["ColumnName"].ToString();
                    string refTable = reader["RefTableName"].ToString();
                    string refColumn = reader["RefColumnName"].ToString();

                    columnLines.Add($"    CONSTRAINT [{fkName}] FOREIGN KEY ([{columnName}]) REFERENCES [{refTable}]([{refColumn}])");
                }
            }
            // Append each generated script to columnLines or append to sb after

            sb.AppendLine(string.Join(",\n", columnLines));
            sb.AppendLine(");");

            // Optionally, include indexes after the CREATE TABLE
            // INDEXES (non-clustered only)
            var indexQuery = @"
            SELECT ind.name AS IndexName, col.name AS ColumnName
            FROM sys.indexes ind 
            JOIN sys.index_columns ic ON ind.object_id = ic.object_id AND ind.index_id = ic.index_id
            JOIN sys.columns col ON ic.object_id = col.object_id AND ic.column_id = col.column_id
            JOIN sys.tables t ON ind.object_id = t.object_id
            WHERE t.name = @TableName AND ind.is_primary_key = 0 AND ind.is_unique_constraint = 0 AND ind.type_desc = 'NONCLUSTERED'";

            using (SqlCommand cmd = new SqlCommand(indexQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                using SqlDataReader reader = cmd.ExecuteReader();

                var indexes = new Dictionary<string, List<string>>();
                while (reader.Read())
                {
                    string idxName = reader["IndexName"].ToString();
                    string column = reader["ColumnName"].ToString();

                    if (!indexes.ContainsKey(idxName))
                        indexes[idxName] = new List<string>();

                    indexes[idxName].Add($"[{column}]");
                }

                foreach (var index in indexes)
                {
                    sb.AppendLine($"\nCREATE NONCLUSTERED INDEX [{index.Key}] ON [{schemaName}].[{tableName}] ({string.Join(", ", index.Value)});");
                }
            }
            sb.AppendLine(); // Add any indexes as additional lines

            return sb.ToString();
        }

        public string GenerateSelctedScript(string connectionString, string schemaName, string SelectedItemName, string Defination)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            var sb = new StringBuilder();

            // Query to get the definition of the view  
            string finalValue = $"{schemaName}.{SelectedItemName}";
            string viewQuery = $@"SELECT OBJECT_DEFINITION(OBJECT_ID('{finalValue}')) AS {Defination}";

            using (SqlCommand cmd = new SqlCommand(viewQuery, conn))
            {
                //cmd.Parameters.AddWithValue("@FinalValue", finalValue);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    sb.AppendLine(result.ToString());
                }
                else
                {
                    sb.AppendLine($"-- {schemaName}.{SelectedItemName} not found or has no definition.");
                }
            }

            return sb.ToString();
        }

        public string GetSchema(string connectionString, string selectedItemText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Get the schema of the selected item (table, view, or stored procedure)  

                    SqlCommand command = new SqlCommand(
                       "SELECT SCHEMA_NAME(schema_id) AS SchemaName FROM sys.objects WHERE name = @ObjectName",
                       connection);

                    //SqlCommand command = new SqlCommand("SELECT OBJECT_DEFINITION(OBJECT_ID(@TableName)) AS TableQuery",
                    //    connection);

                    command.Parameters.AddWithValue("@ObjectName", selectedItemText);
                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : "Schema not found";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public bool IsApllyProcView(string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Replace "CREATE" with "ALTER" in the query to handle existing views or procedures  
                    query = Regex.Replace(query, @"\bCREATE\b", "ALTER", RegexOptions.IgnoreCase);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed  
                    return false;
                }
            }
        }

        public string ExecuteAndGetStatistics(string connectionString, string sqlStatement)
        {
            var statsOutput = new StringBuilder();

            using (var connection = new SqlConnection(connectionString))
            {
                // Capture execution stats (SET STATISTICS IO/TIME go to InfoMessage)
                connection.InfoMessage += (sender, args) =>
                {
                    statsOutput.AppendLine(args.Message);
                };

                try
                {
                    connection.Open();

                    string fullQuery = $@"
                    SET STATISTICS IO ON;
                    SET STATISTICS TIME ON;
                    {sqlStatement};
                    SET STATISTICS IO OFF;
                    SET STATISTICS TIME OFF;
                ";

                    using (var command = new SqlCommand(fullQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    return statsOutput.Length > 0
                        ? statsOutput.ToString()
                        : "Executed successfully, but no statistics were returned.";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }

    }
}
