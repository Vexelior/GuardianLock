using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace GuardianLock.Helper.DAL
{
    /// <summary>
    /// Data Access Layer
    /// </summary>
    public class DAL
    {
        public readonly static string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Database=GuardianLock;Initial Catalog=GuardianLock;Integrated Security=True";

        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Returns the result of the query.</returns>
        public static object ExecuteQuery(string query)
        {
            object result = null;

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
                    result = command.ExecuteScalar();
                }
                else if (query.Contains("INSERT") || query.Contains("UPDATE") || query.Contains("DELETE"))
                {
                    result = command.ExecuteNonQuery();
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

            return result;
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

            object result = null;

            if (parameters != null)
            {
                try
                {
                    connection.Open();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.CommandTimeout = 12 * 3600;

                    command.Parameters.Add(parameters);

                    if (query.Contains("SELECT"))
                    {
                        result = command.ExecuteScalar();
                    }
                    else if (query.Contains("INSERT") || query.Contains("UPDATE") || query.Contains("DELETE"))
                    {
                        result = command.ExecuteNonQuery();
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
            }
            else
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Executes a query with the given parameters.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteQuery(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(query, connection);

            object result = null;

            if (parameters != null)
            {
                try
                {
                    connection.Open();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.CommandTimeout = 12 * 3600;

                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    if (query.Contains("SELECT"))
                    {
                        result = command.ExecuteScalar();
                    }
                    else if (query.Contains("INSERT") || query.Contains("UPDATE") || query.Contains("DELETE"))
                    {
                        result = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                return null;
            }

            return result;
        }
    }
}
