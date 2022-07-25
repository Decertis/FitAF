using System;
using MySql.Data.MySqlClient;
using System.Net;

namespace WebShop.MVC.Models
{
    public abstract class Client
    {
        public int Id;
        public string Ip;
        public bool DatabaseContainsClient;

        MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;UserId=root;Password=55665566;Database=fitAF_db;");

        protected Client(string client_ip)
        {
            Ip = client_ip.Split(':')[0];
            DatabaseContainsClient = IsDatabaseCointainsClient();
            Console.WriteLine("Client exist in database : " + DatabaseContainsClient);
            if (!DatabaseContainsClient)
                InsertClientIpIntoDatabase();

        }
        private byte[] GetBinary(string IPAdresa)
        {
            
            IPAddress IPDec = IPAddress.Parse(IPAdresa);
            byte[] IPByte = IPDec.GetAddressBytes();
            return IPByte;

        }
        public void ConsoleWriteClients()
        {
            using (connection)
            {
                connection.Open();

                int row_count = Convert.ToInt32(new MySqlCommand("SELECT COUNT(ClientID) FROM Clients", connection).ExecuteScalar());

                Console.WriteLine("Row amount : " + row_count);

                var command = new MySqlCommand("SELECT * FROM Clients", connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = reader.FieldCount - 1; i > 0; i--)
                        Console.WriteLine(reader.GetName(i) + " : " + reader[i]);
                    Console.WriteLine();
                }
                connection.Close();
            }
        }

        public void InsertClientIpIntoDatabase()
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = new MySqlCommand($"INSERT INTO Clients(IpAddress) VALUES(@binary_ip)",connection);
                    command.Parameters.Add("@binary_ip", MySqlDbType.Blob).Value = GetBinary(Ip);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (MySqlException) {
                connection.Close();
              }
        }

        public bool IsDatabaseCointainsClient()
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    var command = new MySqlCommand($"SELECT * FROM Clients WHERE IpAddress = @binary_ip;",connection);
                    command.Parameters.Add("@binary_ip", MySqlDbType.Blob).Value = GetBinary(Ip);

                    if (command.ExecuteReader().HasRows)
                    {
                        connection.Close();
                        return true;
                    }
                    connection.Close();
                   return false;
                }
            }
            catch (MySqlException)
            {
                connection.Close();
                return false;
            }
        }
    }
}