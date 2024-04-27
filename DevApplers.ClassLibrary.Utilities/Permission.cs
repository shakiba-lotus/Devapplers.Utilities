using System.IO;
using System.Security.AccessControl;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class Permission
    {
        public static void SetDirectoryPermission(string path, string appSettingsKey)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
            DirectoryPermission.AddDirectorySecurity(path, appSettingsKey
                /*ConfigurationManager.AppSettings["DirectoryPermission"]*/,
                FileSystemRights.Modify, InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit, AccessControlType.Allow);
        }
    }
}