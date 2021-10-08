using Microsoft.Data.SqlClient;

namespace WebAPI.Infrastructure.Data
{
    public static class DatabaseContext
    {
        public static string ConnectionString = "Data Source=DESKTOP-IB22G03\\SQLEXPRESS; Initial Catalog=WebApi; Integrated Security=True; Connect Timeout=30";

        public static SqlConnection DatabaseConnection = new SqlConnection(ConnectionString);
    }
}
