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
    /// Interaction logic for Edit.xaml
    /// </summary>
    ///     /// Authors Ahmed And Yossef
    /// Jobs: UI and Add Function Handled By youssef 
    ///       Rest of function adn Database creation Handled by Ahmed
    public partial class Edit : Window
	{
        string connectionString = "server=mariadb.vamk.fi;user=e2001332;database=e2001332_Taskify;port=3306;password=hutCGmFRgZ9";
        string taskIdString;
        public Edit()
		{
            taskIdString = Microsoft.VisualBasic.Interaction.InputBox("Enter the task ID to edit:", "Edit Task", "");

            // Check if user clicked Cancel or entered an empty string
            if (string.IsNullOrEmpty(taskIdString))
            {
                return;
            }

            // Parse the entered ID string to integer
            if (!int.TryParse(taskIdString, out int taskId))
            {
                MessageBox.Show("Please enter a valid integer ID.");
                return;
            }
            bool val = checkAvilablioty(taskId);
            if (val == true)
            {
                InitializeComponent();
            }
            else {
                MessageBox.Show("Error: NO id Match Found");
                   
                  };

        }
      private bool checkAvilablioty( int id)
		{
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM tasks WHERE id = {id}", conn);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Task with given id exists in the database
                        return true;
                    }
                    else
                    {
                        // Task with given id does not exist in the database
                        return false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return false;

        }
        void editcontent(int id)
		{

        }
        private void Button_Click(object sender, RoutedEventArgs e)
		{
			string title = titlebox.Text;
			string description = discriptionbox.Text;
			int categoryId = category.SelectedIndex;
            string taskid = taskIdString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("UPDATE tasks SET task_title = @title, task_description = @description, due_date = @dueDate, user_id = @userId, category_id = @categoryId WHERE id = @taskId", conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@dueDate", "2023-4-24");
                    cmd.Parameters.AddWithValue("@userId", 1);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId + 1);
                    cmd.Parameters.AddWithValue("@taskId", taskid);
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

