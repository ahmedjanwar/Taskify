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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        string connectionString = "server=mariadb.vamk.fi;user=e2001332;database=e2001332_Taskify;port=3306;password=hutCGmFRgZ9";
        public MainWindow()
		{
			InitializeComponent();
            LoadData();
            
        }
        void LoadData()
		{
            mainGrid0.Children.Clear();
            //on load
            List<string> GetNamesFromDatabase()
            {
                List<string> data = new List<string>();
                //List<data> items = new List<data>();
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
                                string name = "No: " + reader.GetString(0) + "\n" + "Title: " + reader.GetString(1) + "\n" + "" + reader.GetString(2);
                                data.Add(name);
                            }
                            reader.Close();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return data;
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
                    if (i >= 4)
                    {
                        int a = i - 4;
                        nameGrid.Margin = new Thickness((10 + (a * 180)), (220), (568 - (a * 180)), (5));
                    }
                    else nameGrid.Margin = new Thickness((10 + (i * 180)), (19), (568 - (i * 180)), (196));

                    // Add a button to the grid

                    // Create a new ListView control
                    ListView listView = new ListView();
                    listView.Margin = new Thickness(10);
                    listView.SelectionChanged += someone_SelectionChanged;

                    // Create a new GridViewColumn for the ListView
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = "Task Detail";
                    gridViewColumn.Width = 160;
                    gridViewColumn.DisplayMemberBinding = new Binding("id");




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

                        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tasks WHERE id = {i + 1}", conn);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //string fname = r.GetString(1);

                                // Create a new ListViewItem object
                                ListViewItem item = new ListViewItem();
                                item.Content = new datas() { id = "No: " + r.GetString(0) + "\n" + "Title: " + r.GetString(1) + "\n" + "" + r.GetString(2) };

                                listView.Items.Add(item);
                            }
                            r.Close();
                        }
                    }
                }

            }
            //end on load
        }
        private void Button_Click(object sender, RoutedEventArgs e)
		{
            //urgent
            string[] myid = new string[15];
            mainGrid0.Children.Clear();

            // Remove the Grid itself from its parent container
            //nameGrid.Children.Remove(mainGrid0);

            List<string> GetNamesFromDatabase()
            {
                List<string> data = new List<string>();
                //List<data> items = new List<data>();
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM tasks WHERE category_id = 1", conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int y=0;
                            while (reader.Read())
                            {
                                string name = "No: " + reader.GetString(0) + "\n" + "Title: " + reader.GetString(1) + "\n" + "" + reader.GetString(2);
                                data.Add(name);
                               // for (int y = 0; y <= myid.Length; y++)
                               // {
                                    myid[y] = reader.GetString(0);
                                //}
                                y++;
                            }
                            reader.Close();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return data;
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
                    if (i >= 4)
                    {
                        int a = i - 4;
                        nameGrid.Margin = new Thickness((10 + (a * 180)), (220), (568 - (a * 180)), (5));
                    }
                    else nameGrid.Margin = new Thickness((10 + (i * 180)), (19), (568 - (i * 180)), (196));

                    // Add a button to the grid

                    // Create a new ListView control
                    ListView listView = new ListView();
                    listView.Margin = new Thickness(10);
                    listView.SelectionChanged += someone_SelectionChanged;

                    // Create a new GridViewColumn for the ListView
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = "Task Detail";
                    gridViewColumn.Width = 160;
                    gridViewColumn.DisplayMemberBinding = new Binding("id");




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

                        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tasks WHERE id = {myid[i]} and category_id = 1", conn);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //string fname = r.GetString(1);

                                // Create a new ListViewItem object
                                ListViewItem item = new ListViewItem();
                                item.Content = new datas() { id = "No: " + r.GetString(0) + "\n" + "Title: " + r.GetString(1) + "\n" + "" + r.GetString(2) };

                                listView.Items.Add(item);
                            }
                            r.Close();
                        }
                    }
                }

            }
        }
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
            Window1 loginWindow = new Window1();
            loginWindow.Show();
            this.Close();
        }
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
            mainGrid0.Children.Clear();
        }
        private void someone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public class datas
        {
            public string id { get; set; }

            public string task_title { get; set; }

            public string task_description { get; set; }


        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
		{
            add addDataWindow = new add();
            addDataWindow.ShowDialog();

            mainGrid0.Children.Clear();
            MainWindow newWindow = new MainWindow();
            System.Windows.Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();

        }

		private void refreshbtn_Click(object sender, RoutedEventArgs e)
		{
            mainGrid0.Children.Clear();
            MainWindow newWindow = new MainWindow();
            System.Windows.Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }

		private void Button_Click1(object sender, RoutedEventArgs e)
		{
            mainGrid0.Children.Clear();
            //todo
            string[] myid = new string[15];
            mainGrid0.Children.Clear();

            // Remove the Grid itself from its parent container
            //nameGrid.Children.Remove(mainGrid0);

            List<string> GetNamesFromDatabase()
            {
                List<string> data = new List<string>();
                //List<data> items = new List<data>();
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM tasks WHERE category_id = 2", conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int y = 0;
                            while (reader.Read())
                            {
                                string name = "No: " + reader.GetString(0) + "\n" + "Title: " + reader.GetString(1) + "\n" + "" + reader.GetString(2);
                                data.Add(name);
                                // for (int y = 0; y <= myid.Length; y++)
                                // {
                                myid[y] = reader.GetString(0);
                                //}
                                y++;
                            }
                            reader.Close();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return data;
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
                    if (i >= 4)
                    {
                        int a = i - 4;
                        nameGrid.Margin = new Thickness((10 + (a * 180)), (220), (568 - (a * 180)), (5));
                    }
                    else nameGrid.Margin = new Thickness((10 + (i * 180)), (19), (568 - (i * 180)), (196));

                    // Add a button to the grid

                    // Create a new ListView control
                    ListView listView = new ListView();
                    listView.Margin = new Thickness(10);
                    listView.SelectionChanged += someone_SelectionChanged;

                    // Create a new GridViewColumn for the ListView
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = "Task Detail";
                    gridViewColumn.Width = 160;
                    gridViewColumn.DisplayMemberBinding = new Binding("id");




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

                        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tasks WHERE id = {myid[i]} and category_id = 2", conn);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //string fname = r.GetString(1);

                                // Create a new ListViewItem object
                                ListViewItem item = new ListViewItem();
                                item.Content = new datas() { id = "No: " + r.GetString(0) + "\n" + "Title: " + r.GetString(1) + "\n" + "" + r.GetString(2) };

                                listView.Items.Add(item);
                            }
                            r.Close();
                        }
                    }
                }

            }
            //end todo
        }

		private void Button_Click2(object sender, RoutedEventArgs e)
		{
            mainGrid0.Children.Clear();
            //wish list
            string[] myid = new string[15];
            mainGrid0.Children.Clear();

            // Remove the Grid itself from its parent container
            //nameGrid.Children.Remove(mainGrid0);

            List<string> GetNamesFromDatabase()
            {
                List<string> data = new List<string>();
                //List<data> items = new List<data>();
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM tasks WHERE category_id = 3", conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int y = 0;
                            while (reader.Read())
                            {
                                string name = "No: " + reader.GetString(0) + "\n" + "Title: " + reader.GetString(1) + "\n" + "" + reader.GetString(2);
                                data.Add(name);
                                // for (int y = 0; y <= myid.Length; y++)
                                // {
                                myid[y] = reader.GetString(0);
                                //}
                                y++;
                            }
                            reader.Close();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return data;
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
                    if (i >= 4)
                    {
                        int a = i - 4;
                        nameGrid.Margin = new Thickness((10 + (a * 180)), (220), (568 - (a * 180)), (5));
                    }
                    else nameGrid.Margin = new Thickness((10 + (i * 180)), (19), (568 - (i * 180)), (196));

                    // Add a button to the grid

                    // Create a new ListView control
                    ListView listView = new ListView();
                    listView.Margin = new Thickness(10);
                    listView.SelectionChanged += someone_SelectionChanged;

                    // Create a new GridViewColumn for the ListView
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = "Task Detail";
                    gridViewColumn.Width = 160;
                    gridViewColumn.DisplayMemberBinding = new Binding("id");




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

                        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tasks WHERE id = {myid[i]} and category_id = 3", conn);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //string fname = r.GetString(1);

                                // Create a new ListViewItem object
                                ListViewItem item = new ListViewItem();
                                item.Content = new datas() { id = "No: " + r.GetString(0) + "\n" + "Title: " + r.GetString(1) + "\n" + "" + r.GetString(2) };

                                listView.Items.Add(item);
                            }
                            r.Close();
                        }
                    }
                }

            }
            //end wish list
        }

		private void Button_Click3(object sender, RoutedEventArgs e)
		{
            mainGrid0.Children.Clear();
            //shoping
            string[] myid = new string[15];
            mainGrid0.Children.Clear();

            // Remove the Grid itself from its parent container
            //nameGrid.Children.Remove(mainGrid0);

            List<string> GetNamesFromDatabase()
            {
                List<string> data = new List<string>();
                //List<data> items = new List<data>();
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM tasks WHERE category_id = 4", conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int y = 0;
                            while (reader.Read())
                            {
                                string name = "No: " + reader.GetString(0) + "\n" + "Title: " + reader.GetString(1) + "\n" + "" + reader.GetString(2);
                                data.Add(name);
                                // for (int y = 0; y <= myid.Length; y++)
                                // {
                                myid[y] = reader.GetString(0);
                                //}
                                y++;
                            }
                            reader.Close();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return data;
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
                    if (i >= 4)
                    {
                        int a = i - 4;
                        nameGrid.Margin = new Thickness((10 + (a * 180)), (220), (568 - (a * 180)), (5));
                    }
                    else nameGrid.Margin = new Thickness((10 + (i * 180)), (19), (568 - (i * 180)), (196));

                    // Add a button to the grid

                    // Create a new ListView control
                    ListView listView = new ListView();
                    listView.Margin = new Thickness(10);
                    listView.SelectionChanged += someone_SelectionChanged;

                    // Create a new GridViewColumn for the ListView
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = "Task Detail";
                    gridViewColumn.Width = 160;
                    gridViewColumn.DisplayMemberBinding = new Binding("id");




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

                        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tasks WHERE id = {myid[i]} and category_id = 4", conn);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //string fname = r.GetString(1);

                                // Create a new ListViewItem object
                                ListViewItem item = new ListViewItem();
                                item.Content = new datas() { id = "No: " + r.GetString(0) + "\n" + "Title: " + r.GetString(1) + "\n" + "" + r.GetString(2) };

                                listView.Items.Add(item);
                            }
                            r.Close();
                        }
                    }
                }

            }
            //end shoping
        }

		private void Button_Click_4(object sender, RoutedEventArgs e)
		{
            string taskIdString = Microsoft.VisualBasic.Interaction.InputBox("Enter the taskId to delete:", "Delete Task", "");
            if (taskIdString == "")
            {
                MessageBox.Show("Input can not be empty.");
			}
			else
			{
                int taskId;
                if (int.TryParse(taskIdString, out taskId))
                {
                    // Delete the task from the database
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("DELETE FROM tasks WHERE id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", taskId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Task deleted successfully.");

                            // Update the IDs of the remaining tasks
                            cmd = new MySqlCommand("SELECT * FROM tasks ORDER BY id", conn);
                            MySqlDataReader reader = cmd.ExecuteReader();

                            //int newId = 1;
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");

                                updateid(id);
                            }

                            reader.Close();

                            updateidincrement();
                            // TODO: Refresh the task list in the UI
                            mainGrid0.Children.Clear();
                            MainWindow newWindow = new MainWindow();
                            System.Windows.Application.Current.MainWindow = newWindow;
                            newWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Task not found.");
                        }
                    }
                   
                }
                else { MessageBox.Show("Input can not be other than int."); }
               

            }
            void updateid(int id)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
				{
                    int newId = 1;
                    if (id != newId)
                    {
                        conn.Open();
                        // Update the ID of the task in the database
                        MySqlCommand updateCmd = new MySqlCommand("UPDATE tasks SET id = @newId WHERE id = @id", conn);
                        updateCmd.Parameters.AddWithValue("@newId", newId);
                        updateCmd.Parameters.AddWithValue("@id", id);
                        updateCmd.ExecuteNonQuery();
                    }

                    newId++;
                    conn.Close();

                }
  
            }

            void updateidincrement()
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    
                   
                        conn.Open();
                        // Update the ID of the task in the database
                        MySqlCommand updateCmd = new MySqlCommand("ALTER TABLE tasks AUTO_INCREMENT = 1;", conn);
                        updateCmd.ExecuteNonQuery();     
                        conn.Close();

                }

            }

        }

		private void Button_Click_5(object sender, RoutedEventArgs e)
		{
            

            // Call a method to open the edit window with the task data
            Edit editdata = new Edit();
            editdata.ShowDialog();

            mainGrid0.Children.Clear();
            MainWindow newWindow = new MainWindow();
            System.Windows.Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }

	}
}
