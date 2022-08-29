namespace Frontend.Mobile.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Info,
        Database
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
