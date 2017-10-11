using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using EDUC.Common.Bll;
using EDUC.Common.Model;
using PublicLib;

namespace EastElite.ECC
{
    /// <summary>
    /// AjaxUpload 的摘要说明
    /// </summary>
    public class AjaxUpload : IHttpHandler
    {
        private HttpContext con;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                con = context;
                context.Response.ContentType = "text/html";  //响应数据类型
                string uploadtype=  context.Request.Params["uploadtype"].ToString();
                
                HttpPostedFile file = context.Request.Files[0];   //得到上传文件的对象
                //byte[] buffer = new byte[file.ContentLength];
                //file.InputStream.Read(buffer, 0, file.ContentLength);
             
                string filname = Path.GetFileName(file.FileName);//上传文件名称
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                string filecode= Convert.ToInt64(ts.TotalMilliseconds).ToString()+"01";//生成唯一码
             
                string newfilename = DateTime.Now.ToString("yyyyMMddHHmmss") + filecode + Path.GetExtension(file.FileName);
                dynamic dyn = CreateFolder();//获取配置文件路径

                string path = dyn.path + newfilename;//文件绝对路径
                string url = dyn.url + newfilename;//文件相对路径
                string thumbnailPath = dyn.path + "thumbnail" + newfilename;//缩略图的绝对路径
                string thumbnailUrl = dyn.url + "thumbnail" + newfilename;//缩略图的相对路径
                int index=HttpContext.Current.Request.Url.ToString().IndexOf("AjaxUpload");
                string ip = HttpContext.Current.Request.Url.ToString().Substring(0, index-1);

               
                //保存数据到数据库

                bllDataFieldInfo bll = new bllDataFieldInfo();
                DataFieldInfoEntity entity = new DataFieldInfoEntity();
             
                entity.FieldName1 = filname;//上传文件名称
                entity.FieldContent = path;//保存文件绝对路径
                string errormsg = "";
                int errorcode = 0;
                entity.FieldCode = filecode;
               
                if (uploadtype == "image")
                {
                    Stream stream = file.InputStream;
                    System.Drawing.Image image=null;
                    try
                    {
                        image = System.Drawing.Image.FromStream(stream);
                    }
                    catch {
                        context.Response.Write(getJson(1, "上传文件失败：图片解析错误", "", "", "", new Size(0, 0)));
                        return;
                    }
                    Size size = image.Size;
                    int standardImageWidth = Helper.StringToInt(Helper.GetAppSettings("standardImageWidth"));
                    int standardImageHeight = Helper.StringToInt(Helper.GetAppSettings("standardImageHeight"));
                    int thumbnailImageWidth = Helper.StringToInt(Helper.GetAppSettings("thumbnailImageWidth"));
                    int thumbnailImageHeight = Helper.StringToInt(Helper.GetAppSettings("thumbnailImageHeight"));

                    if (image.Width >= image.Height)
                    {
                        if (image.Width >= standardImageWidth)
                        {
                            size=new Size(standardImageWidth,standardImageWidth * image.Height / image.Width);
                            FileHelper.MakeThumbnail(image, path, size.Width, size.Height);
                        }
                        else {
                            //保存文件
                            file.SaveAs(path);
                        }
                    }
                    else {
                        if (image.Height >= standardImageHeight)
                        {
                            size = new Size(standardImageHeight * image.Width / image.Height, standardImageHeight);
                            FileHelper.MakeThumbnail(image, path, standardImageHeight * image.Width / image.Height, standardImageHeight);
                        }
                        else {
                            //保存文件
                            file.SaveAs(path);
                        }
                    }
                    //if (image.Width > standardImageWidth || image.Height > standardImageHeight)
                    //{

                    //    FileHelper.MakeThumbnail(image, path, image.Width > standardImageWidth ? standardImageWidth : image.Width, image.Height > standardImageHeight ? standardImageHeight : image.Height);
                    //}
                    //else
                    //{
                    //    //保存文件
                    //    file.SaveAs(path);
                    //}
                   
                    entity.FieldTypeID = 1;
                   
                    bll.Add(ref entity, new operatelogEntity(), out errorcode, out errormsg);
                    System.Drawing.Image image1 = System.Drawing.Image.FromStream(stream);
                    FileHelper.MakeThumbnail(image1, thumbnailPath, thumbnailImageWidth, thumbnailImageHeight);
                    context.Response.Write(getJson(0, "上传成功", ip + url, thumbnailUrl, filname, size));
                   
                   
                }
                else if (uploadtype == "video")
                {
                    //保存文件
                    file.SaveAs(path);
                    entity.FieldTypeID = 2;
                  
                    
                    bll.Add(ref entity, new operatelogEntity(), out errorcode, out errormsg);
                    context.Response.Write(getJson(0, "上传成功", ip+url, "", filname, new Size(0, 0)));
                   
                }
                else if (uploadtype == "office")
                {
                    //保存文件
                    file.SaveAs(path);
                    entity.FieldTypeID = 2;


                    bll.Add(ref entity, new operatelogEntity(), out errorcode, out errormsg);
                    context.Response.Write(getJson(0, "上传成功", ip + url, "", filname, new Size(0, 0),url));

                }
            }
            catch
            {
                context.Response.Write(getJson(1, "上传文件失败", "", "","", new Size(0, 0)));
            }
        }
    
        /// <summary>
        /// 返回JSON数据
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="mes">消息内容如</param>
        /// <param name="url">文件相对路径</param>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        private string getJson(int status, string mes, string url, string thumnailUrl, string name, Size size,string url1="")
        {
            return ("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\",\"url\":\"" + url + "\",\"url1\":\"" + url1 + "\",\"thumnailUrl\":\"" + thumnailUrl + "\",\"width\":\"" + size.Width + "\",\"height\":\"" + size.Height + "\",\"name\":\"" + name + "\"}");
        }
        private dynamic CreateFolder()
        {
            
            string url;
            url = "/uploads/" + DateTime.Now.ToString("yyyyMMdd")+"/";//拼接上传路径   上传文件的相对路径
            string path = con.Server.MapPath(url);//上传文件绝对路径
            if (!Directory.Exists(path))
            {
                //若不存在则创建
                Directory.CreateDirectory(path);
            }
           
            return new { url = url, path = path };
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}