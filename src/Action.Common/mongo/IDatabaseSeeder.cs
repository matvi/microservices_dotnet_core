using System.Threading.Tasks;

namespace Action.Common.mongo
{
    public interface IDatabaseSeeder
    {
         Task SeedAsync();
    }
}