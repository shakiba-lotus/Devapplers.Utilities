using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class Files
    {
        public static string SaveFile(this HttpPostedFileBase file, string path, string appSettingsKey = "",
            string fileName = "", ImageFile imageFile = null, int thumbWidth = 300)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            }
            var filePath = HostingEnvironment.MapPath(path);
            var fileExtension = Path.GetExtension(file.FileName);
            if (!Directory.Exists(filePath) && filePath != null)
            {
                Permission.SetDirectoryPermission(filePath, appSettingsKey);
                Directory.CreateDirectory(filePath);
            }

            var physicalPath = string.Concat(filePath, fileName, fileExtension);
            file.SaveAs(physicalPath);
            if (imageFile == null) return string.Concat(path, fileName, fileExtension);

            imageFile.FilePath = filePath;
            imageFile.FileName = fileName;
            imageFile.FileExtension = fileExtension;
            imageFile.FileMimeType = string.Concat(fileName, fileExtension).GetImageDetails().FileMimeType;
            imageFile.SaveImage(thumbWidth: thumbWidth, appSettingsKey: appSettingsKey);
            return string.Concat(path, fileName, fileExtension);
        }

        public static string CreateImage(string path, string fileExtension, ImageFile imageFile,
            string appSettingsKey = "", string fileName = "", int thumbWidth = 300)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            }
            var filePath = HostingEnvironment.MapPath(path);
            if (!Directory.Exists(filePath) && filePath != null)
            {
                Permission.SetDirectoryPermission(filePath, appSettingsKey);
                Directory.CreateDirectory(filePath);
            }

            imageFile.FilePath = filePath;
            imageFile.FileName = fileName;
            imageFile.FileExtension = fileExtension;
            imageFile.FileMimeType = string.Concat(fileName, fileExtension).GetImageDetails().FileMimeType;
            imageFile.SaveImage(thumbWidth: thumbWidth, appSettingsKey: appSettingsKey);
            return string.Concat(path, fileName, fileExtension);
        }

        public static bool DeleteFile(string pathWithName)
        {
            var filePath = "";
            if (!string.IsNullOrWhiteSpace(pathWithName))
            {
                filePath = HostingEnvironment.MapPath(pathWithName);
            }

            if (!File.Exists(filePath)) return false;
            File.Delete(filePath ?? throw new InvalidOperationException());
            var thumbPath = Path.Combine(filePath.Substring(0, filePath.LastIndexOf('\\')), "thumb/");
            if (Directory.Exists(thumbPath))
            {
                File.Delete(string.Concat(thumbPath, filePath.Substring(filePath.LastIndexOf('\\'))) ?? throw new InvalidOperationException());
            }

            return true;
        }

        public static bool DeleteDirectory(string pathDeleteDirectory)
        {
            foreach (var directory in Directory.GetDirectories(HostingEnvironment.MapPath(pathDeleteDirectory)))
            {
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }

            return true;
        }
    }
}
