using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

namespace groupSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = "server=mariadb.vamk.fi;user=e2001332;database=e2001332_Taskify;port=3306;password=hutCGmFRgZ9"; // Modify these
        public MainWindow()
        {
            InitializeComponent();
            

            MySqlConnection connection = new MySqlConnection(connectionString);
            List<Person1> items = new List<Person1>();
            List<Person2> it = new List<Person2>();
            try
            {
                connection.Open();
                // Creating query string
                string sql = "SELECT id,task_title,task_description FROM tasks";
                // New command object
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                // New reader object
                MySqlDataReader rdr = cmd.ExecuteReader();

                // While reader has rows
                while (rdr.Read())
                {
                    
                        items.Add(new Person1() { id = "No: "+rdr.GetString(0)+"\n" + "Title: " + rdr.GetString(1) + "\n" + "Discription: "+rdr.GetString(2) });
                  


                }
                rdr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Some problems -> " + ex.Message);
            }

            someone.ItemsSource = items;
            //someone.ItemsSource = it;
            //////TEST
            


        }
        public class Person1
        {
            public string id { get; set; }

            public string task_title { get; set; }

            public string task_description { get; set; }

           
        }
        public class Person2
        {
           

            public string Fname { get; set; }

          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

		private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void someone_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void someone_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{

		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
            taskhide.Visibility = Visibility.Collapsed;

        }
        
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
		{
            taskhide.Visibility = Visibility.Visible;
            List<string> GetNamesFromDatabase()
            {
                List<string> names = new List<string>();

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM tasks", conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader.GetString(0);
                                string title = reader.GetString(1);
                                string description = reader.GetString(2);
                                
                                names.Add(id );
                                names.Add(title);
                                names.Add(description);
                            }
                            reader.Close();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return names;
            }

            // Get the names from the database
            List<string> sample = GetNamesFromDatabase();

            // Create a new Grid control for each name
            foreach (string name in sample)
            {
               
               for (int i = 0; i < sample.Count; i++)
				{
                    // Create a new Grid control
                    Grid nameGrid = new Grid();
                   
                    if (i >= 3)
                    {
                        int a = i - 3;
                        nameGrid.Margin = new Thickness((19 + (a * 280)), (196), (568 - (a * 280)), (10));
                        
                        
                    }
                    else
                    {
                        nameGrid.Margin = new Thickness((19 + (i * 280)), (19), (568 - (i * 280)), (180));
                        // Create the "X" button
                       
                    }


                    // Add a button to the grid
                    
                    

                    // Add the rectangle and buttons to the main grid
                   
                   

                    // Create a new ListView control
                    ListView listView = new ListView();
                    listView.Margin = new Thickness(10);
                    listView.SelectionChanged += someone_SelectionChanged;

                    // Create a new GridViewColumn for the ListView
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = "some name";
                    gridViewColumn.Width = 100;
                    gridViewColumn.DisplayMemberBinding = new Binding("id");
                    gridViewColumn.DisplayMemberBinding = new Binding("task_title");
                    gridViewColumn.DisplayMemberBinding = new Binding("task_description");
                    gridViewColumn.DisplayMemberBinding = new Binding("category_id");




                    // Add the GridViewColumn to the GridView
                    GridView gridView = new GridView();
                    gridView.Columns.Add(gridViewColumn);
                    



                    // Set the GridView as the ListView's view
                    listView.View = gridView;

                    // Add the ListView to the Grid
                    nameGrid.Children.Add(listView);

                    // Add the Grid to the main Grid
                    mainGrid0.Children.Add(nameGrid);

                    // Populate the ListView with data
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tasks WHERE id = {i+1}", conn);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read()) { 

								string id = r.GetString(0);
                                string title = r.GetString(1);
                                string description = r.GetString(2);

                                // Create a new ListViewItem object
                                ListViewItem item = new ListViewItem();
                                item.Content = new Person1()
                                {
                                    id = id,
                                    task_title = title,
                                    task_description = description,

                                };

                                // Add the ListViewItem object to the ListView
                                listView.Items.Add(item);
                            }
                            r.Close();
                        }
                    }
                }  
               
            }
            //end
        }
	}
}
