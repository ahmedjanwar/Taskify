using System;
using MySql.Data.MySqlClient;

namespace ConnectToMySQL
{
    class Program
    {
        static void Main(string[] args)
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
					//Console.WriteLine("ID" + " -- " + "LName " + " -- " + "FName" + " -- " + "ADDR" + " -- " + "City");
					Console.WriteLine(rdr[0] + " " + rdr[1]);
				}

				rdr.Close();

			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            conn.Close();

            Console.WriteLine("Done.");
        }
    }
}