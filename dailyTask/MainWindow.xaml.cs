using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace dailyTask
{
    public partial class MainWindow : Window
    {
        private readonly MySqlConnection connection;
        private readonly List<Task> tasks;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the database connection
            string connectionString = "server=localhost;user=root;database=taskify_db;password=mypassword;";
            connection = new MySqlConnection(connectionString);

            // Initialize the list of tasks
            tasks = new List<Task>();

            // Load the tasks from the database
            LoadTasks();
        }

        private void LoadTasks()
        {
            // Clear the current list of tasks
            tasks.Clear();

            try
            {
                // Open the database connection
                connection.Open();

                // Create the SQL command
                string sql = "SELECT * FROM tasks";
                MySqlCommand command = new MySqlCommand(sql, connection);

                // Execute the command and get the data reader
                MySqlDataReader reader = command.ExecuteReader();

                // Loop through the data reader and add the tasks to the list
                while (reader.Read())
                {
                    Task task = new Task();
                    task.Id = reader.GetInt32("id");
                    task.Title = reader.GetString("title");
                    task.Description = reader.GetString("description");
                    task.Category = reader.GetString("category");
                    task.Completed = reader.GetBoolean("completed");
                    tasks.Add(task);
                }

                // Close the data reader
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close the database connection
                connection.Close();
            }

            // Display the tasks
            DisplayTasks();
        }
        private void DisplayTasks()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Tasks WHERE Category = @Category";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Category", currentCategory);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        taskList.ItemsSource = dataTable.DefaultView;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void urgent_Click(object sender, RoutedEventArgs e)
        {
            currentCategory = "Urgent";
            DisplayTasks();
        }

        private void todo_Click(object sender, RoutedEventArgs e)
        {
            currentCategory = "Todo";
            DisplayTasks();
        }

        private void wish_Click(object sender, RoutedEventArgs e)
        {
            currentCategory = "Wishlist";
            DisplayTasks();
        }

        private void shop_Click(object sender, RoutedEventArgs e)
        {
            currentCategory = "Shoping";
            DisplayTasks();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow(currentCategory);
            addTaskWindow.ShowDialog();
            DisplayTasks();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Functionality not implemented yet.");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string taskText = taskBox.Text;
            string category = ((Button)sender).Tag.ToString();

            if (!string.IsNullOrWhiteSpace(taskText))
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand();
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO tasks (task, category) VALUES (@task, @category)";
                        command.Parameters.AddWithValue("@task", taskText);
                        command.Parameters.AddWithValue("@category", category);
                        command.ExecuteNonQuery();
                    }
                    taskBox.Clear();
                    DisplayTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a task before adding.");
            }
        }
