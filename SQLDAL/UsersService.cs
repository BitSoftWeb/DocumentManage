using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;


namespace SQLDAL
{
    public class UsersService_SQL
    {
        public 用户表 UserLogin(用户表 au)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT a.*,b.名称 as 部门 FROM dbo.用户表 as a , dbo.一级部门表 as b");
                sb.Append("  where a.部门ID = b.ID  AND A.用户名 = @f_user_login_name  ");
                sb.Append("  and  a.[密码]=@f_user_pwd ");
                SqlParameter[] para = { new SqlParameter("f_user_login_name", au.用户名), new SqlParameter("@f_user_pwd", au.密码) };
                SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString(), para);
                用户表 csuser = new 用户表();
                //t_Users user = new t_Users();
                if (read.Read())
                {
                    csuser.ID = Convert.ToInt32(read["ID"]);
                    csuser.用户名 = read["用户名"].ToString();
                    csuser.密码 = read["密码"].ToString();
                    csuser.权限 = Convert.ToInt32(read["权限"]);
                    csuser.姓名 = read["姓名"].ToString();
                    csuser.部门ID = Convert.ToInt32(read["部门ID"]);
                    csuser.电话号码 = read["电话号码"].ToString();
                    csuser.身份证号 = read["身份证号"].ToString();
                    csuser.学校ID = Convert.ToInt32(read["学校ID"]);
                    csuser.部门 = read["部门"].ToString();
                }
                read.Close();
                //判断用户是否在线
                if (csuser != null)
                {

                }

                return csuser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public 用户表 UserLoginID(int ID)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT a.*,b.名称 as 部门 FROM dbo.用户表 as a , dbo.一级部门表 as b");
                sb.Append("  where a.部门ID = b.ID  AND A.ID=" + ID);
                Console.Write(sb);
                SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
                用户表 csuser = new 用户表();
                //t_Users user = new t_Users();
                if (read.Read())
                {
                    csuser.ID = Convert.ToInt32(read["ID"]);
                    csuser.用户名 = read["用户名"].ToString();
                    csuser.密码 = read["密码"].ToString();
                    csuser.权限 = Convert.ToInt32(read["权限"]);
                    csuser.姓名 = read["姓名"].ToString();
                    csuser.部门ID = Convert.ToInt32(read["部门ID"]);
                    csuser.电话号码 = read["电话号码"].ToString();
                    csuser.身份证号 = read["身份证号"].ToString();
                    csuser.学校ID = Convert.ToInt32(read["学校ID"]);
                    csuser.部门 = read["部门"].ToString();
                }
                read.Close();
                //判断用户是否在线
                if (csuser != null)
                {

                }

                return csuser;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}
