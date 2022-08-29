using Commons.Commons.Constants;
using Commons.Commons.Enumerators;
using Commons.Commons.Exceptions;
using Frontend.Mobile.Commons.Exceptions;
using Newtonsoft.Json;
using Services.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commons
{
    public static class HttpClientService
    {

        private static readonly HttpClient client;
        public static bool? tengoConectividad = null;
        static HttpClientService()
        {
            //https://yboard.ypf.com.ar/RelevamientoPreciosBackendServiceSecurity_Test/ url onPremise
            //https://magui-test.ypf.com/apip/rrpp/ url de la nube
            client = new HttpClient();
            //client.BaseAddress = new Uri("https://yboard.ypf.com.ar/RelevamientoPreciosBackendService_Test/");
            client.BaseAddress = new Uri(ApiConstants.BaseApiRest);
        }

        public async static Task<T> Get<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                //Send request
                HttpResponseMessage response = client.GetAsync(endpoint + queryString).Result;

                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                //Deserialize into desired type

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }

            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool IsAlive()
        {
            try
            {

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "isalive.html";

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = response.Content.ReadAsStringAsync().Result;

                if (result != null)
                {
                    return result.Contains("OK");
                }
                else {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }    
                
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static Task<bool> IsAliveAsync()
        {
            try
            {

                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(300);

                //Send request
                var uri = ApiConstants.BaseApiRest + "isalive.html";

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = response.Content.ReadAsStringAsync().Result;

                if (result != null)
                {
                    var isOK = result.Contains("OK");
                    return Task.FromResult(isOK);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }

            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async static Task<T> GetPreciosProductos<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                //Send request
                var uri = ApiConstants.BaseApiRest + "rp/preciosProductos" + queryString;

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async static Task<T> GetCompetidores<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                //Send request
                var uri = ApiConstants.BaseApiRest + "competidor/competidores" + queryString;

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async static Task<T> GetRRCC<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(90);
                //Send request
                var uri = ApiConstants.BaseApiRest + "rrcc/representanteComercial" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                //var rrcc = JsonConvert.DeserializeObject<LlamadaRFC>();

                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }

                // Implementar Enum

            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                //throw;
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else 
                {
                    throw;               
                }
            }
        }

        public async static Task<T> GetProductos<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "producto/productos" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public async static Task<T> GetProductosPorBandera<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "producto/productos​" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async static Task<T> GetBanderasProducto<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "bandera/banderasProducto" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public async static Task<T> GetBanderas<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "bandera/banderas" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public async static Task<T> GetSegmentos<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "segmento/segmentos" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public async static Task<T> GetNegocios<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "negocio/negocios" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                // Implementar Enum
                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public async static Task<T> GetProvincias<T>(string endpoint, List<KeyValuePair<string, string>> values = null)
        {
            try
            {
                //Format values into Query String
                string queryString = GenerateQueryString(values);

                var client = new HttpClient();

                //Send request
                var uri = ApiConstants.BaseApiRest + "provincia/provincias" + queryString;

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                HttpResponseMessage response = client.GetAsync(uri).Result;

                //Deserialize into desired type
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);

                //var rrcc = JsonConvert.DeserializeObject<LlamadaRFC>();

                if (result.Data.ConsultaOk)
                {
                    await ValidateToken(result);
                    return JsonConvert.DeserializeObject<T>(result.Data.LlamadaRfc);
                }
                else
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }

                // Implementar Enum

            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public static T PostRegister<T>(string endpoint, object userInfo = null)
        {
            try
            {
                StringContent content;
                //Encode values

                if (userInfo != null)
                {
                    var stringifiedContent = JsonConvert.SerializeObject(userInfo);

                    content = new StringContent(stringifiedContent, Encoding.UTF8, "application/json");
                }
                else
                {
                    content = null;
                }

                //Send request

                HttpResponseMessage response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();
                //Deserialize response into "ApiResponse"

                var result = JsonConvert.DeserializeObject<AuthTokenResponse>(response.Content.ReadAsStringAsync().Result);

                var stringifiedResponse = JsonConvert.SerializeObject(result);

                return JsonConvert.DeserializeObject<T>(stringifiedResponse);
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            catch (TaskCanceledException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public async static Task<ResultArray> PostContentReturn(string endpoint, object values = null)
        {
            try
            {
                StringContent content;

                //Encode values
                if (values != null)
                {
                    var stringifiedContent = JsonConvert.SerializeObject(values);
                    content = new StringContent(stringifiedContent, Encoding.UTF8, "application/json");
                }
                else
                {
                    content = null;
                }

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                //Send request
                HttpResponseMessage response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ResultArray>(response.Content.ReadAsStringAsync().Result);
                    await ValidateToken(result);
                    return result;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("404");
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("500");
                        }
                        else
                        {
                            throw new Exception("No contemplada");
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else if (e.Message.Contains("Read error")) {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public async static Task PostNoContent(string endpoint, object values = null)
        {
            try
            {
                StringContent content;

                //Encode values
                if (values != null)
                {
                    var stringifiedContent = JsonConvert.SerializeObject(values);
                    content = new StringContent(stringifiedContent, Encoding.UTF8, "application/json");
                }
                else
                {
                    content = null;
                }

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                //Send request
                HttpResponseMessage response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ResultArray>(response.Content.ReadAsStringAsync().Result);
                    await ValidateToken(result);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("404");
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("500");
                        }
                        else
                        {
                            throw new Exception("No contemplada");
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async static Task Post(string endpoint, object values = null)
        {
            try
            {
                StringContent content;
                
                //Encode values
                if (values != null)
                {
                    var stringifiedContent = JsonConvert.SerializeObject(values);
                    content = new StringContent(stringifiedContent, Encoding.UTF8, "application/json");
                }
                else
                {
                    content = null;
                }

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                //Send request
                HttpResponseMessage response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();

                //Deserialize response into "ApiResponse"
                var result = JsonConvert.DeserializeObject<ResultArray>(response.Content.ReadAsStringAsync().Result);

                var resultsOk = result.Data.Where(x => x.ConsultaOk);

                if (resultsOk.Count() > 0)
                {
                    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                }
                else 
                {
                    await ValidateToken(result);
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task<T> PostToRFC<T>(string endpoint, object values = null)
        {
            try
            {
                StringContent content;
                
                //Encode values
                if (values != null)
                {
                    var stringifiedContent = JsonConvert.SerializeObject(values);
                    content = new StringContent(stringifiedContent, Encoding.UTF8, "application/json");
                }
                else
                {
                    content = null;
                }

                string tokenStringified = await GetToken();
                SetAuthorization(client, tokenStringified);

                //Send request
                HttpResponseMessage response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();
                #region ORIGINAL
                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                //}
                //else
                //{
                //    throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                //}
                #endregion


                #region MyRegion
                bool isSuccessStatusCode = response.IsSuccessStatusCode;
                if (isSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var resultReturn = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

                        #region ASOSA VALIDA TOKEN ANTES DEL RESPONSE
                        ResultArray result = JsonConvert.DeserializeObject<ResultArray>(response.Content.ReadAsStringAsync().Result);
                         await ValidateToken(result);
                        #endregion

                        return resultReturn;
                    }
                    else
                    {
                        throw new Exception("Error en la web api. Codigo: " + response.StatusCode);
                    }
                }
                else
                {

                    HttpStatusCode statusCode = response.StatusCode;
                    statusCode = HttpStatusCode.NotFound;// ASOSA SACAR ES PARA TEST
                    if (statusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("404");
                    }
                    else
                    {
                        if (statusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("500");
                        }
                        else
                        {
                            throw new Exception("No contemplada");
                        }
                    }
                }
                #endregion
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to resolve host"))
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw e;
                }
            }
        }

        public async static Task<string> GetToken()
        {
            var username = await LocalStorageService.GetUserLogin();
            var uuid = await LocalStorageService.GetUUID();
            var serial = await LocalStorageService.GetSerial();
            var tokenInfo = await LocalStorageService.GetRefreshToken();

            var token = new TokenValidationInfo()
            {
                Application = ApplicationConstants.ApplicationName,
                Token = tokenInfo,
                UserLogin = username,
                Deviceid = new DeviceInfoShort()
            };

            token.Deviceid.Serial = serial;
            token.Deviceid.Uuid = uuid;

            return JsonConvert.SerializeObject(token);
        }

        private static void SetAuthorization(HttpClient client, string tokenStringified)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "A-YPF-Token " + Convert.ToBase64String(Encoding.Unicode.GetBytes(tokenStringified)));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async static Task ValidateToken(Result response)
        {
            var authToken = response.AuthToken.ToLoginModel();

            if (eErrorLogin.GetEnum(authToken.Error) == ErrorTypeEnum.NO_ERR)
            {
                await LocalStorageService.StoreRefreshToken(authToken.AuthToken.Token);
            }
            else
            {
                await LocalStorageService.ClearUserCache();
                await LocalStorageService.RemoveAuthToken();
                throw new LoginException("Error al autenticarse");
            }
        }

        private async static Task ValidateToken(ResultArray response)
        {
            var authToken = response.AuthToken.ToLoginModel();

            if (eErrorLogin.GetEnum(authToken.Error) == ErrorTypeEnum.NO_ERR)
            {
                await LocalStorageService.StoreRefreshToken(authToken.AuthToken.Token);
            }
            else
            {
                await LocalStorageService.ClearUserCache();
                await LocalStorageService.RemoveAuthToken();
                throw new LoginException("Error al autenticarse");
            }
        }

        private static string GenerateQueryString(List<KeyValuePair<string, string>> values)
        {
            StringBuilder sb = new StringBuilder("?");

            if (values != null && values.Count > 0)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    //Si no es el primer parámetro se agrega un "?"
                    if (i != 0)
                        sb.Append("&");
                    //Agrego la llave y el valor con el formato adecuado para la url
                    sb.AppendFormat("{0}={1}", Uri.EscapeDataString(values[i].Key), Uri.EscapeDataString(values[i].Value));
                }
                return sb.ToString();
            }
            else
                return string.Empty;
        }
    }
}
