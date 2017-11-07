using System.ComponentModel;
using System.Globalization;
using System.Resources;
namespace ThumbnailSharp.Gui
{
    public class LanguageResource: INotifyPropertyChanged
    {
        private static LanguageResource _instance = new LanguageResource();
        private readonly ResourceManager _resourceManager = Properties.Resources.ResourceManager;
        private CultureInfo _cultureInfo;
        public static LanguageResource Instance
        {
            get
            {
                return _instance;
            }
        }
        public string this[string key]
        {
            get
            {
                return _resourceManager.GetString(key, _cultureInfo);
            }
        }
        public CultureInfo CurrentCulture
        {
            get
            {
                return _cultureInfo;
            }
            set
            {
                if (_cultureInfo != value)
                {
                    _cultureInfo = value;
                    OnPropertyChanged("");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
