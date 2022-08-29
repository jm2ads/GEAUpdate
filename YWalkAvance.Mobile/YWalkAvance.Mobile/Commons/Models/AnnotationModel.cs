using Xamarin.Forms;

namespace Frontend.Mobile.Commons.Models
{
    public class AnnotationModel : ObservableModel
    {
        string _Text;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; OnPropertyChanged(); }
        }
        Color _TextColor;
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; OnPropertyChanged(); }
        }

        public static AnnotationModel DefaultAnnotation(string text = "")
        {
            Color defaultColor = Color.DarkGray;
            //Use style color if exists
            if (App.Current.Resources.ContainsKey("grayPrimary"))
                defaultColor = (Color)Application.Current.Resources["grayPrimary"];

            return new AnnotationModel()
            {
                Text = text,
                TextColor = defaultColor
            };
        }

        public static AnnotationModel ValidationErrorAnnotation(string text)
        {
            Color errorColor = Color.Red;
            //Use style color if exists
            if (App.Current.Resources.ContainsKey("purpleDark"))
                errorColor = (Color)Application.Current.Resources["purpleDark"];

            return new AnnotationModel()
            {
                Text = text,
                TextColor = errorColor
            };
        }
    }
}
