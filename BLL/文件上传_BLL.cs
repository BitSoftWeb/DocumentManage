using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SQLDAL;

namespace BLL
{
    public class 文件上传_BLL
    {
        文件上传_SQL sql = new 文件上传_SQL();
        //fileName,newfile, ftyp
        public DataSet 文件上传功能(string fileName, string newfile, string houzhuiming)
        {
            return sql.文件上传功能(fileName, newfile, houzhuiming);
        }
        public DataSet 上传文件查询()
        {
            return sql.上传文件查询();
        }
        public DataSet 删除上传数据(string fileName)
        {
            return sql.删除上传数据(fileName);
        }
    }
}
