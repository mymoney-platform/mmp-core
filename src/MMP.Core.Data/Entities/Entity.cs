namespace MMP.Core.Data.Entities;

public class Entity
{ 
    public long Id { get; set; }
    public static string GetTableName(string entityName) => $"{entityName}s";
}

