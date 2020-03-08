using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using System.Data;
using Common;
using System.IO;
using System.Net;

namespace FineUIPro.EmptyProjectNet40.设备文件
{
    public partial class 设备文件 : PageBase
    {
        设备台账BLL bll = new 设备台账BLL();
        设备文件_BLL sbwjbll = new 设备文件_BLL();
        HHHH_设备文件操作记录表 modela = new HHHH_设备文件操作记录表();
        设备文件操作记录BLL wjczbll = new 设备文件操作记录BLL();
        string ftpURI = "FTPcs";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<SBZC_台账_设备文件类型表> sblx = bll.查询设备文件类型();
                设备文件类型.DataTextField = "设备文件类型";
                设备文件类型.DataValueField = "ID";
                设备文件类型.DataSource = sblx;
                设备文件类型.DataBind();
                设备文件类型.EmptyText = "全部";

                //GridBind();
                
                List<HHHH一级表> sbtz = bll.查询设备台账类型();
                设备台账类型.DataTextField = "类型";
                设备台账类型.DataValueField = "ID";
                设备台账类型.DataSource = sbtz;
                设备台账类型.DataBind();
                设备台账类型.EmptyText = "全部";

                string username = "admin";
                string permissions= sbwjbll.用户权限(username);
                if (permissions=="只读")
                {
                    download.Hidden = true;
                }
                else if (permissions == "读/写")
                {
                    download.Hidden = false;
                }
                else if (permissions == "管理员")
                {
                    download.Hidden = false;
                }

                DataSet ds = sbwjbll.DataSet资产状态查询();
                DataTable dt = ds.Tables[0].Copy();//复制一份table
                DataTable source = dt;
                // 3.绑定到Grid
                Grid1.DataSource = dt;//DataTable
                Grid1.DataBind();
            }

        }

        //public void GridBind()
        //{
        //    DataSet ds = sbwjbll.DataSet资产状态查询(设备文件类型.SelectedText, tSearch.Text.ToString());
        //    DataTable dt = ds.Tables[0].Copy();//复制一份table
        //    DataTable source = dt;
        //    // 3.绑定到Grid
        //    Grid1.DataSource = dt;//DataTable
        //    Grid1.DataBind();
        //}

        protected void 设备文件类型_SelectedIndexChanged(object sender, EventArgs e)
        {
            string flowstate = 设备文件类型.SelectedText;       
            DataSet ds = sbwjbll.DataSet资产状态查询(上传类型.SelectedText, flowstate, tSearch.Text);
            DataTable dt = ds.Tables[0].Copy();//复制一份table
            DataTable source = dt;
            // 3.绑定到Grid
            Grid1.DataSource = dt;//DataTable
            Grid1.DataBind();

        }

        protected void tSearch_Trigger2Click(object sender, EventArgs e)
        {
            string sSearch = tSearch.Text.ToString();
            string type =""; //上传类型.SelectedText;
            if (上传类型.SelectedText == "设备文件类型")
            {
                type = 设备文件类型.SelectedText;
            }
            else if (上传类型.SelectedText == "设备台账类型")
            {
                type = 设备台账类型.SelectedText;
            }
            else if (上传类型.SelectedText == "全部")
            {
                type = "";
            }


            DataSet ds = sbwjbll.DataSet资产状态查询(上传类型.SelectedText,type, sSearch);
            DataTable dt = ds.Tables[0].Copy();//复制一份table
            DataTable source = dt;
            // 3.绑定到Grid
            Grid1.DataSource = dt;//DataTable
            Grid1.DataBind();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            HTTPHelper.SetCertificatePolicy();
            //HttpWebRequest request = WebRequest.Create("https://114.115.220.70:8011/DocumentManage/"+ keys[3].ToString()) as HttpWebRequest;
            string pathUrl = "https://114.115.220.70:8011/DocumentManage/" + keys[3].ToString();
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            //请求网络路径地址
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(pathUrl);
            request.Timeout = 5000; // 超时时间
                                    //获得请求结果
            response = (System.Net.HttpWebResponse)request.GetResponse();
            //文件下载地址
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//文件下载路径-桌面(可自定义)  
            // 如果不存在就创建file文件夹
            if (!Directory.Exists(path))
            {
                if (path != null) Directory.CreateDirectory(path);
            }
            path = path +"\\"+ keys[3].ToString();
            Stream stream = response.GetResponseStream();
            //先创建文件
            Stream sos = new System.IO.FileStream(path, System.IO.FileMode.Create);
            byte[] img = new byte[1024];
            int total = stream.Read(img, 0, img.Length);
            while (total > 0)
            {
                //之后再输出内容
                sos.Write(img, 0, total);
                total = stream.Read(img, 0, img.Length);
            }
            stream.Close();
            stream.Dispose();
            sos.Close();
            sos.Dispose();

            //modela.用户名 = Request.QueryString["userName"];
            modela.用户名 = "admin";
            modela.操作类型 = "下载";
            modela.文件名 = keys[2].ToString(); ;
            modela.操作时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            modela.设备编号 = keys[1].ToString();
            DataSet dst = wjczbll.插入设备文件操作记录(modela);

            //if(e.CommandName == "downloadDocument")
            //{
            //    string fileName = keys[3].ToString();
            //    string extension = keys[6].ToString();
            //    if (".jpg.jpeg.png.gif.bmp".Contains(extension))
            //    {
            //        ftpURI = "FTPcs/pictures";
            //    }
            //    else if (".doc.docx".Contains(extension))
            //    {
            //        ftpURI = "FTPcs/Word";
            //    }
            //    else if (".xls.xlsx".Contains(extension))
            //    {
            //        ftpURI = "FTPcs/Excel";
            //    }
            //    else if ("..ppt".Contains(extension))
            //    {
            //        ftpURI = "FTPcs/PowerPoint";
            //    }
            //    string localPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//文件下载路径-桌面(可自定义)                                                                                                                //string localPath = "C:\\Users\\Administrator\\Desktop\\";//文件下载路径(可自定义)
            //    string errorMsg = "";
            //    bool b = FtpWeb.Download(ftpURI, localPath, fileName, out errorMsg);
            //    if (b)
            //    {
            //        //Response.Write("ok");
            //        string wen = "ftp://192.168.1.130:26/FTPcs/Word/" + fileName;
            //        PageContext.Redirect(wen);

            //        //modela.用户名= Request.QueryString["userName"];
            //        modela.用户名 = "admin";
            //        modela.操作类型 = "下载";
            //        modela.文件名 = keys[2].ToString(); ;
            //        modela.操作时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //        modela.设备编号 = keys[1].ToString();
            //        DataSet dst = wjczbll.插入设备文件操作记录(modela);
            //        Alert.Show("文件下载成功，请在桌面查看！！！",MessageBoxIcon.Success);
            //    }
            //    else
            //    {
            //        Response.Write(errorMsg);
            //    }


            //    //string wen = @"ftp://192.168.1.130:26/" + ftpURI + "/" + keys[3].ToString();
            //    ////string wen = bll.str(FID);//文件路径
            //    //FileStream fs = new FileStream(wen, FileMode.Open);
            //    //byte[] bytes = new byte[(int)fs.Length];
            //    //fs.Read(bytes, 0, bytes.Length);
            //    //fs.Close();
            //    //Response.ContentType = "application/octet-stream";
            //    //Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            //    //Response.BinaryWrite(bytes);
            //    //Response.Flush();

            //    //if (File.Exists(wen))
            //    //{
            //    //    Response.TransmitFile(wen);
            //    //    Response.End();

            //    //    return;
            //    //}
            //    //string url = ftpURI + "/" + fileName;



            //    //string localPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//文件下载路径-桌面(可自定义)          
            //    ////string filepath = localPath.Replace("Administrator\\Desktop\\", " ");//string localPath = "C:\\Users\\Administrator\\Desktop\\";//文件下载路径(可自定义)
            //    //string errorMsg = "";
            //    //bool b = FtpWeb.Download(ftpURI, localPath, fileName, out errorMsg);
            //    //bool c = FtpWeb.Download(ftpURI, fileName, out errorMsg);
            //    //if (b)
            //    //{
            //    //    //Response.Write("ok");
            //    //    Alert.Show("文件下载成功，请在桌面查看！！！");
            //    //}
            //    //else
            //    //{
            //    //    Response.Write(errorMsg);
            //    //}
            //}

        }

        protected void 上传类型_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = 上传类型.SelectedText;
            if (type == "设备文件类型")
            {
                设备文件类型.Hidden = false;
                设备台账类型.Hidden = true;
                设备台账类型.Reset();
                

            }
            else if (type == "设备台账类型")
            {
                设备文件类型.Hidden = true;
                设备台账类型.Hidden = false;
                设备文件类型.Reset();

                //DataSet ds = sbwjbll.DataSet资产状态查询("", tSearch.Text);
                //DataTable dt = ds.Tables[0].Copy();//复制一份table
                //DataTable source = dt;

                //Grid1.DataSource = dt;//DataTable
                //Grid1.DataBind();
            }
            else if (type == "全部")
            {
                设备台账类型.Hidden = true;
                设备文件类型.Hidden = true;
                设备台账类型.Reset();
                设备文件类型.Reset();
            }
            DataSet ds = sbwjbll.DataSet资产状态查询(type, "", tSearch.Text);
            DataTable dt = ds.Tables[0].Copy();//复制一份table
            DataTable source = dt;

            Grid1.DataSource = dt;//DataTable
            Grid1.DataBind();
        }

        protected void 设备台账类型_SelectedIndexChanged(object sender, EventArgs e)
        {
            string flowstate = 设备台账类型.SelectedText;
            DataSet ds = sbwjbll.DataSet资产状态查询(上传类型.SelectedText,flowstate, tSearch.Text);
            DataTable dt = ds.Tables[0].Copy();//复制一份table
            DataTable source = dt;
            // 3.绑定到Grid
            Grid1.DataSource = dt;//DataTable
            Grid1.DataBind();
        }

       
    }
}