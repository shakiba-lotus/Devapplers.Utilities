using System.IO;
using System.Security.AccessControl;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class DirectoryPermission
    {
        public static void AddDirectorySecurity(string fileName, string account, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType accessControlType)
        {
            var directoryInfo = new DirectoryInfo(fileName);
            var directorySecurity = directoryInfo.GetAccessControl();
            directorySecurity.AddAccessRule(new FileSystemAccessRule(account, fileSystemRights, inheritanceFlags,
                propagationFlags, accessControlType));
            directoryInfo.SetAccessControl(directorySecurity);
        }
    }
}