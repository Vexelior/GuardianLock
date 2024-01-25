using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace GuardianLock.Helper.DAL
{
    public class DAL
    {
        public static int ExecuteQuery(string query)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Database=GuardianLock;Initial Catalog=GuardianLock;Integrated Security=True";
            int rowCount = 0;

            SqlConnection connection = new(connectionString);
            SqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.CommandTimeout = 12 * 3600;

                rowCount = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return rowCount;
        }
    }
}
