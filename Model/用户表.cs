using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class 用户表
    {
        public int ID { get; set; }

        public string 用户名 { get; set; }

        public string 密码 { get; set; }

        public int 权限 { get; set; }

        public string 姓名 { get; set; }

        public int 部门ID { get; set; }

        public string 电话号码 { get; set; }

        public string 身份证号 { get; set; }

        public int 学校ID { get; set; }

        public string 部门 { get; set; }

        public string 学校名称 { get; set; }
    }
}
