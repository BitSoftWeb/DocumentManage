using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FineUIPro.EmptyProjectNet40;
using Model;
using BLL;
namespace FineUIPro.Examples.basic
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        //protected void btnLogin_Click(object sender, EventArgs e)
        //{
        //    用户表 loginUser = new 用户表();

        //    loginUser.用户名 = tbxUserName.Text.Trim(); 
        //    loginUser.密码 = tbxPassword.Text.Trim();
        //    Console.Write(loginUser);
        //    UsersManager_BLL umb = new UsersManager_BLL();
        //    用户表 user = umb.UserLogin(loginUser);
        //    Console.Write(user);
        //    if (user.ID == 0)
        //    {
        //        Alert.ShowInTop("失败！");
        //        return;
        //    }
        //    Session["Userid"] = user.ID;
        //    Session["f_user_name"] = user.姓名;
        //    Session["f_user_login_name"] = user.用户名;
        //    Session["stname"] = user.部门;
        //    Session["sid"] = user.部门ID; //用户部门ID
        //    Response.Redirect("index.aspx");
        //    return;
        //}

    }
}