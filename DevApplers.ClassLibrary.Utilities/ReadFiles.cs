using System.IO;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class ReadFiles
    {
        public static byte[] ReadFile(string path)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            var fileInfo = new FileInfo(path);
            var numBytes = fileInfo.Length;

            //Open FileStream to read file
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            var binaryReader = new BinaryReader(fileStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = binaryReader.ReadBytes((int)numBytes);

            return data;
        }
    }
}
