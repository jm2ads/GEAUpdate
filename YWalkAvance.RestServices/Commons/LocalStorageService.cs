using Akavache;
using Commons.Commons.Constants;
using Commons.Commons.Entities;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Services.Commons
{
    public class LocalStorageService
    {
        private const int EXPIRATION_MARGIN_MINUTES = 2;

        #region Token Storage
        public async static Task StoreRefreshToken(string token)
        {
            if (!String.IsNullOrEmpty(token))
            {
                try
                {
                    //Refresh token expiration data comes in token
                    await BlobCache.Secure.InsertObject<string>(ApplicationConstants.STORAGE_REFRESH_TOKEN, token);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static DeviceInfoModel GetInfoModel()
        {
            try
            {
                DeviceInfoModel deviceInfoModel = new DeviceInfoModel();
                deviceInfoModel.deviceid = new DeviceInfo();
                deviceInfoModel.username = Task.Run(() => GetUserLogin().Result).Result; //await LocalStorageService.GetUserLogin();
                deviceInfoModel.deviceid.Uuid = Task.Run(() => GetUUID().Result).Result; //await LocalStorageService.GetUUID();
                deviceInfoModel.deviceid.Serial = Task.Run(() => GetSerial().Result).Result; //await LocalStorageService.GetSerial();
                deviceInfoModel.token = Task.Run(() => GetRefreshToken().Result).Result;
                // TODO: Descomentar cuando lo implementen en la Web API
                deviceInfoModel.usernameimpersonate = Task.Run(() => GetUserNameImpersonate().Result).Result;
                return deviceInfoModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task<string> GetRefreshToken()
        {
            try
            {
                return await BlobCache.Secure.GetObject<string>(ApplicationConstants.STORAGE_REFRESH_TOKEN);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async static Task RemoveAuthToken()
        {
            try
            {
                await BlobCache.Secure.Invalidate(ApplicationConstants.STORAGE_REFRESH_TOKEN);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region User Information
        public async static Task StoreAccountData(UserInfo userModel)
        {
            try
            {
                //Store username separate for easier access
                await BlobCache.UserAccount.InsertObject<string>(ApplicationConstants.STORAGE_USERNAME, userModel.UserName);
                await BlobCache.UserAccount.InsertObject<string>(ApplicationConstants.STORAGE_USERLOGIN, userModel.UserLogin);
                await BlobCache.UserAccount.InsertObject<UserInfo>(ApplicationConstants.STORAGE_USER_ACCOUNT_DATA, userModel);
                await BlobCache.UserAccount.InsertObject<string>(ApplicationConstants.STORAGE_USERNAME_IMPERSONATE, userModel.UserLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task StoreUsernameImpersonate(string usernameImpersonate)
        {
            try
            {
                await BlobCache.UserAccount.InsertObject<string>(ApplicationConstants.STORAGE_USERNAME_IMPERSONATE, usernameImpersonate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<UserInfo> GetAccountData()
        {

            try
            {

                return await BlobCache.UserAccount.GetObject<UserInfo>(ApplicationConstants.STORAGE_USER_ACCOUNT_DATA);


            }
            catch (Exception)
            {

                return null;
                throw;
            }
        }

        public async static Task StoreValidateData(string username, string uuid, string serial)
        {
            try
            {
                await BlobCache.Secure.InsertObject<string>(ApplicationConstants.STORAGE_USERLOGIN, username);
                await BlobCache.Secure.InsertObject<string>(ApplicationConstants.STORAGE_UUID, uuid);
                await BlobCache.Secure.InsertObject<string>(ApplicationConstants.STORAGE_SERIAL, serial);
                await BlobCache.UserAccount.InsertObject<string>(ApplicationConstants.STORAGE_USERNAME_IMPERSONATE, username);

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async static Task ClearUserCache()
        {
            try
            {
                await BlobCache.UserAccount.InvalidateObject<UserInfo>(ApplicationConstants.STORAGE_USER_ACCOUNT_DATA);
                await BlobCache.UserAccount.InvalidateObject<string>(ApplicationConstants.STORAGE_USERNAME);
                await BlobCache.Secure.InvalidateObject<string>(ApplicationConstants.STORAGE_USERLOGIN);
                await BlobCache.Secure.InvalidateObject<string>(ApplicationConstants.STORAGE_UUID);
                await BlobCache.Secure.InvalidateObject<string>(ApplicationConstants.STORAGE_SERIAL);
                await BlobCache.UserAccount.InvalidateObject<UserInfo>(ApplicationConstants.STORAGE_USERNAME_IMPERSONATE);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async static Task<string> GetUserLogin()
        {
            try
            {
                return await BlobCache.Secure.GetObject<string>(ApplicationConstants.STORAGE_USERLOGIN);
            }
            catch (Exception)
            {
               
                throw;
            }

        }
        public async static Task<string> GetUserNameImpersonate()
        {
            try
            {
                return await BlobCache.UserAccount.GetObject<string>(ApplicationConstants.STORAGE_USERNAME_IMPERSONATE);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async static Task<string> GetUUID()
        {
            try
            {
                return await BlobCache.Secure.GetObject<string>(ApplicationConstants.STORAGE_UUID);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async static Task<string> GetSerial()
        {
            try
            {
                return await BlobCache.Secure.GetObject<string>(ApplicationConstants.STORAGE_SERIAL);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async static Task<string> GetUserType()
        {
            try
            {
                return await BlobCache.UserAccount.GetObject<string>(ApplicationConstants.STORAGE_USERTYPE);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region Generic
        public async static Task StoreUserData<T>(object o, string key, int? expirationTimespanMinutes = null)
        {
            try
            {
                Type objectType = o.GetType();

                DateTimeOffset? expiration = null;
                if (expirationTimespanMinutes != null)
                    expiration = DateTimeOffset.Now.AddMinutes((int)expirationTimespanMinutes);

                await BlobCache.UserAccount.InsertObject<T>(key, (T)o, absoluteExpiration: expiration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<T> GetUserData<T>(string key)
        {
            try
            {
                return await BlobCache.UserAccount.GetObject<T>(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task Store<T>(object o, string key)
        {
            try
            {
                Type objectType = o.GetType();
                await BlobCache.UserAccount.InsertObject<T>(key, (T)o);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<T> Get<T>(string key)
        {
            try
            {
                return await BlobCache.UserAccount.GetObject<T>(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task Remove<T>(string key)
        {
            try
            {
                await BlobCache.UserAccount.InvalidateObject<T>(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
