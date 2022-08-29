using Commons.Bootstrapper;
using Commons.Commons.Entities;
using Storage.Commons.Interfaces;
using Storage.Repository.Interfaces;
using System.Linq;
using System.Reflection;

namespace Storage.Commons
{
    public class DatabaseManager : IDatabaseManager
    {
        public void InitDB()
        {
            var assembly = typeof(IRepository<>).GetTypeInfo().Assembly;
            var entities = assembly.DefinedTypes
                                   .Where(x => x.IsSubclassOf(typeof(SyncEntity))
                                          && !x.IsAbstract
                                          && x.GetCustomAttributes(typeof(IgnoreDbReset), true).Length == 0);

            foreach (var entity in entities)
            {
                var genericRepoEntityType = typeof(IRepository<>).MakeGenericType(entity.AsType());
                var repoEntity = ContainerManager.Resolve(genericRepoEntityType);

                var method = repoEntity.GetType().GetMethod("Init");

                method.Invoke(repoEntity, null);
            }
        }
    }
}
