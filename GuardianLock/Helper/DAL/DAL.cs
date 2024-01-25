using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace GuardianLock.Helper.DAL
{
    public class DAL
    {
        private readonly static string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Database=GuardianLock;Initial Catalog=GuardianLock;Integrated Security=True";

        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static object ExecuteQuery(string query)
        {
            object rowCount = null;

            SqlConnection connection = new(connectionString);
            SqlCommand command = new(query, connection);

            try
            {
                connection.Open();

                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.CommandTimeout = 12 * 3600;

                if (query.Contains("SELECT"))
                {
                    rowCount = command.ExecuteScalar();
                }
                else if (query.Contains("INSERT") || query.Contains("UPDATE") || query.Contains("DELETE"))
                {
                    rowCount = command.ExecuteNonQuery();
                }
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

        /// <summary>
        /// Executes a query with the given parameters.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteQuery(string query, SqlParameter parameters)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(query, connection);

            if (parameters != null)
            {
                connection.Open();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.CommandTimeout = 12 * 3600;

                command.Parameters.Add(parameters);
            }
            else
            {
                return null;
            }

            return command.ExecuteScalar();
        }
    }
}
