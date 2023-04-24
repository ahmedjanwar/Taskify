using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace bindingExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            // Defining connection string
            string connectionString = "server=mariadb.vamk.fi;user=e2001332;database=e2001332_bindingExample;port=3306;password=hutCGmFRgZ9"; // Modify these

            MySqlConnection connection = new MySqlConnection(connectionString);
            List<Person> items = new List<Person>();
            try
            {
                connection.Open();
                // Creating query string
                string sql = "SELECT * FROM person";
                // New command object
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                // New reader object
                MySqlDataReader rdr = cmd.ExecuteReader();

                // While reader has rows
                while (rdr.Read())
                {

                    items.Add(new Person() { Id = rdr.GetString(0), Fname = rdr.GetString(1), Lname = rdr.GetString(2), Email = rdr.GetString(3) });

                }
                rdr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Some problems -> " + ex.Message);
            }


            person_list.ItemsSource = items;


        }
        public class Person
        {
            public string Id { get; set; }

            public string Fname { get; set; }

            public string Lname { get; set; }

            public string Email { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            System.Windows.Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }
    }
}