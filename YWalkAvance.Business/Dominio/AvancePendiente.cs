using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Business.Dominio
{

    public class AvancePendiente : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public int? ID { get; set; }

        public int TareaID { get; set; }

        public int PlanoID { get; set; }

        public int PartidaID { get; set; }

        public string CodigoActividad { get; set; }

        public string UnidadMedida { get; set; }

        public string DescripcionActividad { get; set; }

        public string CodigoPartida { get; set; }

        public string DescripcionPartida { get; set; }

        public string CodigoTarea { get; set; }

        public string DescripcionTarea { get; set; }

        public double CantidadEstimada { get; set; }

        public double CantidadAcumulada { get; set; }

        public string _Avance { get; set; }

        public string Avance
        {
            get
            {
                return _Avance;
            }
            set
            {
                _Avance = value;
                OnPropertyChanged();
            }
        }

        public string _AvanceAcumulado { get; set; }

        public string AvanceAcumulado
        {
            get
            {
                return _AvanceAcumulado;
            }
            set
            {
                _AvanceAcumulado = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
