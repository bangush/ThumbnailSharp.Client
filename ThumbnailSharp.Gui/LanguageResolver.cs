using System.Windows.Data;

namespace ThumbnailSharp.Gui
{
    public class LocalizationResolver : Binding
    {
        public LocalizationResolver(string name) : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = LanguageResource.Instance;
        }
    }
}
