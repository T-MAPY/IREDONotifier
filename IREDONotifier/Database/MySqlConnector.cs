using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IREDONotifier.Model;
using MySql.Data.MySqlClient;
using NLog;
using System.Configuration;

namespace IREDONotifier.Database
{
    public class MySqlConnector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ObservableCollection<User> GetAllUsers()
        {
            var users = new ObservableCollection<User>();
            try
            {
                using (var conn = new MySqlConnection($"server='{Convert.ToString((ConfigurationManager.AppSettings["DB_HOST"]))}';user='{Convert.ToString((ConfigurationManager.AppSettings["DB_USER"]))}';database='{Convert.ToString((ConfigurationManager.AppSettings["DB_DATABASE"]))}';port=3306;password='{Convert.ToString((ConfigurationManager.AppSettings["DB_PASSWORD"]))}';"))
                {
                    Logger.Debug("Connecting to MySQL...");
                    conn.Open();
                    using (var command = new MySqlCommand("SELECT * FROM gcm_users;", conn))
                    {
                        var reader = command.ExecuteReader();
                        Logger.Debug("Reading users");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var user = new User();

                                if (!DBNull.Value.Equals(reader["gcm_regid"]))
                                {
                                    user.RegId = Convert.ToString(reader["gcm_regid"]);
                                }

                                if (!DBNull.Value.Equals(reader["name"]))
                                {
                                    user.Name = Convert.ToString(reader["name"]);
                                }

                                if (!DBNull.Value.Equals(reader["email"]))
                                {
                                    user.Email = Convert.ToString(reader["email"]);
                                }

                                if (!DBNull.Value.Equals(reader["created_at"]))
                                {
                                    user.UpdateTime = Convert.ToDateTime(reader["created_at"]);
                                }

                                users.Add(user);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
            }
            return users;            
        }

        public void DeleteUser(string regId)
        {
            try
            {
                using (var conn = new MySqlConnection($"server='{Convert.ToString((ConfigurationManager.AppSettings["DB_HOST"]))}';user='{Convert.ToString((ConfigurationManager.AppSettings["DB_USER"]))}';database='{Convert.ToString((ConfigurationManager.AppSettings["DB_DATABASE"]))}';port=3306;password='{Convert.ToString((ConfigurationManager.AppSettings["DB_PASSWORD"]))}';"))
                {
                    Logger.Debug("Deleting user " + regId);
                    conn.Open();
                    using (var command = new MySqlCommand("DELETE FROM gcm_users WHERE gcm_regid = @regId;", conn))
                    {
                        command.Parameters.AddWithValue("@regId", regId);
                        command.ExecuteScalar();
                        Logger.Debug("User deleted");
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
            }
        }
    }
}
