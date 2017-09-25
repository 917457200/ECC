using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUC.Common.Model
{
    public class DeviceTaskNoticeInfo
    {

        private string _noticecreatetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private string _noticetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

        public string noticecreatetime
        {
            get { return _noticecreatetime; }
            set { _noticecreatetime = value; }
        }
        public string noticeTime
        {
            get { return _noticetime; }
            set { _noticetime = value; }
        }
        public string noticeTitle { get; set; }
        public string noticeContent { get; set; }
        public string taskCode { get; set; }
        public long noticeId { get; set; }


    }
}
