using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.EmptyProjectNet40.文件操作记录
{
    public partial class FileOperationRecords : System.Web.UI.Page
    {
        设备文件操作记录BLL bll = new 设备文件操作记录BLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = bll.查询设备文件操作记录();
                DataTable dt = ds.Tables[0].Copy();
                DataTable source = dt;
                Grid1.DataSource = dt;
                Grid1.DataBind();
            }
        }

        //protected void ttbSearch_Trigger2Click(object sender, EventArgs e)
        //{
        //    //DataSet ds=bll.文件操作记录条件查询(起始日期.Text, 截止日期.Text, ttbSearch.Text);
        //    //DataTable dt = ds.Tables[0].Copy();
        //    //DataTable source = dt;
        //    //Grid1.DataSource = dt;
        //    //Grid1.DataBind();
        //}

        protected void select_Click(object sender, EventArgs e)
        {
            DataSet ds = bll.文件操作记录条件查询(起始日期.Text, 截止日期.Text, ttbSearch.Text);
            DataTable dt = ds.Tables[0].Copy();
            DataTable source = dt;
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
    }
}