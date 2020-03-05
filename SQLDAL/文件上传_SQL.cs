using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;








namespace SQLDAL
{
    public class 文件上传_SQL
    {

        public DataSet 文件上传功能(string fileName, string newfile, string houzhuiming)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("INSERT INTO 上传文件表");
            sb.Append("(文件名,新文件名,文件类型");
            sb.Append(")VALUES(");
            sb.Append("'" + fileName + "','" + newfile + "','" + houzhuiming + "')");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }
        public DataSet 上传文件查询()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from 上传文件表");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }

        public DataSet 删除上传数据(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  DELETE FROM 上传文件表   ");
            sb.Append("   where 新文件名= '" + fileName + "' ");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            //int num = Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, sb.ToString()));
            //return num;
        }
    }
}
