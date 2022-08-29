using Business.Commons;
using Business.Observers.Interfaces;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Commons.Commons.Entities;
using Commons.Commons.Enumerators;
using Services.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class LoginService : ILoginService
    {
        List<IObserver> _myObservers;
        public LoginService()
        {
            _myObservers = new List<IObserver>();
        }

        public async Task<bool> DoLogin(DeviceInfoModel deviceInfo)
        {

            UserRegistrationInfo userRegistrationInfo = new UserRegistrationInfo();
            //TODO: revisar para ver si se debe cambiar o eliminar. 
            deviceInfo.deviceid.Manufacturer = "Marce";
            deviceInfo.deviceid.Model = "Sarasa";
            deviceInfo.deviceid.Platform = "Android";
            deviceInfo.deviceid.Version = "7.0";

            userRegistrationInfo.Application = deviceInfo.application;
            userRegistrationInfo.Userlogin = deviceInfo.username;
            userRegistrationInfo.Userpass = deviceInfo.password;

            DeviceInfo deviceId = new DeviceInfo();
            deviceId.Manufacturer = deviceInfo.deviceid.Manufacturer;
            deviceId.Model = deviceInfo.deviceid.Model;
            deviceId.Platform = deviceInfo.deviceid.Platform;
            deviceId.Version = deviceInfo.deviceid.Version;
            deviceId.Serial = deviceInfo.deviceid.Serial;
            deviceId.Uuid = deviceInfo.deviceid.Uuid;

            userRegistrationInfo.Deviceid = deviceId;
            //TODO: aca tengo que comentar para hacer las pruebas de Prod y poner true junto al registro de datos de Usuario.
            LoginModel loginModel = HttpClientService.PostRegister<LoginModel>(ApiConstants.RegisterUser, userRegistrationInfo);

            if (ErrorLogin.GetEnum(loginModel.Error.ToUpper()) == ErrorTypeEnum.NO_ERR)
            {
                loginModel.AuthToken.UserInfo.UserLogin.ToUpper();
                await LocalStorageService.StoreRefreshToken(loginModel.AuthToken.Token);

                await LocalStorageService.StoreValidateData(loginModel.AuthToken.UserInfo.UserLogin, deviceInfo.deviceid.Uuid, deviceInfo.deviceid.Serial);

                await LocalStorageService.StoreAccountData(loginModel.AuthToken.UserInfo);

                #region ASOSA  AGREGADO PARA MARCAR SESION Ok 
                await LocalStorageService.Store<bool>(false, "IsSessionClosed");
                #endregion

                return true;
            }

            if (ErrorLogin.GetEnum(loginModel.Error.ToUpper()) == ErrorTypeEnum.WRONG_USERPASS || ErrorLogin.GetEnum(loginModel.Error.ToUpper()) == ErrorTypeEnum.ACCESS_DENIED || ErrorLogin.GetEnum(loginModel.Error.ToUpper()) == ErrorTypeEnum.DEFAULT) {
                Trigger(loginModel.Error + " : " + loginModel.Message);
            }
                        
            return false;
        }
        public void AddObserver(IObserver obs)
        {
            _myObservers.Add(obs);
        }

        public void RemoveObserver(IObserver obs)
        {

            if (_myObservers.Contains(obs))
            {
                _myObservers.Remove(obs);
            }
        }

        public void Trigger(string triggerMessage)
        {
            foreach (var observer in _myObservers)
            {
                observer.OnNotify(triggerMessage);
            }
        }
    }
}
