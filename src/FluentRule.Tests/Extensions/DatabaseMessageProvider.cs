/*
using FluentRule.Localization;
using Npgsql;
using System.Globalization;

namespace FluentRule.Tests.Extensions;
public class DatabaseMessageProvider : IMessageProvider
{
    private readonly string _connectionString;

    public DatabaseMessageProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string GetMessage(string key)
    {
        var culture = CultureInfo.CurrentUICulture.Name;

        using var conn = new NpgsqlConnection(_connectionString);
        var sql = "SELECT Message FROM Messages WHERE Culture = @Culture AND Key = @Key LIMIT 1";
        var message = conn.QueryFirstOrDefault<string>(sql, new { Culture = culture, Key = key });

        if (string.IsNullOrEmpty(message))
        {
            // fallback para inglês
            message = conn.QueryFirstOrDefault<string>(sql, new { Culture = "en-US", Key = key }) ?? key;
        }

        return message;
    }
}
*/
