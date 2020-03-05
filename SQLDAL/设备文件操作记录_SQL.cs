using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Model;

namespace SQLDAL
{
    public class 设备文件操作记录_SQL
    {
        public DataSet 查询设备文件操作记录()
        {
            string sql = "SELECT *  FROM AC_设备文件操作记录表  order by 操作时间 ASC ";
         
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sql.ToString());    
        }

        public DataSet 插入设备文件操作记录(HHHH_设备文件操作记录表 model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into AC_设备文件操作记录表  ");
            sb.Append(" (用户名,操作类型,文件名,操作时间,设备编号");
            sb.Append(" ) VALUES ( ");
            sb.Append("@用户名,@操作类型,@文件名,@操作时间,@设备编号)");
            sb.Append(" SELECT ");
            sb.Append(" @@identity");
            SqlParameter[] para ={
                                     new SqlParameter("@用户名",model.用户名),
                                     new SqlParameter("@操作类型",model.操作类型),
                                     new SqlParameter("@文件名",model.文件名),
                                     new SqlParameter("@操作时间",model.操作时间),
                                     new SqlParameter("@设备编号",model.设备编号),
            };
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString(), para);
        }

        public DataSet 文件操作记录条件查询(string startTime, string endTime,string search)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * from AC_设备文件操作记录表 ");
            sb.Append(" where 1=1 ");
            if (startTime=="")
            {
                sb.Append(" ");
            }
            else
            {
                startTime = startTime + " 00:00";
                sb.Append(" and 操作时间 >='"+startTime+"' ");
            }

            if (endTime == "")
            {
                sb.Append(" ");
            }
            else
            {
                endTime = endTime + " 23:59";
                sb.Append(" and 操作时间 <='" + endTime + "' ");
            }
            sb.Append("and (设备编号 like '%" + search + "%' or 文件名 like '%" + search + "%') ");
            sb.Append(" order by 操作时间 ASC ");

            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }

    }
}
