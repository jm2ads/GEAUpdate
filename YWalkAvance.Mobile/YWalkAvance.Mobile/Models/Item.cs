namespace Frontend.Mobile.Models
{
    public class Item
    {
        #region Constructores

        public Item(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }

        public Item()
        {
        }

        #endregion

        #region Propiedades

        public int Id { get; set; }

        public string Descripcion { get; set; }

        #endregion
    }
}