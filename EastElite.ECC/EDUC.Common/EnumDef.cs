using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUC.Common
{
   public class EnumDef
   {
       #region 模式定义 ModelType

       /// <summary>
        /// 结算方式
        /// </summary>
        [Serializable]
        [Description("模式定义")]
        public enum ModelType
        {
            /// <summary>
            /// 班级模式
            /// </summary>
            [Description("班级模式")]
            Class = 1,

            /// <summary>
            /// 考试模式
            /// </summary>
            [Description("考试模式")]
            Exam = 2,

            /// <summary>
            /// 紧急模式
            /// </summary>
            [Description("紧急模式")]
            Urgent = 3,

            /// <summary>
            /// 全屏图片模式
            /// </summary>
            [Description("全屏图片模式")]
            FullScreenPic = 4,

            /// <summary>
            /// 全屏视频模式
            /// </summary>
            [Description("全屏视频模式")]
            FullScreenVideo = 5,

            /// <summary>
            /// 人物评选模式
            /// </summary>
            [Description("人物评选模式")]
             CharacterSelection = 6,

            /// <summary>
            /// 课堂考勤模式
            /// </summary>
            [Description("课堂考勤模式")]
            ClassroomChecks = 7,
        }

        #endregion
    }
}
