using Business.Services;
using Frontend.Mobile.Areas.Competidores.ViewModels;
using Frontend.Mobile.Areas.Competidores.Views;
using Frontend.Mobile.Areas.Componentes.ViewModels;
using Frontend.Mobile.Areas.Componentes.Views;
using Frontend.Mobile.Areas.Login.ViewModels;
using Frontend.Mobile.Areas.Login.ViewModels.Interfaces;
using Frontend.Mobile.Areas.Notificaciones.ViewModels;
using Frontend.Mobile.Areas.Notificaciones.Views;
using Frontend.Mobile.Areas.Precios.ViewModels;
using Frontend.Mobile.Areas.Precios.Views;
using Frontend.Mobile.Areas.RepresentantesComerciales.ViewModels;
using Frontend.Mobile.Areas.RepresentantesComerciales.Views;
using Frontend.Mobile.Areas.Sync.ViewModels;
using Frontend.Mobile.Areas.Sync.Views;
using Frontend.Mobile.Areas.SyncDialog.ViewModels;
using Frontend.Mobile.Areas.SyncDialog.Views;
using Frontend.Mobile.Services;
using Frontend.Mobile.ViewModels;
using Frontend.Mobile.ViewModels.Interfaces;
using SQLite;
using Storage.Commons;
using Storage.Commons.Interfaces;
using Storage.Repository;
using Storage.Repository.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Xamarin.Forms;

namespace Commons.Bootstrapper
{
    public class Startup : IBootstraperStartup
    {
        private static IUnityContainer container;
        public Startup()
        {
            container = ContainerManager.Container;
        }

        public void ConfigureContainer()
        {
            RegisterRepositories();
            RegisterDatabaseConnection();
            RegisterServices();
            RegisterCore();
            ConfigureDatabase();
        }
        private static void RegisterPorConvencion(Type type, string endsWith, Type lifetimeManagerType)
        {
            var assembly = type.GetTypeInfo().Assembly;
            var types = assembly.DefinedTypes.Where(t => t.IsClass && !t.IsGenericType && t.Name.EndsWith(endsWith));
            types.ToList().ForEach(aType => aType.ImplementedInterfaces.Where(t => t.Name != "INotifyPropertyChanged").ToList().ForEach(typeInterface => container.RegisterType(typeInterface, aType.AsType(), Activator.CreateInstance(lifetimeManagerType) as ITypeLifetimeManager)));
        }
        private void ConfigureDatabase()
        {
            var databaseManager = (IDatabaseManager)container.Resolve(typeof(IDatabaseManager));
            databaseManager.InitDB();
        }

        private void RegisterCore()
        {
            container.RegisterType<IDatabaseManager, DatabaseManager>(new ContainerControlledLifetimeManager());
            RegisterPorConvencion(typeof(CompetidoresViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(CompetidoresView), "View", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(CargaPreciosViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(CargaPreciosView), "View", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(RepresentanteComercialViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(RepresentanteComercialView), "View", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(SyncViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(SyncView), "View", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(NotificacionViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(NotificacionView), "View", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(PickerViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(PickerView), "View", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(SyncDialogViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(SyncDialogView), "View", typeof(PerResolveLifetimeManager));
            container.RegisterType<IAppViewModel, AppViewModel>();
            container.RegisterType<ILoginViewModel, LoginViewModel>();
        }

        private void RegisterServices()
        {
            
            // Se pone uno explicitamente, pero con el string ya va a buscar todos los assemblies que cumplan con ese sufijo.
            RegisterPorConvencion(typeof(NavigationService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(CompetidoresService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(RepresentanteComercialService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(RelevamientoPreciosProductoService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(BanderaService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(NegocioService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(SegmentoService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(DireccionesCompetidorService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(BanderaProductoService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(ProductoLocalService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(CabeceraInteraccionLocalService), "Service", typeof(PerResolveLifetimeManager));
            RegisterPorConvencion(typeof(ProvinciaService), "Service", typeof(PerResolveLifetimeManager));
        }

        private SQLiteAsyncConnection CreateDatabaseConnection()
        {
            var fileHelper = DependencyService.Get<IFileHelper>();
            string path;

            if (Device.RuntimePlatform == Device.Android)
                path = fileHelper.GetLocalFilePath("YPF_DB.db3");
            else
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "YPF_DB.db3");

            return new SQLiteAsyncConnection(path, true);
        }

        private void RegisterDatabaseConnection()
        {
            var conn = CreateDatabaseConnection();
            container.RegisterType(typeof(Database<>), new InjectionConstructor(conn));
        }

        private void RegisterRepositories()
        {
            // Ejemplo del registro por convencion, asi habria que implementarlo para todos. Primero probar igualmente.
            RegisterPorConvencion(typeof(DefaultRepository<>), "Repository", typeof(PerResolveLifetimeManager));
            container.RegisterType(typeof(IRepository<>), (typeof(DefaultRepository<>)));
        }
    }
}
