using FineUIPro;
using FineUIPro.code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mydddd.Web
{
    public partial class LoginTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JuageSession();
            }
        }


        protected void JuageSession()
        {
            string name = CookieHelper.GetCookie("UserName");
            string pass = CookieHelper.GetCookie("UserPass");
            if (name != null && pass != null)
            {
                Login_name.Text = name;
                Pass_word.Text = pass;
                CheckBox1.Checked = true;
            }
            else
            {
                Login_name.Text = "";
                Pass_word.Text = "";
            }
        }

        protected void Login_Button_Click(object sender, EventArgs e)
        {
            try
            {
                string WebServicePassword = "LOGINSECRETKEY";//调用接口的特定字符串
                string loginname = Login_name.Text.ToString();
                string password = Pass_word.Text.ToString();
                MD5 md5 = new MD5CryptoServiceProvider();//实例化加密方法 要引用using System.Security.Cryptography;
                byte[] encryption_Pass = System.Text.Encoding.UTF8.GetBytes(password);//使用utf8的编码方式获取字节
                byte[] target_pass = md5.ComputeHash(encryption_Pass);//计算制定字节的哈希值
                string byte2String = "";

                for (int i = 0; i < target_pass.Length; i++)
                {
                    byte2String += target_pass[i].ToString("x2");//将byte2String转化成十六进制
                }
                ServiceReference1.WebServiceSoapClient webservice = new ServiceReference1.WebServiceSoapClient();

                string json = webservice.CheckTheLogin(loginname, byte2String);
                if (json == "0")
                {
                    //PageContext.RegisterStartupScript("alert('用户名密码错误!')");
                    Response.Write("<script>alert('用户名或者密码错误！')</script>");
                }
                else
                {
                   
                    
                    DataSet ds = webservice.GetUsersInfomation(WebServicePassword, loginname,byte2String);
                    //DataSet ds = webservice.GetUsersInfomation(loginname,byte2String);
                    if (ds.Tables.Count > 0)
                    {
                        string StrUser = string.Empty;//定义字符串为空字符
                        string cacheKey= Login_name.Text.ToString();//定义cacheKey为用户登录名
                               StrUser = Convert.ToString(Cache[cacheKey]);
                        //if (StrUser == string.Empty)
                        //{
                            TimeSpan SessTimeOut = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);
                            Cache.Insert(cacheKey, cacheKey, null, DateTime.MaxValue, SessTimeOut, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                            DataTable dt = ds.Tables[0];
                            Session["User"] = dt.Rows[0];
                            //Response.Write("<script>top.location.href='/UserManagement/UserDefault.aspx'</script>");//用户管理主界面
                            //Response.Write("<script>top.location.href='Index.aspx'</script>");//登录后样式一
                            Response.Write("<script>top.location.href='NewDefault.aspx'</script>");//登录后样式一
                            //Response.Write("<script>top.location.href='ButtonTest2.aspx'</script>");//登录后样式二

                        //}
                        //else
                        //{
                        //    Response.Write("<script>alert('此用户已经登陆不可重复登录!');top.location.href='LoginTest.aspx'</script>");
                        //}
                       
                      
                    }
                    else
                    {
                        Response.Write("<script>alert('调用接口错误，传递字符不正确请重新确认！')</script>");
                    }

                }
            }
            catch (Exception ex)
            {
                DateTime time = DateTime.Now;
                string t1 = time.ToString("yyyy-MM-dd");
                string content = ex.ToString();
                string sql = @"insert into NoteError (Error,time) values('"+ content + "','"+ t1 + "')";
                BLL.DBControl.ExecuteSql(sql);
            }

            
            
        }


        public sealed class Singleton
        {
            //定义一个静态变量来保存类的实例
            private static Singleton _instance = null;

            private static object _lock = new object();     //用来加锁，防止多线程访问类时，会产生多个实例

            //定义私有构造函数，使外界不能创建该类实例
            private Singleton()
            {

            }

            /// <summary>
            /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
            /// </summary>
            /// <returns></returns>
            public static Singleton Instance()
            {
              //如果类的实例不存在则创建，否则直接返回
              if (_instance == null)
              {
                  _instance = new Singleton();
              }
              return _instance;
            }
       }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox1.Checked)
            {
                string loginname = Login_name.Text.ToString();
                string password = Pass_word.Text.ToString();
                MD5 md5 = new MD5CryptoServiceProvider();//实例化加密方法 要引用using System.Security.Cryptography;
                byte[] encryption_Pass = System.Text.Encoding.UTF8.GetBytes(password);//使用utf8的编码方式获取字节
                byte[] target_pass = md5.ComputeHash(encryption_Pass);//计算制定字节的哈希值
                string byte2String = "";

                for (int i = 0; i < target_pass.Length; i++)
                {
                    byte2String += target_pass[i].ToString("x2");//将byte2String转化成十六进制
                }
                CookieHelper.SetCookie("UserName",Login_name.Text.ToString(),10);
                CookieHelper.SetCookie("UserPass", Pass_word.Text.ToString(),10);
            }
            
        }
    }
}