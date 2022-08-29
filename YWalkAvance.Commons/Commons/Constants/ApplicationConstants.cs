using System;

namespace Commons.Commons.Constants
{
    public class ApplicationConstants
    {

        #region APP

        public const string ApplicationName = "GEA_RRPP";
        public static readonly DateTime DefaultDateSync = new DateTime(2000, 01, 01);
        public const int TimeoutTime = 3000;
        public const string Enviroment = "DESARROLLO";
        public const string EmailInfo = "DesarrolloMobile@ypf.com";
        public const string ApplicationSecretiOS = "ios=123456";
        public const string ApplicationSecretAndroid = "android=123456";
        public const int MaxVariableSqLite = 900;

        #endregion

        #region IMAGES

        public const string IMG_EDIT = "ic_editar.png";
        public const string IMG_PASSWORD = "";

        #endregion

        #region STORAGE

        public const string STORAGE_USERNAME = "username";
        public const string STORAGE_USERTYPE = "usertype";
        public const string STORAGE_USER_ACCOUNT_DATA = "userdata";
        public const string STORAGE_USERLOGIN = "userlogin";
        public const string STORAGE_UUID = "uuid";
        public const string STORAGE_SERIAL = "serial";
        public const string STORAGE_REFRESH_TOKEN = "refresh-token";
        public const string STORAGE_ROLE = "role";
        public const string STORAGE_USERNAME_IMPERSONATE = "usernameimpersonate";

        #endregion

        #region MESSAGE

        public const string SelectedObra = "SelectedObra";
        public const string SelectedPlano = "SelectedPlano";

        #endregion
        #region PRICE SURVEY BUSINESS VALUES
        public const string RELEVAMIENTO_PRECIOS_CATEGORIA = "Z06";
        public const string RELEVAMIENTO_PRECIOS_COD_FORMULARIO = "0005";
        public const string RELEVAMIENTO_PRECIOS_DESCRIPCION = "Tarea de relevamiento de precios";
        public const string RELEVAMIENTO_PRECIOS_ESTADO = "E0003";
        public const string RELEVAMIENTO_PRECIOS_MOTIVO = "ZR06";
        public const string RELEVAMIENTO_PRECIOS_OPERACION = "ZVRC";
        public const string RELEVAMIENTO_PRECIOS_SEGMENTO = "RETAIL";
        public const string RELEVAMIENTO_PRECIOS_NEGOCIO = "RED_ABANDE";
        #endregion
    }
}
