using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThumbnailSharp.Cli
{
    public class Controller
    {
        public void HandleCommand(string[] args)
        {
            if(args.Length!=4)
            {
                DisplayCommand();
            }
            else
            {
                ProcessCommand(args);
            }
        }
        private void DisplayCommand()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Thumbnail Sharp CLI");
            Console.WriteLine("Created by: Mirza Ghulam Rasyid");
            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("Thumbnail.exe <source-image-location> <target-thumbnail-location> <thumbnail-size> <format>\n");
            Console.WriteLine("> source-image-location     : Image file location (ex: C:\\myimage.jpg)");
            Console.WriteLine("> target-thumbnail-location : Location where thumbnail will be saved to (C:\\mythumbnail.jpg)");
            Console.WriteLine("> thumbnail-size            : The size of thumbnail");
            Console.WriteLine("> format                    : Thumbnail format -> [Jpeg|Bmp|Png|Gif|Icon|Tiff|Exif|Wmf|Emf]");
        }
        private void ProcessCommand(string[] args)
        {
            string[] formats = new string[]
            {
                "jpeg","bmp","png","gif","icon","tiff","exif","wmf","emf"
            };
            string source = args[0];
            string target = args[1];
            if (String.IsNullOrEmpty(source))
                Console.WriteLine("`source-image-location` cannot be empty");
            else
            {
                if(!File.Exists(source))
                    Console.WriteLine("`source-image-location` cannot be found");
                else
                {
                    if(String.IsNullOrEmpty(target))
                        Console.WriteLine("`target-thumbnail-location` cannot be empty");
                    else
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(target)))
                            Console.WriteLine("Directory for `target-thumbnail-location` cannot be found");
                        else
                        {
                            uint size;
                            if (!uint.TryParse(args[2], out size))
                                Console.WriteLine("`thumbnail-size` is invalid");
                            else
                            {
                                string formatStr = args[3];
                                if (String.IsNullOrEmpty(formatStr))
                                    Console.WriteLine("`format` cannot be empty");
                                else
                                {
                                    formatStr = formatStr.ToLower();
                                    if (!formats.Contains(formatStr))
                                        Console.WriteLine("`format` is invalid");
                                    else
                                    {
                                        Format format = Format.Jpeg;
                                        switch(formatStr)
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
                                            case "icon":
                                                format = Format.Icon;
                                                break;
                                            case "tiff":
                                                format = Format.Tiff;
                                                break;
                                            case "exif":
                                                format = Format.Exif;
                                                break;
                                            case "wmf":
                                                format = Format.Wmf;
                                                break;
                                            case "emf":
                                                format = Format.Emf;
                                                break;
                                        }
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ExecutedCommand(string source, string target, uint size, Format format)
        {
            bool success = false;
            try
            {
                using (FileStream fs = new FileStream(target, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    Stream sourceStream = new ThumbnailCreator().CreateThumbnailStream(size, source, format);
                    if (sourceStream != null)
                    {
                        if (sourceStream.Position != 0)
                            sourceStream.Position = 0;

                        sourceStream.CopyTo(fs);
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                success = false;
            }
            finally
            {
                if(success)
                    Console.WriteLine("Thumbnail created successfully...");
                else
                    Console.WriteLine("Operation was not completed successfully...");
            }
        }
    }
}
