using System.ComponentModel;

namespace ThumbnailSharp.Gui.Thumbs
{
    public class ThumbModel : INotifyPropertyChanged
    {
        private string _option, _location, _targetLocation,_format;
        private uint _thumbnailSize;
        public string Option
        {
            get
            {
                return _option;
            }
            set
            {
                if (_option != value)
                {
                    _option = value;
                    OnPropertyChanged(nameof(Option));
                }
            }
        }
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if(_location!=value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));

                }
            }
        }
        public string TargetLocation
        {
            get
            {
                return _targetLocation;
            }
            set
            {
                if (_targetLocation != value)
                {
                    _targetLocation = value;
                    OnPropertyChanged(nameof(TargetLocation));
                }
            }
        }
        public string Format
        {
            get
            {
                return _format;
            }
            set
            {
                if(_format!=value)
                {
                    _format = value;
                    OnPropertyChanged(nameof(Format));
                }
            }
        }
        public uint ThumbnailSize
        {
            get
            {
                return _thumbnailSize;
            }
            set
            {
                if(_thumbnailSize!=value)
                {
                    _thumbnailSize = value;
                    OnPropertyChanged(nameof(ThumbnailSize));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler!=null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
