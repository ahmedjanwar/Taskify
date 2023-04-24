using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Taskify
{
    /// <summary>
    /// Interaction logic for add.xaml
    /// </summary>
    ///     /// Authors Ahmed And Yossef
    /// Jobs: UI and Add Function Handled By youssef 
    ///       Rest of function adn Database creation Handled by Ahmed
    public partial class add : Window
	{
        // Handled by Youssef
        string connectionString = "server=mariadb.vamk.fi;user=e2001332;database=e2001332_Taskify;port=3306;password=hutCGmFRgZ9";
        public add()
		{
			InitializeComponent();
            
        }

		private void AddData_Click(object sender, RoutedEventArgs e)
		{
            // Get the data entered by the user
            string title = titleTextBox.Text;
            string description = descriptionTextBox.Text;
            int categoryId = category.SelectedIndex;
           
            // Add the data to the database
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO tasks (task_title, task_description, due_date, user_id, category_id) VALUES (@title, @description, @dueDate, @userId, @categoryId)", conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@dueDate", "2023-4-24");
                    cmd.Parameters.AddWithValue("@userId", 1);
                   cmd.Parameters.AddWithValue("@categoryId", categoryId+1);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            this.Close();
        }


	}
}
