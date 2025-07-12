using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatGPTIntegration
{
    public partial class ConnectionForm : Form
    {

        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (this.Owner is Form1 mainForm)
            {
                //mainForm.servername = txt_dbServerName.Text;
                //mainForm.username = txt_userName.Text;
                //mainForm.password = txt_pass.Text;
                //mainForm.authenticationType = com_AuthList.SelectedItem.ToString();

                List<string> databases = new List<string>();
                if (com_AuthList.SelectedItem.ToString() == "Windows Authentication")
                {
                    // build connection string for Windows Authentication
                    //mainForm.connectionString = $"Server={txt_dbServerName.Text};Integrated Security=true;Database={comDBName.SelectedItem.ToString()}";
                    mainForm.connectionString = $"Server={txt_dbServerName.Text}\\{comDBName.SelectedItem.ToString()};Integrated Security=true;TrustServerCertificate=True;";
                }
                else
                {
                    mainForm.connectionString = $"Server={txt_dbServerName.Text};User Id={txt_dbServerName.Text};Password={txt_dbServerName.Text};";
                }
                using (SqlConnection connection = new SqlConnection(mainForm.connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT name FROM sys.databases", connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            databases.Add(reader.GetString(0));
                        }
                        reader.Close();
                        connection.Close();

                        // Enable menu of Form1
                        mainForm.dbList.Enabled = true; // Enable the dbList menu
                        mainForm.databasesMenu.Enabled = true; // Enable the databases menu
                        mainForm.databasesMenu.DropDownItems.Clear(); // Clear existing items
                        int tag = 1;
                        foreach (var dbname in databases)
                        {
                            var item = new ToolStripMenuItem(dbname);

                            // Attach click event
                            item.Click += mainForm.ToolStripItem_Click;

                            // Optionally: set a tag for later use
                            item.Tag = tag++;

                            mainForm.databasesMenu.DropDownItems.Add(item);
                        }

                        //mainForm.databasesMenu.DropDownItems.AddRange(databases.Select(db => new ToolStripMenuItem(db)).ToArray());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Parent form is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            this.com_AuthList.Items.Add("Windows Authentication");
            this.com_AuthList.Items.Add("SQL Server Authentication");
        }
        

    private void GetInstalledSqlServerInstances()
    {
        List<string> instances = new List<string>();
        try
        {
            // Check the registry for SQL Server instances
            string registryPath = @"SOFTWARE\Microsoft\Microsoft SQL Server";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (key != null)
                {
                    object installedInstances = key.GetValue("InstalledInstances");
                    if (installedInstances is string[] instanceNames)
                    {
                        this.comDBName.Items.Clear(); // Clear existing items
                        this.comDBName.Items.AddRange(instanceNames);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving SQL Server instances: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void com_AuthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_AuthList.SelectedItem.ToString() == "Windows Authentication")
            {
                // Get SQL Server details if it is Windows Authentication
                string serverName = Environment.MachineName; // Use the machine name as the server name
                string userName = Environment.UserName; // Use the current logged-in user name
                GetInstalledSqlServerInstances();

                // Display the details in the respective text boxes
                txt_dbServerName.Text = serverName;
                txt_userName.Text = $"{txt_dbServerName.Text}\\{userName}";
                comDBName.Enabled = true; // Enable database name selection
                txt_userName.Enabled = false; // Disable username input
                txt_pass.Enabled = false; // Disable password input
            }
            else
            {
                txt_userName.Enabled = true;
                txt_pass.Enabled = true;
                comDBName.Enabled = false; // Disable database name selection
            }
        }
    }
}
