using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.EmptyProjectNet40.FTP
{
    public partial class FTPDownloadCheckInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string paramName = Request.QueryString["新文件名"];
                string[] temp = paramName.Split('.');
                string Name = temp[0];
                string houzhuiming = "." + temp[1];

                if (houzhuiming == ".jpg" || houzhuiming == ".jpeg" || houzhuiming == ".png" || houzhuiming == ".gif" || houzhuiming == ".bmp")
                {
                    //image_1.ImageUrl = @"\FTPcs\" + paramName;
                    //image_1.ImageUrl = "ftp://192.168.1.130:26" + @"\FTPcs\" + paramName;
                    //image_1.ImageUrl = @"ftp://192.168.1.130:26/FTPcs/201912m23d212319a829.jpg";
                    //string localPath = @"\UploadFile\";
                    //string localPath = HttpContext.Current.Server.MapPath(strfile);
                    string localPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    //string localPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//文件下载路径-桌面(可自定义)          
                    //string localPath = "C:\\Users\\Administrator\\Desktop\\";//文件下载路径(可自定义)
                    string errorMsg = "";
                    string ftpURI = "FTPcs/pictures";
                   
                    bool b = Common.FtpWeb.Download(ftpURI, localPath, paramName, out errorMsg);
                    if (b)
                    {
                        image_1.ImageUrl = @"\UploadFile\" + paramName;
                        //Response.Write("ok");
                        //Alert.Show("下载成功！！！");
                    }
                    else
                    {
                        Response.Write(errorMsg);
                    }
                }
                else
                {
                    //string pdfname ="https://view.officeapps.live.com/op/view.aspx?src=" + "ftp://192.168.1.130:26" + @"\FTPcs\" + Name + ".pdf";
                    string pdfname = "https://view.officeapps.live.com/op/view.aspx?src=" + "ftp://192.168.1.130:26" + @"\FTPcs\" + paramName;
                    Response.Redirect(pdfname, false);
                }

            }
        }
    }
}