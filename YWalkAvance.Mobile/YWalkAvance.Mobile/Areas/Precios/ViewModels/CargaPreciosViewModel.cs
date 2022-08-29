using Business.Dominio;
using Business.Services;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Commons.Commons.Entities;
using Frontend.Mobile.Areas.Precios.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Helpers;
using Frontend.Mobile.Commons.Models;
using Frontend.Mobile.Services;
using Frontend.Mobile.ViewModels;
using Microsoft.AppCenter.Crashes;
using Services.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Precios.ViewModels
{
    public class CargaPreciosViewModel : BaseViewModel, ICargaPreciosViewModel
    {
        /*
        TODO: - Agregar máximos y mínimos en los valores de Precio Producto. 
              - División del campo de Precio en dos campos, uno de tres numeros enteros y uno de dos numeros decimales. 
              - Agregar una notificación de Precios cargados no guardados al cambiar de página.
        */
        #region Services
        public IBanderaService BanderaService { get; set; }
        public IDireccionesCompetidorService DireccionesCompetidorService { get; set; }
        public IRelevamientoPreciosProductoService RelevamientoPreciosProductoService { get; set; }
        public IRepresentanteComercialService RepresentanteComercialService { get; set; }
        public INegocioService NegocioService { get; set; }
        public IProvinciaService ProvinciaService { get; set; }
        public ICabeceraInteraccionLocalService CabeceraInteraccionLocalService { get; set; }
        public IProductoLocalService ProductoLocalService { get; set; }
        public ICompetidoresService CompetidoresService { get; set; }


        #endregion
        #region Actions
        public Action ActionRestartPrizesOKButton { get; set; }
        public Action ActionNextOKButton { get; set; }
        public Action ActionPreviousOKButton { get; set; }
        public Action ActionNextCancelButton { get; set; }
        public Action ActionPreviousCancelButton { get; set; }

        public Action ActionPopAsyncOKButton { get; set; }
        public Action ActionPopAsyncCancelButton { get; set; }
        public Action ActionOKEnterosNoDotTextChanged { get; private set; }
        public Action ActionOKDecimalesNoDotTextChanged { get; private set; }
        public Action ActionOKEnterosOnlyNumbersTextChanged { get; private set; }
        public Action ActionOKDecimalesOnlyNumbersTextChanged { get; private set; }
        #endregion

        #region Properties

        public Entry precioEnteros { get; set; }
        public Entry precioDecimales { get; set; }
        public Color BackgroundColorHeader { get; set; }
        public CollectionView ListViewProductos { get; set; }
        public Button BtnGuardar { get; set; }
        public Button BtnReiniciarPrecios { get; set; }
        public ImageButton BtnExpandCollapse { get; set; }
        public string APIESRazonSocial { get; set; }
        public List<RelevamientoPreciosProducto> RelevamientoPreciosProductos { get; set; }
        public Bandera Bandera { get; set; }
        public DireccionCompetidor DireccionCompetidor { get; set; }
        private CompetidoresGroupModel _competidor { get; set; }
        public int Index { get; set; }
        public List<Provincia> Provincias { get; set; }
        private Provincia _provincia { get; set; }
        public Provincia Provincia
        {
            get
            {
                return _provincia;
            }
            set
            {
                _provincia = value;
                OnPropertyChanged("Provincia");
            }
        }
        private string _emptyListViewText { get; set; }
        private int _marginEmptyListViewText;
        public int MarginEmptyListViewText
        {
            get
            {
                return _marginEmptyListViewText;
            }
            set
            {
                _marginEmptyListViewText = value;
                OnPropertyChanged("MarginEmptyListViewText");
            }
        }

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
                {
                    BackgroundColorHeader = Color.GhostWhite;
                }

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
        public bool IsFocusedEnteros { get; set; }
        public bool IsFocusedDecimales { get; set; }

        public CompetidoresGroupModel Competidor
        {
            get
            {
                return _competidor;
            }
            set
            {
                _competidor = value;
                OnPropertyChanged("Competidor");
            }
        }
        private RPProductoGroupModel _producto { get; set; }
        public RPProductoGroupModel Producto
        {
            get
            {
                return _producto;
            }
            set
            {
                _producto = value;
                OnPropertyChanged("Producto");
            }
        }
        public int CurrentRegister { get; set; }
        public int TotalRegisters { get; set; }
        public string PagingCount { get; set; }
        private bool _expanded;
        public bool Expanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                _expanded = value;
                OnPropertyChanged("Expanded");
                OnPropertyChanged("StateIcon");
            }
        }
        public string IconExpandCollapse
        {
            get
            {
                return Expanded ? "collapse.png" : "expand.png";
            }
        }
        private ObservableCollection<RPProductoGroupModel> _Productos;
        public ObservableCollection<RPProductoGroupModel> Productos
        {
            get
            {
                return _Productos;
            }
            set
            {
                _Productos = value;
                OnPropertyChanged("Productos");
            }
        }

        private ObservableCollection<CompetidoresGroupModel> _Items;
        private int tapCount;
        private int textChangedIntegersCount;
        private int textChangedDecimalsCount;

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
        public DateTime InitialSyncDate { get; set; }
        #endregion

        #region Commands
        public ICommand CommandRestartPrizes { get; set; }
        public ICommand CommandGoBack { get; set; }
        public ICommand CommandPreviousRecord { get; set; }
        public ICommand CommandNextRecord { get; set; }
        public ICommand CommandSavePriceSurvey { get; set; }
        public ICommand CommandExpandCollapse { get; set; }
        public bool IsBanderaNoExistente { get; private set; }

        #endregion

        #region Constructors
        public CargaPreciosViewModel(IBanderaService banderaService, IDireccionesCompetidorService direccionesCompetidorService,
            IRelevamientoPreciosProductoService relevamientoPreciosProductoService, IRepresentanteComercialService representanteComercialService,
            INegocioService negocioService, ICabeceraInteraccionLocalService cabeceraInteraccionLocalService,
            IProductoLocalService productoLocalService, IProvinciaService provinciaService, ICompetidoresService competidoresService)
        {
            Productos = new ObservableCollection<RPProductoGroupModel>();
            BanderaService = banderaService;
            DireccionesCompetidorService = direccionesCompetidorService;
            RelevamientoPreciosProductoService = relevamientoPreciosProductoService;
            RepresentanteComercialService = representanteComercialService;
            ProvinciaService = provinciaService;
            NegocioService = negocioService;
            CabeceraInteraccionLocalService = cabeceraInteraccionLocalService;
            CompetidoresService = competidoresService;
            ProductoLocalService = productoLocalService;
            CommandGoBack = new Command(async () => await GoBack());
            CommandPreviousRecord = new Command(() => PreviousRecord());
            CommandNextRecord = new Command(() => NextRecord());
            CommandSavePriceSurvey = new Command(async () => await Save(true));
            CommandRestartPrizes = new Command(async () => await RestartPrizes());
            CommandExpandCollapse = new Command(() => ExpandCollapseCompetitorData());
            EmptyListViewText = "";
            IsEmptyListView = false;
            IsHeaderShowing = true;
            IsFocusedDecimales = true;
            IsFocusedEnteros = true;
            Expanded = false;
            ActionRestartPrizesOKButton += CallbackRestartPrizesOKButton;
            ActionPreviousOKButton += Previous;
            ActionNextOKButton += Next;
            ActionNextCancelButton += NextCancel;
            ActionPreviousCancelButton += PreviousCancel;
            ActionPopAsyncOKButton += SaveAndExit;
            ActionPopAsyncCancelButton += PopAsync;
            ActionOKEnterosNoDotTextChanged += CallbackOKEnterosNoDotTextChanged;
            ActionOKDecimalesNoDotTextChanged += CallbackOKDecimalesNoDotTextChanged;
            ActionOKEnterosOnlyNumbersTextChanged += CallbackOKEnterosOnlyNumbersTextChanged;
            ActionOKDecimalesOnlyNumbersTextChanged += CallbackOKDecimalesOnlyNumbersTextChanged;
        }




        #endregion

        #region Methods
        public override async Task InitializeAsync(object data)
        {

            Dictionary<string, object> keyValuePairs = (Dictionary<string, object>)data;
            IsFirstExecution = (bool)keyValuePairs["IsFirstExecution"];
            Competidor = keyValuePairs["Competidor"] as CompetidoresGroupModel;


            Bandera = await GetBanderaDesc(Competidor.Bandera);
            if (Bandera != null)
            {
                Competidor.BanderaDesc = Bandera.Descripcion;
                Productos = await GetProductosPorBandera(Competidor.Bandera);
                IsBanderaNoExistente = false;
            }
            else
            {
                Competidor.BanderaDesc = "No Existente";
                Productos.Clear();
                IsBanderaNoExistente = true;
            }

            if (Expanded)
            {
                ProductosListView_SizeChanged(40);
            }
            else
            {
                ProductosListView_SizeChanged(60);
            }

            DireccionCompetidor = await GetDireccion(Competidor.InterComercial);
            Competidor.Direccion = DireccionCompetidor != null ? DireccionCompetidor.Calle + " " + DireccionCompetidor.Numero : "N/A";
            Provincia = await GetProvincia(DireccionCompetidor.Provincia);
            Competidor.Provincia = (Provincia == null) ? "N/A" : Provincia.Nombre;
            //Competidor.Localidad = (Provincia == null) ? "N/A" : Provincia.Nombre;
            Items = keyValuePairs["ListaCompetidores"] as ObservableCollection<CompetidoresGroupModel>;
            CurrentRegister = Items.IndexOf(Competidor) + 1;
            TotalRegisters = Items.Count;
            PagingCount = "Registro " + CurrentRegister + " de " + TotalRegisters;
            //APIESRazonSocial = _competidor.APIES + " - " + _competidor.RazonSocial;
            APIESRazonSocial = _competidor.RazonSocial + " (" + _competidor.APIES + ")";
        }

        private void SaveAndExit()
        {
            CommandSavePriceSurvey.Execute(null);
        }

        public async Task GoBack()
        {

            EventsManager.TriggerEvent("IsClickedOnceReset");
            var notSavedProducts = await HasNotSavedProducts();
            if (notSavedProducts)
            {
                await AlertService.DisplayCustomAlertConfirmation(
                    ApplicationMessages.WarningDrawable,
                    ApplicationMessages.Warning,
                    ApplicationMessages.NotSavedProductsNotification,
                    ApplicationMessages.Save,
                    ApplicationMessages.Exit, ActionPopAsyncOKButton, ActionPopAsyncCancelButton);
            }
            else
            {
                PopAsync();
            }
        }

        private async void PopAsync()
        {
            EventsManager.TriggerEvent("OnExitButton");
            await Navigation.PopAsync();
        }

        //TODO: completar el recorrido cíclico del paginado. 
        public async void NextRecord()
        {
            await StartSpinner();
            var notSavedProducts = await HasNotSavedProducts();
            if (notSavedProducts)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                    ApplicationMessages.Atencion,
                    ApplicationMessages.NotSavedProductsNotification,
                    ApplicationMessages.Continue,
                    ApplicationMessages.Cancel,
                    ActionNextOKButton,
                    ActionNextCancelButton);
            }
            else
            {
                Next();
            }
        }

        public async void Next()
        {
            int nextIndex = Items.IndexOf(Competidor) + 1;
            if (nextIndex == Items.Count)
            {
                nextIndex = 0;
            }

            if (nextIndex < Items.Count)
            {
                Competidor = Items[nextIndex];
                CurrentRegister = Items.IndexOf(Competidor) + 1;
                TotalRegisters = Items.Count;
                Bandera = await GetBanderaDesc(Competidor.Bandera);
                if (Bandera != null)
                {
                    Competidor.BanderaDesc = Bandera.Descripcion;
                    Productos = await GetProductosPorBandera(Competidor.Bandera);
                    IsBanderaNoExistente = false;
                }
                else
                {
                    Competidor.BanderaDesc = "No Existente";
                    Productos.Clear();
                    IsBanderaNoExistente = true;
                }

                if (Expanded)
                {
                    ProductosListView_SizeChanged(40);
                }
                else
                {
                    ProductosListView_SizeChanged(60);
                }
                DireccionCompetidor = await GetDireccion(Competidor.InterComercial);
                Competidor.Direccion = DireccionCompetidor != null ? DireccionCompetidor.Calle + " " + DireccionCompetidor.Numero : "N/A";
                Provincia = await GetProvincia(DireccionCompetidor.Provincia);
                Competidor.Provincia = (Provincia == null) ? "N/A" : Provincia.Nombre;
                //Competidor.Localidad = (Provincia == null) ? "N/A" : Provincia.Nombre;
                PagingCount = "Registro " + CurrentRegister + " de " + TotalRegisters;
                APIESRazonSocial = _competidor.RazonSocial + " (" + _competidor.APIES + ")";
            }
            await StopSpinner();
        }


        public async void PreviousRecord()
        {
            await StartSpinner();
            var notSavedProducts = await HasNotSavedProducts();
            if (notSavedProducts)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                    ApplicationMessages.Atencion,
                    ApplicationMessages.NotSavedProductsNotification,
                    ApplicationMessages.Continue,
                    ApplicationMessages.Cancel,
                    ActionPreviousOKButton,
                    ActionPreviousCancelButton);
            }
            else
            {
                Previous();
            }
        }
        private async void PreviousCancel()
        {
            if (Navigation.HasPagesInPopupStack())
            {
                await StopSpinner();
            }
        }

        private async void NextCancel()
        {
            if (Navigation.HasPagesInPopupStack())
            {
                await StopSpinner();
            }
        }
        public async void Previous()
        {
            int previousIndex = Items.IndexOf(Competidor) - 1;
            if (previousIndex < 0)
            {
                previousIndex = TotalRegisters - 1;
            }

            if (previousIndex >= 0)
            {
                Competidor = Items[previousIndex];
                CurrentRegister = Items.IndexOf(Competidor) + 1;
                TotalRegisters = Items.Count;
                Bandera = await GetBanderaDesc(Competidor.Bandera);
                if (Bandera != null)
                {
                    Competidor.BanderaDesc = Bandera.Descripcion;
                    Productos = await GetProductosPorBandera(Competidor.Bandera);
                    IsBanderaNoExistente = false;
                }
                else
                {
                    Competidor.BanderaDesc = "No Existente";
                    Productos.Clear();
                    IsBanderaNoExistente = true;
                }

                if (Expanded)
                {
                    ProductosListView_SizeChanged(40);
                }
                else
                {
                    ProductosListView_SizeChanged(60);
                }
                DireccionCompetidor = await GetDireccion(Competidor.InterComercial);
                Competidor.Direccion = DireccionCompetidor != null ? DireccionCompetidor.Calle + " " + DireccionCompetidor.Numero : "N/A";
                Provincia = await GetProvincia(DireccionCompetidor.Provincia);
                Competidor.Provincia = (Provincia == null) ? "N/A" : Provincia.Nombre;
                //Competidor.Localidad = (Provincia == null) ? "N/A" : Provincia.Nombre;
                PagingCount = "Registro " + CurrentRegister + " de " + TotalRegisters;
                APIESRazonSocial = _competidor.RazonSocial + " (" + _competidor.APIES + ")";
            }
            await StopSpinner();
        }

        public void ExpandCollapseCompetitorData()
        {
            tapCount++;
            if (tapCount % 2 == 0)
            {
                Expanded = true;
                BtnExpandCollapse.Scale = 0.6;
                ProductosListView_SizeChanged(40);
            }
            else
            {
                Expanded = false;
                BtnExpandCollapse.Scale = 0.6;
                ProductosListView_SizeChanged(60);
            }
        }

        public async Task<bool> HasNotSavedProducts()
        {
            List<ProductoLocal> ultimosProductos = await GetLastProducts();

            if (ultimosProductos != null && ultimosProductos.Count > 0 && Productos.Count > 0)
            {
                return CheckNotSavedProducts(ultimosProductos);
            }
            else
            {
                //Bug 14469 WIP - Al modificar precios sin guardar y pasar al siguiente competidor no se muestra el carte para guardar los precios.
                List<ProductoLocal> productoLocales = new List<ProductoLocal>();
                var dateNow = DateTime.Now;
                var TimeStamp = dateNow.ToString("yyyyMMddHHmmssfff");
                foreach (var item in Productos)
                {

                    ProductoLocal prodLocal = new ProductoLocal()
                    {
                        IdProductoLocal = null,
                        IdCabecera = TimeStamp,
                        Precio = "000.00",
                        Envase = item.Envase,
                        Producto = item.CodigoSAP,
                        PrecioCompra = "0.0",
                        PrecioDist = "0.0",
                        PrecioCompraSpecified = true,
                        PrecioDistSpecified = true,
                        PrecioSpecified = true,
                        FechaCreacion = dateNow,
                        Volumen = "",
                        Cliente = Competidor.InterComercial

                    };
                    productoLocales.Add(prodLocal);

                }
                await ProductoLocalService.SaveAll(productoLocales);
                ultimosProductos = await GetLastProducts();

                return CheckNotSavedProducts(ultimosProductos);
            }

            //return false;
        }

        public bool CheckNotSavedProducts(List<ProductoLocal> ultimosProductos)
        {
            foreach (var producto in Productos)
            {
                foreach (var prodLocal in ultimosProductos)
                {
                    //if (string.IsNullOrEmpty(producto.PrecioEnteros))
                    //{
                    //    if (producto.CodigoSAP.Equals(prodLocal.Producto))
                    //    {
                    //        var index = prodLocal.Precio.IndexOf(".");
                    //        var enteros = prodLocal.Precio.Substring(0, index);
                    //        producto.PrecioEnteros = enteros;
                    //    }
                    //}
                    //if (string.IsNullOrEmpty(producto.PrecioDecimales))
                    //{
                    //    if (producto.CodigoSAP.Equals(prodLocal.Producto))
                    //    {
                    //        var index = prodLocal.Precio.IndexOf(".");
                    //        var decimales = prodLocal.Precio.Substring(index + 1);
                    //        producto.PrecioDecimales = decimales;

                    //    }
                    //}
                    //var precio = producto.PrecioEnteros + "." + producto.PrecioDecimales;
                    #region ASOSA PRECIO

                    var precio = producto.PrecioEnteros;
                    #endregion


                    if (producto.CodigoSAP.Equals(prodLocal.Producto))
                    {
                        if (!precio.Equals(prodLocal.Precio))
                        {
                            return true;
                        }
                    }
                    //if (producto.CodigoSAP.Equals(prodLocal.Producto) && !precio.Equals(prodLocal.Precio))
                    //{
                    //    return true;
                    //}
                }

            }
            return false;
        }

        public async Task<List<ProductoLocal>> GetPendingProducts(CabeceraInteraccionLocal cabeceraInteraccion = null)
        {
            List<ProductoLocal> productosLocales = null;
            if (cabeceraInteraccion != null)
            {
                productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", cabeceraInteraccion.Cliente);
            }
            else
            {
                productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", Competidor.InterComercial);
            }

            List<ProductoLocal> productosPending = null;
            var productosPendingToSync = productosLocales.Where(x => x.SyncState == SyncState.PendingToSync || x.SyncState == SyncState.Resetted);
            if (productosPendingToSync.Count() > 0)
            {
                var productosOrdenadosAscendente = productosPendingToSync.OrderBy(x => Convert.ToDouble(x.IdCabecera.Substring(0, 17)));
                var productosAgrupadosPorFecha = productosOrdenadosAscendente.GroupBy(x => x.IdCabecera.Substring(0, 17));
                productosPending = productosAgrupadosPorFecha.Last().ToList();
                //ASOSA Begin Manda a borrar todos los pending , No solo el ultimo
                productosPending = productosLocales.Where(x => x.SyncState == SyncState.PendingToSync || x.SyncState == SyncState.Resetted).ToList();
                //ASOSA End Manda a borrar todos los pending , No solo el ultimo
            }

            return productosPending;
        }

        public async Task<List<ProductoLocal>> GetLastProducts(CabeceraInteraccionLocal cabeceraInteraccion = null)
        {
            List<ProductoLocal> productosLocales = null;
            if (cabeceraInteraccion != null)
            {
                productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", cabeceraInteraccion.Cliente);
            }
            else
            {
                productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", Competidor.InterComercial);
            }

            List<ProductoLocal> ultimosProductos = null;
            if (productosLocales.Count > 0)
            {
                //TODO: encontrar una logica bipartita que contemple la de la fecha completa y la de la fechaCreacion si existiese.
                //var productosOrdenadosAscendente = productosLocales.OrderBy(x => x.FechaCreacion);
                //yyyyMMddHHmmssfff
                //var value = string.Format("yyyy-MM-dd HH:mm:ss.fff", productosLocales[0].IdCabecera);



                #region ASOSA CAMBIO 


                var productosOrdenadosAscendente = productosLocales.OrderBy(x => Convert.ToInt64(x.IdCabecera.Substring(0, 17)));
                var productosAgrupadosPorFecha = productosOrdenadosAscendente.GroupBy(x => Convert.ToInt64(x.IdCabecera.Substring(0, 17)));
                ultimosProductos = productosAgrupadosPorFecha.Last().ToList();

                //var productosOrdenadosAscendente = productosLocales.OrderBy(x => FormatToFilter(x.IdCabecera));
                //var productosAgrupadosPorFecha = productosOrdenadosAscendente.GroupBy(x => FormatToFilter(x.IdCabecera));
                //ultimosProductos = productosAgrupadosPorFecha.Last().ToList();
                #endregion




            }

            return ultimosProductos;
        }

        private DateTime FormatToFilter(string idCabecera)
        {
            StringBuilder sb = new StringBuilder(idCabecera);
            sb = sb.Insert(4, "-");
            sb = sb.Insert(7, "-");
            sb = sb.Insert(10, " ");
            sb = sb.Insert(13, ":");
            sb = sb.Insert(16, ":");
            sb = sb.Insert(19, ".");
            var value = sb.ToString();

            var date = Convert.ToDateTime(value);

            return date;
        }

        public async Task<Provincia> GetProvincia(string CodigoSAP)
        {
            Provincias = await ProvinciaService.Query("SELECT * FROM Provincia WHERE Provincia.CodigoSAP = ?", CodigoSAP);

            return (Provincias != null && Provincias.Count > 0) ? Provincias.First() : null;
        }
        public async Task<List<RelevamientoPreciosProducto>> QueryRelevamientosPreciosProductos(string IdBandera)
        {
            try
            {
                return await RelevamientoPreciosProductoService.Query("SELECT RelevamientoPreciosProducto.IdRelevamientoPreciosProducto, RelevamientoPreciosProducto.CodigoSAP, RelevamientoPreciosProducto.Descripcion,RelevamientoPreciosProducto.IdSegmento, RelevamientoPreciosProducto.Envase " +
                            "FROM Bandera " +
                            "INNER JOIN BanderaProducto ON Bandera.IdBandera = BanderaProducto.IdBandera " +
                            "INNER JOIN RelevamientoPreciosProducto ON BanderaProducto.IdRelevamientoPreciosProducto = RelevamientoPreciosProducto.IdRelevamientoPreciosProducto " +
                            "WHERE Bandera.IdBandera = ?", IdBandera);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ObservableCollection<RPProductoGroupModel>> GetProductosPorBandera(string IdBandera)
        {
            RelevamientoPreciosProductos = new List<RelevamientoPreciosProducto>();
            var productosRP = new ObservableCollection<RPProductoGroupModel>();
            RelevamientoPreciosProductos = await QueryRelevamientosPreciosProductos(IdBandera);
            productosRP.Clear();
            foreach (var rpprod in RelevamientoPreciosProductos)
            {
                Producto = new RPProductoGroupModel()
                {
                    CodigoSAP = rpprod.CodigoSAP,
                    Descripcion = rpprod.Descripcion,
                    Envase = rpprod.Envase,
                    IdRelevamientoPreciosProducto = rpprod.IdRelevamientoPreciosProducto,
                    IdSegmento = rpprod.IdSegmento
                };
                productosRP.Add(Producto);
            }
            CabeceraInteraccionLocal cabecera = await GetCabeceraInteraccionLocal();
            IEnumerable<ProductoLocal> productosPorCliente = null;
            if (cabecera != null)
            {
                if (cabecera.Uploaded.Day == 1 && cabecera.Uploaded.Month == 1 && cabecera.Uploaded.Year == 1)
                {
                    var fechaDownload = cabecera.Downloaded.ToString("dd-MM-yyyy HH:mm:ss");
                    Competidor.FechaUltimaActualizacion = string.IsNullOrEmpty(fechaDownload) ? "N/A" : fechaDownload;
                    productosPorCliente = await GetProductosPorCliente(cabecera);
                }
                else
                {
                    var fechaUpload = cabecera.Uploaded.ToString("dd-MM-yyyy HH:mm:ss");
                    Competidor.FechaUltimaActualizacion = string.IsNullOrEmpty(fechaUpload) ? "N/A" : fechaUpload;
                    productosPorCliente = await GetProductosPorCliente(cabecera);
                }

            }
            else
            {
                Competidor.FechaUltimaActualizacion = "N/A";
            }

            if (productosPorCliente != null)
            {
                foreach (var prodLocal in productosPorCliente)
                {
                    foreach (var prod in productosRP)
                    {
                        if (prod != null && prodLocal.Producto.Equals(prod.CodigoSAP))
                        {
                            #region ASOSA PRECIO


                            prod.PrecioEnteros = prodLocal.Precio;
                            prod.Precio = prodLocal.Precio;
                            #endregion
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
                    prodRP.PrecioEnteros = "000.00";
                    //prodRP.PrecioDecimales = "00";
                }
            }

            return productosRP;

        }

        private async Task RestartPrizes()
        {
            await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable, ApplicationMessages.Warning, ApplicationMessages.RestartPrizesConfirmation, ApplicationMessages.Recover, ApplicationMessages.Cancel, CallbackRestartPrizesOKButton);
        }

        private async void CallbackRestartPrizesOKButton()
        {
            await Save(false);// ASOSA FLAG PARA GRABAR CON MENSAJE O NO

            CabeceraInteraccionLocal cabecera = await GetCabeceraInteraccionLocal();
            var pendingProducts = await GetPendingProducts(cabecera);

            if (cabecera != null)
            {
                if (cabecera.SyncState == SyncState.PendingToSync)
                {
                    cabecera.SyncState = SyncState.New;
                    await CabeceraInteraccionLocalService.Save(cabecera);
                }
            }

            if (pendingProducts != null)
            {
                foreach (var product in pendingProducts)
                {
                    await ProductoLocalService.DeleteProduct(product);
                }

                Productos.Clear();
                Productos = await GetProductosPorBandera(Competidor.Bandera);
                ListViewProductos.ItemsSource = Productos;

                EventsManager.TriggerEvent("RestartPrizes", Items.IndexOf(Competidor));
            }
            else
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable, ApplicationMessages.Warning, ApplicationMessages.NoPrizesToRestart, ApplicationMessages.Accept);
            }
        }

        public async Task<CabeceraInteraccionLocal> GetCabeceraInteraccionLocal()
        {
            List<CabeceraInteraccionLocal> cabeceraInteraccionLocal = await CabeceraInteraccionLocalService.GetAllDB();
            CabeceraInteraccionLocal cabecera = null;
            if (cabeceraInteraccionLocal.Count > 0)
            {
                var cabeceraPorCliente = cabeceraInteraccionLocal.Where(x => x.Cliente == Competidor.InterComercial);
                if (cabeceraPorCliente.Count() > 0)
                {
                    cabecera = cabeceraPorCliente.First();
                }

            }
            return cabecera;
        }

        public async Task<IEnumerable<ProductoLocal>> GetProductosPorCliente(CabeceraInteraccionLocal cabecera)
        {
            List<ProductoLocal> productosLocales = null;
            try
            {
                //productosLocales = await ProductoLocalService.Query("SELECT * FROM ProductoLocal WHERE ProductoLocal.Cliente = ?", cabecera.Cliente);
                productosLocales = await GetLastProducts(cabecera);
            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, e.Message, ApplicationMessages.Accept);
                //AlertService.DisplayAlert("ERROR", e.Message);
            }

            return productosLocales;
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
        public async Task<Bandera> GetBanderaDesc(string idBandera)
        {
            //TODO: terminar esta logica ante una eliminacion de Bandera.
            Bandera bandera = null;
            try
            {
                bandera = await BanderaService.GetByIdDB(int.Parse(idBandera));
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
                return bandera;
                //await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, e.Message, ApplicationMessages.Accept);
            }
            //var bandera = await BanderaService.GetByIdDB(int.Parse(idBandera));

            return bandera;
        }

        private async Task Save(bool withMessage)
        {
            InitialSyncDate = await LocalStorageService.Get<DateTime>("InitialSyncDate");
            CabeceraInteraccionLocal cabecera = await GetCabeceraInteraccionLocal();

            //var negocio = await NegocioService.GetByIdDB(int.Parse(rc.IdNegocio));

            CabeceraInteraccionLocal interaccionCabecera = null;
            //ASOSA SACAR RRCC
            //var rc = await RepresentanteComercialService.GetDB();
            //RepresentanteComercialGroupModel rrcc = new RepresentanteComercialGroupModel()
            //{
            //    Nombre = rc.Nombre,
            //    Apellido = rc.Apellido,
            //    Usuario = rc.Usuario,
            //    CodigoInterlocutor = rc.CodigoInterlocutor,
            //    IdNegocio = negocio.CodigoSAP
            //};
            //ASOSA SACAR RRCC

            UserInfo userInfo = await LocalStorageService.GetAccountData();

            try
            {
                var dateNow = DateTime.Now;
                var TimeStamp = dateNow.ToString("yyyyMMddHHmmssfff");

                if (cabecera == null)
                {
                    interaccionCabecera = new CabeceraInteraccionLocal()
                    {
                        IdCabecera = TimeStamp,
                        FechaCreac = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaFinP = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaFinR = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaIniP = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaIniR = DateTime.Now.ToString("yyyy-MM-dd"),
                        HoraFinP = DateTime.Now.ToString("HH:mm:ss"),
                        HoraFinR = DateTime.Now.ToString("HH:mm:ss"),
                        HoraIniP = DateTime.Now.ToString("HH:mm:ss"),
                        HoraIniR = DateTime.Now.ToString("HH:mm:ss"),
                        CodFormulario = ApplicationConstants.RELEVAMIENTO_PRECIOS_COD_FORMULARIO,
                        Categoria = ApplicationConstants.RELEVAMIENTO_PRECIOS_CATEGORIA,
                        CodTranInt = TimeStamp,
                        Descripcion = ApplicationConstants.RELEVAMIENTO_PRECIOS_DESCRIPCION,
                        Estado = ApplicationConstants.RELEVAMIENTO_PRECIOS_ESTADO,
                        Segmento = ApplicationConstants.RELEVAMIENTO_PRECIOS_SEGMENTO,
                        Negocio = ApplicationConstants.RELEVAMIENTO_PRECIOS_NEGOCIO,
                        Motivo = ApplicationConstants.RELEVAMIENTO_PRECIOS_MOTIVO,
                        Operacion = ApplicationConstants.RELEVAMIENTO_PRECIOS_OPERACION,
                        Cliente = Competidor.InterComercial,
                        NombreCliente = Competidor.RazonSocial,
                        //ASOSA SACAR RRCC
                        //NombreResponsable = rrcc.NombreApellido,
                        //NombreRRCC = rrcc.NombreApellido,
                        //Responsable = rrcc.CodigoInterlocutor,
                        //RRCC = rrcc.CodigoInterlocutor,



                        NombreResponsable = userInfo.UserName,
                        NombreRRCC = userInfo.UserName,
                        Responsable = userInfo.Email,
                        RRCC = userInfo.UserLogin,
                        //ASOSA SACAR RRCC

                        Calle = DireccionCompetidor.Calle,
                        Ciudad = null,
                        CodPostal = DireccionCompetidor.CodPostal,
                        Latitud = null,
                        Longitud = null,
                        NroActividad = null,
                        Numero = DireccionCompetidor.Numero,
                        Pais = null,
                        Provincia = DireccionCompetidor.Provincia,
                        Puntaje = "0",
                        PuntajeSpecified = true,
                        Texto0002 = null,
                        TextoZR01 = null,
                        TextoZR02 = null,
                        TextoZR07 = null,
                        TextoZR08 = null,
                        TextoZR09 = null,
                        TextoZR10 = null,
                        TextoZR11 = null,
                        Downloaded = dateNow,
                        SyncState = SyncState.PendingToSync
                    };
                }
                else
                {
                    interaccionCabecera = new CabeceraInteraccionLocal()
                    {
                        IdCabecera = TimeStamp,
                        FechaCreac = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaFinP = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaFinR = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaIniP = DateTime.Now.ToString("yyyy-MM-dd"),
                        FechaIniR = DateTime.Now.ToString("yyyy-MM-dd"),
                        HoraFinP = DateTime.Now.ToString("HH:mm:ss"),
                        HoraFinR = DateTime.Now.ToString("HH:mm:ss"),
                        HoraIniP = DateTime.Now.ToString("HH:mm:ss"),
                        HoraIniR = DateTime.Now.ToString("HH:mm:ss"),
                        CodFormulario = ApplicationConstants.RELEVAMIENTO_PRECIOS_COD_FORMULARIO,
                        Categoria = ApplicationConstants.RELEVAMIENTO_PRECIOS_CATEGORIA,
                        CodTranInt = TimeStamp,
                        Descripcion = ApplicationConstants.RELEVAMIENTO_PRECIOS_DESCRIPCION,
                        Estado = ApplicationConstants.RELEVAMIENTO_PRECIOS_ESTADO,
                        Segmento = ApplicationConstants.RELEVAMIENTO_PRECIOS_SEGMENTO,
                        Negocio = ApplicationConstants.RELEVAMIENTO_PRECIOS_NEGOCIO,
                        Motivo = ApplicationConstants.RELEVAMIENTO_PRECIOS_MOTIVO,
                        Operacion = ApplicationConstants.RELEVAMIENTO_PRECIOS_OPERACION,
                        Cliente = cabecera.Cliente,
                        NombreCliente = cabecera.NombreCliente,
                        NombreResponsable = cabecera.NombreResponsable,
                        NombreRRCC = cabecera.NombreRRCC,
                        Responsable = cabecera.Responsable,
                        RRCC = cabecera.RRCC,
                        Calle = cabecera.Calle,
                        Ciudad = cabecera.Ciudad,
                        CodPostal = cabecera.CodPostal,
                        Latitud = cabecera.Latitud,
                        Longitud = cabecera.Longitud,
                        NroActividad = cabecera.NroActividad,
                        Numero = cabecera.Numero,
                        Pais = cabecera.Pais,
                        Provincia = cabecera.Provincia,
                        Puntaje = cabecera.Puntaje,
                        PuntajeSpecified = cabecera.PuntajeSpecified,
                        Texto0002 = cabecera.Texto0002,
                        TextoZR01 = cabecera.TextoZR01,
                        TextoZR02 = cabecera.TextoZR02,
                        TextoZR07 = cabecera.TextoZR07,
                        TextoZR08 = cabecera.TextoZR08,
                        TextoZR09 = cabecera.TextoZR09,
                        TextoZR10 = cabecera.TextoZR10,
                        TextoZR11 = cabecera.TextoZR11,
                        Downloaded = cabecera.Downloaded,
                        SyncState = SyncState.PendingToSync
                    };
                }

                List<ProductoLocal> productosLocales = new List<ProductoLocal>();
                foreach (var prod in Productos)
                {
                    //var enteros = string.IsNullOrEmpty(prod.PrecioEnteros) ? "0" : prod.PrecioEnteros;
                    //var decimales = string.IsNullOrEmpty(prod.PrecioDecimales) ? "00" : prod.PrecioDecimales;
                    //prod.Precio = enteros + "." + decimales;
                    #region ASOSA PRECIO
                    var enteros = string.IsNullOrEmpty(prod.PrecioEnteros) ? "0" : prod.PrecioEnteros;

                    prod.Precio = enteros;
                    prod.Precio = decimal.Parse(prod.Precio).ToString("0.00"); ;

                    #endregion
                    var producto = new ProductoLocal()
                    {
                        IdProductoLocal = null,
                        IdCabecera = TimeStamp,
                        Cliente = Items[Items.IndexOf(Competidor)].InterComercial,
                        Producto = prod.CodigoSAP,
                        Precio = prod.Precio,
                        PrecioSpecified = true,
                        Volumen = "0",
                        Envase = prod.Envase,
                        PrecioCompra = "",
                        PrecioCompraSpecified = false,
                        PrecioDist = "",
                        PrecioDistSpecified = false,
                        FechaCreacion = dateNow,
                        SyncState = SyncState.PendingToSync

                    };
                    productosLocales.Add(producto);
                }

                Competidor.Estado = CompetitorState.PrizeNoSync;
                await CompetidoresService.Save(ToCompetitorEntity(Competidor));

                // Comentario prueba

                //EventsManager.TriggerEvent("SavePrecioProducto", Items.IndexOf(Competidor));

                //TODO: cambiar esta logica una vez que agregue en database dejando solo la parte que salva tanto en Save como en
                //SaveAll el cambio.
                /*if (IsFirstExecution)
                {*/
                await CabeceraInteraccionLocalService.Save(interaccionCabecera);
                await ProductoLocalService.SaveAll(productosLocales);
                IsFirstExecution = false;
                /*}
                else
                {*/
                /*CabeceraInteraccionLocal cabeceraDB = await GetCabeceraInteraccionLocal();
                IEnumerable<ProductoLocal> productosPorClienteDB = await GetProductosPorCliente(cabeceraDB);

                await CabeceraInteraccionLocalService.Delete(cabeceraDB);

                List<object> listPKProductos = new List<object>();
                foreach (var item in productosPorClienteDB)
                {
                    listPKProductos.Add(item.IdProductoLocal);
                }

                await ProductoLocalService.DeleteAllByIdsAsync(listPKProductos);*/

                /*    await CabeceraInteraccionLocalService.Save(interaccionCabecera);
                    await ProductoLocalService.Save(productosLocales);
                    IsFirstExecution = true;
                }*/






                if (withMessage)
                {
                    await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ConfirmationDrawable, ApplicationMessages.Success, ApplicationMessages.RecordSavedSuccessfully, ApplicationMessages.Accept);
                }
                //AlertService.DisplayAlert(ApplicationMessages.Success, ApplicationMessages.RecordSavedSuccessfully);


                #region ASOSA Vuelvo todos los Entry Black
                CompetidoresGroupModel competidor = ((CompetidoresGroupModel)Competidor);
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                keyValuePairs.Add("Competidor", competidor);
                keyValuePairs.Add("ListaCompetidores", Items);
                keyValuePairs.Add("IsFirstExecution", IsFirstExecution);


                Dictionary<string, object> data = keyValuePairs;
                await InitializeAsync(data);
                #endregion



            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.ErrorDrawable, ApplicationMessages.Error, e.Message, ApplicationMessages.Accept);
                Crashes.TrackError(e);
            }
        }

        public Competidor ToCompetitorEntity(CompetidoresGroupModel competidor)
        {
            return new Competidor()
            {
                Agrupacion = competidor.Agrupacion,
                APIES = competidor.APIES,
                Atributo = competidor.Atributo,
                CantTarjetaYer = competidor.CantTarjetaYer,
                CodDirent = competidor.CodDirent,
                Contacto = competidor.Contacto,
                CuentaLP2 = competidor.CuentaLP2,
                CuentaLPO = competidor.CuentaLPO,
                CuentaQP1 = competidor.CuentaQP1,
                CuentaSGC = competidor.CuentaSGC,
                Cuit = competidor.CUIT,
                Estado = competidor.Estado,
                InterComercial = competidor.InterComercial,
                Latitud = competidor.Latitud,
                Longitud = competidor.Longitud,
                NumeroExpediente = competidor.NumeroExpediente,
                OperaYer = competidor.OperaYer,
                RazonSocial = competidor.RazonSocial,
                UnidadNegocio = competidor.UnidadNegocio
            };
        }

        public void ProductosListView_SizeChanged(int size)
        {
            ListViewProductos.ItemsSource = Productos;
            if (Productos == null || Productos.Count == 0)
            {
                ListViewProductos.HeightRequest = size;
                if (IsBanderaNoExistente)
                {
                    MarginEmptyListViewText = 10;
                    EmptyListViewText = ApplicationMessages.EmptyProductListViewForNonExistentFlag;
                }
                else
                {
                    MarginEmptyListViewText = 0;
                    EmptyListViewText = ApplicationMessages.EmptyProductListView;
                }

                IsEmptyListView = true;
                IsHeaderShowing = false;
                BtnGuardar.IsEnabled = !IsEmptyListView;
                BtnReiniciarPrecios.IsEnabled = !IsEmptyListView;
            }
            else
            {
                EmptyListViewText = "";
                IsEmptyListView = false;
                IsHeaderShowing = true;
                BtnGuardar.IsEnabled = !IsEmptyListView;
                BtnReiniciarPrecios.IsEnabled = !IsEmptyListView;
                int SizeDefault = size;
                SetHeightRows(size * 1.3f);
                ListViewProductos.HeightRequest = SizeDefault * Productos.Count;
            }

        }

        public void SetHeightRows(float size)
        {
            foreach (var item in Productos)
            {
                item.HeightRowsProductos = size;
            }
        }

        public void SetCollectionViewProductos(CollectionView listViewProductos)
        {
            ListViewProductos = listViewProductos;
        }

        public void SetBtnGuardar(Button btnGuardar)
        {
            BtnGuardar = btnGuardar;
        }

        private void CallbackOKEnterosOnlyNumbersTextChanged()
        {
            precioEnteros.Text = precioEnteros.Text.Replace("-", string.Empty);
        }

        private void CallbackOKDecimalesOnlyNumbersTextChanged()
        {
            precioDecimales.Text = precioDecimales.Text.Replace("-", string.Empty);
        }

        private void CallbackOKDecimalesNoDotTextChanged()
        {
            precioDecimales.Text = precioDecimales.Text.Replace(".", string.Empty);
        }

        private void CallbackOKEnterosNoDotTextChanged()
        {
            precioEnteros.Text = precioEnteros.Text.Replace(".", string.Empty);
        }

        public async void PrecioEnterosEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            precioEnteros = entry;

            if (!string.IsNullOrEmpty(entry.Text) && entry.Text.Contains("-"))
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                    ApplicationMessages.Warning,
                    ApplicationMessages.PrizeValuePositive,
                    ApplicationMessages.Accept, null, ActionOKEnterosOnlyNumbersTextChanged);
            }
            //else if (!string.IsNullOrEmpty(entryEnteros.Text) && entryEnteros.Text.Contains("."))
            //{
            //    await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
            //        ApplicationMessages.Warning,
            //        ApplicationMessages.NumbersOnlyPrize,
            //        ApplicationMessages.Accept, null, ActionOKEnterosNoDotTextChanged);
            //}

            #region ASOSA CAMBIO DE FONTCOLOR


          

            #region ASOSA 2DECIMALES

            if (!String.IsNullOrEmpty(e.NewTextValue))
            {
                string newTextValue = e.NewTextValue.Replace(',', '.');


                if (newTextValue.Contains("."))
                {
                    if (newTextValue.Length - 1 - newTextValue.IndexOf(".") > 2)
                    {
                        var s = newTextValue.Substring(0, newTextValue.IndexOf(".") + 2 + 1);
                        entry.Text = s;
                        entry.SelectionLength = s.Length;
                    }
                    else
                    {
                        if (e.OldTextValue != null)
                        {
                            var s3 = newTextValue;
                            entry.Text = s3;
                            //entry.SelectionLength = s3.Length;
                        }
                    }
                }
                else
                {
                    if (newTextValue.Length > 3)
                    {
                        // var s = newTextValue + ".";
                        var s2 = newTextValue.Substring(0, 3);
                        entry.Text = s2;
                        entry.SelectionLength = s2.Length;
                    }

                }
                //if (e.NewTextValue != string.Empty)
                //{
                //    var s = Decimal.Parse(e.NewTextValue).ToString("###.00", CultureInfo.InvariantCulture);
                //    entry.Text = s;
                //    entry.SelectionLength = s.Length;
                //}
            }
            #endregion



            var selectedProductFiltered = Productos.Where(x => x.IdRelevamientoPreciosProducto == entry.TabIndex);

            List<ProductoLocal> ultimosProductos = await GetLastProducts();

            if (ultimosProductos != null && ultimosProductos.Count > 0 && Productos.Count > 0)
            {
                foreach (var producto in Productos)
                {
                    foreach (var prodLocal in ultimosProductos)
                    {
                        //if (string.IsNullOrEmpty(producto.PrecioEnteros))
                        //{
                        //    if (producto.CodigoSAP.Equals(prodLocal.Producto))
                        //    {
                        //        var index = prodLocal.Precio.IndexOf(".");
                        //        var enteros = prodLocal.Precio.Substring(0, index);
                        //        producto.PrecioEnteros = enteros;
                        //    }
                        //}
                        //if (string.IsNullOrEmpty(producto.PrecioDecimales))
                        //{
                        //    if (producto.CodigoSAP.Equals(prodLocal.Producto))
                        //    {
                        //        var index = prodLocal.Precio.IndexOf(".");
                        //        var decimales = prodLocal.Precio.Substring(index + 1);
                        //        producto.PrecioDecimales = decimales;

                        //    }
                        //}
                        //var precio = producto.PrecioEnteros + "." + producto.PrecioDecimales;
                        if (prodLocal.Producto.Equals(entry.ClassId))
                        {
                            //string numEntero = prodLocal.Precio.Split('.')[0];
                            //int numDecimal = int.Parse(prodLocal.Precio.Split('.')[1]);

                            if (prodLocal.Precio.Equals(entry.Text))
                            {
                                entry.TextColor = Color.Black;

                            }
                            else
                            {
                                entry.TextColor = Color.Green;
                            }
                        }

                    }

                }
            }

            #endregion

        }

        public async void PrecioDecimalesEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entryDecimales = (Entry)sender;
            precioDecimales = entryDecimales;

            if (!string.IsNullOrEmpty(entryDecimales.Text) && entryDecimales.Text.Contains("-"))
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                    ApplicationMessages.Warning,
                    ApplicationMessages.PrizeValuePositive,
                    ApplicationMessages.Accept, null, ActionOKDecimalesOnlyNumbersTextChanged);
            }
            else if (!string.IsNullOrEmpty(entryDecimales.Text) && entryDecimales.Text.Contains("."))
            {
                await AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable,
                    ApplicationMessages.Warning,
                    ApplicationMessages.NumbersOnlyPrize,
                    ApplicationMessages.Accept, null, ActionOKDecimalesNoDotTextChanged);
            }
        }


        public void SetBtnReiniciarPrecios(Button btnReiniciarPrecios)
        {
            BtnReiniciarPrecios = btnReiniciarPrecios;
        }



        public void SetBtnExpandCollapse(ImageButton btnExpandCollapse)
        {
            BtnExpandCollapse = btnExpandCollapse;
        }

        public void PrecioEnterosEntry_Focused(object sender, FocusEventArgs e)
        {

            //if (IsFocusedEnteros) {
            var entry = sender as Entry;
            var selectedProductFiltered = Productos.Where(x => x.IdRelevamientoPreciosProducto == entry.TabIndex);
            RPProductoGroupModel selectedProduct = null;
            if (selectedProductFiltered.Count() > 0)
            {
                selectedProduct = selectedProductFiltered.FirstOrDefault();
                selectedProduct.PrecioEnteros = string.Empty;
            }
            //IsFocusedEnteros = false;
            //}
        }
        public void EntryPrecio_Unfocused(object sender, FocusEventArgs e)
        {


            var entry = sender as Entry;
            if (entry.Text == string.Empty)
            {
                var unfocusedProductFiltered = Productos.Where(x => x.IdRelevamientoPreciosProducto == entry.TabIndex);
                entry.Text = unfocusedProductFiltered.FirstOrDefault().Precio;
            }
            else
            {
                decimal tryParseOut = -1;


                decimal.TryParse(entry.Text,
                    NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                    CultureInfo.InvariantCulture,
                    out tryParseOut);



                entry.Text = tryParseOut.ToString("0.00"); ;
            }

        }
        public void DecimalesEntry_Focused(object sender, FocusEventArgs e)
        {
            //if (IsFocusedDecimales) {
            var entry = sender as Entry;
            var selectedProductFiltered = Productos.Where(x => x.IdRelevamientoPreciosProducto == entry.TabIndex);
            RPProductoGroupModel selectedProduct = null;
            if (selectedProductFiltered.Count() > 0)
            {
                selectedProduct = selectedProductFiltered.FirstOrDefault();
                selectedProduct.PrecioDecimales = string.Empty;
            }
            //IsFocusedDecimales = false;
            //}

        }

        #endregion

    }
}
