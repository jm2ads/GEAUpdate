using Business.Commons;
using Business.Dominio;
using Business.Observers.Interfaces;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Commons.Commons.Entities;
using Commons.Commons.Exceptions;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Areas.Competidores.ViewModels;
using Frontend.Mobile.Areas.Login.ViewModels.Interfaces;
using Frontend.Mobile.Areas.Sync.ViewModels;
using Frontend.Mobile.Commons.Exceptions;
using Frontend.Mobile.Commons.Helpers;
using Frontend.Mobile.Models;
using Frontend.Mobile.Services;
using Frontend.Mobile.ViewModels;
using Microsoft.AppCenter.Crashes;
using Services.Commons;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Login.ViewModels
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel, IObserver
    {

        #region Commands

        public ICommand DoLoginCommand { get; set; }
        public ICommand CheckLoginCommand { get; set; }
        public ICommand DoShowPassword { get; set; }

        #endregion

        #region Services
        private IAppVersion Version { get; set; }

        private ILoginService LoginService { get; set; }
        private ICompetidoresService CompetidoresService { get; set; }
        private INegocioService NegocioService { get; set; }
        private ISegmentoService SegmentoService { get; set; }
        private IRepresentanteComercialService RepresentanteComercialService { get; set; }

        #endregion

        public Action CallbackOKDifferentUserLogged;

        #region Login Properties

        private DeviceInfoModel InfoModel { get; set; }

        public Entry UserInput { get; set; }
        public Entry PasswordInput { get; set; }

        public Button BtnDeleteUser { get; set; }

        public LoginCredentials Login { get; set; }

        public bool PasswordHidden { get; set; }

        public string IconShowHide { get; set; }

        private string LoginMessage { get; set; }
        private bool _isSessionClosed { get; set; }
        private bool IsRetail { get; set; }
        private bool RRCCHasErrors { get; set; }
        private bool IsSAPServerError { get; set; }

        private bool HasPermissions { get; set; }
        private bool _isUserInputEnabled { get; set; }
        public bool IsFirstExecution { get; set; }

        private bool _loggedIn = false;
        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                _loggedIn = value;
            }
        }

        private bool _syncFinished = false;
        public bool SyncFinished
        {
            get { return _syncFinished; }
            set
            {
                _syncFinished = value;
            }
        }
        #endregion

        #region Constructors

        public LoginViewModel(ILoginService loginService, ICompetidoresService competidoresService, INegocioService negocioService, ISegmentoService segmentoService, IRepresentanteComercialService rrccService)
        {
            Login = new LoginCredentials();
            DoLoginCommand = new Command(async () => await DoLogin());
            CheckLoginCommand = new Command(async () => await CheckLogin());
            DoShowPassword = new Command(() => ShowHidePassword());
            LoginService = loginService;
            NegocioService = negocioService;
            SegmentoService = segmentoService;
            CompetidoresService = competidoresService;
            RepresentanteComercialService = rrccService;
            LoginService.AddObserver(this);
            _isUserInputEnabled = true;
            PasswordHidden = true;
            IconShowHide = "show.png";
            Version = DependencyService.Get<IAppVersion>();
            CallbackOKDifferentUserLogged += OKDifferentUserLogged;
            EventsManager.SubscribeToEvent("OnCloseSession", OnCloseSession);
            //EventsManager.SubscribeToEvent("OnSyncFinished", OnSyncFinished);
        }

        private void OnSyncFinished(object[] parameterContainer)
        {
            SyncFinished = true;
        }

        #endregion

        #region Methods

        private void ShowHidePassword()
        {
            if (PasswordHidden)
            {
                PasswordHidden = false;
                IconShowHide = "hide.png";
            }
            else
            {
                PasswordHidden = true;
                IconShowHide = "show.png";
            }
        }

        private void OKDifferentUserLogged()
        {
            UserInput.Text = null;
            PasswordInput.Text = null;
        }

        private async Task CheckLogin()
        {
            await StartSpinner();
            try
            {
                IsFirstExecution = false;

                if (Navigation.HasPagesInPopupStack())
                    await StopSpinner();

                await Navigation.PushAsync<CompetidoresViewModel>(IsFirstExecution);
                
            }
            catch (Exception ex)
            {
                if (Navigation.HasPagesInPopupStack())
                    await StopSpinner();
                
                Crashes.TrackError(ex);
                /*if (ex.InnerException.InnerException.Message.Contains("The given key"))
                {
                    Console.WriteLine("LOGIN >> SE CERRO LA SESION >>");
                }
                else
                {*/
                    await AlertService.DisplayCustomAlertConfirmation(
                                      ApplicationMessages.ErrorDrawable,
                                      ApplicationMessages.Error,
                                      ApplicationMessages.ErrorGeneric,
                                      ApplicationMessages.Accept);
                //}
            }
        }

        private async Task CheckPermissions(string idRed)
        {
            try
            {
                var representantesComerciales = await RepresentanteComercialService.GetRepresentanteComercial(idRed);
                HasPermissions = representantesComerciales != null && representantesComerciales.Any() && representantesComerciales.FirstOrDefault() != null;
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
                return;

            }
            catch (Exception e)
            {
                RRCCHasErrors = true;
                if (Navigation.HasPagesInPopupStack())
                    await StopSpinner();

                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.LoadRRCCError,
                                            ApplicationMessages.Accept);

                Crashes.TrackError(e);
                return;
            }
        }

        private void OnCloseSession(object[] parameterContainer)
        {
            if (parameterContainer[0] != null)
            {
                _isSessionClosed = (bool)parameterContainer[0];
                UserInput.Text = null;
                PasswordInput.Text = null;
            }

        }

        private async Task DoLogin()
        {
            try
            {
                await StartSpinner();
                RRCCHasErrors = false;
                if (await Util.HasInternetConnectionAsync())
                {
                    if (await Util.HasServiceConnectivityAsync())
                    {
                        if (!string.IsNullOrEmpty(UserInput.Text) && !string.IsNullOrEmpty(PasswordInput.Text))
                        {

                            GetModelInfo();
                            IsFirstExecution = false;
                           
                            RepresentanteComercial RRCC = await RepresentanteComercialService.GetDB();
                            IsFirstExecution = RRCC == null;

                            if (await LoginService.DoLogin(InfoModel))
                            {
                                if (RRCC == null || InfoModel.username.Contains(RRCC.Usuario))
                                {
                                    /*try
                                    {*/
                                    //ASOSA SACAR RRCC
                                        //await CheckPermissions(ProcessIdRed(InfoModel.username));
                                    //ASOSA SACAR RRCC

                                    /*}
                                    catch (Exception)
                                    {
                                        if (Navigation.HasPagesInPopupStack())
                                            await StopSpinner();

                                        await AlertService.DisplayCustomAlertConfirmation(
                                                                                    ApplicationMessages.WarningDrawable,
                                                                                    ApplicationMessages.Warning,
                                                                                    ApplicationMessages.No_RRCC_SAP_Registered,
                                                                                    ApplicationMessages.Accept);
                                        return;
                                    }*/
                                    if (!IsSAPServerError)
                                    {
                                        if (!RRCCHasErrors)
                                        {
                                            //ASOSA SACAR RRCC
                                            //if (HasPermissions)
                                            //{
                                                //ASOSA SACAR RRCC
                                                //ASOSA SACAR RRCC
                                                // await CheckRRCCIfRetail();
                                                //ASOSA SACAR RRCC
                                                if (IsFirstExecution)
                                                {
                                                    if (Navigation.HasPagesInPopupStack())
                                                        await StopSpinner();

                                                    await Navigation.PushPopUpAsync<SyncViewModel>(IsFirstExecution);
                                                }
                                                else
                                                {
                                                    if (!RRCCHasErrors)
                                                    {
                                                        //ASOSA SACAR RRCC
                                                        //if (IsRetail)
                                                        //{
                                                            //ASOSA SACAR RRCC
                                                            if (Navigation.HasPagesInPopupStack())
                                                                await StopSpinner();

                                                            _isSessionClosed = false;

                                                            await Navigation.PushAsync<CompetidoresViewModel>(IsFirstExecution);
                                                            //ASOSA SACAR RRCC
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (Navigation.HasPagesInPopupStack())
                                                        //        await StopSpinner();

                                                        //    await AlertService.DisplayCustomAlertConfirmation(
                                                        //        ApplicationMessages.WarningDrawable,
                                                        //        ApplicationMessages.Warning,
                                                        //        ApplicationMessages.RRCCNotRetail,
                                                        //        ApplicationMessages.Accept);
                                                        //}
                                                        //ASOSA SACAR RRCC
                                                    }
                                                }


                                                //ASOSA SACAR RRCC
                                            //}
                                            //else
                                            //{
                                            //    if (Navigation.HasPagesInPopupStack())
                                            //        await StopSpinner();

                                            //    await AlertService.DisplayCustomAlertConfirmation(
                                            //                                                ApplicationMessages.WarningDrawable,
                                            //                                                ApplicationMessages.Warning,
                                            //                                                ApplicationMessages.No_RRCC_SAP_Registered,
                                            //                                                ApplicationMessages.Accept);
                                            //}
                                            //ASOSA SACAR RRCC
                                        }
                                    }
                                }
                                else
                                {
                                    if (Navigation.HasPagesInPopupStack())
                                        await StopSpinner();

                                    await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.DifferentUserLoggedError,
                                                    ApplicationMessages.Accept, null, CallbackOKDifferentUserLogged);
                                }
                            }
                            else
                            {
                                if (Navigation.HasPagesInPopupStack())
                                    await StopSpinner();

                                StringBuilder sb = new StringBuilder();
                                if (!string.IsNullOrEmpty(LoginMessage))
                                {
                                    if (LoginMessage.Contains("2")) 
                                    {
                                       // sb.Append(ApplicationMessages.AccessDeniedLogin); ASOSA ORIGINAL COMENTADO DEJO EL IF POR FUTUROS CAMBIOS
                                        sb.Append(ApplicationMessages.LoginCredentialsError);
                                    }
                                    else if (LoginMessage.Contains("3"))
                                    {
                                        sb.Append(ApplicationMessages.LoginCredentialsError);
                                    }
                                    else
                                    {
                                        sb.Append(ApplicationMessages.ErrorGeneric);
                                    }
                                }
                                await AlertService.DisplayCustomAlertConfirmation(
                                        ApplicationMessages.ErrorDrawable,
                                        ApplicationMessages.Error,
                                        sb.ToString(),
                                        ApplicationMessages.Accept);
                            }

                        }
                        else
                        {
                            await StopSpinner();
                            if (string.IsNullOrEmpty(UserInput.Text) && string.IsNullOrEmpty(PasswordInput.Text))
                            {
                                await AlertService.DisplayCustomAlertConfirmation(
                                                ApplicationMessages.ErrorDrawable,
                                                ApplicationMessages.Error,
                                                ApplicationMessages.NoEmptyCredentials,
                                                ApplicationMessages.Accept);
                            }
                            else if (string.IsNullOrEmpty(PasswordInput.Text))
                            {
                                await AlertService.DisplayCustomAlertConfirmation(
                                                ApplicationMessages.ErrorDrawable,
                                                ApplicationMessages.Error,
                                                ApplicationMessages.NoEmptyPassword,
                                                ApplicationMessages.Accept);
                            }
                            else
                            {
                                await AlertService.DisplayCustomAlertConfirmation(
                                                ApplicationMessages.ErrorDrawable,
                                                ApplicationMessages.Error,
                                                ApplicationMessages.NoEmptyUser,
                                                ApplicationMessages.Accept);
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
                }
                else
                {
                    throw new NoConnectionException();
                }
            }
            catch (NoConnectionException ex)
            {
                if (Navigation.HasPagesInPopupStack())
                    await StopSpinner();

                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.NoInternetConnection,
                                            ApplicationMessages.Accept);
            }
            catch (Exception ex)
            {
                if (Navigation.HasPagesInPopupStack())
                    await StopSpinner();

                await AlertService.DisplayCustomAlertConfirmation(
                                            ApplicationMessages.ErrorDrawable,
                                            ApplicationMessages.Error,
                                            ApplicationMessages.ErrorGeneric,
                                            ApplicationMessages.Accept);
                Crashes.TrackError(ex);
            }
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
                        string IdRed = "";
                        if (InfoModel != null)
                        {
                            IdRed = InfoModel.username;
                        }
                        else
                        {
                            InfoModel = LocalStorageService.GetInfoModel();
                            IdRed = InfoModel.username;
                        }

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
                    }
                    catch (Exception e)
                    {
                        RRCCHasErrors = true;
                        await AlertService.DisplayCustomAlertConfirmation(
                                                    ApplicationMessages.ErrorDrawable,
                                                    ApplicationMessages.Error,
                                                    ApplicationMessages.LoadRRCCError,
                                                    ApplicationMessages.Accept);

                        Crashes.TrackError(e);
                    }
                }
                else
                {
                    RRCCHasErrors = true;
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

        private void GetModelInfo()
        {
            InfoModel = new DeviceInfoModel();
            var username = Login.Username.Trim();
            var password = Login.Password.Trim();
            InfoModel.username = username.ToUpper();
            InfoModel.password = password;
            InfoModel.deviceid = new DeviceInfo();
            InfoModel.deviceid.Uuid = DependencyService.Get<IDeviceInformation>().GetUuid();
            InfoModel.deviceid.Serial = DependencyService.Get<IDeviceInformation>().GetSerial();
        }

        public void OnNotify(string message)
        {
            LoginMessage = message;
        }

        public async Task<string> GetUser()
        {
            return await LocalStorageService.GetUserLogin();
        }

        public string GetVersionCode()
        {
            return Version.GetBuildVersion();
        }

        public async Task DeleteUserData()
        {
            await LocalStorageService.ClearUserCache();
        }

        public void SetInput(Entry input)
        {
            UserInput = input;
        }

        public void SetBtnDeleteUser(Button btnDeleteUser)
        {
            BtnDeleteUser = btnDeleteUser;
        }

        public void SetPassInput(Entry passEntry)
        {
            PasswordInput = passEntry;
        }

        public bool IsLoggedIn()
        {
            return LoggedIn;
        }

        public async Task<bool> IsSyncFinished()
        {
            bool syncFinished = false;
            try
            {
               syncFinished = await LocalStorageService.Get<bool>("SyncFinished");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("The given key"))
                {
                    syncFinished = false;
                    return syncFinished;
                }
                else 
                {
                    syncFinished = false;
                    return syncFinished;
                }
            }

            return syncFinished;
        }

        // ASOSA IsSessionClosed ORIGINAL
        //public bool IsSessionClosed()
        //{
        //    return _isSessionClosed;
        //}
        public async Task<bool> IsSessionClosed()
        {
            bool IsSessionClosedReturn = true;
            try
            {
                IsSessionClosedReturn = await LocalStorageService.Get<bool>("IsSessionClosed");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("The given key"))
                {
                    IsSessionClosedReturn = true;
                    return IsSessionClosedReturn;
                }
                else
                {
                    IsSessionClosedReturn = true;
                    return IsSessionClosedReturn;
                }
            }

            return IsSessionClosedReturn;
        }
        #endregion

    }
}
