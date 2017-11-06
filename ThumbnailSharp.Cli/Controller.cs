using System;
using System.IO;
using System.Linq;
namespace ThumbnailSharp.Cli
{
    public class Controller
    {
        public void HandleCommand(string[] args)
        {
            if (args.Length != 5)
            {
                DisplayCommandText();
            }
            else
            {
                ProcessCommand(args);
            }
        }
        private void DisplayCommandText()
        {
            Console.WriteLine(" ----------------------------------------------");
            Console.WriteLine("             Thumbnail Sharp CLI");
            Console.WriteLine("        Created by: Mirza Ghulam Rasyid");
            Console.WriteLine(" ----------------------------------------------\n");
            Console.WriteLine(" Thumbnail.exe <option> <source-image-location> <target-thumbnail-location> <thumbnail-size> <format>\n");
            Console.WriteLine(" > option                    : Source of image => [local|internet]");
            Console.WriteLine(" > source-image-location     : If option is set to `local` then specify local file location. (eg: C:\\myimage.jpg)");
            Console.WriteLine("                               If option is set to `internet` then specify valid url. (eg: https://mywebsite/myimage.jpg)");
            Console.WriteLine(" > target-thumbnail-location : Location where thumbnail will be saved to. (eg: C:\\mythumbnail.jpg)");
            Console.WriteLine(" > thumbnail-size            : The size of thumbnail.");
            Console.WriteLine(" > format                    : Thumbnail processing format => [Jpeg|Bmp|Png|Gif|Tiff]");
        }

        private void ProcessCommand(string[] args)
        {
            string[] formats = new string[]
            {
                "jpeg","bmp","png","gif","tiff"
            };
            string[] options = new string[]
            {
                "local","internet"
            };
            string option = args[0];
            option = option.ToLower();
            if (!options.Any(x => x == option))
                Console.WriteLine("`option` is invalid");
            else
            {
                string source = args[1];
                string target = args[2];
                uint size;
                Format format = Format.Jpeg;
                
                if (option=="local")
                {
                    if (!File.Exists(source))
                        Console.WriteLine("`source-image-location` cannot be found");
                    else
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(target)))
                            Console.WriteLine("Directory for `target-thumbnail-location` cannot be found");
                        else
                        {
                            if (!uint.TryParse(args[3], out size))
                                Console.WriteLine("`thumbnail-size` is invalid");
                            else
                            {
                                string formatStr = args[4];
                                formatStr = formatStr.ToLower();
                                if (!formats.Any(x => x == formatStr))
                                    Console.WriteLine("`format` is invalid");
                                else
                                {
                                    format = GetFormat(formatStr);
                                    ExecuteCommand(source, target, size, format);
                                }
                            }
                        }
                    }
                }
                else if(option == "internet")
                {
                    if (!Uri.IsWellFormedUriString(source,UriKind.Absolute))
                        Console.WriteLine("Please specify valid url address with scheme. Eg: http://address.com/data.jpg or https://address.com/data.jpg");
                    else
                    {
                        if (!uint.TryParse(args[3], out size))
                            Console.WriteLine("`thumbnail-size` is invalid");
                        else
                        {
                            string formatStr = args[4];
                            formatStr = formatStr.ToLower();
                            if (!formats.Any(x => x == formatStr))
                                Console.WriteLine("`format` is invalid");
                            else
                            {
                                format = GetFormat(formatStr);
                                ExecuteCommand(new Uri(source, UriKind.Absolute), target, size, format);
                            }
                        }
                    }
                }
               
            }
            
        }
        private Format GetFormat(string formatStr)
        {
            Format format = Format.Jpeg;
            switch (formatStr)
            {
                case "bmp":
                    format = Format.Bmp;
                    break;
                case "png":
                    format = Format.Png;
                    break;
                case "gif":
                    format = Format.Gif;
                    break;
                case "tiff":
                    format = Format.Tiff;
                    break;
            }
            return format;
        }
        private void ExecuteCommand(string source, string target, uint size, Format format)
        {
            Console.WriteLine("Processing...");
            bool success = false;
            try
            {


                using (Stream sourceStream = new ThumbnailCreator().CreateThumbnailStream(size, source, format))
                {
                    if (sourceStream != null)
                    {
                        using (FileStream fs = new FileStream(target, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            if (sourceStream.Position != 0)
                                sourceStream.Position = 0;

                            sourceStream.CopyTo(fs);
                            success = true;
                        }
                    }
                    else
                    {
                        success = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                success = false;
            }
            finally
            {
                if (success)
                    Console.WriteLine("Thumbnail created successfully...");
                else
                    Console.WriteLine("Operation was not completed successfully...");
            }
        }
        private void ExecuteCommand(Uri source, string target, uint size, Format format)
        {
            Console.WriteLine("Downloading image...");
            bool success = false;
            try
            {

                ThumbnailCreator thumbnailCreator = new ThumbnailCreator();
                using (Stream sourceStream = thumbnailCreator.CreateThumbnailStreamAsync(size, source, format).Result)
                {
                    
                    if (sourceStream != null)
                    {
                        Console.WriteLine("Processing...");
                        using (FileStream fs = new FileStream(target, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            if (sourceStream.Position != 0)
                                sourceStream.Position = 0;

                            sourceStream.CopyTo(fs);
                            success = true;
                        }
                    }
                    else
                    {
                        success = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                success = false;
            }
            finally
            {
                if (success)
                    Console.WriteLine("Thumbnail created successfully...");
                else
                    Console.WriteLine("Operation was not completed successfully...");
            }
        }
    }
}
