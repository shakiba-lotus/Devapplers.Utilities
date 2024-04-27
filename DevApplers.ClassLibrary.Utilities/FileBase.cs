using System.Drawing.Imaging;

namespace DevApplers.ClassLibrary.Utilities
{
    public class FileBase
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileMimeType { get; set; }
        public string FileFriendlyName { get; set; }
        public ImageFormat FileImageFormat { get; set; }
    }

    public static class FileBaseDetails
    {
        public static FileBase GetFileDetails(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return new FileBase
                {
                    FileFriendlyName = "Unknown",
                    FileMimeType = "application/octet-stream",
                    FileExtension = ""
                };

            var data = base64.Substring(0, 4);

            switch (data.ToUpper())
            {
                case "IVBO":
                    return new FileBase
                    {
                        FileFriendlyName = "Photo",
                        FileMimeType = "image/png",
                        FileImageFormat = ImageFormat.Png,
                        FileExtension = ".png"
                    };

                case "/9J/":
                    return new FileBase
                    {
                        FileFriendlyName = "Photo",
                        FileMimeType = "image/jpeg",
                        FileImageFormat = ImageFormat.Jpeg,
                        FileExtension = ".jpg"
                    };

                case "R0lG":
                    return new FileBase
                    {
                        FileFriendlyName = "Photo",
                        FileMimeType = "image/gif",
                        FileImageFormat = ImageFormat.Gif,
                        FileExtension = ".gif"
                    };

                case "SUQZ":
                    return new FileBase
                    {
                        FileFriendlyName = "Audio",
                        FileMimeType = "audio/mpeg",
                        FileExtension = ".mp3"
                    };

                case "AAAA":
                    return new FileBase
                    {
                        FileFriendlyName = "Video",
                        FileMimeType = "video/mp4",
                        FileExtension = ".mp4"
                    };

                case "JVBE":
                    return new FileBase
                    {
                        FileFriendlyName = "Document",
                        FileMimeType = "application/pdf",
                        FileExtension = ".pdf"
                    };

                case "AAAB":
                    return new FileBase
                    {
                        FileFriendlyName = "Icon",
                        FileMimeType = "image/x-icon",
                        FileImageFormat = ImageFormat.Icon,
                        FileExtension = ".ico"
                    };

                case "UMFY":
                    return new FileBase
                    {
                        FileFriendlyName = "Archive",
                        FileMimeType = "application/x-rar-compressed",
                        FileExtension = ".rar"
                    };

                case "E1XY":
                    return new FileBase
                    {
                        FileFriendlyName = "Rich Text Format",
                        FileMimeType = "application/rtf",
                        FileExtension = ".rtf"
                    };

                case "U1PK":
                    return new FileBase
                    {
                        FileFriendlyName = "Text",
                        FileMimeType = "text/plain",
                        FileExtension = ".txt"
                    };

                case "MQOW":
                case "77U/":
                    return new FileBase
                    {
                        FileFriendlyName = "Text",
                        FileMimeType = "text/srt",
                        FileExtension = ".srt"
                    };

                default:
                    return new FileBase
                    {
                        FileFriendlyName = "Unknown",
                        FileMimeType = "application/octet-stream",
                        FileExtension = ""
                    };
            }
        }

        public static FileBase GetImageDetails(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return new FileBase
                {
                    FileMimeType = "application/octet-stream"
                };

            var data = fileName.Substring(fileName.LastIndexOf('.'));

            switch (data.ToUpper())
            {
                case ".PNG":
                    return new FileBase
                    {
                        FileMimeType = "image/png"
                    };

                case ".JPEG":
                case ".JPG":
                    return new FileBase
                    {
                        FileMimeType = "image/jpeg"
                    };

                case ".GIF":
                    return new FileBase
                    {
                        FileMimeType = "image/gif"
                    };

                case ".MP3":
                    return new FileBase
                    {
                        FileMimeType = "audio/mpeg"
                    };

                case ".MP4":
                    return new FileBase
                    {
                        FileMimeType = "video/mp4"
                    };

                default:
                    return new FileBase
                    {
                        FileMimeType = "application/octet-stream"
                    };
            }
        }
    }
}