using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SQLDAL
{
    public class 上传文件_SQL
    {
        public DataSet 上传文件(string fileName, string newFileName, string extension, string nowTIme)
        {
           
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("INSERT INTO 上传文件表");
            sb.Append("(文件名,新文件名,扩展名,上传时间");
            sb.Append(")VALUES(");
            //sb.Append("" + deviceType + ",'" + fileName + "','" + newfile + "','" + nowTime + "','" + houzhuiming + "')");
            sb.Append("'" + fileName + "','" + newFileName + "','" + extension + "','" + nowTIme + "')");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }

        public DataSet 上传文件查询()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from 上传文件表");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }



        public int 图片上传(设备图片上传表 model)
        {
            string sql = string.Format("Select * from AC_设备图片上传 where 设备编号='{0}'", model.设备编号);
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            if (read.Read())
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update AC_设备图片上传 set   ");
                    sb.Append("  文件名=@文件名,上传路径=@上传路径,上传时间=@上传时间,文件后缀=@文件后缀,上传状态=@上传状态 ");
                    sb.Append("  where  设备编号=@设备编号  ");
                    SqlParameter[] para ={
                                     
                                     new SqlParameter("@文件名",model.文件名),
                                     new SqlParameter("@上传路径",model.上传路径),
                                     new SqlParameter("@上传时间",model.上传时间),
                                     new SqlParameter("@文件后缀",model.文件后缀),
                                     new SqlParameter("@上传状态",model.上传状态),
                                     new SqlParameter("@设备编号",model.设备编号),
                                 };
                    int num = Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, sb.ToString(), para));
                    
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                    //throw;
                }
            }
            else
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into AC_设备图片上传 ");
                    sb.Append(" (设备编号,文件名,上传路径,上传时间,文件后缀,上传状态");
                    sb.Append(" ) VALUES ( ");
                    sb.Append("@设备编号,@文件名,@上传路径,@上传时间,@文件后缀,@上传状态 )");
                    sb.Append(" SELECT ");
                    sb.Append(" @@identity");
                    SqlParameter[] para ={
                                     new SqlParameter("@设备编号",model.设备编号),
                                     new SqlParameter("@文件名",model.文件名),
                                     new SqlParameter("@上传路径",model.上传路径),
                                     new SqlParameter("@上传时间",model.上传时间),
                                     new SqlParameter("@文件后缀",model.文件后缀),
                                     new SqlParameter("@上传状态",model.上传状态),
                                 };
                    int num = Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, sb.ToString(), para));
                    return 2;
                }
                catch (Exception)
                {
                    return 0;
                    //throw;
                }
            }

        }

    }
}
