using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace PublicLib
{
    /// <summary>
    /// 文件处理类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool ThumbnailCallback()
        {
            return false;
        }
        public static void MakeThumbnail(System.Drawing.Image ig, string newPath, int width, int height)
        {
            //System.Drawing.Image ig = System.Drawing.Image.FromFile(sourcePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = ig.Width;
            int oh = ig.Height;
            if ((double)ig.Width / (double)ig.Height > (double)towidth / (double)toheight)
            {
                oh = ig.Height;
                ow = ig.Height * towidth / toheight;
                y = 0;
                x = (ig.Width - ow) / 2;

            }
            else
            {
                ow = ig.Width;
                oh = ig.Width * height / towidth;
                x = 0;
                y = (ig.Height - oh) / 2;
            }
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(ig, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);
            try
            {
                bitmap.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ig.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }
        /// <summary>
        /// 生成缩略图重载方法1，返回缩略图的Image对象
        /// </summary>
        /// <param name="ReducedImage">Image资源</param>
        /// <param name="Width">缩略图的宽度</param>
        /// <param name="Height">缩略图的高度</param>
        /// <returns>缩略图的Image对象</returns>
        public static System.Drawing.Image GetThumbnailImageByImage(System.Drawing.Image ReducedImage, int Width, int Height)
        {
            if (ReducedImage == null)
            {
                return ReducedImage;
            }
            try
            {
                System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                ReducedImage = ReducedImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
                return ReducedImage;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
                return null;
            }
        }

        /// <summary>
        /// 图片裁剪
        /// </summary>
        /// <param name="img">图片源</param>
        /// <param name="cutX"></param>
        /// <param name="cutY"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static System.Drawing.Image CutImage(System.Drawing.Image img, int cutX, int cutY, out string status)
        {
            status = "0";//成功
            if (img == null)
            {
                status = "1";//源图像为空
                return img;
            }
            int imgX = img.Width;
            int imgY = img.Height;

            if (imgX < cutX || imgY < cutY)
            {
                status = "2";//源图像尺寸太小
                return img;
            }
            double per = cutX * 1.0 / cutY;
            int newX;
            int newY;
            if (imgX > imgY)
            {
                newY = imgY;
                newX = (int)(imgY * per);
            }
            else
            {
                per = cutY * 1.0 / cutX;
                newX = imgX;
                newY = (int)(imgX * per);
            }

            Bitmap bitmap = new Bitmap(img);//原图

            Bitmap destBitmap = new Bitmap(newX, newY, PixelFormat.Format24bppRgb);//目标图
            destBitmap.SetResolution(72, 72);
            Rectangle destRect = new Rectangle(0, 0, cutX, cutY);//矩形容器
            Rectangle srcRect = new Rectangle(0, 0, newX, newY);

            Graphics g = Graphics.FromImage(destBitmap);
            //设置质量 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);

            //保存缩略图
            //destBitmap.Save("d:\\123-1.jpg");
            System.Drawing.Image timg = GetThumbnailImageByImage(destBitmap, cutX, cutY);
            //timg.Save("d:\\123.jpg");
            return timg;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fUpload">上传控件</param>
        /// <returns>返回文件保存的路径</returns>
        public static string UploadFileInfo(FileUpload fUpload)
        {
            string FullPath = string.Empty;
            
            //保存图片
            if (fUpload != null && fUpload.FileName.Length > 0)
            {
                string fileExtension = Path.GetExtension(fUpload.FileName).ToLower();
                string ApplicationFilePath = string.Empty;
                FullPath = GetFileFullPath(fileExtension, out ApplicationFilePath);
                string FileName = FileUploadToServer(fUpload, "uploadfiles\\" + DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid().ToString());
                FullPath = "/uploadfiles/" + DateTime.Now.ToString("yyyyMMdd")+"/" + FileName;
            }
            return FullPath;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileExtension">扩展名</param>
        /// <param name="FileString">文件信息</param>
        /// <returns>返回文件保存的路径</returns>
        public static string FileUploadToServer(string fileExtension, string FileString)
        {
            string path = (HttpContext.Current.Request.PhysicalApplicationPath + "uploadfiles").ToLower();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += "\\" + Guid.NewGuid().ToString() + fileExtension;
            FileStream fsFile = File.Create(path);
            try
            {
                byte[] buf = Helper.Base64StringToByte(FileString);//把字符串读到字节数组中
                if (buf.Length > 0)
                {
                    fsFile.Write(buf, 0, buf.Length);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            finally
            {
                fsFile.Close();
            }
            return path;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fUpload"></param>
        /// <param name="PathName"></param>
        /// <param name="Filename"></param>
        /// <returns></returns>
        private static string FileUploadToServer(FileUpload fUpload, string PathName, string Filename)
        {
            if (fUpload == null)
                return string.Empty;
            string path = (HttpContext.Current.Request.PhysicalApplicationPath + PathName).ToLower();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Filename.ToLower();
            if (fUpload.HasFile)
            {
                bool fileOK = false;
                String fileExtension = Path.GetExtension(fUpload.FileName).ToLower();
                fileName += fileExtension;
                String[] allowedExtensions = { ".gif", ".png", ".bmp", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
                if (!fileOK)
                    return string.Empty;
                System.Web.HttpPostedFile postedFile = fUpload.PostedFile;//上传控件名FiPc
                int fileLength = postedFile.ContentLength;
                byte[] buff = new byte[fileLength];
                System.IO.Stream filestream = postedFile.InputStream;//建立文件流对象
                filestream.Read(buff, 0, fileLength);//读取流内容			
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                path += "\\" + fileName;
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Write(buff, 0, buff.Length);
                fs.Close();
                return fileName;
            }
            return string.Empty;
        }

        /// <summary>
        /// 取得虚拟路径的物理地址
        /// </summary>
        /// <param name="strPath">虚拟路径</param>
        /// <returns>返回：物理路径</returns>
        public static string GetPhysicalPath(string strPath)
        {
            strPath = strPath.Replace("/", "\\");
            string fulPath = string.Empty;
            if (HttpContext.Current != null)
            {
                fulPath = HttpContext.Current.Request.PhysicalApplicationPath + "\\" + strPath;
            }
            else
            {
                fulPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "\\" + strPath;
            }
            return fulPath + "\\";
        }

        /// <summary>
        /// 获取文件物理
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private static string GetFileFullPath(string fileExtension, out string ApplicationFilePath)
        {
            string d = DateTime.Now.ToString("yyyyMMdd");
            string filename = Guid.NewGuid().ToString() + fileExtension;
            ApplicationFilePath = "uploadfiles/" + d + "/" + filename; ;
            string path = GetPhysicalPath("uploadfiles") + d;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += "\\" + filename;

            return path;
        }

        public static string SaveImage(System.Drawing.Image img, string PathName, string Filename)
        {
            string path = (HttpContext.Current.Request.PhysicalApplicationPath + PathName).ToLower();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = PathName.TrimEnd('/')+"/"+Filename.ToLower();
            if (img!=null)
            {
                try
                {
                    img.Save(path + Filename);
                    return fileName;
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorMessage(ex);
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}