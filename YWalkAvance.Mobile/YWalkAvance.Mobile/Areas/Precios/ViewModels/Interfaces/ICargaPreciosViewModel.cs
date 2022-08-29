using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Precios.ViewModels.Interfaces
{
    public interface ICargaPreciosViewModel
    {
        void ProductosListView_SizeChanged(int size);
        void SetCollectionViewProductos(CollectionView listViewProductos);
        void SetBtnGuardar(Button btnGuardar);
        void SetBtnReiniciarPrecios(Button btnReiniciarPrecios);
        void SetBtnExpandCollapse(ImageButton btnExpandCollapse);
        void PrecioEnterosEntry_TextChanged(object sender, TextChangedEventArgs e);
        void PrecioDecimalesEntry_TextChanged(object sender, TextChangedEventArgs e);
        void PrecioEnterosEntry_Focused(object sender, FocusEventArgs e);
        void EntryPrecio_Unfocused(object sender, FocusEventArgs e);
        void DecimalesEntry_Focused(object sender, FocusEventArgs e);
        Task<bool> HasNotSavedProducts();
        Task GoBack();
    }
}
