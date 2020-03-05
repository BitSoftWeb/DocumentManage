//<%@ WebHandler Language="C#" Class="ftpDownload" %>

using Common;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FineUIPro.EmptyProjectNet40.FTP
{
    /// <summary>
    /// ftpDownload 的摘要说明
    /// </summary>
    public class ftpDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string ftpURI = context.Request.QueryString["ftpURI"];//ftpURI
            string fileName = context.Request.QueryString["fileName"];//fileName
            string localPath = "C:\\Users\\Administrator\\Desktop\\";//文件下载路径(可自定义)
            string errorMsg = "";
            bool b = FtpWeb.Download(ftpURI, localPath, fileName, out errorMsg);
            if (b)
            {
                context.Response.Write("ok");
            }
            else
            {
                context.Response.Write(errorMsg);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}