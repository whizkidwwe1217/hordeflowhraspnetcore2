namespace HordeFlow.HR.Infrastructure
{
    public class Tenant
    {
        public string Name { get; set; }
        public string[] HostNames { get; set; }
        public string ConnectionString { get; set; }
    }
}