using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Aspose.Cells;
using EDUC.Common.Bll;
using EDUC.Common.Model;
using PublicLib;

namespace EastElite.ECC
{
    public class ExamtionAPI : ServiceBase
    {
        DataTable dt = new DataTable();
        #region (public) 基本请求入口 DoProcess
        public void _DoProcess(HttpContext context)
        {
            try
            {
                if (CheckLongin(context))
                {
                    //记录日志信息

                    logentity.module = "考试模块";//模块名称
                    logentity.pageurl = GetQueryUrl();
                    switch (context.Request.QueryString[0])
                    {
                        //导入信息
                        case "05-01":
                            logentity.functionName = "导入考试信息";
                            //operatelog.Add(logentity);
                            ExamImport(context);
                            break;
                        //查询考试信息(电子班牌)
                        case "05-02":
                            logentity.functionName = "查询考试信息";
                            //operatelog.Add(logentity);
                            GetExamInfoByClassCode(context);
                            break;
                        //删除考试信息
                        case "05-03":
                            logentity.functionName = "删除考试信息";
                            //operatelog.Add(logentity);
                            DelExamInfo(context);
                            break;
                        //清空考试信息
                        case "05-04":
                            logentity.functionName = "清空考试信息";
                            //operatelog.Add(logentity);
                            EmptyExamInfo(context);
                            break;
                        //获取年级信息
                        case "05-05":
                            logentity.functionName = "获取年级信息";
                            //operatelog.Add(logentity);
                            GetExamGrade(context);
                            break;
                        //获取班级信息
                        case "05-06":
                            logentity.functionName = "获取班级信息";
                            //operatelog.Add(logentity);
                            GetExamClass(context);
                            break;
                        case "05-07":
                            logentity.functionName = "查询考试信息";
                            //operatelog.Add(logentity);
                            GetExamInfo(context);
                            break;
                        default:
                            logentity.otype = "1";
                            logentity.logcontent = "没有找到提供的该方法"; ;
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
        #endregion

        private void ExamImport(HttpContext context)
        {
            //必填参数
            List<string> param = new List<string>() { "pageSize", "currentPage", "url", "campus","campusCode","isDel" };
            //必填参数检查
            if (!CheckParameters(param))
            {
                return;
            }
            string campus = context.Request.Form["campus"].ToString();
            string campusCode = context.Request.Form["campusCode"].ToString();
            string isDel = context.Request.Form["isDel"].ToString();
            int pageSize = Helper.StringToInt(context.Request.Form["pageSize"].ToString());
            int currentPage = Helper.StringToInt(context.Request.Form["currentPage"].ToString());

            string path = context.Request.Form["url"].ToString();
            path = context.Server.MapPath(path);
         
            Aspose.Cells.Workbook workbook = new Workbook();
            try
            {
                workbook.Open(path);
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "This Excel files contains (Excel3.0 or earlier file format) records.":
                        //ErrorStr = "您上传的Excel文件包含更早的版本的记录，请重新选择要上传的文件。";
                        break;
                    case "This Excel files contains (Excel4.0 or earlier file format) records.":
                        //ErrorStr = "您上传的Excel文件包含更早的版本的记录，请重新选择要上传的文件。";
                        break;
                    case "This file's format is not supported or you don't specify a correct format.":
                        //ErrorStr = "您上传的文件不是Excel格式，请重新选择要上传的文件。";
                        break;
                }
            }

            Worksheets wsts = workbook.Worksheets;
            DataTable dt = new DataTable();
            try
            {
                if (wsts.Count > 0)
                {
                    Worksheet wst = wsts[0];
                    int MaxR = wst.Cells.MaxRow;
                    int MaxC = wst.Cells.MaxColumn;
                    if (MaxR > 0 && MaxC > 0)
                    {
                        dt = wst.Cells.ExportDataTableAsString(0, 0, MaxR + 1, MaxC + 1, true);

                        string className = "";
                        string visibleTime = "";
                        string hideTime = "";
                        string examName = "";
                        string examRoom = "";
                        string examSubject = "";
                        string examTime = "";
                        string studentNumberRange = "";
                        string studentNumber = "";
                        string teachers = "";
                        string notice = "";
                        string note = "";

                        for (int n = 0; n < dt.Columns.Count; n++)
                        {
                            if (dt.Columns[n].ColumnName.Contains("班级"))
                                className = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("开始时间"))
                                visibleTime = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("关闭时间"))
                                hideTime = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("考场名称"))
                                examName = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("考场地址"))
                                examRoom = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("考试科目"))
                                examSubject = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("考试时间"))
                                examTime = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("学号区间范围"))
                                studentNumberRange = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("人数"))
                                studentNumber = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("监考教师"))
                                teachers = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("通知"))
                                notice = dt.Columns[n].ColumnName;
                            if (dt.Columns[n].ColumnName.Contains("备注"))
                                note = dt.Columns[n].ColumnName;
                        }

                        List<ExaminationInfoEntity> list = new List<ExaminationInfoEntity>();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            #region MyRegion
                             bool ClassIsNull = false;
                            ExaminationInfoEntity entity = new ExaminationInfoEntity();
                            entity.Campus = campusCode;
                            for (int m = 0; m < dt.Columns.Count; m++)
                            {
                                if (!string.IsNullOrWhiteSpace(className))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][className].ToString()))
                                        entity.ClassName = dt.Rows[j][className].ToString();
                                    else
                                    {
                                        ClassIsNull = true;
                                        //context.Response.Write(JsonHelper.ToJsonResult("1", "班级不能为空，请检查数据"));
                                        //return;
                                    }
                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少班级列，请检查数据"));
                                    return;
                                }

                                DateTime vTime = new DateTime(1900, 1, 1);
                                if (!string.IsNullOrWhiteSpace(visibleTime))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][visibleTime].ToString()))
                                    {
                                        if (ClassIsNull)
                                        {
                                            context.Response.Write(JsonHelper.ToJsonResult("1", "班级不能为空，请检查数据"));
                                            return;
                                        }

                                        if (DateTime.TryParse(dt.Rows[j][visibleTime].ToString(), out vTime))
                                        {
                                            entity.VisibleTime = vTime.ToString("yyyy-MM-dd HH:mm:ss");
                                        }
                                        else
                                        {
                                            context.Response.Write(JsonHelper.ToJsonResult("1", string.Format("{0}{1}开始时间格式错误，请检查数据", campus, dt.Rows[j][className].ToString())));
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (ClassIsNull)
                                        {
                                            break;
                                        }
                                        context.Response.Write(JsonHelper.ToJsonResult("1", string.Format("{0}{1}开始时间不能为空，请检查数据", campus, dt.Rows[j][className].ToString())));
                                        return;
                                    }

                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少开始时间列，请检查数据"));
                                    return;
                                }
                                DateTime hTime;

                                if (!string.IsNullOrWhiteSpace(hideTime))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][hideTime].ToString()))
                                    {
                                        if (DateTime.TryParse(dt.Rows[j][hideTime].ToString(), out hTime))
                                        {
                                            entity.HideTime = hTime.ToString("yyyy-MM-dd HH:mm:ss");
                                        }
                                        else
                                        {
                                            context.Response.Write(JsonHelper.ToJsonResult("1", string.Format("{0}{1}关闭时间格式错误，请检查数据", campus, dt.Rows[j][className].ToString())));
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        context.Response.Write(JsonHelper.ToJsonResult("1", string.Format("{0}{1}关闭时间不能为空，请检查数据", campus, dt.Rows[j][className].ToString())));
                                        return;
                                    }
                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少关闭时间列，请检查数据"));
                                    return;
                                }

                                if (!string.IsNullOrWhiteSpace(examName))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][examName].ToString()))
                                    {
                                        entity.ExamName = dt.Rows[j][examName].ToString();
                                    }
                                    else
                                    {
                                        entity.ExamName = "";
                                        //context.Response.Write(JsonHelper.ToJsonResult("1", string.Format("{0}{1}考场名称不能为空，请检查数据", campus, dt.Rows[j][className].ToString())));
                                        //return;
                                    }

                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少考场名称列，请检查数据"));
                                    return;
                                }
                                if (!string.IsNullOrWhiteSpace(examRoom))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][examRoom].ToString()))
                                    {
                                        entity.ExamRoom = dt.Rows[j][examRoom].ToString();
                                    }
                                    else {
                                        entity.ExamRoom = "";
                                    }
                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少考场地址列，请检查数据"));
                                    return;
                                }
                                if (!string.IsNullOrWhiteSpace(examSubject))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][examSubject].ToString()))
                                    {
                                        entity.ExamSubject = dt.Rows[j][examSubject].ToString();
                                    }
                                    else
                                    {
                                        entity.ExamSubject = "";
                                    }

                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少考试科目列，请检查数据"));
                                    return;
                                }
                                if (!string.IsNullOrWhiteSpace(examTime))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][examTime].ToString()))
                                    {
                                        entity.ExamTime = dt.Rows[j][examTime].ToString();
                                    }
                                    else
                                    {
                                        entity.ExamTime = "";
                                    }

                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少考试时间列，请检查数据"));
                                    return;
                                }
                                if (!string.IsNullOrWhiteSpace(studentNumberRange))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][studentNumberRange].ToString()))
                                    {
                                        entity.StudentNumberRange = dt.Rows[j][studentNumberRange].ToString();
                                    }
                                    else
                                    {
                                        entity.StudentNumberRange = "";
                                    }

                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少学号区间范围列，请检查数据"));
                                    return;
                                }
                                int sNumber;
                                if (!string.IsNullOrWhiteSpace(studentNumber))
                                {
                                    if (!string.IsNullOrWhiteSpace(dt.Rows[j][studentNumber].ToString()))
                                    {
                                        if (Int32.TryParse(dt.Rows[j][studentNumber].ToString(), out sNumber))
                                        {
                                            entity.StudentNumber = sNumber;
                                        }
                                        else
                                        {
                                            entity.StudentNumber = 0;
                                        }
                                    }
                                    else
                                    {
                                        entity.StudentNumber = 0;
                                    }

                                }
                                else
                                {
                                    context.Response.Write(JsonHelper.ToJsonResult("1", "缺少人数列，请检查数据"));
                                    return;
                                }
                                if (!string.IsNullOrWhiteSpace(teachers))
                                {
                                    entity.Teachers = dt.Rows[j][teachers].ToString();
                                }
                                else
                                {
                                    entity.Teachers = "";
                                }
                                if (!string.IsNullOrWhiteSpace(notice))
                                {
                                    entity.Notice = dt.Rows[j][notice].ToString();
                                }
                                else
                                {
                                    entity.Notice = "";
                                }

                            }
                            list.Add(entity);
                            #endregion
                        }

                        //创建日志对象
                        operatelogEntity logentity = new operatelogEntity();
                        bllExaminationInfo bll = new bllExaminationInfo();
                        int recordCount = 0;
                        int totalPage = 0;

                        if (list != null && list.Count > 0)
                        {
                            dt = bll.ImportExaminationData(isDel,list, pageSize, currentPage, "", "", out recordCount, out totalPage, logentity);

                            if (dt != null)
                                ReturnListJson(dt, pageSize, recordCount, currentPage, totalPage);
                            else
                            {
                                ReturnListJson("1", "fail");
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "文件打开失败,请下载模板填写数据"));

            }

        }

        private void GetExamInfo(HttpContext context)
        {
            try
            {
                string filter = "IsValid=1";
                string campus = "";
                if (context.Request.Form["campus"] != null)
                {
                    campus = context.Request.Form["campus"].ToString();
                    if (!string.IsNullOrWhiteSpace(campus))
                    {
                        filter += string.Format(" AND substring(classcode,1,10)='{0}'", campus);
                    }
                }
                string gradeCode = "";
                if (context.Request.Form["gradeCode"] != null)
                {
                    gradeCode = context.Request.Form["gradeCode"].ToString();
                    if (!string.IsNullOrWhiteSpace(gradeCode))
                    {
                        filter += string.Format(" AND substring(classcode,11,6)='{0}'", gradeCode);
                    }
                }
                string classCode = "";
                if (context.Request.Form["classCode"] != null)
                {
                    classCode = context.Request.Form["classCode"].ToString();
                    if (!string.IsNullOrWhiteSpace(classCode))
                    {
                        filter += string.Format(" AND classCode='{0}'", classCode);
                    }
                   
                }
                int pageSize = 10;
                if (context.Request.Form["pageSize"] != null)
                {
                    pageSize = Helper.StringToInt(context.Request.Form["pageSize"].ToString());
                }
                int currentPage = 1;
                if (context.Request.Form["currentPage"] != null)
                {
                    currentPage = Helper.StringToInt(context.Request.Form["currentPage"].ToString());
                }
                bllExaminationInfo bll = new bllExaminationInfo();
                int recordCount = 0;
                int pageCount = 0;
                string order = "SUBSTRING(classcode,1,10) asc,SUBSTRING([ClassCode],11,2) DESC,SUBSTRING([ClassCode],13,4) DESC,SUBSTRING([ClassCode],17,2) asc";
                dt = bll.GetPagingListInfo(pageSize, currentPage, filter, order, out recordCount, out pageCount);
                ReturnListJson(dt, pageSize, recordCount, currentPage, pageCount);
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }
        private void GetExamInfoByClassCode(HttpContext context)
        {
            try
            {
                //必填参数
                List<string> param = new List<string>() { "classCode"};
                //必填参数检查
                if (!CheckParameters(param))
                {
                    return;
                }
                string classCode = context.Request.Form["classCode"].ToString();
                bllExaminationInfo bll = new bllExaminationInfo();

                dt = bll.GetExamInfos(classCode);
                ReturnListJson(dt);
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }

        private void DelExamInfo(HttpContext context)
        {
            //必填参数
            List<string> param = new List<string>() { "ids", "isValid" };
            //必填参数检查
            if (!CheckParameters(param))
            {
                return;
            }
            string ids = context.Request.Form["ids"].ToString();
            string isValid = context.Request.Form["isValid"].ToString();

            bllExaminationInfo bll = new bllExaminationInfo();
            bll.DeleteExamInfo(ids, isValid);
            context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
        }

        private void EmptyExamInfo(HttpContext context)
        {
            bllExaminationInfo bll = new bllExaminationInfo();
            bll.EmptyExamInfo();
            context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
        }

        private void GetExamGrade(HttpContext context)
        {   //必填参数
            List<string> param = new List<string>() { "campus" };
            //必填参数检查
            if (!CheckParameters(param))
            {
                return;
            }
            string campus = context.Request.Form["campus"].ToString();

            bllExaminationInfo bll = new bllExaminationInfo();
            dt = bll.GetGradeCode(campus);
            ReturnListJson(dt);

        }
        private void GetExamClass(HttpContext context)
        {
            //必填参数
            List<string> param = new List<string>() { "campus", "gradecode" };
            //必填参数检查
            if (!CheckParameters(param))
            {
                return;
            }
            string campus = context.Request.Form["campus"].ToString();
            string gradecode = context.Request.Form["gradecode"].ToString();
            bllExaminationInfo bll = new bllExaminationInfo();
            dt = bll.GetClassCode(campus, gradecode);
            ReturnListJson(dt);
        }
    }
}