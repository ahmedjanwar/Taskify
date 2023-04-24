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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Taskify
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	///     /// Authors Ahmed And Yossef
	/// Jobs: UI and Add Function Handled By youssef 
	///       Rest of function adn Database creation Handled by Ahmed
	public partial class Window1 : Window
	{
		
		public Window1()
		{
			InitializeComponent();
		}


		private void button1_Click(object sender, RoutedEventArgs e)
		{
			string connStr = "server=mariadb.vamk.fi;user=e2001332;database=e2001332_Taskify;port=3306;password=hutCGmFRgZ9";
			MySqlConnection conn = new MySqlConnection(connStr);
			try
			{
				//Console.WriteLine("Connecting to MySQL...");

				conn.Open();
				//Console.WriteLine("insert tabel data:");
				//string insert= Console.ReadLine();

				//string sql = "INSERT INTO Persons (PersonID,LASTNAME,FirstName,Address,City) VALUES (1,'Anwar','Ahmed','palosari','Vaasa');";
				string sql = "SELECT name,password FROM users";
				MySqlCommand cmd = new MySqlCommand(sql, conn);

				MySqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					string user = textBoxEmail.Text;
					string pass = passwordBox.Text;
					if (rdr[0].ToString() == user && rdr[1].ToString() == pass)
					{
						MainWindow mw = new MainWindow();
						mw.Show();
						this.Close();
					}
					else errormessage.Text = "Wrong Username or Password!";
				}

				rdr.Close();
				
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }

			conn.Close();

			

		}
	}
}
