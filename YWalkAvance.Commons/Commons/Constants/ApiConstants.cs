namespace Commons.Commons.Constants
{
    public static class ApiConstants
    {
        #region BASE API

        //public static string BaseApiRest = "https://piwik.ypf.com.ar/yWalkBackendService_Test/";
        //public static string BaseApiRest = "http://localhost:55413/";

        //public static string BaseApiRest = "https://magui-test.ypf.com/apip/rrpp/";
       public static string BaseApiRest = "https://magui-test.ypf.com/apip/rrpp/";
        #endregion

        #region USER

        public static string LoginUser = "seguridad/Login";
        public static string RegisterUser = "seguridad/Register";

        #endregion

        #region COMPETIDORES

        public static string GetCompetidores = "competidor/competidores";

        #endregion

        #region RELEVAMIENTO PRECIOS

        public static string SetRelevamientoPrecios = "rp/relevamientoPreciosUpdate";
        public static string SetPreciosProductos = "rp/preciosProductos";

        #endregion

        #region BANDERA 

        public static string GetBanderas = "bandera/banderas​";
        public static string GetBanderasProducto = "bandera/banderas​Producto";

        #endregion

        #region PRODUCTO

        public static string GetProductos = "producto/productos​";

        #endregion

        #region REPRESENTANTE COMERCIAL

        public static string GetRepresentanteComercial = "rrcc/representanteComercial​";

        #endregion

        #region NEGOCIO

        public static string GetNegocios = "negocio/negocios";

        #endregion

        #region SEGMENTO

        public static string GetSegmentos = "segmento/segmentos";

        #endregion

        #region PROVINCIA

        public static string GetProvincias = "provincia/provincias";

        #endregion

        #region OBRA

        public static string GetObra = "obra/Obra/";
        public static string GetObras = "obra/obras";
        public static string GetObrasPorUsuario = "obra/obrasPorUsuario";
        #endregion

        #region AREA

        public static string GetAreasByObra = "area/areas";

        #endregion

        #region PLANO

        public static string GetPlanosByObra = "plano/planos​";

        public static string GetArchivoPlano = "planoArchivo/archivo";

        #endregion

        #region TAREA

        public static string GetTareasByObra = "tarea/tareasPorObra​";

        #endregion

        #region PARTIDA

        public static string GetPartidas = "partida/partidas​";

        #endregion

        #region ESPECIALIDAD

        public static string GetEspecialidades = "especialidad/especialidades​";
        public static string GetEspecialidadesAvance = "especialidad/especialidadAvance​";

        #endregion

        #region ACTIVIDAD

        public static string GetActividades = "actividad/actividades";

        #endregion

        #region TAREAS PARTIDAS PLANOS

        public static string GetTareasPartidasPlanosByObra = "tpp/tareasPartidaPlanoPorObra​";

        public static string SetTareasPartidasPlanos = "tpp/tareasPartidaPlanoUpdate";

        #endregion

        #region UNIDAD MEDIDA

        public static string GetUnidadesDeMedida = "actividad/actividades";

        #endregion

        #region SINCRONIZACION PENDIENTE

        public static string GenerarSincronizacionesPendientes = "sp/generarSincronizacionPendiente";

        #endregion
    }
}
