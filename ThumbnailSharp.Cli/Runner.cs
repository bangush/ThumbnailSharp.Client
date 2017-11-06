namespace ThumbnailSharp.Cli
{
    class Runner
    {

        /* 
         *  
         *  thumbnail.exe <source-image-location> <target-thumbnail-location> <thumbnail-size> <format>
         *  format: [Jpeg|Bmp|Png|Gif|Icon|Tiff|Exif|Wmf|Emf]
         *  
         */

        static void Main(string[] args)
        {
            new Controller().HandleCommand(args);
        }
    }
}
