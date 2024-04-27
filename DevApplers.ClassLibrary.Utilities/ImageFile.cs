using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace DevApplers.ClassLibrary.Utilities
{
    public class ImageFile : FileBase
    {
        private int PictureWidth { get; set; }
        private long EncoderValue { get; set; }
        private bool IsCreateThumb { get; set; }


        public ImageFile(int pictureWidth = 700, long encoderValue = 100L, bool isCreateThumb = true)
        {
            PictureWidth = pictureWidth;
            EncoderValue = encoderValue;
            IsCreateThumb = isCreateThumb;
        }

        internal void SaveImage(int thumbWidth, string appSettingsKey = "")
        {
            int width, height;
            var largPic = string.Concat(FilePath, FileName, FileExtension);
            var bitmapOrginalPic = new Bitmap(largPic);

            if (bitmapOrginalPic.Width >= PictureWidth && PictureWidth != 0)
            {
                width = PictureWidth;
                height = PictureWidth * bitmapOrginalPic.Height / bitmapOrginalPic.Width;
            }
            else
            {
                width = bitmapOrginalPic.Width;
                height = bitmapOrginalPic.Height;
            }

            var imageCodecInfo = GetEncoderInfo(FileMimeType);
            var encoder = Encoder.Quality;
            var encoderParameter = new EncoderParameter(encoder, EncoderValue);
            var encoderParameters = new EncoderParameters(1) { Param = { [0] = encoderParameter } };

            if (IsCreateThumb)
            {
                var thumbPath = Path.Combine(FilePath, "thumb/");
                if (!Directory.Exists(thumbPath))
                {
                    Permission.SetDirectoryPermission(thumbPath, appSettingsKey);
                    Directory.CreateDirectory(thumbPath);
                }

                var thumbPic = string.Concat(thumbPath, FileName, FileExtension);
                int thumbHeight;
                if (bitmapOrginalPic.Width >= thumbWidth && thumbWidth != 0)
                    thumbHeight = thumbWidth * bitmapOrginalPic.Height / bitmapOrginalPic.Width;
                else
                {
                    thumbWidth = bitmapOrginalPic.Width;
                    thumbHeight = bitmapOrginalPic.Height;
                }

                var thumbSize = new Size(thumbWidth, thumbHeight);
                var bitmapThumbPic = new Bitmap(bitmapOrginalPic, thumbSize);
                bitmapThumbPic.Save(thumbPic, imageCodecInfo, encoderParameters);
            }

            var largSize = new Size(width, height);
            var bitmapLargPic = new Bitmap(bitmapOrginalPic, largSize);
            bitmapOrginalPic.Dispose();
            File.Delete(largPic);
            bitmapLargPic.Save(largPic, imageCodecInfo, encoderParameters);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();
            //foreach (var t in encoders)
            //{
            //    if (t.MimeType == mimeType)
            //        return t;
            //}
            //return null;
            return encoders.FirstOrDefault(t => t.MimeType == mimeType);
        }
    }
}
