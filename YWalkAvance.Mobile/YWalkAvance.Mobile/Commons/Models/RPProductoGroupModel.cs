using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Frontend.Mobile.Commons.Models
{
    public class RPProductoGroupModel : INotifyPropertyChanged
    {
        public int IdRelevamientoPreciosProducto { get; set; }
        public string Descripcion { get; set; }
        public string CodigoSAP { get; set; }
        public int IdSegmento { get; set; }
        public string Envase { get; set; }
        public string Precio { get; set; }
        public string PrecioEnteros { get; set; }
        public string PrecioDecimales { get; set; }
        public float _heightRowsProductos;
        public float HeightRowsProductos
        {
            get
            {
                return _heightRowsProductos;
            }
            set
            {
                _heightRowsProductos = value;
                OnPropertyChanged("HeightRowsProductos");
            }
        }
        //public string Volumen { get; set; }
        public RPProductoGroupModel() {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
