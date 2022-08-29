using Business.Dominio;
using Business.Services.Interfaces;
using Commons.Bootstrapper;
using Commons.Commons.Constants;
using Commons.Commons.Entities;
using Commons.Commons.Exceptions;
using Commons.Commons.Extensions;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Areas.Competidores.ViewModels.Interfaces;
using Frontend.Mobile.Areas.Precios.ViewModels;
using Frontend.Mobile.Areas.RepresentantesComerciales.ViewModels;
using Frontend.Mobile.Areas.Sync.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Exceptions;
using Frontend.Mobile.Commons.Helpers;
using Frontend.Mobile.Commons.Models;
using Frontend.Mobile.Services;
using Frontend.Mobile.ViewModels;
using Frontend.Mobile.Areas.Login.ViewModels;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Commons;
using Services.Commons.Exceptions;
using Services.Commons.PrecioProductos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xam.Plugin;
using Xamarin.Forms;
using Frontend.Mobile.Areas.Login.Views;


namespace Frontend.Mobile.Areas.Competidores.ViewModels
{
    public class CompetidoresViewModel : BaseViewModel, ICompetidoresViewModel
    {

        #region Commands
        /*TODO: 
         * - Agregar popup y opción Acerca de GEA RRPP con la versión.
           - Color de los Estados más apagados.
           - Filtro por Estado
           - Agregar un botón de ordenado para APIES, Razon Social, Estado.
        */
        public ICommand DoCustomPickerTestCommand { get; set; }
        public ICommand DoThreeDottedMenuCommand { get; set; }
        public ICommand DoNewSurveyCommand { get; set; }
        public ICommand DoCloseSessionCommand { get; set; }
        public ICommand DoAboutRRCCCommand { get; set; }
        public ICommand DoSyncCommand { get; set; }
        public ICommand DoSyncSingleCompetitorCommand { get; set; }
        public ICommand DoSendCompetitorsCommand { get; set; }


        #endregion

        #region Actions
        public Action<string> actionOKPickerButton;

        public Action actionOKCerrarSesionButton;

        public Action actionOKSincronizarPreciosButton;

        public Action actionOKSincronizarPreciosSingleCompetitorButton;

        public Action actionOKNuevoRelevamientoButton;

        #endregion

        #region Services
        public IBanderaService BanderaService { get; set; }
        private INegocioService NegocioService { get; set; }
        private ISegmentoService SegmentoService { get; set; }
        public ICompetidoresService CompetidoresService { get; set; }
        public IRelevamientoPreciosProductoService PreciosProductosService { get; set; }
        public IRepresentanteComercialService RepresentanteComercialService { get; set; }
        public IProductoLocalService ProductoLocalService { get; set; }
        public ICabeceraInteraccionLocalService CabeceraInteraccionLocalService { get; set; }
        public IDireccionesCompetidorService DireccionesCompetidorService { get; set; }

        public IProvinciaService ProvinciaService { get; set; }

        private ILoginService LoginService { get; set; }
        #endregion

        #region Properties
        private ICloseApplication Application { get; set; }
        private ISyncViewModel SyncViewModel { get; set; }
        public Color BackgroundColorHeader { get; set; }

        private ObservableCollection<CompetidoresGroupModel> _Items;
        private List<CompetidoresGroupModel> ItemsBackup { get; set; }
        public List<Cabecera> CabecerasSync { get; set; }
        public List<PreciosProductos> ProductosSync { get; set; }
        public List<ProductoLocal> ProductosDB { get; set; }
        public List<ProductoLocal> ProductosToReset { get; set; }
        public List<CabeceraInteraccionLocal> InteraccionesDB { get; set; }
        public List<CabeceraInteraccionLocal> InteraccionesToReset { get; set; }
        public List<Competidor> CompetidoresStateSaved { get; set; }
        public List<Competidor> CompetidoresError { get; set; }
        public string CompetidoresErrorText { get; private set; }
        public List<Competidor> CompetidoresDeleted { get; set; }
        public string CompetidoresDeletedText { get; private set; }
        public List<CompetidoresGroupModel> CompetidoresStateSynced { get; set; }
        public List<Competidor> Competidores { get; set; }
        public int tapCountAPIES { get; set; }
        public int tapCountRazonSocial { get; set; }
        public int tapCountEstado { get; set; }

        public int errorCompetitorsCount { get; set; }
        public int syncedCompetitorsCount { get; set; }
        private bool _isDoubleTapAPIES { get; set; }
        public bool IsDoubleTapAPIES
        {
            get
            {
                return _isDoubleTapAPIES;
            }
            set
            {
                _isDoubleTapAPIES = value;
                OnPropertyChanged("IsDoubleTapAPIES");
            }
        }
        private bool _isTappedAPIES { get; set; }
        public bool IsTappedAPIES
        {
            get
            {
                return _isTappedAPIES;
            }
            set
            {
                _isTappedAPIES = value;
                OnPropertyChanged("IsTappedAPIES");
            }
        }

        private bool _isDoubleTapRazonSocial { get; set; }
        public bool IsDoubleTapRazonSocial
        {
            get
            {
                return _isDoubleTapRazonSocial;
            }
            set
            {
                _isDoubleTapRazonSocial = value;
                OnPropertyChanged("IsDoubleTapRazonSocial");
            }
        }
        private bool _isTappedRazonSocial { get; set; }
        public bool IsTappedRazonSocial
        {
            get
            {
                return _isTappedRazonSocial;
            }
            set
            {
                _isTappedRazonSocial = value;
                OnPropertyChanged("IsTappedRazonSocial");
            }
        }
        private bool _isDoubleTapEstado { get; set; }
        public bool IsDoubleTapEstado
        {
            get
            {
                return _isDoubleTapEstado;
            }
            set
            {
                _isDoubleTapEstado = value;
                OnPropertyChanged("IsDoubleTapEstado");
            }
        }
        //private bool IsSAPServerError { get; set; }
        private bool HasNoSuddenConnection { get; set; }
        private bool _isTappedEstado { get; set; }
        public bool IsTappedEstado
        {
            get
            {
                return _isTappedEstado;
            }
            set
            {
                _isTappedEstado = value;
                OnPropertyChanged("IsTappedEstado");
            }
        }

        private bool _isOptionMenuTapped { get; set; }
        public bool IsOptionMenuTapped
        {
            get
            {
                return _isOptionMenuTapped;
            }
            set
            {
                _isOptionMenuTapped = value;
                OnPropertyChanged("IsOptionMenuTapped");
            }
        }
        private string _emptyListViewText { get; set; }
        public string EmptyListViewText
        {
            get
            {


                return _emptyListViewText;
            }
            set
            {
                _emptyListViewText = value;
                OnPropertyChanged("EmptyListViewText");
            }
        }
        private bool _isHeaderShowing { get; set; }
        public bool IsHeaderShowing
        {
            get
            {
                return _isHeaderShowing;
            }
            set
            {
                _isHeaderShowing = value;
                OnPropertyChanged("IsHeaderShowing");
            }
        }

        private bool _isEmptyListView { get; set; }
        public bool IsEmptyListView
        {
            get
            {
                if (_isEmptyListView)
                    BackgroundColorHeader = Color.White;
                else
                    BackgroundColorHeader = Color.White;


                return _isEmptyListView;
            }
            set
            {
                _isEmptyListView = value;
                OnPropertyChanged("IsEmptyListView");
            }
        }
        private bool _isFirstExecution { get; set; }
        public bool IsFirstExecution
        {
            get
            {
                return _isFirstExecution;
            }
            set
            {
                _isFirstExecution = value;
                OnPropertyChanged("IsFirstExecution");
            }
        }
        private bool _hasPrizesToSincronize;
        public bool HasPrizesToSincronize
        {
            get
            {
                return _hasPrizesToSincronize;
            }
            set
            {
                _hasPrizesToSincronize = value;
                OnPropertyChanged("HasPrizesToSincronize");
            }
        }

        private bool _isExitPricesPressed { get; set; }
        public bool IsExitPricesPressed
        {
            get
            {
                return _isExitPricesPressed;
            }
            set
            {
                _isExitPricesPressed = value;
                OnPropertyChanged("IsExitPricesPressed");
            }
        }
        public ImageButton BtnNuevoRelevamiento;
        public ImageButton BtnPickerState;

        private SearchBar InputSearch { get; set; }
        //private Picker PickerState { get; set; }
        public ListView CompetidoresListView { get; set; }
        private bool _isClosingSession;
        public bool IsClosingSession
        {
            get
            {
                return _isClosingSession;
            }
            set
            {
                _isClosingSession = value;
                OnPropertyChanged("IsClosingSession");
            }
        }
        private bool _searchEnabled;
        public bool SearchEnabled
        {
            get
            {
                return _searchEnabled;
            }
            set
            {
                _searchEnabled = value;
                OnPropertyChanged("SearchEnabled");
            }
        }
        private string _selectedState { get; set; }
        public string SelectedState
        {
            get
            {
                return _selectedState;
            }
            set
            {
                _selectedState = value;
                if (_selectedState != null)
                {
                    EventsManager.TriggerEvent("SelectedStateFilter");
                }
                OnPropertyChanged("SelectedState");
            }
        }

        public ObservableCollection<CompetidoresGroupModel> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
                OnPropertyChanged("Items");
            }
        }

        public int Index { get; set; }
        public Button BtnSincronizarPrecios { get; private set; }
        public bool IsClickedOnce { get; private set; }
        public string SyncedCompetitorsText { get; private set; }
        public string DeletedCompetitorsText { get; private set; }
        public string ErrorCompetitorsText { get; private set; }
        public bool IsRetail { get; set; }
        public bool RRCCHasErrors { get; set; }
        public string Cliente { get; private set; }

        #endregion

        #region Constructor
        public CompetidoresViewModel(ILoginService loginService, IProvinciaService provinciaService, ICompetidoresService competidoresService, IProductoLocalService productoLocalService, ICabeceraInteraccionLocalService cabeceraInteraccionLocalService, IRepresentanteComercialService representanteComercialService, IRelevamientoPreciosProductoService relevamientoPreciosProductoService, IBanderaService banderaService, INegocioService negocioService, ISegmentoService segmentoService, IDireccionesCompetidorService direccionesCompetidorService)
        {
            Application = DependencyService.Get<ICloseApplication>();
            BanderaService = banderaService;
            DireccionesCompetidorService = direccionesCompetidorService;
            ProvinciaService = provinciaService;
            CompetidoresService = competidoresService;
            ProductoLocalService = productoLocalService;
            CabeceraInteraccionLocalService = cabeceraInteraccionLocalService;
            RepresentanteComercialService = representanteComercialService;
            PreciosProductosService = relevamientoPreciosProductoService;
            NegocioService = negocioService;
            SegmentoService = segmentoService;
            DoThreeDottedMenuCommand = new Command(() => DoThreeDottedMenuClicked());
            DoNewSurveyCommand = new Command(async () => await DoNewSurvey());
            DoCustomPickerTestCommand = new Command(async () => await DoCustomPickerTest());
            DoCloseSessionCommand = new Command(async () => await DoCloseSessionApp());
            DoAboutRRCCCommand = new Command(async () => await DoAboutRRCC());
            DoSyncCommand = new Command(async () => await DoSync());
            DoSyncSingleCompetitorCommand = new Command(async (object sender) => await DoSyncSingleCompetitor(sender));
            DoSendCompetitorsCommand = new Command(async () => await DoSendCompetitors());
            Items = new ObservableCollection<CompetidoresGroupModel>();
            ItemsBackup = new List<CompetidoresGroupModel>();
            ProductosDB = new List<ProductoLocal>();
            InteraccionesDB = new List<CabeceraInteraccionLocal>();
            ProductosToReset = new List<ProductoLocal>();
            InteraccionesToReset = new List<CabeceraInteraccionLocal>();
            ProductosSync = new List<PreciosProductos>();
            CabecerasSync = new List<Cabecera>();
            CompetidoresStateSaved = new List<Competidor>();
            CompetidoresDeleted = new List<Competidor>();
            CompetidoresError = new List<Competidor>();
            Competidores = new List<Competidor>();
            //EventsManager.SubscribeToEvent("SavePrecioProducto", OnSavePrecioProducto);
            EventsManager.SubscribeToEvent("RestartPrizes", OnRestartPrizes);
            EventsManager.SubscribeToEvent("OnDeletedCompetitors", OnDeletedCompetitors);
            IsEmptyListView = false;
            IsHeaderShowing = true;
            HasPrizesToSincronize = false;
            SyncViewModel = ContainerManager.Resolve<ISyncViewModel>();
            EventsManager.SubscribeToEvent("SelectedStateFilter", OnSelectedStateFilter);
            EventsManager.SubscribeToEvent("IsClickedOnceReset", OnClickedOnceReset);
            EventsManager.SubscribeToEvent("OnExitButton", OnExitButton);
            actionOKPickerButton += CallbackOKButtonPicker;
            actionOKCerrarSesionButton += CallbackCerrarSesionOKButton;
            actionOKSincronizarPreciosButton += CallbackSincronizarPreciosOKButton;
            actionOKSincronizarPreciosSingleCompetitorButton += CallbackSincronizarPreciosPorCompetidorOKButton;
            actionOKNuevoRelevamientoButton += CallbackOKNuevoRelevamientoButton;
            IsOptionMenuTapped = true;
            LoginService = loginService;

        }



        private void OnExitButton(object[] parameterContainer)
        {
            IsExitPricesPressed = true;

        }

        private void OnDeletedCompetitors(object[] parameterContainer)
        {
            CompetidoresDeleted = (List<Competidor>)parameterContainer[0];
        }

        private void OnClickedOnceReset(object[] parameterContainer)
        {
            IsClickedOnce = true;
        }

        private async Task DoNewSurvey()
        {
            if (HasNoSyncCompetitors())
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                   ApplicationMessages.Warning, ApplicationMessages.ResetStatesNoSyncNotification,
                   ApplicationMessages.Accept, ApplicationMessages.Cancel,
                   CallbackOKNuevoRelevamientoButton);
            }
            else
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                   ApplicationMessages.Warning, ApplicationMessages.ResetStatesNoPrizeNotification,
                   ApplicationMessages.Accept, ApplicationMessages.Cancel,
                   CallbackOKNuevoRelevamientoButton);
            }
        }

        private void CallbackOKNuevoRelevamientoButton()
        {
            ResetCompetitorStates();
        }

        private async void DoThreeDottedMenuClicked()
        {
            await DoNewSurvey();
        }

        public async void ResetCompetitorStates()
        {
            foreach (var competidor in Items)
            {
                competidor.Estado = CompetitorState.NoPrizeSurvey;
                SaveCompetitorsState(competidor, false);
            }
            await CompetidoresService.Save(CompetidoresStateSaved);
            CompetidoresStateSaved.Clear();

            var cabecerasLocales = await CabeceraInteraccionLocalService.GetAllDB();
            var cabecerasLocalesPending = cabecerasLocales.Where(x => x.SyncState == SyncState.PendingToSync);

            foreach (var cab in cabecerasLocalesPending)
            {
                cab.SyncState = SyncState.New;
                InteraccionesToReset.Add(cab);
            }
            await CabeceraInteraccionLocalService.Save(InteraccionesToReset);

            var productosLocales = await ProductoLocalService.GetAllDB();
            var prodLocalesPending = productosLocales.Where(x => x.SyncState == SyncState.PendingToSync);

            foreach (var prod in prodLocalesPending)
            {
                prod.SyncState = SyncState.Resetted;
                ProductosToReset.Add(prod);
            }
            await ProductoLocalService.SaveAll(ProductosToReset);
        }

        public bool HasNoSyncCompetitors()
        {
            return Items.Any(x => x.Estado == CompetitorState.PrizeNoSync || x.Estado == CompetitorState.ErrorSync);
        }

        private void CallbackOKButtonPicker(string selectedOption)
        {
            SelectedState = selectedOption;
        }

        private async Task DoCustomPickerTest()
        {
            string[] images = new string[6];

            images[0] = "todos.png";
            images[1] = "sincronizados.png";
            images[2] = "pendienteSincronizar.png";
            images[3] = "errorSincronizar.png";
            images[4] = "selectionState.png";
            images[5] = "manualSync.png";

            string[] options = new string[5];
            options[0] = "Todos";
            options[1] = EnumExtensions.GetDescription(CompetitorState.PrizeSync);
            options[2] = EnumExtensions.GetDescription(CompetitorState.PrizeNoSync);
            options[3] = EnumExtensions.GetDescription(CompetitorState.ErrorSync);
            options[4] = EnumExtensions.GetDescription(CompetitorState.ManualSync);


            await AlertService.DisplayCustomPicker(images, options, ApplicationMessages.Close, actionOKPickerButton);
        }

        public void CompetidoresListView_SizeChanged()
        {
            int SizeDefault = 30;
            CompetidoresListView.ItemsSource = Items;
            if (Items == null || Items.Count == 0)
            {
                CompetidoresListView.HeightRequest = SizeDefault;
            }
            else
            {
                CompetidoresListView.HeightRequest = SizeDefault * Items.Count;
            }

        }
        public void SetListViewCompetidores(ListView listViewCompetidores)
        {
            CompetidoresListView = listViewCompetidores;
        }

        #endregion

        #region Methods
        private async void OnSyncPrecioProducto(List<CabeceraInteraccionLocal> interacciones, List<ProductoLocal> productos, List<LlamadaRFC_ResponseUpload> responseUpload)
        {
            try
            {
                errorCompetitorsCount = 0;
                syncedCompetitorsCount = 0;

                foreach (var interaccion in interacciones)
                {
                    foreach (var responseInteraccion in responseUpload)
                    {
                        if (interaccion.CodTranInt.Equals(responseInteraccion.cabecera[0].CodTranInt))
                        {
                            responseInteraccion.cabecera[0].Cliente = interaccion.Cliente;
                        }
                    }
                }


                /*foreach (var competidor in Items)
                {*/
                foreach (var interaccion2 in responseUpload)
                {
                    var competidor = Items.Where(x => x.InterComercial == interaccion2.cabecera[0].Cliente).FirstOrDefault();
                    if (competidor != null && interaccion2.cabecera[0].Cliente.Equals(competidor.InterComercial))
                    {
                        if (interaccion2.retornorfc == null)
                        {
                            competidor.Estado = CompetitorState.ErrorSync;

                            SaveCompetitorsState(competidor, true);
                            errorCompetitorsCount++;
                        }
                        else if (interaccion2.retornorfc != null && interaccion2.retornorfc[0].TYPE.Equals("E"))
                        {
                            competidor.Estado = CompetitorState.ErrorSync;
                            SaveCompetitorsState(competidor, true);
                            errorCompetitorsCount++;
                        }
                        else
                        {
                            competidor.Estado = CompetitorState.PrizeSync;
                            SaveCompetitorsState(competidor, false);
                            syncedCompetitorsCount++;
                        }
                    }
                }
                //}
                await CompetidoresService.UpdateAll(CompetidoresStateSaved);

                StringBuilder sbSynced = new StringBuilder();
                StringBuilder sbDeleted = new StringBuilder();
                StringBuilder sbError = new StringBuilder();
                if (syncedCompetitorsCount > 0)
                    sbSynced.Append("- " + syncedCompetitorsCount + ((syncedCompetitorsCount == 1) ? " competidor se sincronizó con éxito." : " competidores se sincronizaron con éxito. "));
                //CompetidoresDeleted.ForEach(x => CompetidoresDeletedList.Add(x.RazonSocial));
                //CompetidoresError.ForEach(x => CompetidoresErrorList.Add(x.RazonSocial));
                foreach (var compDeleted in CompetidoresDeleted)
                {
                    sbDeleted.Append("- " + compDeleted.RazonSocial);
                    sbDeleted.Append(Environment.NewLine);
                }
                foreach (var compError in CompetidoresError)
                {
                    sbError.Append("- " + compError.RazonSocial);
                    sbError.Append(Environment.NewLine);
                }
                CompetidoresDeletedText = sbDeleted.ToString();
                CompetidoresErrorText = sbError.ToString();
                SyncedCompetitorsText = sbSynced.ToString();
                CompetidoresStateSaved.Clear();
                CompetidoresDeleted.Clear();
                CompetidoresError.Clear();
                //TODO: agregar la actualización del precio de producto para updatearlo en la 
                //base local y contemplar el tema de los productos que se eliminaron. Sería una 
                //lógica que pregunte si existe que lo updatee si no que siga de largo. 
                foreach (var interaccion in interacciones)
                {
                    interaccion.SyncState = SyncState.Synchronized;
                    if (responseUpload.Any(x => (x.retornorfc == null || (x.retornorfc != null && x.retornorfc[0].TYPE.Equals("E"))) && x.cabecera[0].Cliente == interaccion.Cliente))
                    {
                        interaccion.SyncState = SyncState.ErrorToSync;
                    }
                    var cabecerasDB = await CabeceraInteraccionLocalService.Query("SELECT * FROM CabeceraInteraccionLocal WHERE Cliente = ?", interaccion.Cliente);
                    if (cabecerasDB.Count() > 0)
                    {
                        await CabeceraInteraccionLocalService.Update(interaccion);
                    }
                }

                foreach (var producto in productos)
                {
                    producto.SyncState = SyncState.Synchronized;
                    if (responseUpload.Any(x => (x.retornorfc == null || (x.retornorfc != null && x.retornorfc[0].TYPE.Equals("E"))) && x.cabecera[0].Cliente == producto.Cliente))
                    {
                        producto.SyncState = SyncState.ErrorToSync;
                    }

                    var productosDB = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE IdProductoLocal = ? AND (SyncState = ? OR SyncState = ?)", producto.IdProductoLocal, SyncState.PendingToSync, SyncState.ErrorToSync);
                    if (productosDB.Count() > 0)
                    {
                        await ProductoLocalService.Update(producto);
                    }
                }

                await ReloadCompetitors();

                // TODO: if (no hay competidores en la DB local)
                //              cambiar al mensaje de "No tiene competidores asignados"
                //var listComp = await CompetidoresService.Query("SELECT COUNT(*) FROM Competidor");
                var listComp = await CompetidoresService.GetAllDB();
                if (listComp.Count == 0)
                {
                    IsEmptyListView = true;
                    IsHeaderShowing = false;
                    EmptyListViewText = ApplicationMessages.EmptyCompetitorsFromSyncListView;
                }

                InteraccionesDB.Clear();
                ProductosDB.Clear();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void OnRestartPrizes(object[] parameterContainer)
        {
            if (parameterContainer != null && parameterContainer.Length > 0)
            {
                Index = (int)parameterContainer[0];
                Items[Index].Estado = CompetitorState.NoPrizeSurvey;
                SaveCompetitorsState(Items[Index], false);
                //CompetidoresService.UpdateAll(CompetidoresStateSaved);
                CompetidoresService.Save(CompetidoresStateSaved);
                CompetidoresStateSaved.Clear();
            }
        }

        //private void OnSavePrecioProducto(object[] parameterContainer)
        //{
        //    if (parameterContainer != null && parameterContainer.Length > 0)
        //    {
        //        Index = (int)parameterContainer[0];
        //        Items[Index].Estado = CompetitorState.PrizeNoSync;
        //        SaveCompetitorsState(Items[Index], false);
        //        CompetidoresService.Save(CompetidoresStateSaved);
        //        CompetidoresStateSaved.Clear();
        //    }
        //}

        public override async Task InitializeAsync(object data)
        {
            IsFirstExecution = (bool)data;
            IsClickedOnce = true;
        }

        public async Task InitializeCompetidores()
        {
            var competidores = await GetCompetidores();

            if (competidores.Count > 0)
            {
                if (IsExitPricesPressed)
                    FiltrarCompetidores();

                Items = competidores;
                ItemsBackup = Items.ToList();
                var itemsRazonSocialAscendingOrder = Items.OrderBy(x => x.APIES).ToList();
                Items.Clear();
                itemsRazonSocialAscendingOrder.ForEach(x => Items.Add(x));
                IsTappedAPIES = true;
            }
            else
            {
                IsEmptyListView = true;
                IsHeaderShowing = false;
                EmptyListViewText = ApplicationMessages.EmptyCompetitorsFromSyncListView;
            }

        }

        public void SaveCompetitorsState(CompetidoresGroupModel item, bool isErrorSynced)
        {
            Competidor competidorStateSave = new Competidor()
            {
                Agrupacion = item.Agrupacion,
                APIES = item.APIES,
                Atributo = item.Atributo,
                CantTarjetaYer = item.CantTarjetaYer,
                CodDirent = item.CodDirent,
                Contacto = item.Contacto,
                CuentaLP2 = item.CuentaLP2,
                CuentaLPO = item.CuentaLPO,
                CuentaQP1 = item.CuentaQP1,
                CuentaSGC = item.CuentaSGC,
                Cuit = item.CUIT,
                Estado = item.Estado,
                InterComercial = item.InterComercial,
                Latitud = item.Latitud,
                Longitud = item.Longitud,
                NumeroExpediente = item.NumeroExpediente,
                OperaYer = item.OperaYer,
                RazonSocial = item.RazonSocial,
                UnidadNegocio = item.UnidadNegocio
            };
            CompetidoresStateSaved.Add(competidorStateSave);
            if (isErrorSynced)
                CompetidoresError.Add(competidorStateSave);
        }

        public async Task<IEnumerable<ProductoLocal>> GetProductosPorCliente(CabeceraInteraccionLocal cabecera, string interComercial)
        {
            List<ProductoLocal> productosLocales = null;
            try
            {
                productosLocales = await GetProducts(interComercial, cabecera);
            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, e.Message, ApplicationMessages.Accept);
            }

            return productosLocales;
        }

        public async Task<CabeceraInteraccionLocal> GetCabeceraInteraccionLocal(string interComercial)
        {
            List<CabeceraInteraccionLocal> cabeceraInteraccionLocal = await CabeceraInteraccionLocalService.GetAllDB();
            CabeceraInteraccionLocal cabecera = null;

            if (cabeceraInteraccionLocal.Count > 0)
            {
                var cabeceraPorCliente = cabeceraInteraccionLocal.Where(x => x.Cliente == interComercial);

                if (cabeceraPorCliente.Count() > 0)
                    cabecera = cabeceraPorCliente.First();
            }
            return cabecera;
        }

        public async Task<DatosSincronizacionPendiente> GetProductosPorBandera(string IdBandera, string interComercial)
        {
            var RelevamientoPreciosProductos = new List<RelevamientoPreciosProducto>();
            var productosRP = new ObservableCollection<RPProductoGroupModel>();
            var datosSincronizacionPendiente = new DatosSincronizacionPendiente();
            datosSincronizacionPendiente.ProductosPendientes = new List<ProductoPendiente>();

            RelevamientoPreciosProductos = await QueryRelevamientosPreciosProductos(IdBandera);
            productosRP.Clear();

            foreach (var rpprod in RelevamientoPreciosProductos)
            {
                var Producto = new RPProductoGroupModel()
                {
                    CodigoSAP = rpprod.CodigoSAP,
                    Descripcion = rpprod.Descripcion,
                    Envase = rpprod.Envase,
                    IdRelevamientoPreciosProducto = rpprod.IdRelevamientoPreciosProducto,
                    IdSegmento = rpprod.IdSegmento
                };
                productosRP.Add(Producto);
            }

            CabeceraInteraccionLocal cabecera = await GetCabeceraInteraccionLocal(interComercial);
            IEnumerable<ProductoLocal> productosPorCliente = null;
            if (cabecera != null)
            {
                if (cabecera.Uploaded.Day == 1 && cabecera.Uploaded.Month == 1 && cabecera.Uploaded.Year == 1)
                {
                    var fechaDownload = cabecera.Downloaded.ToString("dd-MM-yyyy HH:mm:ss");
                    productosPorCliente = await GetProductosPorCliente(cabecera, interComercial);
                }
                else
                {
                    var fechaUpload = cabecera.Uploaded.ToString("dd-MM-yyyy HH:mm:ss");
                    productosPorCliente = await GetProductosPorCliente(cabecera, interComercial);
                }
            }

            if (productosPorCliente != null)
            {
                datosSincronizacionPendiente.FechaRelevamiento = productosPorCliente.First().FechaCreacion;
                foreach (var prodLocal in productosPorCliente)
                {
                    foreach (var prod in productosRP)
                    {
                        if (prod != null && prodLocal.Producto.Equals(prod.CodigoSAP))
                        {
                            var precioCompetidor = prodLocal.Precio.Replace('.', ',');
                          
                            var productoPendiente = new ProductoPendiente() { CodigoSAP = prod.CodigoSAP, Nombre = prod.Descripcion, Precio = precioCompetidor };
                            datosSincronizacionPendiente.ProductosPendientes.Add(productoPendiente);
                        }
                    }
                }
                if (productosRP.Count > 0 && productosPorCliente.Count() > 0)
                {
                    var productosInexistentes = productosRP.Where(x => productosPorCliente.All(y => y.Producto != x.CodigoSAP));
                    foreach (var item in productosInexistentes)
                    {
                        foreach (var prod in productosRP)
                        {
                            if (prod.CodigoSAP == item.CodigoSAP)
                            {
                                if (prod.Precio == null)
                                {
                                    prod.PrecioEnteros = "0.00";
                                    prod.Precio = "0.00";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var prodRP in productosRP)
                {
                    prodRP.PrecioEnteros = "0.00";
                }
            }

            return datosSincronizacionPendiente;
        }

        public async Task<List<RelevamientoPreciosProducto>> QueryRelevamientosPreciosProductos(string IdBandera)
        {
            return await PreciosProductosService.Query("SELECT RelevamientoPreciosProducto.IdRelevamientoPreciosProducto, RelevamientoPreciosProducto.CodigoSAP, RelevamientoPreciosProducto.Descripcion,RelevamientoPreciosProducto.IdSegmento, RelevamientoPreciosProducto.Envase " +
                                                       "FROM Bandera " +
                                                       "INNER JOIN BanderaProducto ON Bandera.IdBandera = BanderaProducto.IdBandera " +
                                                       "INNER JOIN RelevamientoPreciosProducto ON BanderaProducto.IdRelevamientoPreciosProducto = RelevamientoPreciosProducto.IdRelevamientoPreciosProducto " +
                                                       "WHERE Bandera.IdBandera = ?", IdBandera);
        }

        public async void SendCompetitors()
        {
            try
            {
                await StartSpinner();
                await StartSpinner();
                List<CompetidorPendiente> competidoresPendientes = new List<CompetidorPendiente>();
                var IdRed = await LocalStorageService.GetUserLogin();

                foreach (var item in Items)
                {
                    if (item.Estado == CompetitorState.ErrorSync)
                    {
                        var datosSincronizacionPendiente = await GetProductosPorBandera(item.Bandera, item.InterComercial);
                        var bandera = await GetBanderaDesc(item.Bandera);
                        var direccion = await GetDireccion(item.InterComercial);
                        var provincia = await ProvinciaService.GetByIdDB(int.Parse(direccion.Provincia));
                        var competidorPendiente = new CompetidorPendiente()
                        {
                            Bandera = bandera.Descripcion,
                            APIES = CompleteAPIES(item.APIES),
                            Direccion = direccion.Calle + " " + direccion.Numero,
                            RazonSocial = item.RazonSocial,
                            Provincia = provincia.Nombre,
                            RepresentanteComercial = IdRed,
                            ProductosPendientes = datosSincronizacionPendiente.ProductosPendientes,
                            FechaRelevamiento = datosSincronizacionPendiente.FechaRelevamiento
                        };

                        competidoresPendientes.Add(competidorPendiente);

                        item.Estado = CompetitorState.ManualSync;
                        SaveCompetitorsState(item, false);
                    }
                }

                if (competidoresPendientes.Count > 0)
                {
                    await CompetidoresService.GenerarSincronizacionesPendientes(competidoresPendientes);

                    await StopSpinner();

                    await AlertService.DisplayCustomAlertConfirmation(
                                        ApplicationMessages.ConfirmationDrawable,
                                        ApplicationMessages.Success,
                                        ApplicationMessages.SendSuccess,
                                        ApplicationMessages.Accept, null);
                }
                else
                {
                    await StopSpinner();

                    await AlertService.DisplayCustomAlertConfirmation(
                        ApplicationMessages.WarningDrawable,
                        ApplicationMessages.Atencion,
                        ApplicationMessages.SendNoContent,
                        ApplicationMessages.Accept, null);
                }

                if (CompetidoresStateSaved.Count > 0)
                {
                    await CompetidoresService.Save(CompetidoresStateSaved);
                    CompetidoresStateSaved.Clear();
                }
            }
            catch (Exception e)
            {
                foreach (var item in Items)
                {
                    if (item.Estado == CompetitorState.ManualSync)
                        item.Estado = CompetitorState.ErrorSync;
                }

                await StopSpinner();
                CompetidoresStateSaved.Clear();
                if (e.Message == "404")
                {
                    await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, ApplicationMessages.ErrorSendCompetitorsRequest, ApplicationMessages.Accept);
                }
                else
                {
                    if (e.Message == "500")
                        await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, ApplicationMessages.ErrorSendCompetitorsProcess, ApplicationMessages.Accept);
                    else
                        await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, ApplicationMessages.ErrorSendCompetitorsRequest, ApplicationMessages.Accept);
                }
            }
        }

        public async Task DoSendCompetitors()
        {
            await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable, ApplicationMessages.Warning, ApplicationMessages.EnviarGEANotification, ApplicationMessages.Send, ApplicationMessages.Cancel, SendCompetitors);
        }

        public string CompleteAPIES(string apies)
        {
            if (apies.Length < 5)
                return apies.PadLeft(5, '0');

            return apies;
        }

        public async Task<Bandera> GetBanderaDesc(string idBandera)
        {
            try
            {
                var bandera = await BanderaService.GetByIdDB(int.Parse(idBandera));
                return bandera;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DireccionCompetidor> GetDireccion(string interComercial)
        {
            var direccionCompetidor = new DireccionCompetidor();
            try
            {
                List<DireccionCompetidor> direccionesCompetidor = await DireccionesCompetidorService.GetByInterComercial(interComercial);

                if (direccionesCompetidor != null && direccionesCompetidor.Count > 0)
                {
                    direccionCompetidor = direccionesCompetidor.First();

                }
            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, e.Message, ApplicationMessages.Accept);
                Crashes.TrackError(e);
            }

            return direccionCompetidor;

        }

        private async Task DoSyncSingleCompetitor(object sender)
        {
            var cliente = sender as ImageButton;
            Cliente = cliente.CommandParameter as string;
            try
            {
                if (await Util.HasInternetConnectionAsync())
                {
                    if (await Util.HasServiceConnectivityAsync())
                    {
                        await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                           ApplicationMessages.Atencion, ApplicationMessages.SyncSingleCompetitorNotification, ApplicationMessages.Syncronization, ApplicationMessages.Cancel, actionOKSincronizarPreciosSingleCompetitorButton);
                    }
                    else
                    {
                        await AlertService.DisplayCustomAlertConfirmation(
                                                ApplicationMessages.ErrorDrawable,
                                                ApplicationMessages.Error,
                                                ApplicationMessages.NoServiceConnection,
                                                ApplicationMessages.Accept);
                    }
                }
                else
                {
                    await AlertService.DisplayCustomAlertConfirmation(
                                ApplicationMessages.ErrorDrawable,
                                ApplicationMessages.Error,
                                ApplicationMessages.NoInternetConnection,
                                ApplicationMessages.Accept);
                }
            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ProcessExceptionMessage(e.Message),
                                            ApplicationMessages.Accept);
                Crashes.TrackError(e);
            }
        }

        private async Task DoSync()
        {

            try
            {
                if (await Util.HasInternetConnectionAsync())
                {
                    if (await Util.HasServiceConnectivityAsync())
                    {
                        await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                           ApplicationMessages.Atencion, ApplicationMessages.SyncNotification, ApplicationMessages.Syncronization, ApplicationMessages.Cancel, actionOKSincronizarPreciosButton);
                    }
                    else
                    {
                        await AlertService.DisplayCustomAlertConfirmation(
                                                ApplicationMessages.ErrorDrawable,
                                                ApplicationMessages.Error,
                                                ApplicationMessages.NoServiceConnection,
                                                ApplicationMessages.Accept);
                    }
                }
                else
                {
                    await AlertService.DisplayCustomAlertConfirmation(
                                ApplicationMessages.ErrorDrawable,
                                ApplicationMessages.Error,
                                ApplicationMessages.NoInternetConnection,
                                ApplicationMessages.Accept);
                }
            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                             // ProcessExceptionMessage(e.Message),
                                             ApplicationMessages.SyncGralError,
                                            ApplicationMessages.Accept);
                Crashes.TrackError(e);
            }
        }

        public string ProcessExceptionMessage(string message)
        {
            string messageProcessed = "";
            if (message.Contains("ECONNREFUSED"))
            {
                messageProcessed = ApplicationMessages.NoInternetConnection;
            }
            else
            {
                messageProcessed = message;
            }

            return messageProcessed;
        }
        private async void CallbackSincronizarPreciosPorCompetidorOKButton()
        {
            try
            {
                await StartSpinner();
                await StartSpinner();
                //ASOSA BEGIN COMENTADO PARA QUE SOLO TENGA LA VALIDACION ZCRM_ACTRRCC_AB
                //RRCCHasErrors = false;
                //await CheckRRCCIfRetail();
                //if (!RRCCHasErrors)
                //{
                //    if (!HasNoSuddenConnection)
                //    {
                //        HasNoSuddenConnection = false;
                //        if (IsRetail)
                //        {
                //ASOSA END COMENTADO PARA QUE SOLO TENGA LA VALIDACION ZCRM_ACTRRCC_AB
                await GetLocalInteractionAndProducts();

                var syncInteractionsAndProductsCompleted = await SyncAndUploadToRFC();
                if (syncInteractionsAndProductsCompleted)
                {
                    await StopSpinner();
                    await AlertService.DisplayCustomAlertSyncronizationConfirmation(
                        ApplicationMessages.ConfirmationDrawable,
                        ApplicationMessages.SyncStatus,
                        SyncedCompetitorsText,
                        ApplicationMessages.DefaultSyncronizedInteractionsText,
                        ApplicationMessages.CompetitorsDeletedText,
                        ApplicationMessages.CompetitorsErrorText,
                        CompetidoresErrorText,
                        CompetidoresDeletedText,
                        ApplicationMessages.Close);
                    errorCompetitorsCount = 0;
                    syncedCompetitorsCount = 0;
                }



                //ASOSA BEGIN COMENTADO PARA QUE SOLO TENGA LA VALIDACION ZCRM_ACTRRCC_AB
                //        }
                //        else
                //        {
                //            await StopSpinner();
                //            await AlertService.DisplayCustomAlertConfirmation(
                //                ApplicationMessages.ErrorDrawable,
                //                ApplicationMessages.Error,
                //                ApplicationMessages.RRCCNotRetailSync,
                //                ApplicationMessages.Accept);
                //        }
                //    }
                //    else
                //    {
                //        await StopSpinner();
                //        await AlertService.DisplayCustomAlertConfirmation(
                //                                    ApplicationMessages.ErrorDrawable,
                //                                    ApplicationMessages.Error,
                //                                    ApplicationMessages.NoInternetConnection,
                //                                    ApplicationMessages.Accept);
                //    }

                //}

                //ASOSA END COMENTADO PARA QUE SOLO TENGA LA VALIDACION ZCRM_ACTRRCC_AB
            }
            catch (NoConnectionException ex)
            {
                await StopSpinner();
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.SyncGralError,
                                            ApplicationMessages.Accept);
            }
        }

        private async void CallbackSincronizarPreciosOKButton()
        {
            try
            {
                await StartSpinner();
                await StartSpinner();

                #region ASOSA LOGIN VEO SI ESTA OK BEGIN
                //DeviceInfoModel InfoModel = new DeviceInfoModel();
                //InfoModel = LocalStorageService.GetInfoModel();
                //bool isLogged = await LoginService.DoLogin(InfoModel);
                //isLogged = false; //ASOSA SACAR . FORZADO PARA PRUEBA
                //if (isLogged)
                //{
                #endregion ASOSA LOGIN VEO SI ESTA OK END
                RRCCHasErrors = false;
                //ASOSA SACAR RRCC
                //await CheckRRCCIfRetail();
                //ASOSA SACAR RRCC
                if (!RRCCHasErrors)
                {
                    /*if (!IsSAPServerError) 
                    {*/
                    if (!HasNoSuddenConnection)
                    {
                        HasNoSuddenConnection = false;
                        //ASOSA SACAR RRCC
                        //if (IsRetail)
                        //{
                            //ASOSA SACAR RRCC
                            //IsSAPServerError = false;
                            await GetAllLocalProductsAndInteractions();
                            EventsManager.TriggerEvent("IsInteractionsSynced");
                       
                        var syncMasterDownloadCompleted = await SyncViewModel.GetData();
                      
                        var syncMasterSaved = await SyncViewModel.GuardarDB();



                        if (syncMasterDownloadCompleted && syncMasterSaved)
                        {





                            //TODO: Bug 11435 - Hacer un recorrido de las listas temporales de cabecera y sus correspondientes productos 
                            //por si fueron desasignados. Y si es asi eliminarlos de la lista temporal a sincronizar, si no dejarlo. 
                            CheckForUnassignedCompetitorsAndProducts();

                                var syncInteractionsAndProductsCompleted = await SyncAndUploadToRFC();
                                if (syncInteractionsAndProductsCompleted)
                                {
                                    await StopSpinner();
                                    await AlertService.DisplayCustomAlertSyncronizationConfirmation(
                                        ApplicationMessages.ConfirmationDrawable,
                                        ApplicationMessages.SyncStatus,
                                        SyncedCompetitorsText,
                                        ApplicationMessages.DefaultSyncronizedInteractionsText,
                                        ApplicationMessages.CompetitorsDeletedText,
                                        ApplicationMessages.CompetitorsErrorText,
                                        CompetidoresErrorText,
                                        CompetidoresDeletedText,
                                        ApplicationMessages.Close);
                                    errorCompetitorsCount = 0;
                                    syncedCompetitorsCount = 0;
                                }
                            }
                            //ASOSA SACAR RRCC
                        //}
                        //else
                        //{
                        //    await StopSpinner();
                        //    await AlertService.DisplayCustomAlertConfirmation(
                        //        ApplicationMessages.ErrorDrawable,
                        //        ApplicationMessages.Error,
                        //        ApplicationMessages.RRCCNotRetailSync,
                        //        ApplicationMessages.Accept);
                        //}
                        //ASOSA SACAR RRCC
                    }
                    else
                    {
                        await StopSpinner();
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.NoInternetConnection,
                                                    ApplicationMessages.Accept);
                    }
                    //}
                }
                else
                {

                    //ASOSA ACA ERROR PONER EL ICONO EN ROJO PERO NO ES ERROR SAP  TYPE != S

                    //ASOSA ACA ERROR PONER EL ICONO EN ROJO
                }


                #region ASOSA LOGIN VEO SI ESTA OK BEGIN
                //}
                //else
                //{
                //    if (Navigation.HasPagesInPopupStack())
                //        await StopSpinner();

                //    await AlertService.DisplayCustomAlertConfirmation(
                //            ApplicationMessages.ErrorDrawable,
                //            ApplicationMessages.Error,
                //           ApplicationMessages.ExpiredCredentialsError,
                //            ApplicationMessages.Accept);
                //    // ASOSA Go to Login Begin
                //    await DeleteLogin();
                //    await Navigation.PopAsync();
                //    // ASOSA Go to Login End
                //}
                #endregion ASOSA LOGIN VEO SI ESTA OK END


            }
            catch (LoginException e)
            {
                if (Navigation.HasPagesInPopupStack())
                    await Navigation.PopPopUpAsync();
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.ExpiredCredentialsError,
                                            ApplicationMessages.Accept);
                Crashes.TrackError(e);

            }
            catch (NoConnectionException ex)
            {
                await StopSpinner();
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.NoInternetConnection,
                                            ApplicationMessages.Accept);
            }
        }

        private async void CheckForUnassignedCompetitorsAndProducts()
        {
            var competitorsLocal = await CompetidoresService.GetAllDB();
            List<string> clientesAEliminar = new List<string>();

            for (int i = 0; i < InteraccionesDB.Count; i++)
            {
                if (!competitorsLocal.Any(x => x.InterComercial == InteraccionesDB[i].Cliente))
                {
                    clientesAEliminar.Add(InteraccionesDB[i].Cliente);
                }
            }

            for (int i = 0; i < clientesAEliminar.Count; i++)
            {
                for (int j = 0; j < InteraccionesDB.Count; j++)
                {
                    if (clientesAEliminar[i].Equals(InteraccionesDB[j].Cliente))
                    {
                        InteraccionesDB.Remove(InteraccionesDB[j]);
                    }
                }
            }

            var matchingProducts = ProductosDB.Where(x => IsMatchingProduct(clientesAEliminar, x)).ToList();
            var notMatchingProducts = ProductosDB.Except(matchingProducts).ToList();
            ProductosDB = notMatchingProducts;

        }
        public bool IsMatchingProduct(List<string> clientesAEliminar, ProductoLocal productoLocal)
        {
            //return clientesAEliminar.Any(x => x.Equals(productoLocal.Cliente));
            return clientesAEliminar.Contains(productoLocal.Cliente);
        }
        //TODO: terminar metodo
        public async Task<bool> GetLocalInteractionAndProducts()
        {
            var LastSyncDate = DateTime.Now;
            await LocalStorageService.Store<DateTime>(LastSyncDate, "LastSyncDate");
            List<ProductoLocal> ultimosProductos = new List<ProductoLocal>();
            try
            {
                var queryInteracciones = await CabeceraInteraccionLocalService.Query("SELECT * FROM CabeceraInteraccionLocal WHERE CabeceraInteraccionLocal.Cliente = ?", Cliente);
                var interaccion = queryInteracciones.FirstOrDefault();
                InteraccionesDB.Add(interaccion);

                var productosASincronizar = await GetLastProducts(interaccion);
                productosASincronizar.ForEach(x => ProductosDB.Add(x));

            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, e.Message, ApplicationMessages.Accept);
                Crashes.TrackError(e);
                return false;
            }


            return true;
        }

        public async Task<bool> GetAllLocalProductsAndInteractions()
        {
            var LastSyncDate = DateTime.Now;
            await LocalStorageService.Store<DateTime>(LastSyncDate, "LastSyncDate");
            List<ProductoLocal> ultimosProductos = new List<ProductoLocal>();
            try
            {
                var interaccionesDB = await CabeceraInteraccionLocalService.GetAllDB();
                var interaccionesToSync = interaccionesDB.Where(x => Items.Any(y => y.InterComercial.Equals(x.Cliente)));
                // YA NO SE DEBEN SINCRONIZAR LOS QUE ESTÉN EN ESTADO ROJO DEBIDO A QUE SE VA A AGREGAR UN NUEVO ESTADO RELACIONADO A LA WEB
                var filteredInteractionsPending = interaccionesToSync.Where(x => x.SyncState == SyncState.PendingToSync); // || x.SyncState == SyncState.ErrorToSync
                foreach (var interactionPending in filteredInteractionsPending)
                {
                    interactionPending.Uploaded = LastSyncDate;
                    InteraccionesDB.Add(interactionPending);
                }

                /*var productosDB = await ProductoLocalService.GetAllDB();
                var productosToSync = productosDB.Where(x => filteredInteractionsPending.Any(y => y.Cliente.Equals(x.Cliente)));
                var filteredProductsPending = productosToSync.Where(x => x.SyncState == SyncState.PendingToSync || x.SyncState == SyncState.ErrorToSync);
                var productosOrdenadosAscendente = filteredProductsPending.OrderBy(x => x.FechaCreacion);
                var productosAgrupadosPorFecha = productosOrdenadosAscendente.GroupBy(x => x.FechaCreacion);
                if (productosAgrupadosPorFecha.Count() > 0) {
                    ultimosProductos = productosAgrupadosPorFecha.Last().ToList();
                }

                foreach (var prodPending in ultimosProductos)
                {
                    prodPending.Uploaded = LastSyncDate;
                    ProductosDB.Add(prodPending);
                }*/
                foreach (var item in filteredInteractionsPending)
                {
                    var productosDB = await GetLastProducts(item);
                    productosDB.ForEach(x => ProductosDB.Add(x));

                }


            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, e.Message, ApplicationMessages.Accept);
                Crashes.TrackError(e);
                return false;
            }


            return true;
        }

        public async Task<List<ProductoLocal>> GetProducts(string interComercial, CabeceraInteraccionLocal cabeceraInteraccion = null)
        {
            List<ProductoLocal> productosLocales = null;
            if (cabeceraInteraccion != null)
            {
                productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", cabeceraInteraccion.Cliente);
            }
            else
            {
                productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", interComercial);
            }

            List<ProductoLocal> ultimosProductos = null;
            if (productosLocales.Count > 0)
            {
                //TODO: hay que contemplar toda la fecha en formato AAAAMMDD HHMMSS
                var productosOrdenadosAscendente = productosLocales.OrderBy(x => x.FechaCreacion);
                //var productosOrdenadosAscendente = productosLocales.OrderBy(x => Convert.ToDateTime(x.IdCabecera));
                //var productosAgrupadosPorFecha = productosOrdenadosAscendente.GroupBy(x => Convert.ToDateTime(x.IdCabecera));
                var productosAgrupadosPorFecha = productosOrdenadosAscendente.GroupBy(x => x.FechaCreacion);
                ultimosProductos = productosAgrupadosPorFecha.Last().ToList();
            }

            return ultimosProductos;
        }

        public async Task<List<ProductoLocal>> GetLastProducts(CabeceraInteraccionLocal cabeceraInteraccion)
        {
            List<ProductoLocal> productosLocales = null;
            productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", cabeceraInteraccion.Cliente);

            List<ProductoLocal> ultimosProductos = null;
            if (productosLocales.Count > 0)
            {
                //TODO: hay que contemplar toda la fecha en formato AAAAMMDD HHMMSS
                var productosPending = productosLocales.Where(x => x.SyncState == SyncState.PendingToSync || x.SyncState == SyncState.ErrorToSync);
                var productosOrdenadosAscendente = productosPending.OrderBy(x => x.FechaCreacion);
                var productosAgrupadosPorFecha = productosOrdenadosAscendente.GroupBy(x => x.FechaCreacion);
                ultimosProductos = productosAgrupadosPorFecha.Last().ToList();
            }

            return ultimosProductos;
        }

        private bool GetCompetitorsErrors(CabeceraInteraccionLocal x)
        {
            var competidor = CompetidoresService.GetCompetitorByHeader(x.Cliente).Result;
            if (competidor != null)
            {
                return competidor.Estado == CompetitorState.ErrorSync;
            }

            return false;
        }

        /*public async void CheckForUnassignedProductsAndCompetitors() {

            if (Util.HasServiceConnectivity())
            {
                var rrcc = await RepresentanteComercialService.GetDB();

                var competidoresSAP = await GetCompetitorsFromSAP(rrcc.Usuario);
                var competidoresDB = await CompetidoresService.GetAllDB();

                foreach (var itemDB in competidoresDB)
                {
                    if (!competidoresSAP.Any(x => x.InterComercial == itemDB.InterComercial))
                    {
                        Console.WriteLine("--- BORRO COMPETIDOR " + itemDB.APIES + " - " + itemDB.RazonSocial + "---");
                        await CompetidoresService.Delete(itemDB.APIES);
                    }
                }
                await ReloadCompetitors(competidoresSAP);
                var listProductos = await PreciosProductosService.GetRelevamientoPreciosProducto();
                var listProductosDB = await PreciosProductosService.GetAllDB();

                foreach (var prodDB in listProductosDB)
                {
                    if (!listProductos.Any(x => x.CodigoSAP == prodDB.CodigoSAP))
                    {
                        Console.WriteLine("--- BORRO PRODUCTO " + prodDB.CodigoSAP + " - " + prodDB.Descripcion + "---");
                        await PreciosProductosService.Delete(prodDB);
                    }
                }

                foreach (var prodSAP in listProductos)
                {
                    if (!listProductosDB.Any(x => x.CodigoSAP == prodSAP.CodigoSAP)) { 
                        await PreciosProductosService.Save(prodSAP);
                    }
                }
            }
            else
            {
                await AlertService.DisplayCustomAlertConfirmation(
                    ApplicationMessages.ErrorDrawable,
                    ApplicationMessages.Error,
                    ApplicationMessages.NoServiceConnection,
                    ApplicationMessages.Accept);
            }
            
        }*/
        public async Task<List<Competidor>> GetCompetitorsFromSAP(string IdRed)
        {
            LlamadaRFC_Competidores llamadaRFC_Competidores = await CompetidoresService.GetCompetidores(IdRed);

            var competidoresSAP = GetCompetitors(llamadaRFC_Competidores);

            return competidoresSAP;
        }

        public List<Competidor> GetCompetitors(LlamadaRFC_Competidores llamadaRFC_Competidores)
        {
            List<Competidor> competidores = new List<Competidor>();

            Competidor Competidor = null;

            foreach (var competidor in llamadaRFC_Competidores.datosGenerales)
            {
                var compDB = CompetidoresService.GetCompetitorByHeader(competidor.InterComercial).Result;
                Competidor = new Competidor()
                {
                    Agrupacion = competidor.Agrupacion,
                    APIES = string.IsNullOrEmpty(competidor.APIES) ? "00000" : competidor.APIES,
                    Atributo = competidor.Atributo,
                    CantTarjetaYer = competidor.CantTarjetaYer,
                    CodDirent = competidor.CodDirent,
                    Contacto = competidor.Contacto,
                    CuentaLP2 = competidor.CuentaLP2,
                    CuentaLPO = competidor.CuentaLPO,
                    CuentaQP1 = competidor.CuentaQP1,
                    CuentaSGC = competidor.CuentaSGC,
                    Cuit = competidor.CUIT,
                    InterComercial = competidor.InterComercial,
                    RazonSocial = competidor.RazSoc,
                    NumeroExpediente = competidor.NumExp,
                    OperaYer = competidor.OperaYer,
                    UnidadNegocio = competidor.UnidadNegocio,
                    Latitud = competidor.Latitud,
                    Longitud = competidor.Longitud,
                    Estado = (compDB == null) ? CompetitorState.NoPrizeSurvey : compDB.Estado
                };
                competidores.Add(Competidor);
            }
            return competidores;
        }

        public async Task<bool> SyncAndUploadToRFC()
        {
            try
            {
                if (await Util.HasInternetConnectionAsync())
                {
                    if (await Util.HasServiceConnectivityAsync())
                    {
                        string IdRed = await LocalStorageService.GetUserLogin();
                        UploadRelevamientoPreciosModel relevamientoPreciosModel = new UploadRelevamientoPreciosModel();
                        relevamientoPreciosModel.Owner = IdRed;
                        relevamientoPreciosModel.Interacciones = LoadInteractionsToModel(InteraccionesDB);
                        relevamientoPreciosModel.RelevamientoPrecios = LoadProductsToModel(ProductosDB);

                        //throw new Exception();//ASOSA SACAR
                        ResultArray resultArray = await ProductoLocalService.UploadToRFC(relevamientoPreciosModel);



                        List<GenericResultModelResponse> listInteractionsProcessed = resultArray.Data;
                        List<LlamadaRFC_ResponseUpload> listLlamadasRFC = GetUploadResponses(listInteractionsProcessed);
                        OnSyncPrecioProducto(InteraccionesDB, ProductosDB, listLlamadasRFC);

                    }
                    else
                    {
                        await StopSpinner();
                        await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, ApplicationMessages.NoServiceConnection, ApplicationMessages.Accept);
                        return false;
                        //AlertService.DisplayError(ApplicationMessages.NoServiceConnection);
                    }

                }
                else
                {
                    throw new NoConnectionException();
                }
            }
            catch (NoConnectionException ex)
            {
                await StopSpinner();
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.NoInternetConnection,
                                            ApplicationMessages.Accept);
                return false;
            }
            catch (LoginException e)
            {
                await StopSpinner();
                if (Navigation.HasPagesInPopupStack())
                {

                    await AlertService.DisplayCustomAlertConfirmation(
                                                ApplicationMessages.ErrorDrawable,
                                                ApplicationMessages.Error,
                                                ApplicationMessages.ExpiredCredentialsError,
                                                ApplicationMessages.Accept);
                    CallbackCerrarSesionOKButton();
                }

                Crashes.TrackError(e);
                return false;

            }
            catch (Exception e)
            {
                await StopSpinner();
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, ApplicationMessages.SyncPriceSurveyError, ApplicationMessages.Accept);
                CallbackCerrarSesionOKButton();
                Crashes.TrackError(e);
                return false;
            }
            /*finally {
                InteraccionesDB.Clear();
                ProductosDB.Clear();
            }*/

            return true;
        }
        public List<LlamadaRFC_ResponseUpload> GetUploadResponses(List<GenericResultModelResponse> list)
        {
            List<string> RFCCalls = new List<string>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            List<LlamadaRFC_ResponseUpload> UploadResponses = new List<LlamadaRFC_ResponseUpload>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(x => ProcessUploadResponses(keyValues, x));
                foreach (var item in keyValues)
                {
                    if (item.Value != null)
                    {
                        LlamadaRFC_ResponseUpload responseUpload = JsonConvert.DeserializeObject<LlamadaRFC_ResponseUpload>(item.Value);
                        if (responseUpload.cabecera != null)
                        {
                            responseUpload.cabecera[0].CodTranInt = item.Key;
                        }
                        else
                        {
                            responseUpload.cabecera = new List<Cabecera>();
                            responseUpload.cabecera.Add(new Cabecera());
                            responseUpload.cabecera[0].CodTranInt = item.Key;
                        }

                        UploadResponses.Add(responseUpload);
                    }
                    else
                    {
                        LlamadaRFC_ResponseUpload responseUpload = new LlamadaRFC_ResponseUpload();
                        responseUpload.cabecera = new List<Cabecera>();
                        Cabecera cabecera = new Cabecera();
                        cabecera.CodTranInt = item.Key;
                        responseUpload.cabecera.Add(cabecera);

                        responseUpload.retornorfc = new List<RetornoRFC>();
                        RetornoRFC retornoRFC = new RetornoRFC();
                        retornoRFC.TYPE = "E";
                        retornoRFC.MESSAGE = ApplicationMessages.SendInteractionsErrorNotification;
                        responseUpload.retornorfc.Add(retornoRFC);

                        UploadResponses.Add(responseUpload);
                    }
                }
            }

            return UploadResponses;
        }

        public void ProcessUploadResponses(Dictionary<string, string> keyValues, GenericResultModelResponse response)
        {
            if (response.SubidaOk && response.LlamadaRfc != null)
            {
                keyValues.Add(response.CodigoInterno, response.LlamadaRfc);
            }
            else
            {
                response.LlamadaRfc = null;
                keyValues.Add(response.CodigoInterno, response.LlamadaRfc);
            }

        }

        public List<RelevamientoPreciosProductoUpload> LoadProductsToModel(List<ProductoLocal> productos)
        {
            List<RelevamientoPreciosProductoUpload> relevamientoPreciosProductos = new List<RelevamientoPreciosProductoUpload>();
            RelevamientoPreciosProductoUpload relevamientoPreciosProducto = null;
            foreach (var item in productos)
            {
                relevamientoPreciosProducto = new RelevamientoPreciosProductoUpload()
                {
                    Id = item.IdCabecera,
                    Envase = item.Envase,
                    Precio = item.Precio,
                    PrecioSpecified = item.PrecioSpecified,
                    PrecioCompra = item.PrecioCompra,
                    PrecioCompraSpecified = item.PrecioCompraSpecified,
                    PrecioDist = item.PrecioDist,
                    PrecioDistSpecified = item.PrecioDistSpecified,
                    Producto = item.Producto,
                    Volumen = item.Volumen
                };
                relevamientoPreciosProductos.Add(relevamientoPreciosProducto);
            }

            return relevamientoPreciosProductos;
        }

        public List<CabeceraInteraccion> LoadInteractionsToModel(List<CabeceraInteraccionLocal> interacciones)
        {
            List<CabeceraInteraccion> cabeceraInteraccionesModelList = new List<CabeceraInteraccion>();
            CabeceraInteraccion cabeceraInteraccion = null;
            foreach (var item in interacciones)
            {
                cabeceraInteraccion = new CabeceraInteraccion()
                {
                    Id = item.IdCabecera,
                    Calle = item.Calle,
                    Categoria = item.Categoria,
                    Ciudad = item.Ciudad,
                    Cliente = item.Cliente,
                    CodFormulario = item.CodFormulario,
                    CodPostal = item.CodPostal,
                    CodTranInt = item.CodTranInt,
                    Descripcion = item.Descripcion,
                    Estado = item.Estado,
                    FechaCreac = item.FechaCreac,
                    FechaFinP = item.FechaFinP,
                    FechaFinR = item.FechaFinR,
                    FechaIniP = item.FechaIniP,
                    FechaIniR = item.FechaIniR,
                    HoraFinP = item.HoraFinP,
                    HoraFinR = item.HoraFinR,
                    HoraIniP = item.HoraIniP,
                    HoraIniR = item.HoraIniR,
                    Latitud = item.Latitud,
                    Longitud = item.Longitud,
                    Motivo = item.Motivo,
                    Negocio = item.Negocio,
                    NombreCliente = item.NombreCliente,
                    NombreResponsable = item.NombreResponsable,
                    NombreRRCC = item.NombreRRCC,
                    NroActividad = item.NroActividad,
                    Numero = item.Numero,
                    Operacion = item.Operacion,
                    Pais = item.Pais,
                    Provincia = item.Provincia,
                    Puntaje = item.Puntaje,
                    PuntajeSpecified = item.PuntajeSpecified,
                    Responsable = item.Responsable,
                    RRCC = item.RRCC,
                    Segmento = item.Segmento,
                    Texto0002 = item.Texto0002,
                    TextoZR01 = item.TextoZR01,
                    TextoZR02 = item.TextoZR02,
                    TextoZR07 = item.TextoZR07,
                    TextoZR08 = item.TextoZR08,
                    TextoZR09 = item.TextoZR09,
                    TextoZR10 = item.TextoZR10,
                    TextoZR11 = item.TextoZR11

                };
                cabeceraInteraccionesModelList.Add(cabeceraInteraccion);
            }

            return cabeceraInteraccionesModelList;
        }


        private async Task DoAboutRRCC()
        {
            await Navigation.PushPopUpAsync<RepresentanteComercialViewModel>();
        }

        private async Task DoCloseSessionApp()
        {
            await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable, ApplicationMessages.Warning, ApplicationMessages.CloseSession, ApplicationMessages.Affirmative, ApplicationMessages.Negative, actionOKCerrarSesionButton);
        }

        public async void CallbackCerrarSesionOKButton()
        {
            await DeleteLogin();

            IsClosingSession = true;
            EventsManager.TriggerEvent("OnCloseSession", IsClosingSession);
            await LocalStorageService.ClearUserCache();
            await Navigation.PopAsync();
        }



        private async Task DeleteLogin()
        {
            #region ASOSA  AGREGADO PARA QUE DESPUES DE CERRAR SESION AL REINICIAR , No SE LOGUEE SOLO 
            await LocalStorageService.Store<bool>(true, "IsSessionClosed");
            #endregion

            await LocalStorageService.ClearUserCache();
            await LocalStorageService.RemoveAuthToken();

        }

        /*public void SetPickerState(Picker picker)
        {
            PickerState = picker;
        }*/

        public void SetInputSearch(SearchBar input)
        {
            InputSearch = input;
        }

        private void OnSelectedStateFilter(object[] parameterContainer)
        {
            FiltrarCompetidores();
            CheckOrder();
        }

        public async void InputSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarCompetidores();
            CheckOrder();
        }
        public async Task ReloadCompetitors()
        {
            Items.Clear();
            IsEmptyListView = false;
            IsHeaderShowing = true;
            EmptyListViewText = "";
            Items = await GetCompetidores();
            ItemsBackup = Items.ToList();
            var itemsRazonSocialAscendingOrder = Items.OrderBy(x => x.APIES).ToList();
            Items.Clear();
            itemsRazonSocialAscendingOrder.ForEach(x => Items.Add(x));
            IsTappedAPIES = true;

            FiltrarCompetidores();
            CheckOrder();
        }

        public async Task<bool> ReloadCompetitors(List<Competidor> competidoresSAP = null)
        {
            try
            {
                Items.Clear();
                IsEmptyListView = false;
                IsHeaderShowing = true;
                EmptyListViewText = "";
                Items = await GetCompetidores(competidoresSAP);
                ItemsBackup = Items.ToList();
                var itemsRazonSocialAscendingOrder = Items.OrderBy(x => x.APIES).ToList();
                Items.Clear();
                itemsRazonSocialAscendingOrder.ForEach(x => Items.Add(x));
                IsTappedAPIES = true;
            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, ApplicationMessages.LoadCompetitorsError, ApplicationMessages.Accept);
                Crashes.TrackError(e);
                return false;

            }

            return true;
        }

        public async Task<ObservableCollection<CompetidoresGroupModel>> GetCompetidores(List<Competidor> competidoresSAP = null)
        {

            ObservableCollection<CompetidoresGroupModel> CompetidoresGroupModels = new ObservableCollection<CompetidoresGroupModel>();
            if (competidoresSAP != null && competidoresSAP.Count > 0)
            {
                Competidores = competidoresSAP;
            }
            else
            {
                Competidores = await CompetidoresService.GetAllDB();
            }

            CompetidoresGroupModel competidor = null;
            Items.Clear();
            foreach (var item in Competidores)
            {
                competidor = new CompetidoresGroupModel()
                {
                    APIES = string.IsNullOrEmpty(item.APIES) ? "00000" : item.APIES,
                    Bandera = item.Atributo,
                    CUIT = item.Cuit,
                    Contacto = item.Contacto,
                    CantTarjetaYer = item.CantTarjetaYer,
                    CuentaLP2 = item.CuentaLP2,
                    CuentaLPO = item.CuentaLPO,
                    CuentaQP1 = item.CuentaQP1,
                    CuentaSGC = item.CuentaSGC,
                    InterComercial = item.InterComercial,
                    NumeroExpediente = item.NumeroExpediente,
                    UnidadNegocio = item.UnidadNegocio,
                    Agrupacion = item.Agrupacion,
                    Atributo = item.Atributo,
                    CodDirent = item.CodDirent,
                    OperaYer = item.OperaYer,
                    RazonSocial = item.RazonSocial,
                    Latitud = item.Latitud,
                    Longitud = item.Longitud,
                    Estado = item.Estado
                };
                CompetidoresGroupModels.Add(competidor);

            }
            return CompetidoresGroupModels;
        }


        public async void CompetidoresListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (IsClickedOnce)
            {
                CompetidoresGroupModel competidor = ((CompetidoresGroupModel)e.Item);
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                keyValuePairs.Add("Competidor", competidor);
                keyValuePairs.Add("ListaCompetidores", Items);
                keyValuePairs.Add("IsFirstExecution", IsFirstExecution);

                IsFirstExecution = false;
                IsClickedOnce = false;
                await Navigation.PushAsync<CargaPreciosViewModel>(keyValuePairs);

            }

        }
        #region Filters


        /*public void PickerState_SelectedIndexChanged(object sender, EventArgs e) {
            Picker picker = (Picker)sender;
            var valueSelected = picker.SelectedIndex;
            if (valueSelected != -1) {
                SelectedState = _statesList[valueSelected];
                picker.SelectedItem = null;
            }
            
        }*/

        public async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var label = (Label)sender;

            if (label.Text.Equals("APIES"))
            {
                tapCountAPIES++;

                if (tapCountAPIES % 2 == 0)
                {
                    IsDoubleTapAPIES = true;
                    IsTappedAPIES = false;
                    IsDoubleTapRazonSocial = false;
                    IsTappedRazonSocial = false;
                    IsDoubleTapEstado = false;
                    IsTappedEstado = false;
                    OrderByDescending(label.Text);
                }
                else
                {
                    IsDoubleTapAPIES = false;
                    IsTappedAPIES = true;
                    IsDoubleTapRazonSocial = false;
                    IsTappedRazonSocial = false;
                    IsDoubleTapEstado = false;
                    IsTappedEstado = false;
                    OrderByAscending(label.Text);
                }

            }
            else if (label.Text.Equals("Razón Social"))
            {
                tapCountRazonSocial++;
                if (tapCountRazonSocial % 2 == 0)
                {
                    IsDoubleTapRazonSocial = true;
                    IsTappedRazonSocial = false;
                    IsTappedAPIES = false;
                    IsDoubleTapAPIES = false;
                    IsDoubleTapEstado = false;
                    IsTappedEstado = false;
                    OrderByDescending(label.Text);
                }
                else
                {
                    IsDoubleTapRazonSocial = false;
                    IsTappedRazonSocial = true;
                    IsTappedAPIES = false;
                    IsDoubleTapAPIES = false;
                    IsDoubleTapEstado = false;
                    IsTappedEstado = false;
                    OrderByAscending(label.Text);
                }
            }
            else
            {
                tapCountEstado++;
                if (tapCountEstado % 2 == 0)
                {
                    IsDoubleTapEstado = true;
                    IsTappedEstado = false;
                    IsDoubleTapAPIES = false;
                    IsTappedAPIES = false;
                    IsDoubleTapRazonSocial = false;
                    IsTappedRazonSocial = false;
                    OrderByDescending(label.Text);
                }
                else
                {
                    IsDoubleTapEstado = false;
                    IsTappedEstado = true;
                    IsDoubleTapAPIES = false;
                    IsTappedAPIES = false;
                    IsDoubleTapRazonSocial = false;
                    IsTappedRazonSocial = false;
                    OrderByAscending(label.Text);
                }
            }
        }

        public void OrderByDescending(string orderFilter)
        {

            if (orderFilter.Equals("APIES"))
            {
                var itemsAPIESDescendingOrder = Items.OrderByDescending(x => x.APIES).ToList();
                Items.Clear();
                itemsAPIESDescendingOrder.ForEach(x => Items.Add(x));
            }
            else if (orderFilter.Equals("Razón Social"))
            {
                var itemsRazonSocialDescendingOrder = Items.OrderByDescending(x => x.RazonSocial).ToList();
                Items.Clear();
                itemsRazonSocialDescendingOrder.ForEach(x => Items.Add(x));
            }
            else if (orderFilter.Equals("Estado"))
            {
                var temp = Items.OrderByDescending(c => Convert.ToInt32(c.Estado)).ToList();
                Items.Clear();
                foreach (var item in temp) Items.Add(item);
            }


        }

        public void OrderByAscending(string orderFilter)
        {

            if (orderFilter.Equals("APIES"))
            {
                var itemsAPIESAscendingOrder = Items.OrderBy(x => x.APIES).ToList();
                Items.Clear();
                itemsAPIESAscendingOrder.ForEach(x => Items.Add(x));
            }
            else if (orderFilter.Equals("Razón Social"))
            {
                var itemsRazonSocialAscendingOrder = Items.OrderBy(x => x.RazonSocial).ToList();
                Items.Clear();
                itemsRazonSocialAscendingOrder.ForEach(x => Items.Add(x));
            }
            else if (orderFilter.Equals("Estado"))
            {
                var temp = Items.OrderBy(c => Convert.ToInt32(c.Estado)).ToList();
                Items.Clear();
                foreach (var item in temp) Items.Add(item);
            }

        }

        private async void FiltrarCompetidores()
        {
            var inputSearch = string.IsNullOrEmpty(InputSearch.Text) ? "" : InputSearch.Text.ToUpper();
            IEnumerable<Bandera> banderaFiltered = new List<Bandera>();
            if (!string.IsNullOrEmpty(inputSearch))
            {
                var banderas = await BanderaService.GetAllDB();
                banderaFiltered = banderas.Where(x => x.Descripcion.ToUpper().Equals(inputSearch));
            }

            string atributo = "";
            List<CompetidoresGroupModel> temp = null;
            if (banderaFiltered.Count() > 0)
            {
                atributo = banderaFiltered.FirstOrDefault().CodigoSAP;
                temp = ItemsBackup.Where(x => x.Atributo.Contains(atributo)).ToList();
            }
            else
            {
                temp = ItemsBackup.Where(x => x.APIES.ToUpper().Contains(inputSearch) || x.RazonSocial.ToUpper().Contains(inputSearch)).ToList();
            }

            if (SelectedState != null && SelectedState != "Todos")
                temp = temp.Where(x => EnumExtensions.GetDescription(x.Estado).ToUpper().Equals(SelectedState.ToUpper())).ToList();

            Items.Clear();
            temp.ForEach(x => Items.Add(x));

            if (Items.Count == 0)
            {
                IsEmptyListView = true;
                IsHeaderShowing = false;
                if (await HasCompetitorsLocal())
                    EmptyListViewText = ApplicationMessages.EmptyCompetitorsListView;
                else
                    EmptyListViewText = ApplicationMessages.EmptyCompetitorsFromSyncListView;
            }
            else
            {
                IsEmptyListView = false;
                IsHeaderShowing = true;
            }
        }

        private async Task<bool> HasCompetitorsLocal()
        {
            var competitorsDB = await CompetidoresService.GetAllDB();
            return competitorsDB.Any();
        }

        public void CheckOrder()
        {
            if (IsTappedAPIES)
            {
                OrderByAscending("APIES");
            }
            else if (IsDoubleTapAPIES)
            {
                OrderByDescending("APIES");
            }
            else if (IsTappedRazonSocial)
            {
                OrderByAscending("Razón Social");
            }
            else if (IsDoubleTapRazonSocial)
            {
                OrderByDescending("Razón Social");
            }
            else if (IsTappedEstado)
            {
                OrderByAscending("Estado");
            }
            else if (IsDoubleTapEstado)
            {
                OrderByDescending("Estado");
            }
        }
        /*public bool HasPrizesToSync() {
            return Items.Count > 0 && Items.Any(x => x.Estado == CompetitorState.PrizeNoSync);
        }*/

        public void SetBtnSincronizarPrecios(Button syncPrices)
        {
            BtnSincronizarPrecios = syncPrices;
        }

        public void SetBtnNuevoRelevamiento(ImageButton btnNuevoRelevamiento)
        {
            BtnNuevoRelevamiento = btnNuevoRelevamiento;
        }

        public void SetPickerState(ImageButton picker)
        {
            BtnPickerState = picker;
        }

        public async Task CheckRRCCIfRetail()
        {
            Negocio negocio = null;
            Segmento segmento = null;
            if (await Util.HasInternetConnectionAsync())
            {
                if (await Util.HasServiceConnectivityAsync())
                {
                    try
                    {


                        //var IdRed = InfoModel.username;
                        var IdRed = await LocalStorageService.GetUserLogin();
                        var idRedFiltered = ProcessIdRed(IdRed);
                        var repComerciales = await RepresentanteComercialService.GetRepresentanteComercial(idRedFiltered);

                        if (repComerciales.Count() > 0)
                        {
                            var rrccSync = repComerciales.First();

                            if (rrccSync != null)
                            {
                                var rrccDB = await RepresentanteComercialService.GetOneFromDB(rrccSync.CodigoInterlocutor);
                                if (rrccDB != null && rrccDB.IdNegocio != rrccSync.IdNegocio)
                                {
                                    rrccDB.IdNegocio = rrccSync.IdNegocio;
                                    await RepresentanteComercialService.Update(rrccDB);
                                }

                                negocio = await GetNegocioById(int.Parse(rrccSync.IdNegocio));
                                if (negocio != null)
                                {
                                    segmento = await GetSegmentoById(negocio.IdSegmento);
                                    IsRetail = segmento.CodigoSAP.Equals(ApplicationConstants.RELEVAMIENTO_PRECIOS_SEGMENTO) && negocio.Descripcion.Equals(ApplicationConstants.RELEVAMIENTO_PRECIOS_NEGOCIO);
                                }
                                else
                                {
                                    Exception e = new Exception("NEGOCIO EXCEPTION: No existe el ID Negocio.");

                                    Crashes.TrackError(e);
                                    throw e;
                                }
                            }
                        }
                    }
                    catch (LoginException e)
                    {
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.ExpiredCredentialsError,
                                                    ApplicationMessages.Accept);
                        Crashes.TrackError(e);

                        #region ASOSA Go to Login 
                        CallbackCerrarSesionOKButton();
                        await Navigation.PopAsync();
                        #endregion


                        return;

                    }
                    catch (NoConnectionException ex)
                    {
                        HasNoSuddenConnection = true;
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.NoInternetConnection,
                                                    ApplicationMessages.Accept);

                        return;
                    }
                    catch (Exception e)
                    {

                        RRCCHasErrors = true;
                        await StopSpinner();
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.SyncGralError,
                                                    ApplicationMessages.Accept);


                        Crashes.TrackError(e);

                    }
                }
                else
                {
                    RRCCHasErrors = true;
                    await StopSpinner();
                    await AlertService.DisplayCustomAlertConfirmation(
                        ApplicationMessages.ErrorDrawable,
                        ApplicationMessages.Error,
                        ApplicationMessages.NoServiceConnection,
                        ApplicationMessages.Accept);
                }
            }
            else
            {
                RRCCHasErrors = true;
                throw new NoConnectionException();
            }
        }

        public async Task<Segmento> GetSegmentoById(int idSegmento)
        {
            var segmentos = await SegmentoService.GetSegmentos();
            var segmentoById = segmentos.Where(x => x.IdSegmento == idSegmento);
            var segmento = segmentoById.FirstOrDefault();

            return segmento;
        }

        public async Task<Negocio> GetNegocioById(int idNegocio)
        {
            var negocios = await NegocioService.GetNegocios();
            var negocioById = negocios.Where(x => x.IdNegocio == idNegocio);
            var negocio = negocioById.FirstOrDefault();

            return negocio;
        }

        public string ProcessIdRed(string idRed)
        {
            string IdRed = "";
            if (idRed.Contains("@"))
            {
                var index = idRed.IndexOf('@');
                var idRedFiltered = idRed.Substring(0, index);
                IdRed = idRedFiltered;
            }
            else
            {
                IdRed = idRed;
            }

            return IdRed;
        }

        void ICompetidoresViewModel.SyncSingleCompetitorCommand(object sender, EventArgs e)
        {
            DoSyncSingleCompetitorCommand.Execute(sender);
        }

        #endregion

        #endregion
    }
}
