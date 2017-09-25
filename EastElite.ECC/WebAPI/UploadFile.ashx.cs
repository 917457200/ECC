using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    /// <summary>
    /// UploadFile 的摘要说明
    /// </summary>
    /// <summary>
    /// 图像上传功能的实现
    /// </summary>
    public class UploadImpl
    {
        public UploadImpl(IFileUploadSize fileUploadSize)
        {

            _fileUploadSize = fileUploadSize ?? new TestFileUploadSize();
        }
        public UploadImpl()
            : this(null)
        {

        }
        #region Fields & Consts
        static string FileHostUri = System.Configuration.ConfigurationManager.AppSettings["FileHostUri"]
            ?? HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;

        Point point = new Point(0, 0); //图像从那个坐标点进行截取
        double wRate = 1, hRate = 1, setRate = 1;
        int newWidth = 0, newHeight = 0;
        IFileUploadSize _fileUploadSize;
        #endregion

        #region  图像缩放
        /// <summary>
        /// 图像的缩放
        /// </summary>
        /// <param name="file">缩放文件</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="isEqualScale">是否等比例缩放</param>
        /// <param name="name">缩放后存放的地址</param>
        /// <returns></returns>
        bool CreateThumbnail(HttpPostedFile file, ImageSize imageSize, bool isEqualScale, string name)
        {
            double width = (double)imageSize.Width;
            double height = (double)imageSize.Height; ;

            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);
                if (isEqualScale)
                {
                    if (image.Height > height)
                    {
                        hRate = height / image.Height;
                    }

                    if (image.Width > width)
                    {
                        wRate = width / image.Width;
                    }

                    if (wRate != 1 || hRate != 1)
                    {
                        if (wRate > hRate)
                        {
                            setRate = hRate;
                        }
                        else
                        {
                            setRate = wRate;
                        }
                    }

                    newWidth = (int)(image.Width * setRate);
                    newHeight = (int)(image.Height * setRate);
                    if (height > newHeight)
                    {
                        point.Y = Convert.ToInt32(height / 2 - newHeight / 2);
                    }
                    if (width > newWidth)
                    {
                        point.X = Convert.ToInt32(width / 2 - newWidth / 2);
                    }

                }
                Bitmap bit = new Bitmap((int)(width), (int)(height));
                Rectangle r = new Rectangle(point.X, point.Y, (int)(image.Width * setRate), (int)(image.Height * setRate));

                Graphics g = Graphics.FromImage(bit);
                g.Clear(Color.White);
                g.DrawImage(image, r);


                MemoryStream ms = new MemoryStream();
                bit.Save(ms, ImageFormat.Jpeg);
                byte[] bytes = ms.ToArray();
                string fileName = name + imageSize.ToString();//为缩放图像重新命名
                using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                bit.Dispose();
                ms.Dispose();
                image.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 图像的等比例缩放,默认文件名不改变，会将原文件覆盖
        /// </summary>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        bool CreateThumbnail(HttpPostedFile file, ImageSize imageSize, string name)
        {
            return CreateThumbnail(file, imageSize, true, name);
        }
        #endregion

        public string Upload(HttpContext context, UpLoadType type, bool isScale)
        {

            ImageSize imageSize = _fileUploadSize.ImageSizeForType[type];

            HttpFileCollection files = context.Request.Files;

            if (files.Count == 0)
            {
                throw new ArgumentNullException("please choose file for upload.");
            }

            string path = "/upload/" + type.ToString();//相对路径

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // 只取第 1 个文件
            var file = files[0];

            if (file != null && file.ContentLength > 0)
            {

                try
                {
                    string filename = context.Request.Form["fileName"].Split('.')[0]
                       + "_"
                       + DateTime.Now.ToString("yyyyMMddhhssmm")
                       + imageSize.ToString();

                    // 本地文件系统路径
                    string savePath = Path.Combine(context.Server.MapPath(path), filename);
                    file.SaveAs(savePath);
                    if (isScale)
                        CreateThumbnail(file, imageSize, savePath);

                    //返回URI路径
                    string ImageUri = FileHostUri
                        + path
                        + "/"
                        + filename;

                    return "1|" + ImageUri;
                }
                catch (Exception ex)
                {

                    return "0|" + ex.Message;
                }

            }
            return null;
        }

    }
}