using ChatGPTIntegration.Helper;
using ChatGPTIntegration.Services;
using Microsoft.Data.SqlClient;
using ScintillaNET;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ChatGPTIntegration
{
    public partial class Form1 : Form
    {
        public string servername;
        public string username;
        public string password;
        public string databaseName;
        public string connectionString;
        private string newConnection;
        public string authenticationType;
        private readonly SQLHelper sqlHelper;
        private readonly GenAiServices genAiServices;
        private readonly SqlObjectExtractor sqlObjectExtractor;
        public Form1()
        {
            InitializeComponent();
            ConfigureScintilla();
            sqlHelper = new SQLHelper();
            genAiServices = new GenAiServices();
            sqlObjectExtractor = new SqlObjectExtractor();
        }

        private void ConfigureScintilla()
        {
            Scintilla scintilla = new Scintilla
            {
                Dock = DockStyle.Fill,
                //Margin = new Padding(0),
                //BorderStyle = BorderStyle.None,
                //BackColor = Color.White,
                //ForeColor = Color.Black,
                //FontQuality = FontQuality.AntiAliased
            };
            this.Controls.Add(scintilla);
            // Set the lexer to SQL
            scintilla.Lexer = Lexer.Sql;

            // Set the keywords for SQL
            scintilla.SetKeywords(0, "select from where insert into update delete join inner left right on as and or not null");

            // Configure styles
            scintilla.Styles[Style.Sql.Default].ForeColor = Color.Black;
            scintilla.Styles[Style.Sql.Comment].ForeColor = Color.Green;
            scintilla.Styles[Style.Sql.Number].ForeColor = Color.Orange;
            scintilla.Styles[Style.Sql.String].ForeColor = Color.Brown;
            scintilla.Styles[Style.Sql.Character].ForeColor = Color.Brown;
            scintilla.Styles[Style.Sql.Word].ForeColor = Color.Blue;
            scintilla.Styles[Style.Sql.Word].Bold = true;

            // Additional configuration as needed
            scintilla.Margins[0].Width = 30;
        }

        private void databaseEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionForm connectionForm = new ConnectionForm();
            connectionForm.Owner = this; // 'this' refers to the parent Form1 instance
            connectionForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Write onClick event for the menu item of databases menue list which get tables, views and stored procedures from the selected database
        //Add click event on selected menu item



        public void ToolStripItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                int itemId = (int)item.Tag;
                databaseName = item.Text;
                //connectionString = $"Server={servername};Database={databaseName};User Id={username};Password={password};";
                newConnection = connectionString + $"Database={databaseName};";

                dbList.Enabled = true; // Enable the dbList menu
                tablesMenu.Enabled = true; // Enable the tables menu
                tablesMenu.DropDownItems.Clear(); // Clear existing items
                viewsMenu.Enabled = true; // Enable the views menu
                viewsMenu.DropDownItems.Clear(); // Clear existing items
                proceduresMenu.Enabled = true; // Enable the procedures menu
                proceduresMenu.DropDownItems.Clear();
                btnApply.Enabled = true; // Enable the apply button
                btn_Generate.Enabled = true; // Enable the generate button
                btnOptimize.Enabled = true; // Enable the optimize button

                using (SqlConnection connection = new SqlConnection(newConnection))
                {
                    try
                    {
                        connection.Open();
                        // Get tables, views and stored procedures from the selected database  
                        SqlCommand command = new SqlCommand("SELECT name FROM sys.tables", connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string tableName = reader.GetString(0);
                            // Do something with the table name  
                            var tableitem = new ToolStripMenuItem(tableName);
                            tableitem.Tag = "Table";
                            tableitem.Click += selectedMenuItem_Click;
                            tablesMenu.DropDownItems.Add(tableitem);
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
                //get the views of Selcted database
                using (SqlConnection connection = new SqlConnection(newConnection))
                {
                    try
                    {
                        connection.Open();
                        // Get tables, views and stored procedures from the selected database  
                        SqlCommand command = new SqlCommand("SELECT name FROM sys.views", connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string viewName = reader.GetString(0);
                            // Do something with the table name  
                            var viewitem = new ToolStripMenuItem(viewName);
                            viewitem.Tag = "View";
                            viewitem.Click += selectedMenuItem_Click;
                            viewsMenu.DropDownItems.Add(viewitem);
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
                //get the stored procedures of Selcted database
                using (SqlConnection connection = new SqlConnection(newConnection))
                {
                    try
                    {
                        connection.Open();
                        // Get tables, views and stored procedures from the selected database  
                        SqlCommand command = new SqlCommand("SELECT name FROM sys.procedures", connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string storedProcedureName = reader.GetString(0);
                            // Do something with the table name
                            var procedureitem = new ToolStripMenuItem(storedProcedureName);
                            procedureitem.Tag = "Proc";
                            procedureitem.Click += selectedMenuItem_Click;
                            proceduresMenu.DropDownItems.Add(procedureitem);
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

        private void selectedMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                string selectedItemText = menuItem.Text;

                // Get the selected item's SQL schema  
                string selectedItemSchema = string.Empty;
                string SchemaName = sqlHelper.GetSchema(newConnection, selectedItemText);


                if (menuItem.Tag.ToString() == "Table")
                {
                    // Get the script of the selected table
                    txtSQLOutput.Text = sqlHelper.GenerateTableScript(newConnection, SchemaName, selectedItemText);
                }
                else if (menuItem.Tag.ToString() == "View")
                {
                    // Get the script of the selected view
                    txtSQLOutput.Text = sqlHelper.GenerateSelctedScript(newConnection, SchemaName, selectedItemText, "ViewDefinition");
                }
                else if (menuItem.Tag.ToString() == "Proc")
                {
                    // Get the script of the selected stored procedure
                    txtSQLOutput.Text = sqlHelper.GenerateSelctedScript(newConnection, SchemaName, selectedItemText, "ProcedureDefinition");
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            string fullQuery = string.Empty;
            var procedureName = sqlObjectExtractor.GetObjectNameFromQuery(txtSQLOutput.Text);
            if (txtSQLOutput.Text.Contains("PROCEDURE", StringComparison.OrdinalIgnoreCase))
            {
                fullQuery = $"EXEC {procedureName.ObjectName}";
            }
            else if (txtSQLOutput.Text.Contains("VIEW", StringComparison.OrdinalIgnoreCase))
            {
                fullQuery = $"SELECT * FROM {procedureName.ObjectName}";
            }
            if (string.IsNullOrEmpty(fullQuery))
            {
                MessageBox.Show("Please select a valid SQL query.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtNonOptimize.Text = sqlHelper.ExecuteAndGetStatistics(
                newConnection,
                fullQuery
            );
            if (!string.IsNullOrEmpty(txtSelectedSuggession.Text))
            {
                bool isOptimized = sqlHelper.IsApllyProcView(
                    newConnection,
                    txtSelectedSuggession.Text);

                if (isOptimized)
                {
                    txtResultAnalytics.Text = sqlHelper.ExecuteAndGetStatistics(
                        newConnection,
                        fullQuery);

                    sqlHelper.IsApllyProcView(
                        newConnection,
                        txtSQLOutput.Text);
                }
                else
                {
                    MessageBox.Show("Procedure or View does not optimized.\nSomething went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a suggestion from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
        

        private void btnApply_Click(object sender, EventArgs e)
        {
            //string substr = txtGPToutPut.Text.Substring(txtGPToutPut.Text.IndexOf('`') + 6);
            //substr = substr.Substring(0, substr.IndexOf('`'));
            if(sqlHelper.IsApllyProcView(
                newConnection,
                txtSelectedSuggession.Text))
            {
                MessageBox.Show($"Optimized query aplly successfully. {txtResultAnalytics.Text}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void btnOptimize_ClickAsync(object sender, EventArgs e)
        {
            string prompt = txtSQLOutput.Text;

            if (string.IsNullOrWhiteSpace(prompt))
            {
                MessageBox.Show("Please provide a valid SQL query or prompt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            loaderPanel.Visible = true;
            string optimizedQuery = await genAiServices.GenerateOptimizedQueryAsync(prompt);
            loaderPanel.Visible = false;
            if (!string.IsNullOrEmpty(optimizedQuery))
            {
                txtGPToutPut.Text = optimizedQuery;
                var matches = Regex.Matches(txtGPToutPut.Text, "```(.*?)```", RegexOptions.Singleline);
                suggessionList.Items.Clear();
                //List<string> extractedQueries = new List<string>();
                foreach (Match match in matches)
                {
                    string extractedQuery = match.Groups[1].Value.Trim();
                    suggessionList.Items.Add(Regex.Replace(extractedQuery, "sql", ""));
                }
                MessageBox.Show("Optimized query generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to generate optimized query.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suggessionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suggessionList.SelectedItem != null)
            {
                // Set the selected item to the txtSelectedSuggession TextBox
                txtSelectedSuggession.Text = suggessionList.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Please select a suggestion from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
