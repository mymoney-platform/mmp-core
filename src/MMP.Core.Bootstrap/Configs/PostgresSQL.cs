namespace MMP.Core.Bootstrap.Configs;

public class PostgresSql
{
    public Database Server { get; set; }
    public Credential Root { get; set; }
    public Credential App { get; set; }
}

public class Database
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string DatabaseName { get; set; }
}

public class Credential
{
    public string User { get; set; }
    public string Password { get; set; }
}