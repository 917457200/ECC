using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PublicLib;
using ThoughtWorks.QRCode.Codec;

namespace EastElite.DCS.Ajax
{
    public class QRCodeAPI : ServiceBase
    {
        public void _DoProcess(HttpContext context)
        {

            try
            {
                if (CheckLongin(context))
                {
                    //记录日志信息

                    logentity.module = "二维码";//模块名称
                    logentity.pageurl = GetQueryUrl();
                    switch (context.Request.QueryString[0])
                    {
                        //创建二维码图片
                        case "05-01":
                            logentity.functionName = "创建二维码图片";
                            CreateCode_Simple(context);
                            break;
                       
                        default:
                            logentity.otype = "1";
                            logentity.logcontent = "没有找到提供的该方法";
                            operatelog.Add(logentity);
                            context.Response.Write("没有找到提供的该方法");
                            break;
                    }
                }
                else
                {
                    logentity.otype = "1";
                    logentity.logcontent = "未登录";
                    operatelog.Add(logentity);
                }
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(ex.Message);
            }
        }
        //生成二维码方法一
        private void CreateCode_Simple(HttpContext context)
        {
            string userCode = context.Request.Form["userCode"].ToString();
            string iscut = context.Request.Form["iscut"].ToString();
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 8;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //System.Drawing.Image image = qrCodeEncoder.Encode("4408810820 深圳－广州 小江");
            string imageEditPage = Helper.GetAppSettings("imageeditpage");
           

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
         string token = Convert.ToInt64(ts.TotalMilliseconds).ToString();
         System.Drawing.Image image = qrCodeEncoder.Encode(string.Format("{0}?user={1}&iscut={2}&token={3}", imageEditPage, userCode,iscut, token));

         string filename = "QR" + token + ".jpg";
            

            //获取配置文件路径
         string strurl = "/uploads/" + userCode + "/mobile" + "/";//拼接上传路径   上传文件的相对路径
            string strpath = context.Server.MapPath(strurl);//上传文件绝对路径  
            if (Directory.Exists(strpath))
            {
                DirectoryInfo di = new DirectoryInfo(strpath);
                di.Delete(true);
            }
            Directory.CreateDirectory(strpath);
            //if (!Directory.Exists(strpath))
            //{
              
            //}
         
      

            string filepath = strpath+"\\" + filename;
            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();

            context.Response.Write(getJson(0,"成功", strurl + filename,token));
        }
        private string getJson(int status, string mes, string url,string token)
        {
            return ("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\",\"url\":\"" + url  + "\",\"token\":\"" + token + "\"}");
        }
      
      
    }
}