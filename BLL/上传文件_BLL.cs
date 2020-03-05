using Model;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class 上传文件_BLL
    {
        上传文件_SQL sql = new 上传文件_SQL();
        public DataSet 上传文件(string fileName, string newFileName, string extension, string nowTIme)
        {
            return sql.上传文件(fileName, newFileName, extension, nowTIme);
        }

        public DataSet 上传文件查询()
        {
            return sql.上传文件查询();
        }

        public int 图片上传(设备图片上传表 model)
        {
            return sql.图片上传(model);
        }
    }
}
