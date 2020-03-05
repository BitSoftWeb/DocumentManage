using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.EmptyProjectNet40.设备台账
{
    public partial class 设备台账 : PageBase
    {
        string ftpURI = "FTPcs";
        设备台账BLL bll = new 设备台账BLL();
        设备图片上传表 model = new 设备图片上传表();
        HHHH_设备文件操作记录表 modela = new HHHH_设备文件操作记录表();
        设备文件操作记录BLL wjczbll = new 设备文件操作记录BLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//是否是客户端回发而加载
            {
                //上传设备文件类型查询
                List<SBZC_台账_设备文件类型表> sblx = bll.查询设备文件类型();
                设备文件类型.DataTextField = "设备文件类型";
                设备文件类型.DataValueField = "ID";
                DropDownList3.DataTextField = "设备文件类型";
                DropDownList3.DataValueField = "ID";
                设备文件类型.DataSource = sblx;
                DropDownList3.DataSource = sblx;
                设备文件类型.DataBind();
                DropDownList3.DataBind();

                //树结构 一级菜单
                List<HHHH一级表> sbtz = bll.查询设备台账类型();
                设备台账类型.DataTextField = "类型";
                设备台账类型.DataValueField = "ID";
                DropDownList2.DataTextField = "类型";
                DropDownList2.DataValueField = "ID";
                设备台账类型.DataSource = sbtz;
                DropDownList2.DataSource = sbtz;
                设备台账类型.DataBind();
                DropDownList2.DataBind();

                //绑定树
                LoadDatas();
            }
        }
        public void LoadDatas()
        {
            List<HHHH一级表> listone = bll.一级结构();
            List<HHHH二级表> listtwo = bll.查询二级类型所有();
            foreach (HHHH一级表 row in listone)
            {
                if (row.ID > 0)
                {
                    TreeNode node = new TreeNode();
                    node.IconUrl = @"~/res/icon/asterisk_orange.png";
                    node.Text = row.类型;
                    node.NodeID = row.ID + "-一级";
                    Tree1.Nodes.Add(node);
                    node.EnableClickEvent = true;
                    foreach (HHHH二级表 rowtwo in listtwo)
                    {
                        if (rowtwo.ID > 0)
                        {
                            if (row.ID == rowtwo.SID)
                            {
                                TreeNode nodetwo = new TreeNode();
                                nodetwo.IconUrl = @"~/res/icon/asterisk_red.png";
                                nodetwo.Text = rowtwo.二级类型;
                                nodetwo.EnableClickEvent = true;
                                nodetwo.NodeID = rowtwo.ID + "-二级";
                                node.Nodes.Add(nodetwo);
                                continue;
                            }
                        }
                    }
                }
            }
        }
        #region
        //public void LoadDatas()
        //{
        //    List<HHHH一级表> listone = bll.一级结构();
        //    foreach (HHHH一级表 row in listone)
        //    {
        //        if (row.ID > 0)
        //        {
        //            TreeNode node = new TreeNode();
        //            node.IconUrl = @"~/res/icon/asterisk_orange.png";
        //            node.Text = row.类型;
        //            node.NodeID = row.ID + "-一级";
        //            Tree1.Nodes.Add(node);
        //            node.EnableClickEvent = true;
        //            ResolveSubTwo(row, node);
        //        }
        //    }
        //}

        //private void ResolveSubTwo(HHHH一级表 Row, TreeNode treeNode)
        //{
        //    if (Row.ID > 0)
        //    {
        //        // 如果是目录，则默认展开
        //        //treeNode.Expanded = true;
        //        //treeNode.NodeID = "123";

        //        //public List<HHHH二级表> 查询二级类型结构(int 一级ID)
        //        List<HHHH二级表> listyh = bll.查询二级类型结构(Row.ID);
        //        int xx = 0;
        //        foreach (HHHH二级表 row in listyh)
        //        {
        //            xx++;
        //            if (row.SID > 0)
        //            {
        //                TreeNode node = new TreeNode();
        //                node.IconUrl = @"~/res/icon/asterisk_yellow.png";
        //                node.Text = row.二级类型;
        //                node.EnableClickEvent = true;
        //                //node.NodeID = ;
        //                node.NodeID = row.SID + "-二级";
        //                treeNode.Nodes.Add(node);
        //                //ResolveSubTree(row, node);
        //            }
        //        }
        //    }
        //}
        #endregion

        //树结构点击事件
        protected void Tree1_NodeCommand(object sender, TreeCommandEventArgs e)
        {

            //labResult.Text = "<span style='color:blue;font-weight:bold;'><H2>" + "设备总数:5000" + "</H2></span>";

            string nodeid = e.Node.NodeID;
            //Label1.Text = nodeid;

            string rank = "";//级别
            int ID = 0;
            if (nodeid.Length > 0)
            {
                string[] sArray = nodeid.Split('-');
                ID = Convert.ToInt32(sArray[0]);
                rank = sArray[1].ToString();
                // 3.绑定到Grid
                //Grid1.DataSource = bll.查询树结构设备台账(ID, rank);
                储存ID.Text = Convert.ToString(ID.ToString());
                储存rank.Text = rank;
                                     
                Grid1.DataSource = bll.查询树结构设备类型台账(ID, rank, ttbSearch.Text.ToString());
                Grid1.DataBind();
                
                
                //Grid1.DataBind();
            }

        }

        //上传图片功能
        protected void Button2_Click(object sender, EventArgs e)
        {
            string errorMsg = "";

            //string name = upFile.FileName.ToString();
            string fileName = upFile.ShortFileName;
            string oldName = fileName;
            fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
            fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
            string url = ResolveUrl(imgPhoto.ImageUrl);
            int star = url.LastIndexOf("/");
            string name = url.Substring(star + 1);//文件名
            //if (upFile.HasFile)
            //判断是否还有文件
            if (name != "")
            {
                int fileLength = upFile.PostedFile.ContentLength;//文件大小，单位byte
                //string fileName = Path.GetFileName(upFile.PostedFile.FileName);//文件名称
                //string newFileName = GetNewFileName(fileName);//新文件名
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

                //文件路径
                string path = "/uploadFile";
                //本地路径
                string localPath = Server.MapPath(Request.ApplicationPath + path);
                //全路径
                string fullPath = localPath + "\\" + name;
                //string fullPathNew = localPath + "\\" + name;
                this.upFile.SaveAs(fullPath);
                //this.upFile.SaveAs(fullPathNew);

                //准备上传文件
                Stream fileStream = null;
                try
                {
                    if (".jpg.jpeg.png.gif.bmp".Contains(extension))
                    {
                        ftpURI = "FTPcs/pictures";//给定上传图片路径
                    }
                    fileStream = upFile.PostedFile.InputStream;//读取本地文件流
                    string newfile = fileName;

                    var b = FtpWeb.Upload(ftpURI, newfile, fileLength, fileStream, out errorMsg);//开始上传
                    if (b == true)
                    {
                        string newrul = fileName;//文件名
                        //DateTime dt = DateTime.Now;
                        //string Y = dt.Year.ToString();
                        //string M = dt.Month.ToString();
                        //string D = dt.Day.ToString();
                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//时间
                        
                        int di = url.LastIndexOf(".");

                        string didi = url.Substring(di + 1);//扩展文件名

                        string zt = "已上传";//状态
                        //设备图片上传表 model = new 设备图片上传表();
                        //model.上传路径 = url;
                        model.上传路径 = "/UploadFile/" + fileName;
                        model.上传时间 = time;
                        model.文件名 = newrul;
                        model.文件后缀 = didi;
                        model.上传状态 = zt;
                        model.设备编号 = Grid1.SelectedRow.Values[4].ToString();
                        上传文件_BLL bll = new 上传文件_BLL();
                        int xx = bll.图片上传(model);
                        if (xx >= 1)
                        {                       
                            if (xx == 1)
                            {

                                string record = "修改";
                                //modela.用户名= Request.QueryString["userName"];
                                modela.用户名 = "admin";
                                modela.操作类型 = record;
                                modela.文件名 = oldName;
                                modela.操作时间 = time;
                                modela.设备编号= Grid1.SelectedRow.Values[4].ToString();
                                DataSet ds= wjczbll.插入设备文件操作记录(modela);
                            }
                            else if (xx == 2)
                            {
                                string record = "上传";
                                //modela.用户名 = Request.QueryString["userName"];
                                modela.用户名 = "admin";
                                modela.操作类型 = record;
                                modela.文件名 = oldName;
                                modela.操作时间 = time;
                                modela.设备编号 = Grid1.SelectedRow.Values[4].ToString();
                                DataSet dsc = wjczbll.插入设备文件操作记录(modela);
                            }                          
                            window3.Hidden = true;
                            upFile.Reset();
                            Alert.Show("上传成功!", MessageBoxIcon.Success);
                            imgPhoto.ImageUrl = "/res/images/blank.png";
                        }
                        else
                        {
                            Alert.Show("上传失败!", MessageBoxIcon.Warning);
                            imgPhoto.ImageUrl = "/res/images/blank.png";
                        }
                    }
                    else
                    {
                        Alert.Show("无法连接到远程服务器！！！",MessageBoxIcon.Warning);
                    }
                    #region
                    //if (b)
                    //{
                    //    if (File.Exists(txtPath))
                    //    {
                    //        FileStream myStream = new FileStream(txtPath, FileMode.Append, FileAccess.Write);// FileMode.Append,追加一行数据
                    //        StreamWriter sw = new StreamWriter(myStream);
                    //        sw.WriteLine(ftpURI + "|" + name + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ";");//写入文件
                    //        sw.Close();

                    //    }

                    //    upFile.Reset();
                    //    Alert.Show("上传成功！");
                    //}
                    //else
                    //{                       
                    //    Alert.Show("上传失败！");
                    //}
                    #endregion
                }
                catch (Exception ex)
                {
                    Alert.Show("上传失败！",MessageBoxIcon.Warning);
                    imgPhoto.ImageUrl = "/res/images/blank.png";
                }
                finally
                {
                    if (fileStream != null) fileStream.Close();
                }
            }
            else
            {
                upFile.Reset();
                Alert.Show("请选择一个文件再上传！",MessageBoxIcon.Warning);
            }
        }

        //获取随机文件名
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

        //选择上传图片
        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {

            if (upFile.HasFile)
            {
                string fileName = upFile.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    // 清空文件上传控件
                    //upFile.Reset();

                    FineUIPro.Alert.Show("无效的文件类型!",MessageBoxIcon.Warning);

                    return;
                }


                //fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                upFile.SaveAs(Server.MapPath("~/UploadFile/" + fileName));//保存到给定路径

                imgPhoto.ImageUrl = "~/UploadFile/" + fileName;
                imgPhoto.Hidden = false;

                // 清空文件上传组件（上传后要记着清空，否则点击提交表单时会再次上传！！）

            }
        }

        //判断文件类型是否符合
        protected bool ValidateFileType(string newfile)
        {

            string houzhuiming = Path.GetExtension(upFile.PostedFile.FileName).ToLower();


            if (houzhuiming == ".jpg" || houzhuiming == ".jpeg" || houzhuiming == ".png" || houzhuiming == ".gif" || houzhuiming == ".bmp")
            {
                if (AddWord(newfile) > 0)
                {

                    return true;
                }
                else
                {
                    Alert.Show("上传失败!", MessageBoxIcon.Warning);
                    return false;
                }

            }
            else
            {
                Alert.Show("无效的文件类型!", MessageBoxIcon.Warning);
                return false;
            }

        }


        private int AddWord(string newfile)
        {
            string name = upFile.FileName.ToString();
            if (name != "")
            {
                //文件名
                string fileName = this.upFile.FileName;
                //文件路径
                string path = "/UploadFile";
                //本地路径
                string localPath = Server.MapPath(Request.ApplicationPath + path);
                //全路径
                string fullPath = localPath + "\\" + fileName;
                this.upFile.SaveAs(fullPath);

            }


            string strFileFullName = System.IO.Path.GetFileName(this.upFile.PostedFile.FileName);
            if (strFileFullName.Length > 0)
            {
                if (name != "")
                {
                    //新文件名
                    //string newFileName = newfile;
                    string newFileName = GetNewFileName(strFileFullName);
                    //路径
                    string path = Server.MapPath(@"~/UploadFile" + "/" + newFileName);
                    //保存路径
                    string pathSaveImg = Server.MapPath(@"~/UploadFile" + "/" + newFileName);

                    if (Directory.Exists(Server.MapPath(@"~/UploadFile")))
                    {
                        model.文件名 = newFileName;
                        Directory.CreateDirectory(Server.MapPath(@"~/UploadFile"));
                    }
                    this.upFile.SaveAs(path);
                    //model.DocumentAddress = @"Documents" + "/" + newFileName;
                }
                else
                {
                    Alert.Show("找不到此文档!", MessageBoxIcon.Warning);

                }
                return 1;
            }
            else
            {
                return 0;
            }

        }

        protected void 设备文件类型_SelectedIndexChanged(object sender, EventArgs e)
        {
            string 文件类型 = 设备文件类型.SelectedText;
        }

        //上传文件
        protected void Button3_Click(object sender, EventArgs e)
        {
            string errorMsg = "";

            string name = uploadDocument.FileName.ToString();
            //string type = 设备文件类型.SelectedText;
            if (设备文件类型.SelectedText == null && 设备台账类型.SelectedText==null)
            {
                Alert.Show("请选择设备文件类型");
            }
            else
            {
                window2.Hidden = true;
                //if (upFile.HasFile)
                if (name != "")
                {
                    int fileLength = uploadDocument.PostedFile.ContentLength;//文件大小，单位byte
                    string fileName = Path.GetFileName(uploadDocument.PostedFile.FileName);//文件名称
                    string newFileName = GetNewFileName(fileName);//新文件名
                    string extension = Path.GetExtension(uploadDocument.PostedFile.FileName).ToLower();//文件扩展名
                                                                                                       //限制上传文件最大不能超过500M  
                    if (!(fileLength < 512 * 1024 * 1024))
                    {
                        //Response.Write("<script>alert('文件最大不能超过500M！');</script>");
                        Alert.Show("文件最大不能超过500M！",MessageBoxIcon.Warning);
                        uploadDocument.Reset();
                        return;
                    }
                    //限制文件格式houzhuiming == ".jpg" || houzhuiming == ".jpeg" || houzhuiming == ".png" || houzhuiming == ".gif" || houzhuiming == ".bmp"
                    if (!".doc.docx.xls.xlsx.ppt.pdf.txt.jpg.jpeg.png.gif.bmp".Contains(extension))
                    {
                        //Response.Write("<script>alert('不支持的文件格式！');</script>");
                        Alert.Show("不支持的文件格式！", MessageBoxIcon.Warning);
                        uploadDocument.Reset();
                        return;
                    }

                    //获取设备编号
                    string equipmentNumber = "";
                    //int aa= Convert.ToInt32(Grid2.SelectedRowID.ToString());
                    int[] setion = Grid1.SelectedRowIndexArray;
                    foreach (int item in setion)
                    {
                        equipmentNumber = Convert.ToString(Grid1.DataKeys[item][3]);
                        //string sblx = Grid1.DataKeys[item][1].ToString();
                    }
                    

                    string num = bll.判断上传文件(equipmentNumber);
                    
                    if (num != "")
                    {
                        //Alert.Show("此设备已上传过文件，是否替换此文件？",MessageBoxIcon.Question);
                        window5.Hidden = false;
                    }
                    else
                    {
                        //文件名
                        fileName = this.uploadDocument.FileName;
                        //文件路径
                        string path = "/uploadFile";
                        //本地路径
                        string localPath = Server.MapPath(Request.ApplicationPath + path);
                        //全路径
                        string fullPath = localPath + "\\" + fileName;
                        string fullPathNew = localPath + "\\" + newFileName;
                        //this.uploadDocument.SaveAs(fullPath);

                        //this.uploadDocument.SaveAs(fullPathNew);
                        string upFilePath = path + "/" + newFileName;

                        //准备上传文件
                        Stream fileStream = null;
                        try
                        {
                            if (".jpg.jpeg.png.gif.bmp".Contains(extension))
                            {
                                ftpURI = "FTPcs/pictures";
                            }
                            else if (".doc.docx".Contains(extension))
                            {
                                ftpURI = "FTPcs/Word";
                            }
                            else if (".xls.xlsx".Contains(extension))
                            {
                                ftpURI = "FTPcs/Excel";
                            }
                            else if ("..ppt".Contains(extension))
                            {
                                ftpURI = "FTPcs/PowerPoint";
                            }

                            fileStream = uploadDocument.PostedFile.InputStream;//读取本地文件流

                            var b = FtpWeb.Upload(ftpURI, newFileName, fileLength, fileStream, out errorMsg);//开始上传
                            if (b == true)
                            {
                                if (上传类型.SelectedText == "设备台账类型")
                                {
                                    DataSet dsa = bll.上传文件(equipmentNumber, fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, 设备台账类型.SelectedText);


                                }
                                else if (上传类型.SelectedText == "设备文件类型")
                                {
                                    DataSet ds = bll.上传文件(equipmentNumber, fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, 设备文件类型.SelectedText);


                                }

                                //modela.用户名= Request.QueryString["userName"];
                                modela.用户名 = "admin";
                                modela.操作类型 = "上传";
                                modela.文件名 = fileName;
                                modela.操作时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                modela.设备编号 = equipmentNumber;
                                DataSet dst = wjczbll.插入设备文件操作记录(modela);


                                uploadDocument.Reset();
                                设备台账类型.Hidden = true;
                                设备文件类型.Hidden = true;
                                设备文件类型.Reset();
                                设备台账类型.Reset();
                                window2.Hidden = true;
                                Alert.Show("文件上传成功");
                            }
                            else
                            {
                                Alert.Show("无法连接到FTP远程服务器！！！", MessageBoxIcon.Warning);
                            }

                        }
                        catch (Exception ex)
                        {
                            //Response.Write("<script>alert('上传失败！" + ex.ToString() + "');</script>");
                            Alert.Show("上传失败！", MessageBoxIcon.Warning);
                        }
                        finally
                        {
                            if (fileStream != null) fileStream.Close();
                        }
                    }
                }
                else
                {
                    uploadDocument.Reset();
                    Alert.Show("请选择一个文件再上传！", MessageBoxIcon.Warning);
                    //Response.Write("<script>alert('请选择一个文件再上传！');</script>");
                }
            }

        }

        protected bool ValidateFileType(string fType, string newfile)
        {

            string houzhuiming = Path.GetExtension(uploadDocument.PostedFile.FileName).ToLower();


            if (fType == "application/msword" || fType == "application/pdf" || houzhuiming == ".doc" || houzhuiming == ".pdf" || houzhuiming == ".ppt" || houzhuiming == ".pptx" || houzhuiming == ".wps" || houzhuiming == ".et" || houzhuiming
                 == ".dps" || houzhuiming == ".docx")
            {
                if (AddWord(newfile) > 0)
                {
                    FineUIPro.Alert.Show("文件上传成功!", MessageBoxIcon.Success);
                }
                return true;
            }
            else if (fType == "application/vnd.ms-excel" || fType == "application/pdf" || houzhuiming == ".xlsx" || houzhuiming == ".pdf" || houzhuiming == ".ppt" || houzhuiming == ".pptx" || houzhuiming == ".wps" || houzhuiming == ".et" || houzhuiming
                 == ".dps" || houzhuiming == ".xls" || houzhuiming == ".csv")
            {
                if (AddWord(newfile) > 0)
                {
                    FineUIPro.Alert.Show("文件上传成功!", MessageBoxIcon.Success);
                }
                return true;
            }
            else if (houzhuiming == ".jpg" || houzhuiming == ".jpeg" || houzhuiming == ".png" || houzhuiming == ".gif" || houzhuiming == ".bmp")
            {
                if (AddWord(newfile) > 0)
                {
                    FineUIPro.Alert.Show("文件上传成功!", MessageBoxIcon.Success);
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        protected void window2_Close(object sender, WindowCloseEventArgs e)
        {
            上传类型.Reset();
            设备台账类型.Reset();
            设备文件类型.Reset();
            uploadDocument.Reset();


        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            if (e.CommandName == "SelectPictures")
            {
                string EquipmentNum = keys[3].ToString();
                //string photo = bll.查询设备照片(EquipmentNum);
                string photo = bll.查询设备照片(EquipmentNum);
                string localPath = System.AppDomain.CurrentDomain.BaseDirectory + "UploadFile\\";//文件下载路径(可自定义)                
                string errorMsg = "";
                string ftpURI = "FTPcs/pictures";
                string paramName = photo;

                if (photo == "")
                {
                    Alert.Show("未上传照片,请先上传照片！！！");
                }
                else
                {
                    window1.Hidden = false;

                    bool b = Common.FtpWeb.Download(ftpURI, localPath, paramName, out errorMsg);
                    if (b)
                    {
                        image.ImageUrl = @"\UploadFile\" + paramName;

                    }
                    //image.ImageUrl = @"\UploadFile\" + photo;
                }

            }
            else if (e.CommandName == "UploadFiles")
            {
                window2.Hidden = false;
            }
            else if (e.CommandName == "UpPictures")
            {
                window3.Hidden = false;
                imgPhoto.ImageUrl = "/res/images/blank.png";

            }
        }

        //模糊查询
        protected void ttbSearch_Trigger2Click(object sender, EventArgs e)
        {
            string sSearch = ttbSearch.Text.ToString();
            int ID = Convert.ToInt32(储存ID.Text);
            string rank =储存rank.Text;
            Grid1.DataSource = bll.查询树结构设备类型台账(ID, rank, sSearch);
            Grid1.DataBind();
        }


        protected void 上传类型_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = 上传类型.SelectedText;
            if (type== "设备文件类型")
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
            }
            else if (type == "全部")
            {
                设备台账类型.Hidden = true;
                设备文件类型.Hidden = true;
                设备台账类型.Reset();
                设备文件类型.Reset();
            }
        }

        protected void 设备台账类型_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            上传类型.Reset();
            设备台账类型.Reset();
            设备文件类型.Reset();
            uploadDocument.Reset();
            window2.Hidden = true;
        }

        protected void multiSelect_Click(object sender, EventArgs e)
        {
            Grid1.EnableCheckBoxSelect = true;
            multiSelectPpload.Hidden = false;
            uploadfileInternet.Hidden = true;
            cancelMultiSelect.Hidden = false;
            uppictures.Hidden = true;
            selectpictures.Hidden = true;
        }

        //多选提交按钮
        protected void multiSelectPpload_Click(object sender, EventArgs e)
        {
            
            List<int> intlist = new List<int>();
            int[] selections = Grid1.SelectedRowIndexArray;

            if (selections.Length == 0)
            {
                Alert.Show("您未选择数据,请选择！");
            }
            else
            {
                //int sID = 0;
                //int aa= Convert.ToInt32(Grid2.SelectedRowID.ToString());
                int[] setion = Grid1.SelectedRowIndexArray;
                List<string> a = new List<string>();
                //foreach (int item in setion)
                //{
                //    sID = Convert.ToInt32(Grid1.DataKeys[item][0]);
                //    string sblx = Grid1.DataKeys[item][3].ToString();
                //    a.Add(sID.ToString());
                //}   
                foreach (int rowIndex in selections)
                {
                    int ID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                    intlist.Add(ID);
                }
                List<HHHH设备_设备信息表> listdata = bll.多选上传文件查询(intlist);
                Grid2.DataSource = listdata;//DataTable
                Grid2.DataBind();
                window4.Hidden = false;
                
                //List<HHHH设备_设备信息表> listdata = bll.资产申报确定设备(intlist, 类别.SelectedValue);
            }
        }
        //取消多选
        protected void cancelMultiSelect_Click(object sender, EventArgs e)
        {
            Grid1.EnableCheckBoxSelect = false;
            multiSelectPpload.Hidden = true;
            uploadfileInternet.Hidden = false;
            cancelMultiSelect.Hidden = true;
            uppictures.Hidden = false;
            selectpictures.Hidden = false;
        }

        //多选数据展示窗口关闭
        protected void window4_Close(object sender, WindowCloseEventArgs e)
        {
            window4.Hidden = true;
        }
        //多选数据展示窗口关闭
        protected void Button6_Click(object sender, EventArgs e)
        {
            window4.Hidden = true;
        }

        //多选确认上传
        protected void Button5_Click(object sender, EventArgs e)
        {
            List<int> intlist = new List<int>();
            int[] selections = Grid1.SelectedRowIndexArray;

            int sID = 0;
            //int aa= Convert.ToInt32(Grid2.SelectedRowID.ToString());
            int[] setion = Grid1.SelectedRowIndexArray;
            List<string> array = new List<string>();
            
            StringBuilder ssb = new StringBuilder();
            foreach (int item in setion)
            {                
                sID = Convert.ToInt32(Grid1.DataKeys[item][0]);
                string sblx = Grid1.DataKeys[item][3].ToString();
                array.Add(sID.ToString());
                ssb.Append(sblx+ "," );
            }
            
            string errorMsg = "";

            string name = uploadDocument2.FileName.ToString();
            if (DropDownList3.SelectedText == null && DropDownList2.SelectedText == null)
            {
                Alert.Show("请选择设备文件类型", MessageBoxIcon.Warning);
            }
            else
            {
                if (name != "")
                {
                    int fileLength = uploadDocument2.PostedFile.ContentLength;//文件大小，单位byte
                    string fileName = Path.GetFileName(uploadDocument2.PostedFile.FileName);//文件名称
                    string newFileName = GetNewFileName(fileName);//新文件名
                    string extension = Path.GetExtension(uploadDocument2.PostedFile.FileName).ToLower();//文件扩展名                                                                                                     
                    if (!(fileLength < 512 * 1024 * 1024))//限制上传文件最大不能超过500M  
                    {
                        //Response.Write("<script>alert('文件最大不能超过500M！');</script>");
                        Alert.Show("文件最大不能超过500M！");
                        uploadDocument2.Reset();
                        return;
                    }
                    //限制文件格式houzhuiming == ".jpg" || houzhuiming == ".jpeg" || houzhuiming == ".png" || houzhuiming == ".gif" || houzhuiming == ".bmp"
                    if (!".doc.docx.xls.xlsx.ppt.pdf.txt.jpg.jpeg.png.gif.bmp".Contains(extension))
                    {
                        //Response.Write("<script>alert('不支持的文件格式！');</script>");
                        Alert.Show("不支持的文件格式！");
                        uploadDocument.Reset();
                        return;
                    }

                    StringBuilder sbder = new StringBuilder();
                    for (int i=0;i< array.ToArray().Length;i++)
                    {
                        sbder.Append(array[i]+",");
                    }
                    //ID集参数
                    string IDset = sbder.ToString();
                    string contract = fileName;
                    int num = bll.判断上传合同文件(contract);
                    if (num==1)
                    {
                        Alert.Show("此文件已上传过");
                    }
                    else
                    {
                        //文件名
                        fileName = this.uploadDocument2.FileName;
                        //文件路径
                        string path = "/uploadFile";
                        //本地路径
                        string localPath = Server.MapPath(Request.ApplicationPath + path);
                        //全路径
                        string fullPath = localPath + "\\" + fileName;
                        string fullPathNew = localPath + "\\" + newFileName;
                        //this.uploadDocument2.SaveAs(fullPath);

                        //this.upFile.SaveAs(fullPathNew);
                        string upFilePath = path + "/" + newFileName;


                        //准备上传文件
                        Stream fileStream = null;
                        try
                        {
                            if (".jpg.jpeg.png.gif.bmp".Contains(extension))
                            {
                                ftpURI = "FTPcs/pictures";
                            }
                            else if (".doc.docx".Contains(extension))
                            {
                                ftpURI = "FTPcs/Word";
                            }
                            else if (".xls.xlsx".Contains(extension))
                            {
                                ftpURI = "FTPcs/Excel";
                            }
                            else if ("..ppt".Contains(extension))
                            {
                                ftpURI = "FTPcs/PowerPoint";
                            }

                            fileStream = uploadDocument2.PostedFile.InputStream;//读取本地文件流

                            var b = FtpWeb.Upload(ftpURI, newFileName, fileLength, fileStream, out errorMsg);//开始上传
                            if (b == true)
                            {
                                if (DropDownList1.SelectedText == "设备台账类型")
                                {
                                    DataSet dsa = bll.上传合同文件(fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, DropDownList2.SelectedText, ssb.ToString(), IDset);
                                }
                                else if (DropDownList1.SelectedText == "设备文件类型")
                                {
                                    if (DropDownList3.SelectedText == "设备说明书")
                                    {
                                        //for (int i=0;i<arraySerialNumber.l;)
                                        //{

                                        //}
                                        DataSet dsa = bll.上传合同文件(fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, DropDownList3.SelectedText, ssb.ToString(),IDset);
                                    }
                                    else
                                    {
                                        DataSet dsa = bll.上传合同文件(fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, DropDownList3.SelectedText,ssb.ToString(),IDset);
                                    }

                                }
                                //modela.用户名= Request.QueryString["userName"];
                                modela.用户名 = "admin";
                                modela.操作类型 = "上传";
                                modela.文件名 = fileName;
                                modela.操作时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                modela.设备编号 = ssb.ToString();
                                DataSet dst = wjczbll.插入设备文件操作记录(modela);

                                DropDownList1.Reset();
                                uploadDocument2.Reset();
                                DropDownList2.Reset();
                                DropDownList3.Reset();
                                window4.Hidden = true;
                                Alert.Show("文件上传成功", MessageBoxIcon.Success);

                            }
                            else
                            {
                                Alert.Show("无法连接到远程服务器！！！", MessageBoxIcon.Warning);
                            }

                        }
                        catch (Exception ex)
                        {
                            //Response.Write("<script>alert('上传失败！" + ex.ToString() + "');</script>");
                            Alert.Show("上传失败！", MessageBoxIcon.Warning);
                        }
                        finally
                        {
                            if (fileStream != null) fileStream.Close();
                        }
                    }

                }
                else
                {
                    uploadDocument2.Reset();
                    Alert.Show("请选择一个文件再上传！");
                    //Response.Write("<script>alert('请选择一个文件再上传！');</script>");
                }
            }


            


            #region
            //foreach (int rowIndex in selections)
            //{

            //}




            //List<int> intlist = new List<int>();
            //int[] selections = Grid2.SelectedRowIndexArray;
            //if (selections.Length == 0)
            //{
            //    Alert.Show("请选择");
            //}
            //else
            //{
            //    Console.Write(selections);
            //    foreach (int rowIndex in selections)
            //    {
            //        int ID = Convert.ToInt32(Grid2.DataKeys[rowIndex][0]);
            //        intlist.Add(ID);
            //    }
            //    List<办公设备信息表> listdata = bll.资产申报确定设备(intlist, 类别.SelectedValue);
            //    string flowstate = 类别.SelectedValue;
            //    //int 总数 = 0;
            //    //int 总价 = 0;
            //    float 总数 = 0.0f;
            //    float 总价 = 0.0f;
            //    if (listdata != null)
            //    {
            //        foreach (办公设备信息表 itemjj in listdata)
            //        {
            //            总数 += itemjj.数量;
            //            总价 += Convert.ToInt32(itemjj.价格);
            //        }
            //    }
            //    Grid3.DataSource = listdata;//DataTable
            //    Grid3.DataBind();

            //    Grid4.DataSource = listdata;//DataTable
            //    Grid4.DataBind();

            //    //Grid7.DataSource = listdata;//DataTable
            //    //Grid7.DataBind();
            //    //Grid8.DataSource = listdata;//DataTable
            //    //Grid8.DataBind();
            //    JObject summary = new JObject();
            //    //summary.Add("major", "全部合计");
            //    summary.Add("数量", 总数.ToString("F2"));
            //    summary.Add("价格", 总价.ToString("F2"));

            //    Grid3.SummaryData = summary;
            //    Grid4.SummaryData = summary;
            //    //Grid7.SummaryData = summary;
            //    //Grid8.SummaryData = summary;
            //    //待报废Grid3
            //    流程状态.Text = "待审核";
            //    流程状态.Enabled = true;
            //    申报单位.Text = HttpContext.Current.Session["stname"].ToString();
            //    申报单位.Enabled = true;
            //    申报日期.Text = DateTime.Now.ToShortDateString();
            //    单据编号.Text = "ZCSB001";
            //    单据编号.Enabled = true;
            //    申请人.Text = HttpContext.Current.Session["f_user_name"].ToString();
            //    申请人.Enabled = true;
            //    //待报废明细表Grid4
            //    明细表流程状态.Text = "待审核";
            //    明细表流程状态.Enabled = true;
            //    明细表申报单位.Text = HttpContext.Current.Session["stname"].ToString();
            //    明细表申报单位.Enabled = true;
            //    明细表申报日期.Text = DateTime.Now.ToShortDateString();
            //    明细表单据编号.Text = "ZCSB001";
            //    明细表单据编号.Enabled = true;
            //    明细表申请人.Text = HttpContext.Current.Session["f_user_name"].ToString();
            //    明细表申请人.Enabled = true;
            //}
            #endregion
        }

        //多选窗口类型选择
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = DropDownList1.SelectedText;
            if (type == "设备文件类型")
            {
                DropDownList3.Hidden = false;
                DropDownList2.Hidden = true;
                DropDownList2.Reset();
            }
            else if (type == "设备台账类型")
            {
                DropDownList3.Hidden = true;
                DropDownList2.Hidden = false;
                DropDownList3.Reset();
                //DataSet listdata = bll.多选上传文件查询(intlist, 类别.SelectedValue);
                //Grid2.DataSource = listdata;//DataTable
                //Grid2.DataBind();
            }
            else if (type == "全部")
            {
                DropDownList2.Hidden = true;
                DropDownList3.Hidden = true;
                DropDownList2.Reset();
                DropDownList3.Reset();
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            uploadDocument.Reset();
            设备文件类型.Reset();
            window2.Hidden = true;
            window5.Hidden = true;
        }

        //上传过文件修改数据库语句
        protected void replace_Click(object sender, EventArgs e)
        {

            //获取设备编号
            string equipmentNumber = "";
            //int aa= Convert.ToInt32(Grid2.SelectedRowID.ToString());
            int[] setion = Grid1.SelectedRowIndexArray;
            foreach (int item in setion)
            {
                equipmentNumber = Convert.ToString(Grid1.DataKeys[item][3]);
                //string sblx = Grid1.DataKeys[item][1].ToString();
            }

            string errorMsg = "";
            int fileLength = uploadDocument.PostedFile.ContentLength;//文件大小，单位byte
            string fileName = Path.GetFileName(uploadDocument.PostedFile.FileName);//文件名称
            string newFileName = GetNewFileName(fileName);//新文件名
            string extension = Path.GetExtension(uploadDocument.PostedFile.FileName).ToLower();//文件扩展名
            //获取设备编号
            
            //文件名
            fileName = this.uploadDocument.FileName;
            //文件路径
            string path = "/uploadFile";
            //本地路径
            string localPath = Server.MapPath(Request.ApplicationPath + path);
            //全路径
            string fullPath = localPath + "\\" + fileName;
            string fullPathNew = localPath + "\\" + newFileName;
            //this.uploadDocument.SaveAs(fullPath);

            //this.uploadDocument.SaveAs(fullPathNew);
            string upFilePath = path + "/" + newFileName;

            //准备上传文件
            Stream fileStream = null;
            try
            {
                if (".jpg.jpeg.png.gif.bmp".Contains(extension))
                {
                    ftpURI = "FTPcs/pictures";
                }
                else if (".doc.docx".Contains(extension))
                {
                    ftpURI = "FTPcs/Word";
                }
                else if (".xls.xlsx".Contains(extension))
                {
                    ftpURI = "FTPcs/Excel";
                }
                else if ("..ppt".Contains(extension))
                {
                    ftpURI = "FTPcs/PowerPoint";
                }

                fileStream = uploadDocument.PostedFile.InputStream;//读取本地文件流

                var b = FtpWeb.Upload(ftpURI, newFileName, fileLength, fileStream, out errorMsg);//开始上传
                if (b == true)
                {
                    if (上传类型.SelectedText == "设备台账类型")
                    {
                        DataSet dsa = bll.修改文件(equipmentNumber, fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, 设备台账类型.SelectedText);


                    }
                    else if (上传类型.SelectedText == "设备文件类型")
                    {
                        DataSet ds = bll.修改文件(equipmentNumber, fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, 设备文件类型.SelectedText);

                    }

                    //modela.用户名= Request.QueryString["userName"];
                    modela.用户名 = "admin";
                    modela.操作类型 = "修改";
                    modela.文件名 = fileName;
                    modela.操作时间 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    modela.设备编号 = equipmentNumber;
                    DataSet dst = wjczbll.插入设备文件操作记录(modela);


                    uploadDocument.Reset();
                    设备文件类型.Reset();
                    设备台账类型.Reset();
                    设备台账类型.Hidden = true;
                    设备文件类型.Hidden = true;
                    window2.Hidden = true;
                    window5.Hidden = true;
                    Alert.Show("文件上传成功");
                }
                else
                {
                    Alert.Show("无法连接到FTP远程服务器！！！", MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('上传失败！" + ex.ToString() + "');</script>");
                Alert.Show("上传失败！", MessageBoxIcon.Warning);
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
        }

        protected void window5_Close(object sender, WindowCloseEventArgs e)
        {
            window5.Hidden = true;
        }

        protected void window5_Close1(object sender, WindowCloseEventArgs e)
        {
            uploadDocument.Reset();
            设备文件类型.Reset();
            window2.Hidden = true;
            window5.Hidden = true;
        }
    }
}