using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common;
using Model;

namespace SQLDAL
{
    public class 设备文件_SQL
    {
        public DataSet DataSet资产状态查询()
        {
            
            string sql= "SELECT  * FROM  AC_上传文件表";           
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sql.ToString());

        }
        public DataSet DataSet资产状态查询(string 上传类型,string flowstate, string sSearch)
        {
            StringBuilder sb = new StringBuilder();
            if (上传类型== "设备台账类型")
            {
                sb.Append("SELECT  A.*,b.* FROM  AC_上传文件表 as A,AC_设备类型一级表 as B where A.文件类型=B.类型 ");

                if (flowstate == "全部")
                {
                    sb.Append("");
                }
                else if (flowstate == "" || flowstate == null)
                {
                    sb.Append("");
                }
                else
                {
                    sb.Append(" and A.文件类型 ='" + flowstate + "'");
                }

            }
            else if (上传类型 == "设备文件类型")
            {
                sb.Append("SELECT  A.*,b.* FROM  AC_上传文件表 as A,dbo.AC_设备文件类型表 as B where A.文件类型=B.设备文件类型  ");
              
                
                if (flowstate == "全部")
                {
                    sb.Append("");
                }
                else if(flowstate==""|| flowstate==null)
                {
                    sb.Append("");
                }else
                {
                    sb.Append(" and A.文件类型 ='" + flowstate + "'");                    
                }
            }
            else if(上传类型 == "全部")
            {
                sb.Append("  select * from AC_上传文件表   ");

              
            }

                     
            //if (flowstate!="全部")
            //{
            //    sb.Append(" and 文件类型 ='"+ flowstate + "'");
            //}
            //else
            //{
            //    sb.Append(" ");
            //}
            
            if (sSearch == "")
            {
                sb.Append("");
            }
            else
            {
                if (上传类型 == "全部")
                {
                    sb.Append(" where 1=1 and (原文件名 like '%" + sSearch + "%' or 设备编号 like '%" + sSearch + "%') ");
                    //"and (设备编号 like '%"+sSearch + "%' or A.设备名称 like '%" + sSearch + "%') "
                }
                else
                {
                    sb.Append(" and A.原文件名 like '%" + sSearch + "%'");
                }
                
                
            }
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());

        }

        public string 用户权限(string username)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  SELECT A.*,B.名称 FROM  用户表 as A,用户读写权限表 as B  ");
            //  SELECT A.*,B.名称 FROM  用户表 as A,用户读写权限表 as B  where A.权限=B.ID and A.用户名= 'admin'
            sb.Append(" where A.权限=B.ID ");
            sb.Append(" and  A.用户名='" + username+"' ");
            string permissions = "";
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            while (read.Read())
            {
                permissions = read["名称"].ToString();
            }
            read.Close();
            return permissions;
        }


    }
}
