using Npgsql;

namespace Infrastructure.Data;

public static class Database
{
    private static string connectionString = "Host=localhost;Port=5432;Database=EXAM4;Username=postgres;Password=1234";
    public static NpgsqlConnection Connection() => new NpgsqlConnection(connectionString);
}
