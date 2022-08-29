namespace Commons.Commons.Constants
{
    public class ApplicationMessages
    {

        #region Titles

        public const string Menu = "Menu Principal";

        public const string Exit = "SALIR";

        public const string Error = "ERROR";

        public const string Atencion = "ATENCIÓN";

        public const string EnviarGEA = "ENVIAR A GEA";

        public const string Save = "GUARDAR";

        public const string Continue = "CONTINUAR";

        #endregion

        #region Options

        public const string Accept = "ACEPTAR";

        public const string Recover = "RECUPERAR";

        public const string Cancel = "CANCELAR";

        public const string Close = "CERRAR";

        public const string CancelLowerCase = "Cancelar";

        public const string Affirmative = "SI";

        public const string Negative = "NO";

        public const string Advances = "Avances";

        public const string Work = "Obra Completa";

        public const string Syncronization = "SINCRONIZAR";

        public const string Send = "ENVIAR";

        public const string Synchronize = "Sincronizar";

        public const string SendErrorLog = "Enviar Log de Errores";

        public const string SendDB = "Enviar Database";

        public const string CloseSession = "¿Desea cerrar sesión?";

        public const string NoInteractionsNoProducts = "No hay 'Interacciones' y 'Productos' a sincronizar.";

        public const string About = "Acerca de... ";

        public const string WarningDrawable = "warning.png";

        public const string ErrorDrawable = "error.png";

        public const string ConfirmationDrawable = "confirmation.png";

        #endregion

        #region Questions

        public const string DownloadPlano = "¿Desea descargar los planos?";

        public const string Sync = "¿Que desea sincronizar?";

        public const string ExitApp = "¿Desea salir de la app?";

        #endregion

        #region Error Description
        public const string SyncGralError = "Se ha producido un error al intentar sincronizar los datos. Por favor reintente más tarde";
      
        public const string SyncObraError = "Ha surgido un error al momento de sincronizar la obra";

        public const string LoginCredentialsError = "Credenciales inválidas";

        public const string LoginError = "Ha surgido un error al momento de realizar el login de usuario";

        public const string DownloadFileError = "Ha surgido un error al momento de obtener el archivo";

        public const string SyncErrorGetDB = "Ha surgido un error al momento de obtener datos de la base local";

        public const string SyncErrorSaveDB = "Ha surgido un error al momento de guardar datos en la base local";

        public const string ErrorDeletingDB = "Ha surgido un error al momento de borrar los datos de la base local";

        public const string AdvanceSaveError = "Ha surgido un error al momento de guardar el avance en la base local";

        public const string AdvanceRedirectError = "Ha surgido un error al momento de ingresar a la pantalla 'Registrar Avance'";

        public const string AdvanceInitializeError = "Ha surgido un error al momento de inicializar la pantalla 'Avance'";

        public const string ExpiredCredentialsError = "Ha ocurrido un error al intentar autenticarse. Vuelva a ingresar sus Credenciales.";

        public const string ProductSaveError = "Ha surgido un error al momento de guardar el Producto en la base local";

        public const string SyncPriceSurveyError = "Se ha producido un error en la sincronización de las interacciones, por favor póngase en contacto con el equipo de soporte.";

        public const string NoEmptyCredentials = "Los campos 'Usuario' y 'Contraseña' son obligatorios.";

        public const string NoEmptyUser = "El campo 'Usuario' es obligatorio.";

        public const string NoEmptyPassword = "El campo 'Contraseña' es obligatorio.";

        public const string DifferentUserLoggedError = "Sólo un usuario puede ingresar a la aplicación por dispositivo. Por favor póngase en contacto con el equipo de soporte.";

        public const string RRCCNotRetail = "El usuario no tiene permisos para ingresar a la aplicación. Por favor póngase en contacto con el equipo de soporte.";

        public const string AccessDeniedLogin = "El usuario no tiene permisos para ingresar a la aplicación. Por favor póngase en contacto con el equipo de soporte.";

        public const string RRCCNotRetailSync = "No tiene permisos para realizar la sincronización. Por favor, comuníquese con el Administrador.";

        public const string NoServiceConnection = "Se ha producido un error en el Servicio, por favor póngase en contacto con el soporte.";

        public const string ErrorSendCompetitorsProcess = "Ha ocurrido un error al procesar los datos, por favor póngase en contacto con el equipo de soporte.";

        public const string ErrorSendCompetitorsRequest = "Ha ocurrido un error al enviar los datos, intente de nuevo en unos minutos.";

        public const string ClosedSession = "La sesión se ha cerrado, por favor vuelva a loguearse.";

        public const string ErrorGeneric = "Se ha producido un error en la aplicación, por favor póngase en contacto con el equipo de soporte.";
        #endregion

        #region Notifications

        public const string EmptyProductListView = "No hay 'Productos' para mostrar.";
        
        public const string EmptyProductListViewForNonExistentFlag = "La bandera del competidor no existe o no se encuentra sincronizada. Por favor, vuelva a sincronizar y de persistir el error, contáctese con el Administrador del sistema.";

        public const string EmptyCompetitorsListView = "No se ha encontrado resultado posible para la búsqueda realizada.";

        public const string EmptyCompetitorsFromSyncListView = "No tiene competidores asignados.";

        public const string BluePrintNotification = "No se ha descargado el Plano. Vuelva a la pantalla anterior y descárguelo";

        public const string Warning = "ATENCIÓN";

        public const string ClosingApp = "Los datos de la obra han sido eliminados. A continuación se cerrará la aplicación";

        public const string SyncSuccess = "Sincronización finalizada exitosamente";

        public const string SendSuccess = "Los competidores fueron enviados de forma exitosa a la web de contingencia.";

        public const string SendNoContent = "No existen competidores para ser enviados a la web de contingencia.";

        public const string CompetitorsSyncSuccess = "Los competidores se sincronizaron con éxito!";

        public const string SyncWorkSelection = "Se debe seleccionar una Obra para sincronizar";

        public const string DownloadFileReminder = "Recuerde que deberá descargar los planos de forma manual";

        public const string NoInternetConnection = "Se ha producido un error en la conexión de red.";

        public const string DownloadFileSuccess = "Archivo guardado exitosamente";

        public const string AdvanceAcumPositive = "El valor 'Acumulado' debe ser positivo";

        public const string AdvanceAcumEstimated = "Ingrese un valor 'Acumulado' que no sea mayor a 'Comp. APC'";

        public const string AdvanceInputRequired = "Se requiere el ingreso del avance 'Actual' y 'Acumulado'";

        public const string NoAdvances = "Plano inhabilitado, no se podran registrar avances";

        public const string PendingAdvances = "La obra tiene información pendiente de sincronizar. ¿Seguro que desea eliminar la obra: ";

        public const string ConfirmationWorkDeleted = "¿Seguro que desea eliminar la obra: ";

        public const string NumbersOnlyPrize = "Debe ingresar sólo números";

        public const string PrizeValuePositive = "El valor 'Precio' debe ser positivo";

        public const string Success = "ÉXITO";

        public const string RecordSavedSuccessfully = "La información se guardó con éxito.";

        public const string SyncNotification = "¿Desea sincronizar los datos?";

        public const string SyncSingleCompetitorNotification = "¿Desea sincronizar los datos de este competidor?";

        public const string EnviarGEANotification = "¿Desea enviar a la web de contingencia los competidores con error al sincronizar?";

        public const string SyncStatus = "Estado de Sincronización";

        public const string CompetitorsDeletedText = "- Los siguientes competidores fueron eliminados de tu cartera: ";
        
        public const string CompetitorsErrorText = "- Los siguientes competidores tuvieron problemas para ser sincronizados: ";

        public const string DefaultSyncronizedInteractionsText = "Se ha actualizado correctamente la información procedente de SAP.";

        public const string ResetStatesNoPrizeNotification = "Antes de comenzar un Nuevo Relevamiento le sugerimos Sincronizar Datos ¿Desea continuar con operación?";

        public const string ResetStatesNoSyncNotification = "Posee estaciones pendientes por sincronizar. Antes de comenzar un Nuevo Relevamiento le sugerimos Sincronizar Datos ¿Desea continuar con operación?";

        public const string No_RRCC_SAP_Registered = "El usuario no tiene permisos para ingresar a la aplicación, por favor póngase en contacto con el equipo de soporte.";

        public const string DeleteUserConfirmationFirst = "Desea eliminar la cuenta ";

        public const string DeleteUserConfirmationSecond = " , si presiona “Aceptar” borra todos los datos almacenados.";

        public const string LoadCompetitorsError = "Se ha producido un error en la sincronización de los datos de los competidores, por favor póngase en contacto con el equipo de soporte.";

        public const string LoadRRCCError = "Se ha producido un error en la sincronización de los datos del usuario, por favor póngase en contacto con el equipo de soporte.";

        public const string LoadMasterParametricTablesError = "Se ha producido un error en la sincronización de los datos maestros, por favor póngase en contacto con el equipo de soporte.";
        
        public const string NoPrizesToRestart = "No hay precios nuevos para reiniciar.";

        public const string RestartPrizesConfirmation = "¿Desea recuperar los precios iniciales?";

        public const string NotSavedProductsNotification = "Los precios ingresados no fueron guardados. ¿Desea continuar?";

        public const string SAPErrorNotification = "El servicio de SAP está caído.";
        
        public const string SendInteractionsErrorNotification = "Ha ocurrido un error en el envío de los datos.";
        #endregion

        #region Sync Process

        public const string GettingRows = "Obteniendo Registros... ";

        public const string SavingDB = "Guardando Base De Datos";

        #endregion

    }
}
