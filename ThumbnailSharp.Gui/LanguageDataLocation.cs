using System;
using System.IO;

namespace ThumbnailSharp.Gui
{
    public class LanguageDataLocation
    {
        public static string Location
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create), "lang.dat");
            }
        }
    }
}
