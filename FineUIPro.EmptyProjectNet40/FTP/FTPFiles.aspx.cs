using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace FineUIPro.EmptyProjectNet40.FTP
{
    
    public partial class FTPFiles : PageBase
    {
        上传文件_BLL scwjbll = new 上传文件_BLL();
        string ftpURI = "FTPcs";

        static string strfile = "测试.xlsx";//txt文件名
        string txtPath = HttpContext.Current.Server.MapPath(strfile);//相对路径转绝对路径
        string strout = string.Empty;//txt文件里读出来的内容      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bind();
                
                BindGrid();
            }

        }

        private void BindGrid()
        {
            DataSet ds = scwjbll.上传文件查询();
            System.Data.DataTable dt = ds.Tables[0].Copy();//复制一份table
            
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        
    protected void Button2_Click(object sender, EventArgs e)
    {
        string errorMsg = "";
        string name = upFile.FileName.ToString();
        
        //if (upFile.HasFile)
        if (name!="")
        {
            int fileLength = upFile.PostedFile.ContentLength;//文件大小，单位byte
            string fileName = Path.GetFileName(upFile.PostedFile.FileName);//文件名称
            string newFileName = GetNewFileName(fileName);//新文件名
            string extension = Path.GetExtension(upFile.PostedFile.FileName).ToLower();//文件扩展名
            //限制上传文件最大不能超过500M  
            if (!(fileLength < 512 * 1024 * 1024))
            {
                //Response.Write("<script>alert('文件最大不能超过500M！');</script>");
                Alert.Show("文件最大不能超过500M！");
                upFile.Reset();
                return;
            }
            //限制文件格式houzhuiming == ".jpg" || houzhuiming == ".jpeg" || houzhuiming == ".png" || houzhuiming == ".gif" || houzhuiming == ".bmp"
            if (!".doc.docx.xls.xlsx.ppt.pdf.txt.jpg.jpeg.png.gif.bmp".Contains(extension))
            {
                //Response.Write("<script>alert('不支持的文件格式！');</script>");
                Alert.Show("不支持的文件格式！");
                upFile.Reset();
                return;
            }

                //文件名
                fileName = this.upFile.FileName;
                //文件路径
                string path = "/uploadFile";
                //本地路径
                string localPath = Server.MapPath(Request.ApplicationPath + path);
                //全路径
                string fullPath = localPath + "\\" + fileName;
                string fullPathNew = localPath + "\\" + newFileName;
                this.upFile.SaveAs(fullPath);
                this.upFile.SaveAs(fullPathNew);
                ////创建文件夹
                //string ftpURI = DateTime.Now.ToString("yyyyMMdd");//以日期作为文件夹名称
                //try
                //{
                //    FtpWeb.CreateDirectory(ftpURI);//创建文件夹
                //}
                //catch { }
                ////服务器存放路径
                string office = @"\UploadFile\" + newFileName ;
                string[] temp = newFileName.Split('.');
                string Name = temp[0];
                //FTP路径
                string pdfname = @"\PDF\" + Name + ".pdf";
                //string pdfname= "ftp://192.168.1.171:26/"+ftpURI+"/"+ Name+".pdf";

                if (extension == ".docx" || extension == ".doc")
                {
                    Common.OfficeOfPDF.DOCConvertToPDF(MapPath(office), MapPath(pdfname));
                }
                else if (extension == ".xlsx" || extension == ".xls")
                {
                    Common.OfficeOfPDF.XLSConvertToPDF(MapPath(office), MapPath(pdfname));
                }
                else if (extension == ".ppt" || extension == ".pptx")
                {
                    Common.OfficeOfPDF.PPTConvertToPDF(MapPath(office), MapPath(pdfname));
                }



                //准备上传文件
                Stream fileStream = null;
                try
                {
                    if (".jpg.jpeg.png.gif.bmp".Contains(extension))
                    {
                        ftpURI = "FTPcs/pictures";
                    }
                    fileStream = upFile.PostedFile.InputStream;//读取本地文件流

                    var b = FtpWeb.Upload(ftpURI, newFileName, fileLength, fileStream, out errorMsg);//开始上传
                    if (b == true)
                    {
                        DataSet ds = scwjbll.上传文件(fileName, newFileName, extension, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        Alert.Show("无法连接到远程服务器！！！");
                    }
                    if (b)
                    {
                        if (File.Exists(txtPath))
                        {
                            FileStream myStream = new FileStream(txtPath, FileMode.Append, FileAccess.Write);// FileMode.Append,追加一行数据
                            StreamWriter sw = new StreamWriter(myStream);
                            sw.WriteLine(ftpURI + "|" + fileName + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ";");//写入文件
                            sw.Close();

                        }

                        BindGrid();
                        upFile.Reset();
                        //Response.Write("<script>alert('上传成功！');</script>");
                        Alert.Show("上传成功！");
                    }
                    else
                    {
                        //Response.Write("<script>alert('上传失败！" + errorMsg + "');</script>");
                        Alert.Show("上传失败！");
                    }
                }
                catch (Exception ex)
                {
                    //Response.Write("<script>alert('上传失败！" + ex.ToString() + "');</script>");
                    Alert.Show("上传失败！");
                }
                finally
                {
                    if (fileStream != null) fileStream.Close();
                }
            }
        else
        {
            upFile.Reset();
            Alert.Show("请选择一个文件再上传！");
            //Response.Write("<script>alert('请选择一个文件再上传！');</script>");
        }
    }

    public string GetNewFileName(string FileName)
    {
        //获取随机文件名称
        Random rand = new Random();
        string newfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "m" +
         DateTime.Now.Day.ToString() + "d"
        + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString()
        + DateTime.Now.Millisecond.ToString()
            + "a" + rand.Next(1000).ToString()
        + FileName.Substring(FileName.LastIndexOf("."), FileName.Length - FileName.LastIndexOf("."));
        return newfilename;
    }

    public class Info
    {
        public int fileNo { get; set; }
        public string ftpURI { get; set; }
        public string fileName { get; set; }
        public DateTime datetime { get; set; }
    }

    protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
    {
        object[] keys = Grid1.DataKeys[e.RowIndex];
        if(e.CommandName=="download"){
            Response.ContentType = "text/plain";
            //string ftpURI = Request.QueryString["ftpURI"];//ftpURI
            string fileName = keys[2].ToString();
            //if (".jpg.jpeg".Contains(keys[3].ToString()))
            //{
            //    ftpURI = "FTPcs/pictures";
            //}
            //FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            //System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            //Response.Clear();
            //Response.AddHeader("content-disposition","attachment;filename="+Server.UrlEncode(fileInfo.Name.ToString()));
            //Response.AddHeader("content-length",fileInfo.Length.ToString());
            //Response.ContentType = "application/octet-stream";
            //Response.ContentEncoding = System.Text.Encoding.Default;
            //Response.WriteFile(fileName); 

            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//文件下载路径-桌面(可自定义)          
            //string localPath = "C:\\Users\\Administrator\\Desktop\\";//文件下载路径(可自定义)
            string errorMsg = "";
            bool b = FtpWeb.Download(ftpURI, localPath, fileName, out errorMsg);
            if (b)
            {
                //Response.Write("ok");
                Alert.Show("下载成功！！！");
            }
            else
            {
                Response.Write(errorMsg);
            }
        }
    }
    protected string GetEditUrl(object 新文件名)
    {

        JsObjectBuilder joBuilder = new JsObjectBuilder();
        joBuilder.AddProperty("id", "grid_newtab_edit_" + ID);
        joBuilder.AddProperty("title", "查看详情 - 新文件名 ");
        joBuilder.AddProperty("iframeUrl", ResolveUrl(String.Format("./FTPDownloadCheckInformation.aspx?新文件名={0}", 新文件名)));
        joBuilder.AddProperty("refreshWhenExist", true);
        //joBuilder.AddProperty("iconFont", "pencil");

        // addExampleTab函数定义在default.aspx
        return String.Format("parent.addExampleTab({0});", joBuilder);
    }
    }
}