using System.Threading.Tasks;

namespace Action.Common.mongo
{
    public interface IDatabaseInitializer
    {
         Task InitializeAsync();
    }
}