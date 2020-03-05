using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SQLDAL;

namespace BLL
{
    public class 设备文件_BLL
    {
        设备文件_SQL sql = new 设备文件_SQL();

        public DataSet DataSet资产状态查询()
        {
            return sql.DataSet资产状态查询();
        }
        public DataSet DataSet资产状态查询(string 上传类型, string flowstate,string sSearch)
        {
            return sql.DataSet资产状态查询( 上传类型, flowstate,sSearch);
        }

        public string 用户权限(string username)
        {
            return sql.用户权限(username);
        }
    }
}
