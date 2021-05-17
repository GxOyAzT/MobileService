using Microsoft.EntityFrameworkCore;
using MobileService.DataAccess;

namespace MobileService.Tests
{
    public class MockDatabaseFactory
    {
        public static AppDbContext Build() =>
            new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(DbMockConnectionString).Options);

        public static AppDbContext Build(string connectionString) =>
            new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options);

        public static string DbMockConnectionString => @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MobileService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static string ProductionConnectionString => @"workstation id=MobileService.mssql.somee.com;packet size=4096;user id=PortfolioDB;pwd=Truskawka1;data source=MobileService.mssql.somee.com;persist security info=False;initial catalog=MobileService";
    }
}
