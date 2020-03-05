using SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Model;

namespace BLL
{
    public class 设备文件操作记录BLL
    {
        设备文件操作记录_SQL sql = new 设备文件操作记录_SQL();
        public DataSet 查询设备文件操作记录()
        {
            return sql.查询设备文件操作记录();
        }


        public DataSet 插入设备文件操作记录(HHHH_设备文件操作记录表 model)
        {
            return sql.插入设备文件操作记录(model);
        }


        public DataSet 文件操作记录条件查询(string startTime, string endTime, string search)
        {
            return sql.文件操作记录条件查询(startTime,endTime,search);
        }
    }
}
