using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
using System.Text;
using System.IO;

namespace FineUIPro.EmptyProjectNet40
{
    public partial class index : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Constants.IS_BASE)
                {

                    string Name = Request.QueryString["userName"];
                    string pass = Request.QueryString["Password"];
                    btn1.Text = Name;
                    //try
                    //{
                    //    btn1.Text = HttpContext.Current.Session["f_user_name"].ToString();
                    //}
                    //catch (Exception)
                    //{
                    //    btn1.Text = "未获取Session";

                    //}
                    //treeMenu.HideHScrollbar = false;
                    //treeMenu.HideVScrollbar = false;
                    //treeMenu.ExpanderToRight = false;
                    //treeMenu.HeaderStyle = false;
                }
                //this.getInformation();
            }

        }


        //public void 文件上传_button()
        //{

        //}
        //protected void Bind()
        //{
        //    //房间信息表 room = new 房间信息表();
        //    Model.房间信息表 model = new Model.房间信息表();
        //    UserRoom_BLL bll = new UserRoom_BLL();
        //    房间信息表 roominfo = bll.SelectRoom(model);


        //}
        //public void getInformation()
        //{
        //    房间信息表 indexRoom = new 房间信息表();
        //    UserRoom_BLL urb = new UserRoom_BLL();
        //    Console.Write(indexRoom);
        //    房间信息表 user = urb.SelectRoom(indexRoom);
        //    Console.Write(urb);

        //}


        //protected void Bind1()
        //{
        //    //办公设备信息表 room = new 办公设备信息表();
        //    Model.办公设备信息表 mode2 = new Model.办公设备信息表();
        //    UserEquipment_BLL bll_1 = new UserEquipment_BLL();
        //    办公设备信息表 equipmentinfo = bll_1.SelectEquipment(mode2);


        //}
        //public void GetEquipmentInformation()
        //{
        //    办公设备信息表 indexEquipment = new 办公设备信息表();
        //    UserEquipment_BLL ueb = new UserEquipment_BLL();
        //    Console.Write(indexEquipment);
        //    办公设备信息表 userequipment = ueb.SelectEquipment(indexEquipment);
        //    Console.Write(ueb);

        //}
    }
}