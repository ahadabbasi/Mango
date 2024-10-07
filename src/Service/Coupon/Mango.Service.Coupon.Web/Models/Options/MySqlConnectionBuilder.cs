using MySql.Data.MySqlClient;

namespace Mango.Service.Coupon.Web.Models.Options;

/// <summary>
/// 
/// </summary>
public sealed class MySqlConnectionBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public const string ApplicationSettingSectionName = "MySqlConnection";
    
    /// <summary>
    /// 
    /// </summary>
    public string Server { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Port { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Database { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public string ConnectionString()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
        {
            Server = Server,
            Port = uint.Parse(Port),
            Database = Database,
            UserID = Username,
            Password = Password
        };

        return builder.ConnectionString;
    }
    
}