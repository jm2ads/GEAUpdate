using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Frontend.Mobile.Commons.Models
{
    public class RepresentanteComercialGroupModel : INotifyPropertyChanged
    {
        public string Usuario { get; set; }

        public string CodigoInterlocutor { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string IdNegocio { get; set; }

        public string NombreApellido
        {
            get { return Nombre + " " + Apellido; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
