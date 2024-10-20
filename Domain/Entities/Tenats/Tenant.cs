namespace Domain.Entities.Tenats;

public class Tenant
{
    public Tenant(CustomerId tenantId, string connectionString,
                  string databaseType)
    {
        TenantId = tenantId;
        ConnectionString = connectionString;
        DatabaseType = databaseType;
    }

    public CustomerId TenantId { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseType { get; set; }
}
