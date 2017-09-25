using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Dal
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public partial class dalDeviceClassInfo
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn = 0;


        public int UpdateDeviceSN( out int errorcode, out string errormsg, string classCode, string deviceSN, string roomNum, string IPAddress, int IPPort, string modifiedID, string modifiedName, int isCheckInt, int isCheckRoomNum, string JPushID, int deviceTypeID, string version )
        {
            errorcode = 0;
            errormsg = "";
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@Errorcode", SqlDbType.Int),
				new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
                new SqlParameter("@RoomNum", roomNum),
                new SqlParameter("@IPAddress", IPAddress),
                new SqlParameter("@IPPort", IPPort),
				new SqlParameter("@ClassCode", classCode),
                new SqlParameter("@DeviceSN", deviceSN),
				new SqlParameter("@ModifiedID", modifiedID),
                new SqlParameter("@ModifiedName", modifiedName),
                new SqlParameter("@DeviceTypeID", deviceTypeID),
                      new SqlParameter("@JPushID", JPushID),
                         new SqlParameter("@IsCheckInt", isCheckInt),
                      new SqlParameter("@IsCheckRoomNum", isCheckRoomNum),
                       new SqlParameter("@version", version)
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;

            intReturn = DBHelper.ExecuteNonQuery( "dbo.spDeviceClassInfoInit", CommandType.StoredProcedure, sqlParameters );
            if( intReturn == 0 )
            {

                errorcode = Convert.ToInt32( sqlParameters[0].Value );
                errormsg = sqlParameters[1].Value.ToString();
            }

            return intReturn;
        }

        public int UpdateDeviceClass( out int errorcode, out string errormsg, string ID, string BanZhuRenPhotoPath, string BanZhuRenQRPath, string RoomNum, string ClassNickName, string ClassSlogan, string ZuoYouMing, string Introduction, string Recommended, string ClassLogoPath, string ClassQRPath, string ModefiedID, string ModifiedName )
        {
            errorcode = 0;
            errormsg = "";
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@Errorcode", SqlDbType.Int),
				new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
                  new SqlParameter("@ID", ID),
                  new SqlParameter("@BanZhuRenPhotoPath", BanZhuRenPhotoPath),
                    new SqlParameter("@BanZhuRenQRPath", BanZhuRenQRPath),
                      new SqlParameter("@RoomNum", RoomNum),
                        new SqlParameter("@ClassNickName", ClassNickName),
                          new SqlParameter("@ClassSlogan", ClassSlogan),
                            new SqlParameter("@ZuoYouMing", ZuoYouMing),
                              new SqlParameter("@Introduction", Introduction),
                                new SqlParameter("@Recommended", Recommended),
                      new SqlParameter("@ClassLogoPath", ClassLogoPath),
                new SqlParameter("@ClassQRPath", ClassQRPath),
              
             new SqlParameter("@ModifiedID", ModefiedID),
                new SqlParameter("@ModifiedName", ModifiedName)
            
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;

            intReturn = DBHelper.ExecuteNonQuery( "dbo.spDeviceClassInfoUpdate", CommandType.StoredProcedure, sqlParameters );
            if( intReturn == 0 )
            {

                errorcode = Convert.ToInt32( sqlParameters[0].Value );
                errormsg = sqlParameters[1].Value.ToString();
            }

            return intReturn;
        }

        public int RepeatDeviceClass( out int errorcode, out string errormsg, string JPushID, string ClassCode, string OldClassName, string ClassName, string ClassFullCode, string BanZhuRenPhotoPath, string BanZhuRenQRPath, string ClassNickName, string ClassSlogan, string ZuoYouMing, string Introduction, string Recommended, string ClassLogoPath, string ClassQRPath, int SubjectTypeID, string SubjectTypeIDText, int ClassTypeID, string ClassTypeIDText, string SemesterName, string BanZhuRenCode, string BanZhuRenName, string ModefiedID, string ModifiedName, string RoomNum )
        {
            errorcode = 0;
            errormsg = "";
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@Errorcode", SqlDbType.Int),
				new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
                  new SqlParameter("@JPushID", JPushID),
                  new SqlParameter("@ClassCode", ClassCode),
                  new SqlParameter("@OldClassName", OldClassName),
                   new SqlParameter("@ClassName", ClassName),
                  new SqlParameter("@ClassFullCode", ClassFullCode),
                   new SqlParameter("@BanZhuRenPhotoPath", BanZhuRenPhotoPath),
                  new SqlParameter("@BanZhuRenQRPath", BanZhuRenQRPath),
                   new SqlParameter("@ClassNickName", ClassNickName),
                  new SqlParameter("@ClassSlogan", ClassSlogan),
                   new SqlParameter("@ZuoYouMing", ZuoYouMing),
                  new SqlParameter("@Introduction", Introduction),
                   new SqlParameter("@Recommended", Recommended),
                  new SqlParameter("@ClassLogoPath", ClassLogoPath),
                new SqlParameter("@ClassQRPath", ClassQRPath),
                new SqlParameter("@SubjectTypeID", SubjectTypeID),
                new SqlParameter("@SubjectTypeIDText", SubjectTypeIDText),
                new SqlParameter("@ClassTypeID", ClassTypeID),
                new SqlParameter("@ClassTypeIDText", ClassTypeIDText),
                new SqlParameter("@SemesterName", SemesterName),
                new SqlParameter("@BanZhuRenCode", BanZhuRenCode),
                new SqlParameter("@BanZhuRenName", BanZhuRenName),
                new SqlParameter("@ModifiedID", ModefiedID),
                new SqlParameter("@ModifiedName", ModifiedName),
                new SqlParameter("@RoomNum", RoomNum)

            
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;

            intReturn = DBHelper.ExecuteNonQuery( "dbo.spRepeatDeviceClassInfo", CommandType.StoredProcedure, sqlParameters );
            if( intReturn == 0 )
            {

                errorcode = Convert.ToInt32( sqlParameters[0].Value );
                errormsg = sqlParameters[1].Value.ToString();
            }

            return intReturn;
        }



        public void RepeatUserClass( string UserCode, int UserType, string ClassCode, string ClassFullCode, string ClassName, int RoleType, int IsValid, string Note, string HandledID, string HandledName, DateTime HandledDate )
        {

            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@UserCode", UserCode),
				new SqlParameter("@UserType", UserType),
                  new SqlParameter("@ClassCode", ClassCode),
                  new SqlParameter("@ClassFullCode", ClassFullCode),
                  new SqlParameter("@ClassName", ClassName),
                   new SqlParameter("@RoleType", RoleType),
                  new SqlParameter("@IsValid", IsValid),
                   new SqlParameter("@Note", Note),
                  new SqlParameter("@HandledID", HandledID),
                   new SqlParameter("@HandledName", HandledName),
                  new SqlParameter("@HandledDate", HandledDate)
             };
            intReturn = DBHelper.ExecuteNonQuery( "dbo.spRepeatUserClassInfo", CommandType.StoredProcedure, sqlParameters );
        }

    }
}