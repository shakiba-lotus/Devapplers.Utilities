using System;
using System.IO;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class ConvertBase64
    {
        public static void SaveByteArrayAsFile(string path, string fileName, string base64String, string appSettingsKey = "")
        {
            var base64 = base64String.Substring(base64String.IndexOf(',') + 1);
            var bytes = Convert.FromBase64String(base64);
            if (!Directory.Exists(path) && path != null)
            {
                Permission.SetDirectoryPermission(path, appSettingsKey);
                Directory.CreateDirectory(path);
            }
            using (var fileStream = new FileStream(string.Concat(path, fileName), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //fileStream.Write(bytes, 0, bytes.Length);
                //fileStream.Close();
                foreach (var varByte in bytes)
                {
                    fileStream.WriteByte(varByte);
                }
                fileStream.Seek(0, SeekOrigin.Begin);
            }
        }
        public static string ConvertFileToBase64(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var filebytes = new byte[fileStream.Length];
                fileStream.Read(filebytes, 0, Convert.ToInt32(fileStream.Length));
                var base64String = Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);
                fileStream.Close();
                return string.Concat("data:", base64String.GetFileDetails().FileMimeType, ";base64,", base64String);
            }
        }

        public static void SaveByteArrayAsImage(string path, string fileName, string base64String, string appSettingsKey = "")
        {
            var base64 = base64String.Substring(base64String.IndexOf(',') + 1);
            var bytes = Convert.FromBase64String(base64);
            if (!Directory.Exists(path) && path != null)
            {
                Permission.SetDirectoryPermission(path, appSettingsKey);
                Directory.CreateDirectory(path);
            }
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var varImage = System.Drawing.Image.FromStream(memoryStream))
                {
                    varImage.Save(string.Concat(path, fileName), base64.GetFileDetails().FileImageFormat);
                }
            }
        }
        public static string ConvertImageToBase64(string path)
        {
            using (var image = System.Drawing.Image.FromFile(path))
            {
                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, image.RawFormat);
                    var imageBytes = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);
                    return string.Concat("data:", base64String.GetFileDetails().FileMimeType, ";base64,", base64String);
                }
            }
        }
    }
}