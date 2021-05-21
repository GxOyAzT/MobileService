using Microsoft.EntityFrameworkCore;
using MobileService.DataAccess;

namespace MobileService.TestsSpeed
{
    public class MockDatabaseFactory
    {
        public static AppDbContext Build() =>
            new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(DbMockConnectionString).Options);

        public static string DbMockConnectionString => @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MobileService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
