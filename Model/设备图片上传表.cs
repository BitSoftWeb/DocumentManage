using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class 设备图片上传表
    {
        public int ID { get; set; }
        public string 设备编号 { get; set; }
        public string 文件名 { get; set; }
        public string 上传路径 { get; set; }
        public string 上传时间 { get; set; }
        public string 文件后缀 { get; set; }
        public string 上传状态 { get; set; }
    }
}
