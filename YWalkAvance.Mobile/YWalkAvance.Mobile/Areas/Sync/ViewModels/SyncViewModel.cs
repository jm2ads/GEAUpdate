using Business.Dominio;
using Business.Services;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Commons.Commons.Entities;
using Commons.Commons.Exceptions;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Areas.Competidores.ViewModels;
using Frontend.Mobile.Areas.Sync.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Exceptions;
using Frontend.Mobile.Commons.Helpers;
using Frontend.Mobile.Services;
using Frontend.Mobile.ViewModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Services.Commons;
using Services.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Sync.ViewModels
{
    public class SyncViewModel : BaseViewModel, ISyncViewModel
    {
        #region Services
        public IRepresentanteComercialService RRCCService { get; set; }
        public IBanderaService BanderaService { get; set; }
        public IBanderaProductoService BanderaProductoService { get; set; }
        public ICompetidoresService CompetidoresService { get; set; }
        public IDireccionesCompetidorService DireccionesCompetidorService { get; set; }
        public IRelevamientoPreciosProductoService RelevamientoPreciosProductoService { get; set; }
        public INegocioService NegocioService { get; set; }
        public ISegmentoService SegmentoService { get; set; }
        public IProvinciaService ProvinciaService { get; set; }
        public IProductoLocalService ProductoLocalService { get; set; }
        public ICabeceraInteraccionLocalService CabeceraInteraccionLocalService { get; set; }
        public ICloseApplication Application { get; set; }
        public ICloseApplicationIOS ApplicationIOS { get; set; }

        #endregion

        #region Properties
        public RepresentanteComercial RepresentanteComercial { get; set; }
        public List<Bandera> Banderas { get; set; }
        public List<BanderaProducto> BanderasProducto { get; set; }
        public List<Competidor> CompetidoresSAP { get; set; }
        public List<RelevamientoPreciosProducto> Productos { get; set; }
        public List<Negocio> Negocios { get; set; }
        public List<Segmento> Segmentos { get; set; }
        public List<Provincia> Provincias { get; set; }
        public List<DireccionCompetidor> DireccionesCompetidor { get; set; }
        public List<ProductoLocal> ProductosLocales { get; set; }
        public List<CabeceraInteraccionLocal> CabecerasInteraccionLocales { get; set; }
        public List<Competidor> CompetidoresDeleted { get; private set; }
        public LlamadaRFC_Competidores llamadaRFC_Competidores { get; set; }
        public LlamadaRFC_PreciosProductos llamadaRFC_PreciosProductos { get; set; }
        public CabeceraInteraccionLocal CabeceraLocal { get; set; }
        public ProductoLocal ProductoLocal { get; set; }

        //private bool IsSAPServerError { get; set; }
        public string IdRed { get; set; }
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
        private bool _isInteractionsSynced { get; set; }
        public bool IsInteractionsSynced
        {
            get
            {
                return _isInteractionsSynced;
            }
            set
            {
                _isInteractionsSynced = value;
                OnPropertyChanged("IsInteractionsSynced");
            }
        }

        public ICommand CommandSyncSuccess { get; set; }
        public ICommand CommandCloseApp { get; set; }
        public Action ActionCloseAppOKButton { get; private set; }
        public Action ActionSyncSuccessOKButton { get; private set; }
        #endregion

        #region Constructors
        public SyncViewModel(IRepresentanteComercialService rrccService, IBanderaService banderaService, ICompetidoresService competidoresService, IRelevamientoPreciosProductoService relevamientoPreciosProductoService,
            INegocioService negocioService, ISegmentoService segmentoService, IDireccionesCompetidorService direccionesCompetidorService, IBanderaProductoService banderaProductoService,
            ICabeceraInteraccionLocalService cabeceraInteraccionLocalService, IProductoLocalService productoLocalService
            , IProvinciaService provinciaService)
        {
            RRCCService = rrccService;
            BanderaService = banderaService;
            CompetidoresService = competidoresService;
            RelevamientoPreciosProductoService = relevamientoPreciosProductoService;
            NegocioService = negocioService;
            SegmentoService = segmentoService;
            DireccionesCompetidorService = direccionesCompetidorService;
            CabeceraInteraccionLocalService = cabeceraInteraccionLocalService;
            ProductoLocalService = productoLocalService;
            BanderaProductoService = banderaProductoService;
            ProvinciaService = provinciaService;
            Banderas = new List<Bandera>();
            CompetidoresSAP = new List<Competidor>();
            Productos = new List<RelevamientoPreciosProducto>();
            Negocios = new List<Negocio>();
            Segmentos = new List<Segmento>();
            DireccionesCompetidor = new List<DireccionCompetidor>();
            BanderasProducto = new List<BanderaProducto>();
            Provincias = new List<Provincia>();
            ProductosLocales = new List<ProductoLocal>();
            CabecerasInteraccionLocales = new List<CabeceraInteraccionLocal>();
            CompetidoresDeleted = new List<Competidor>();
            Application = DependencyService.Get<ICloseApplication>();
            ApplicationIOS = DependencyService.Get<ICloseApplicationIOS>();
            ActionCloseAppOKButton += CallbackCloseAppOKButton;
            ActionSyncSuccessOKButton += CallbackSyncSuccessOKButton;
            CommandSyncSuccess = new Command(async () => await SyncSuccess());
            CommandCloseApp = new Command(async () => await CloseApp());
            EventsManager.SubscribeToEvent("IsInteractionsSynced", OnInteractionsSynced);
        }

        private void OnInteractionsSynced(object[] parameterContainer)
        {
            IsInteractionsSynced = true;
        }
        #endregion

        #region Methods
        public override async Task InitializeAsync(object data)
        {
            IsFirstExecution = (bool)data;
        }

        public async Task<bool> GetData()
        {

            try
            {
                IdRed = await LocalStorageService.GetUserLogin();
                var InitialSyncDate = DateTime.Now;
                await LocalStorageService.Store<DateTime>(InitialSyncDate, "InitialSyncDate");

                if (IsFirstExecution || IsInteractionsSynced)
                {
                    //ASOSA SACAR RRCC
                    //IList<RRCC> repComerciales = null;
                    //RRCC repComercial = null;

                    //try
                    //{
                    //    if (await Util.HasInternetConnectionAsync())
                    //    {
                    //        if (await Util.HasServiceConnectivityAsync())
                    //        {
                    //            repComerciales = await RRCCService.GetRepresentanteComercial(IdRed);
                    //            repComercial = repComerciales.First();
                    //        }
                    //        else
                    //        {
                    //            if (Navigation.HasPagesInPopupStack())
                    //                await Navigation.PopPopUpAsync();
                    //            await AlertService.DisplayCustomAlertConfirmation(
                    //                            ApplicationMessages.ErrorDrawable,
                    //                            ApplicationMessages.Error,
                    //                            ApplicationMessages.SyncGralError,
                    //                            ApplicationMessages.Accept);
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        throw new NoConnectionException();
                    //    }
                    //}
                    //catch (LoginException e)
                    //{
                    //    if (Navigation.HasPagesInPopupStack())
                    //        await Navigation.PopPopUpAsync();
                    //    await AlertService.DisplayCustomAlertConfirmation(
                    //                                ApplicationMessages.ErrorDrawable,
                    //                                ApplicationMessages.Error,
                    //                                ApplicationMessages.ExpiredCredentialsError,
                    //                                ApplicationMessages.Accept);
                    //    Crashes.TrackError(e);
                    //    return false;

                    //}
                    //catch (NoConnectionException ex)
                    //{
                    //    if (Navigation.HasPagesInPopupStack())
                    //        await Navigation.PopPopUpAsync();
                    //    await AlertService.DisplayCustomAlertConfirmation(
                    //                                ApplicationMessages.ErrorDrawable,
                    //                                ApplicationMessages.Error,
                    //                                ApplicationMessages.NoInternetConnection,
                    //                                ApplicationMessages.Accept);
                    //    return false;
                    //}
                    //catch (Exception e)
                    //{
                    //    if (Navigation.HasPagesInPopupStack())
                    //        await Navigation.PopPopUpAsync();
                    //    await AlertService.DisplayCustomAlertConfirmation(
                    //                        ApplicationMessages.ErrorDrawable,
                    //                        ApplicationMessages.Error,
                    //                        ApplicationMessages.SyncGralError,
                    //                        ApplicationMessages.Accept, null,
                    //                        ActionCloseAppOKButton);
                    //    Crashes.TrackError(e);
                    //    await DeleteDB();
                    //    return false;
                    //}



                    //var rc = repComercial;
                    //RepresentanteComercial = new RepresentanteComercial()
                    //{
                    //    Usuario = rc.Usuario,
                    //    CodigoInterlocutor = rc.CodigoInterlocutor,
                    //    Nombre = rc.Nombre,
                    //    Apellido = rc.Apellido,
                    //    IdNegocio = rc.IdNegocio,
                    //    Downloaded = InitialSyncDate
                    //};
                    //ASOSA SACAR RRCC
                    try
                    {
                        if (await Util.HasInternetConnectionAsync())
                        {
                            if (await Util.HasServiceConnectivityAsync())
                            {
                                llamadaRFC_Competidores = await CompetidoresService.GetCompetidores(IdRed);

                                Competidor Competidor = null;
                                DireccionCompetidor DireccionCompetidor = null;
                                foreach (var competidor in llamadaRFC_Competidores.datosGenerales)
                                {
                                    var compDB = await CompetidoresService.GetCompetitorByHeader(competidor.InterComercial);
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
                                        Downloaded = InitialSyncDate,
                                        Estado = (compDB == null) ? CompetitorState.NoPrizeSurvey : compDB.Estado
                                    };

                                    CompetidoresSAP.Add(Competidor);

                                }
                                if (IsInteractionsSynced)
                                {
                                    var competidoresDB = await CompetidoresService.GetAllDB();

                                    foreach (var itemDB in competidoresDB)
                                    {
                                        if (!CompetidoresSAP.Any(x => x.InterComercial == itemDB.InterComercial))
                                        {
                                            Console.WriteLine("--- BORRO COMPETIDOR " + itemDB.APIES + " - " + itemDB.RazonSocial + "---");
                                            CompetidoresDeleted.Add(itemDB);
                                            await CompetidoresService.Delete(itemDB.APIES);
                                        }
                                    }

                                    foreach (var prodSAP in CompetidoresSAP)
                                    {
                                        if (!competidoresDB.Any(x => x.InterComercial == prodSAP.InterComercial))
                                        {
                                            await CompetidoresService.Save(prodSAP);
                                        }
                                    }
                                }



                                foreach (var direccion in llamadaRFC_Competidores.direcciones)
                                {
                                    DireccionCompetidor = new DireccionCompetidor()
                                    {
                                        InterComercial = direccion.InterComercial,
                                        Calle = direccion.Calle,
                                        Numero = direccion.Numero,
                                        CodPostal = direccion.CodPostal,
                                        Provincia = direccion.Provincia,
                                        Downloaded = InitialSyncDate
                                    };
                                    DireccionesCompetidor.Add(DireccionCompetidor);
                                }

                                if (IsInteractionsSynced)
                                {
                                    var direccionesCompDB = await DireccionesCompetidorService.GetAllDB();

                                    foreach (var dirCompDB in direccionesCompDB)
                                    {
                                        if (!DireccionesCompetidor.Any(x => x.InterComercial == dirCompDB.InterComercial))
                                        {
                                            Console.WriteLine("--- BORRO DIRECCION COMPETIDOR " + dirCompDB.InterComercial + "---");
                                            await DireccionesCompetidorService.Delete(dirCompDB.InterComercial);
                                        }
                                    }

                                    foreach (var dirCompSAP in DireccionesCompetidor)
                                    {
                                        if (!direccionesCompDB.Any(x => x.InterComercial == dirCompSAP.InterComercial))
                                        {
                                            Console.WriteLine("--- AGREGO DIRECCION COMPETIDOR " + dirCompSAP.InterComercial + "---");
                                            await DireccionesCompetidorService.Save(dirCompSAP);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Navigation.HasPagesInPopupStack())
                                    await Navigation.PopPopUpAsync();
                                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.SyncGralError,
                                            ApplicationMessages.Accept);
                                return false;
                            }
                        }
                        else
                        {
                            throw new NoConnectionException();
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
                        return false;

                    }
                    catch (NoConnectionException ex)
                    {
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.NoInternetConnection,
                                                    ApplicationMessages.Accept);
                        return false;
                    }
                    catch (Exception e)
                    {
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                        ApplicationMessages.ErrorDrawable,
                                        ApplicationMessages.Error,
                                        ApplicationMessages.SyncGralError,
                                        ApplicationMessages.Accept);
                        Crashes.TrackError(e);
                        await DeleteDB();
                        return false;
                    }


                    try
                    {

                        if (await Util.HasInternetConnectionAsync())
                        {
                            if (await Util.HasServiceConnectivityAsync())
                            {
                                var valores = new List<CompetidorDTO>();


                                foreach (var CompetidorSAP in CompetidoresSAP)
                                {
                                    var comp = new CompetidorDTO();
                                    comp.CodigoCompetidor = CompetidorSAP.InterComercial;
                                    //comp.IdRRCC = CompetidorSAP.c;
                                    valores.Add(comp);
                                }


                                //foreach (var competidor in CompetidoresSAP)
                                //{
                                //    var comp = new CompetidorDTO() { CodigoCompetidor = competidor.InterComercial, IdRRCC = RepresentanteComercial.CodigoInterlocutor };
                                //    valores.Add(comp);
                                //}

                                //var resultados = await RelevamientoPreciosProductoService.GetPreciosProductosSAPAlternativo(valores);
                                var resultados = await GetInteractionsByBatch(valores);

                                //TODO: hacer un foreach de los Competidores para llamar cada vez por Competidor. 
                                //El foreach de cabeceras probablemente no se use más. 
                                foreach (var resultado in resultados)
                                {
                                    var llamadaRFC_PreciosProductos = JsonConvert.DeserializeObject<LlamadaRFC_PreciosProductos>(resultado.LlamadaRfc);

                                    foreach (var cabeceraSAP in llamadaRFC_PreciosProductos.cabecera)
                                    {
                                        var cabecerasLocales = await CabeceraInteraccionLocalService.Query("SELECT * FROM CabeceraInteraccionLocal WHERE Cliente = ?", cabeceraSAP.Cliente);
                                        var cabeceraDB = cabecerasLocales.FirstOrDefault();
                                        CabeceraLocal = new CabeceraInteraccionLocal()
                                        {
                                            Calle = cabeceraSAP.Calle,
                                            Categoria = cabeceraSAP.Categoria,
                                            Ciudad = cabeceraSAP.Ciudad,
                                            Cliente = cabeceraSAP.Cliente,
                                            CodFormulario = cabeceraSAP.CodFormulario,
                                            CodPostal = cabeceraSAP.CodPostal,
                                            CodTranInt = cabeceraSAP.CodTranInt,
                                            Descripcion = cabeceraSAP.Descripcion,
                                            Estado = cabeceraSAP.Estado,
                                            FechaCreac = cabeceraSAP.FechaCreac,
                                            FechaFinP = cabeceraSAP.FechaFinP,
                                            FechaFinR = cabeceraSAP.FechaFinR,
                                            FechaIniP = cabeceraSAP.FechaIniP,
                                            FechaIniR = cabeceraSAP.FechaIniR,
                                            HoraFinP = cabeceraSAP.HoraFinP,
                                            HoraFinR = cabeceraSAP.HoraFinR,
                                            HoraIniP = cabeceraSAP.HoraIniP,
                                            HoraIniR = cabeceraSAP.HoraIniR,
                                            Latitud = cabeceraSAP.Latitud,
                                            Longitud = cabeceraSAP.Longitud,
                                            Motivo = cabeceraSAP.Motivo,
                                            Negocio = cabeceraSAP.Negocio,
                                            IdCabecera = cabeceraSAP.Id,
                                            NombreCliente = cabeceraSAP.NombreCliente,
                                            NombreResponsable = cabeceraSAP.NombreResponsable,
                                            NombreRRCC = cabeceraSAP.NombreRRCC,
                                            NroActividad = cabeceraSAP.NroActividad,
                                            Numero = cabeceraSAP.Numero,
                                            Operacion = cabeceraSAP.Operacion,
                                            Pais = cabeceraSAP.Pais,
                                            Provincia = cabeceraSAP.Provincia,
                                            Puntaje = cabeceraSAP.Puntaje,
                                            PuntajeSpecified = cabeceraSAP.PuntajeSpecified,
                                            Responsable = cabeceraSAP.Responsable,
                                            RRCC = cabeceraSAP.RRCC,
                                            Segmento = cabeceraSAP.Segmento,
                                            Texto0002 = cabeceraSAP.Texto0002,
                                            TextoZR01 = cabeceraSAP.TextoZR01,
                                            TextoZR02 = cabeceraSAP.TextoZR02,
                                            TextoZR07 = cabeceraSAP.TextoZR07,
                                            TextoZR08 = cabeceraSAP.TextoZR08,
                                            TextoZR09 = cabeceraSAP.TextoZR09,
                                            TextoZR10 = cabeceraSAP.TextoZR10,
                                            TextoZR11 = cabeceraSAP.TextoZR11,
                                            SyncState = (cabeceraDB == null) ? SyncState.New : cabeceraDB.SyncState,
                                            Downloaded = InitialSyncDate
                                        };
                                        //if (!IsInteractionsSynced)
                                        //{
                                        //    CabeceraLocal.IdCabecera = cabeceraSAP.Id;
                                        //}

                                        foreach (var productoSAP in llamadaRFC_PreciosProductos.preciosProductos)
                                        {
                                            if (cabeceraSAP.Id.Equals(productoSAP.Id))
                                            {
                                                if (productoSAP.Id.Length >= 17)// ASOSA SACO IdCabecera Corruptas
                                                {
                                                    DateTime parsedDate;
                                                    var valid = DateTime.TryParseExact(productoSAP.Id, "yyyyMMddHHmmssffff", null, DateTimeStyles.None, out parsedDate);
                                                    ProductoLocal = new ProductoLocal()
                                                    {
                                                        IdProductoLocal = null,
                                                        Cliente = cabeceraSAP.Cliente,
                                                        IdCabecera = productoSAP.Id,
                                                        Precio = productoSAP.Precio,
                                                        PrecioSpecified = productoSAP.PrecioSpecified,
                                                        Producto = productoSAP.Producto,
                                                        PrecioCompra = productoSAP.PrecioCompra,
                                                        PrecioCompraSpecified = productoSAP.PrecioCompraSpecified,
                                                        PrecioDist = productoSAP.PrecioDist,
                                                        PrecioDistSpecified = productoSAP.PrecioDistSpecified,
                                                        Envase = productoSAP.Envase,
                                                        Volumen = productoSAP.Volumen,
                                                        SyncState = SyncState.New,
                                                        FechaCreacion = parsedDate,
                                                        Downloaded = InitialSyncDate
                                                    };
                                                    ProductosLocales.Add(ProductoLocal);
                                                }
                                            }
                                        }
                                        CabecerasInteraccionLocales.Add(CabeceraLocal);
                                    }
                                }

                                if (IsInteractionsSynced)
                                {
                                    var productosLocales = await ProductoLocalService.GetAllDB();
                                    var productosLocalesSAP = productosLocales.Where(x => x.SyncState == SyncState.New);

                                    foreach (var prodLocalDB in productosLocalesSAP)
                                    {
                                        if (!ProductosLocales.Any(x => x.IdCabecera.Substring(0, 17) == prodLocalDB.IdCabecera.Substring(0, 17) && x.Precio == prodLocalDB.Precio))
                                        {
                                            Console.WriteLine("--- BORRO PRECIO PRODUCTO " + prodLocalDB.Producto + "---");
                                            await ProductoLocalService.DeleteProduct(prodLocalDB);
                                        }
                                    }

                                    foreach (var prodLocalSAP in ProductosLocales)
                                    {
                                        if (!productosLocalesSAP.Any(x => x.IdCabecera.Substring(0, 17) == prodLocalSAP.IdCabecera.Substring(0, 17) && x.Precio == prodLocalSAP.Precio))
                                        {
                                            Console.WriteLine("--- AGREGO PRECIO PRODUCTO " + prodLocalSAP.Producto + "---");
                                            await ProductoLocalService.Save(prodLocalSAP);
                                        }
                                    }
                                }


                            }
                            else
                            {
                                if (Navigation.HasPagesInPopupStack())
                                    await Navigation.PopPopUpAsync();
                                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.SyncGralError,
                                            ApplicationMessages.Accept);
                                return false;
                            }
                        }
                        else
                        {
                            throw new NoConnectionException();
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
                        return false;

                    }
                    catch (NoConnectionException ex)
                    {
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.NoInternetConnection,
                                                    ApplicationMessages.Accept);
                        return false;
                    }
                    catch (Exception e)
                    {
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                        ApplicationMessages.ErrorDrawable,
                                        ApplicationMessages.Error,
                                        ApplicationMessages.SyncGralError,
                                        ApplicationMessages.Accept);
                        Crashes.TrackError(e);
                        await DeleteDB();
                        return false;
                    }

                    try
                    {
                        if (await Util.HasInternetConnectionAsync())
                        {
                            if (await Util.HasServiceConnectivityAsync())
                            {
                                Provincias = await ProvinciaService.GetProvincias();
                                Negocios = await NegocioService.GetNegocios();
                                Segmentos = await SegmentoService.GetSegmentos();
                                Productos = await RelevamientoPreciosProductoService.GetRelevamientoPreciosProducto();
                                Banderas = await BanderaService.GetBanderas();
                                BanderasProducto = await BanderaProductoService.GetBanderasProducto();

                                if (IsInteractionsSynced)
                                {
                                    var listBanderasDB = await BanderaService.GetAllDB();

                                    foreach (var banderaDB in listBanderasDB)
                                    {
                                        if (!Banderas.Any(x => x.CodigoSAP == banderaDB.CodigoSAP))
                                        {

                                            Console.WriteLine("--- BORRO BANDERA " + banderaDB.CodigoSAP + " - " + banderaDB.Descripcion + "---");
                                            await BanderaService.Delete(banderaDB);
                                        }
                                    }
                                    foreach (var banderaSAP in Banderas)
                                    {
                                        if (!listBanderasDB.Any(x => x.CodigoSAP == banderaSAP.CodigoSAP))
                                        {
                                            Console.WriteLine("--- AGREGO BANDERA " + banderaSAP.CodigoSAP + " - " + banderaSAP.Descripcion + "---");
                                            await BanderaService.Save(banderaSAP);
                                        }
                                    }

                                    var listBanderasProductoDB = await BanderaProductoService.GetAllDB();

                                    foreach (var banderaProductoDB in listBanderasProductoDB)
                                    {
                                        if (!BanderasProducto.Any(x => x.IdBanderaProducto == banderaProductoDB.IdBanderaProducto))
                                        {

                                            Console.WriteLine("--- BORRO BANDERA PRODUCTO " + banderaProductoDB.IdBanderaProducto + " - " + banderaProductoDB.IdBandera + "---");
                                            await BanderaProductoService.Delete(banderaProductoDB);
                                        }
                                    }

                                    foreach (var banderaProductoSAP in BanderasProducto)
                                    {
                                        if (!listBanderasProductoDB.Any(x => x.IdBanderaProducto == banderaProductoSAP.IdBanderaProducto))
                                        {

                                            Console.WriteLine("--- AGREGO BANDERA PRODUCTO " + banderaProductoSAP.IdBanderaProducto + " - " + banderaProductoSAP.IdBandera + " - " + banderaProductoSAP.IdRelevamientoPreciosProducto + "---");
                                            await BanderaProductoService.Save(banderaProductoSAP);
                                        }
                                    }

                                    var listProductosDB = await RelevamientoPreciosProductoService.GetAllDB();

                                    foreach (var prodDB in listProductosDB)
                                    {
                                        if (!Productos.Any(x => x.CodigoSAP == prodDB.CodigoSAP))
                                        {

                                            Console.WriteLine("--- BORRO PRODUCTO " + prodDB.CodigoSAP + " - " + prodDB.Descripcion + "---");
                                            await RelevamientoPreciosProductoService.Delete(prodDB);
                                        }
                                    }

                                    foreach (var prodSAP in Productos)
                                    {
                                        if (!listProductosDB.Any(x => x.CodigoSAP == prodSAP.CodigoSAP))
                                        {

                                            Console.WriteLine("--- AGREGO PRODUCTO " + prodSAP.CodigoSAP + " - " + prodSAP.Descripcion + "---");
                                            await RelevamientoPreciosProductoService.Save(prodSAP);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Navigation.HasPagesInPopupStack())
                                    await Navigation.PopPopUpAsync();
                                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.SyncGralError,
                                            ApplicationMessages.Accept);
                                return false;
                            }

                        }
                        else
                        {
                            throw new NoConnectionException();
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
                        return false;

                    }
                    catch (NoConnectionException ex)
                    {
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.NoInternetConnection,
                                                    ApplicationMessages.Accept);
                        return false;
                    }
                    catch (Exception e)
                    {
                        if (Navigation.HasPagesInPopupStack())
                            await Navigation.PopPopUpAsync();
                        await AlertService.DisplayCustomAlertConfirmation(
                                        ApplicationMessages.ErrorDrawable,
                                        ApplicationMessages.Error,
                                        ApplicationMessages.SyncGralError,
                                        ApplicationMessages.Accept);
                        Crashes.TrackError(e);
                        await DeleteDB();
                        return false;
                    }
                }

                return true;
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
                return false;

            }
            catch (Exception e)
            {
                if (Navigation.HasPagesInPopupStack())
                    await Navigation.PopPopUpAsync();
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.SyncGralError + " " + e.Message,
                                            ApplicationMessages.Accept);
                Crashes.TrackError(e);
                await DeleteDB();
            }
            return false;
        }
        public async Task<List<GenericResultModelResponse>> GetInteractionsByBatch(List<CompetidorDTO> valores)
        {
            int j = 0;
            int chunkSize = 10;
            List<GenericResultModelResponse> resultados = new List<GenericResultModelResponse>();
            while (j < valores.Count)
            {
                var chunkCompetidores = valores.Skip(j).Take(chunkSize).ToList();

                resultados.AddRange(await RelevamientoPreciosProductoService.GetPreciosProductosSAPAlternativo(chunkCompetidores));

                j += chunkSize;
            }

            return resultados;
        }
        private async Task CloseApp()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                await LocalStorageService.ClearUserCache();
                Application.CloseApplication();
            }
            else
            {
                await LocalStorageService.ClearUserCache();
                ApplicationIOS.CloseApplication();
            }
        }

        private async Task SyncSuccess()
        {
            IsFirstExecution = false;
            IsInteractionsSynced = false;
            await LocalStorageService.Store<bool>(true, "SyncFinished");
            //EventsManager.TriggerEvent("OnSyncFinished");
            if (Navigation.HasPagesInPopupStack())
                await Navigation.PopPopUpAsync();
            if (Navigation.HasPagesInPopupStack())
                await Navigation.PopPopUpAsync();
            await Navigation.PushAsync<CompetidoresViewModel>(IsFirstExecution);

        }
        public async Task SyncSuccess2()
        {
            IsFirstExecution = false;
            IsInteractionsSynced = false;
            await LocalStorageService.Store<bool>(true, "SyncFinished");
            //EventsManager.TriggerEvent("OnSyncFinished");
            if (Navigation.HasPagesInPopupStack())
                await Navigation.PopPopUpAsync();
            if (Navigation.HasPagesInPopupStack())
                await Navigation.PopPopUpAsync();
            await Navigation.PushAsync<CompetidoresViewModel>(IsFirstExecution);

        }
        private void CallbackSyncSuccessOKButton()
        {
            CommandSyncSuccess.Execute(null);
        }

        public void CallbackCloseAppOKButton()
        {
            CommandCloseApp.Execute(null);
        }
        private async Task DeleteDB()
        {
            try
            {
                await RRCCService.Format();
                await CompetidoresService.Format();
                await RelevamientoPreciosProductoService.Format();
                await SegmentoService.Format();
                await NegocioService.Format();
                await BanderaProductoService.Format();
                await BanderaService.Format();
                await DireccionesCompetidorService.Format();
                await CabeceraInteraccionLocalService.Format();
                await ProductoLocalService.Format();
                await ProvinciaService.Format();


            }
            catch (Exception e)
            {
                await AlertService.DisplayError(ApplicationMessages.ErrorDeletingDB);
            }

        }

        public async Task<bool> GuardarDB()
        {
            try
            {
                if (IsFirstExecution)
                {
                    await NegocioService.Save(Negocios);
                    await SegmentoService.Save(Segmentos);
                    await RRCCService.Save(RepresentanteComercial);
                    await CompetidoresService.Save(CompetidoresSAP);
                    await RelevamientoPreciosProductoService.Save(Productos);
                    await BanderaService.Save(Banderas);
                    await ProvinciaService.Save(Provincias);
                    await BanderaProductoService.Save(BanderasProducto);
                    await DireccionesCompetidorService.SaveAll(DireccionesCompetidor);
                    await CabeceraInteraccionLocalService.Save(CabecerasInteraccionLocales);
                    await ProductoLocalService.SaveAll(ProductosLocales);
                }

                if (IsInteractionsSynced)
                {
                    await NegocioService.UpdateAll(Negocios);
                    await SegmentoService.UpdateAll(Segmentos);
                    await RRCCService.Update(RepresentanteComercial);
                    await CompetidoresService.UpdateAll(CompetidoresSAP);
                    await RelevamientoPreciosProductoService.UpdateAll(Productos);
                    await BanderaService.UpdateAll(Banderas);
                    await ProvinciaService.UpdateAll(Provincias);
                    await BanderaProductoService.UpdateAll(BanderasProducto);
                    await DireccionesCompetidorService.UpdateAll(DireccionesCompetidor);
                    await CabeceraInteraccionLocalService.UpdateAll(CabecerasInteraccionLocales);
                    await ProductoLocalService.UpdateAll(ProductosLocales);
                }
                EventsManager.TriggerEvent("OnDeletedCompetitors", CompetidoresDeleted);
                return true;
            }
            catch (Exception e)
            {
                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.SyncErrorSaveDB,
                                            ApplicationMessages.Accept);
                Crashes.TrackError(e);
                return false;
            }
            finally
            {
                CompetidoresSAP.Clear();
                Negocios.Clear();
                Segmentos.Clear();
                RepresentanteComercial = null;
                Productos.Clear();
                Banderas.Clear();
                Provincias.Clear();
                BanderasProducto.Clear();
                DireccionesCompetidor.Clear();
                CabecerasInteraccionLocales.Clear();
                ProductosLocales.Clear();
            }
            //return false;
        }
        public async Task CerrarPantalla()
        {
            await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ConfirmationDrawable,
                                            ApplicationMessages.Success,
                                            ApplicationMessages.SyncSuccess,
                                            ApplicationMessages.Accept, null, ActionSyncSuccessOKButton);
        }
        public async Task CerrarPantallasinPopUp()
        {
          await  SyncSuccess();
        }
        #endregion
    }
}
