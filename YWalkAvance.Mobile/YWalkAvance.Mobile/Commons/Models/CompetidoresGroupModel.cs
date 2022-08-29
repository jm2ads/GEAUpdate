using Commons.Commons.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Frontend.Mobile.Commons.Models
{
    public class CompetidoresGroupModel : INotifyPropertyChanged
    {
        public string APIES { get; set; }
        public string CUIT { get; set; }
        public string ProvinciaLocalidad { get; set; }
        //public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Bandera { get; set; }
        public string BanderaDesc { get; set; }
        public string Direccion { get; set; }
        public string FechaUltimaActualizacion { get; set; }
        public string RazonSocial { get; set; }
        public string InterComercial { get; set; }
        public string Agrupacion { get; set; }
        public string UnidadNegocio { get; set; }
        public string Contacto { get; set; }
        public string CuentaSGC { get; set; }
        public string CuentaLPO { get; set; }
        public string CuentaLP2 { get; set; }
        public string CuentaQP1 { get; set; }
        public string NumeroExpediente { get; set; }
        public string OperaYer { get; set; }
        public string CantTarjetaYer { get; set; }
        public string CodDirent { get; set; }
        public string Atributo { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public CompetitorState Estado { get; set; }
        private bool _isPendingToSyncCompetitor { get; set; }
        public bool IsPendingToSyncCompetitor
        {
            get
            {
                // ASOSA SE AGREGO "|| Estado == CompetitorState.ErrorSync"
                if (Estado == CompetitorState.PrizeNoSync || Estado == CompetitorState.ErrorSync)
                {
                    _isPendingToSyncCompetitor = true;
                    OpacityDisabledEnabled = 1f;
                }
                else 
                {
                    _isPendingToSyncCompetitor = false;
                    OpacityDisabledEnabled = 0.25f;
                }
                return _isPendingToSyncCompetitor;
            }
            set
            {
                _isPendingToSyncCompetitor = value;
                OnPropertyChanged("IsPendingToSyncCompetitor");
            }
        }
        public float OpacityDisabledEnabled { get; set; }
        public string StateIcon
        {
            get {
                //ASOSA PNG en ROJO
                return (Estado == CompetitorState.NoPrizeSurvey) ? "noPrize.png" : (Estado == CompetitorState.PrizeSync) ? "prizeSync.png" : (Estado == CompetitorState.ErrorSync) ? "errorSync.png" : (Estado == CompetitorState.ManualSync) ? "sincronizacionManual.png" : "prizeNoSync.png";

            }
        }
        public CompetidoresGroupModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
