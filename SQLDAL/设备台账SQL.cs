using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public class 设备台账SQL
    {



        public List<Z_一级结构表> 查询一级结构()
        {
            string sql = "SELECT * from Z_一级结构表";
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            List<Z_一级结构表> listonejg = new List<Z_一级结构表>();
            while (read.Read())
            {
                Z_一级结构表 model = new Z_一级结构表();
                model.ID = Convert.ToInt32(read["ID"].ToString());
                model.名称 = read["名称"].ToString();
                model.程序显示名称 = read["程序显示名称"].ToString();
                listonejg.Add(model);
            }
            read.Close();
            return listonejg;
        }


        public List<HHHH一级表> 一级结构()
        {
            string sql = "SELECT * from AC_设备类型一级表";
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            List<HHHH一级表> listone = new List<HHHH一级表>();
            while (read.Read())
            {
                HHHH一级表 model = new HHHH一级表();
                model.ID = Convert.ToInt32(read["ID"].ToString());
                model.类型 = read["类型"].ToString();
                //model.程序显示名称 = read["程序显示名称"].ToString();
                listone.Add(model);
            }
            read.Close();
            return listone;
        }

        public List<用户单位表> 查询二级结构(int 一级结构ID)
        {
            string sql = string.Format("SELECT * from dbo.用户_单位表 where 一级结构 ={0}   order by 单位属性 desc", 一级结构ID);
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            List<用户单位表> listyhdw = new List<用户单位表>();
            while (read.Read())
            {
                用户单位表 model = new 用户单位表();
                model.ID = Convert.ToInt32(read["ID"].ToString());
                model.名称 = read["名称"].ToString();
                model.成本中心 = read["成本中心"].ToString();
                listyhdw.Add(model);
            }
            read.Close();
            return listyhdw;

        }

        public List<HHHH二级表> 查询二级类型结构(int 一级ID)
        {
            string sql = string.Format("SELECT * from AC_设备类型二级表 where SID ={0} ", 一级ID);
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            List<HHHH二级表> listyh = new List<HHHH二级表>();
            while (read.Read())
            {
                HHHH二级表 model = new HHHH二级表();
                model.SID = Convert.ToInt32(read["SID"].ToString());
                model.二级类型 = read["二级类型"].ToString();
                //model.成本中心 = read["成本中心"].ToString();
                listyh.Add(model);
            }
            read.Close();
            return listyh;

        }

        public int 查询设备总数()
        {
            string sql = "SELECT COUNT(*) as 总数 FROM 设备_设备信息表";
            return Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, sql));
        }
        public int 查询故障设备总数()
        {
            string sql = " SELECT COUNT(*) as 总数 FROM c_设备故障维修表 WHERE 完成情况 = '正在进行'";
            return Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, sql));

        }

        public List<部门表> 查询三级结构(int 二级结构ID)
        {
            string sql = string.Format("SELECT * from dbo.部门表 where 所属单位 ={0}", 二级结构ID);
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            List<部门表> listbm = new List<部门表>();
            while (read.Read())
            {
                部门表 model = new 部门表();
                model.ID = Convert.ToInt32(read["ID"].ToString());
                model.名称 = read["名称"].ToString();
                model.成本中心 = read["成本中心"].ToString();
                listbm.Add(model);
            }
            read.Close();
            return listbm;
        }

        public DataSet 查询树结构设备台账(int ID, string rank)
        {
            if (rank == "一级")
            {
                return null;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT A.*,B.名称 AS 部门名称, C.名称 AS 单位名称 FROM	 ");
                sb.Append("dbo.设备_设备信息表 AS A ,部门表 AS B , 用户_单位表 AS C where A.使用单位 = B.ID and b.所属单位 = c.ID ");
                if (rank == "二级")
                {
                    sb.Append("and C.ID =" + ID);
                }
                else if (rank == "三级")
                {
                    sb.Append("and B.ID =" + ID);
                }
                return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            }
        }

        public DataSet 查询树结构设备类型台账(int ID, string rank,string sSearch)
        {
            if (rank == "一级")
            {
                return null;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT A.*,B.类型,C.二级类型 FROM    ");
                sb.Append(" AC_设备信息表 as A,AC_设备类型二级表 as C,AC_设备类型一级表 as B where   B.ID = C.SID and C.ID= A.分类ID ");
                
                if (rank == "二级")
                {
                    sb.Append("  and C.ID =" + ID);
                }
                if (sSearch=="")
                {
                    sb.Append(" ");
                }
                else
                {
                    sb.Append( "and (A.设备编号 like '%"+sSearch + "%' or A.设备名称 like '%" + sSearch + "%') ");
                }
                //else if (rank == "三级")
                //{
                //    sb.Append("and B.ID =" + ID);
                //}
                return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            }
        }

        public DataSet 查询树结构类别台账(int ID, string rank)
        {
            if (rank == "一级")
            {
                return null;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select A.*,B.二级类型 FROM	 ");
                sb.Append(" AC_设备类型一级表 AS A,AC_设备类型二级表 AS B where A.ID = B.SID  ");
                //if (rank == "二级")
                //{
                //    sb.Append("and C.ID =" + ID);
                //}
                //else if (rank == "三级")
                //{
                //    sb.Append("and B.ID =" + ID);
                //}
                return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            }
        }


        public DataSet 测试查询转向架台账数据(int id=2) 
        {
            string sql = string.Format("SELECT top(200) A.*,B.名称 AS 部门名称, C.名称 AS 单位名称 FROM	 dbo.设备_设备信息表 AS A ,部门表 AS B , 用户_单位表 AS C where A.使用单位 = B.ID and b.所属单位 = c.ID and C.ID ={0}",id);
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sql);
        }


        public int 树形结构查询设备总数(int ID, string rank)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(*) 总数 FROM	 ");
            sb.Append("dbo.设备_设备信息表 AS A ,部门表 AS B , 用户_单位表 AS C where A.使用单位 = B.ID and b.所属单位 = c.ID ");
            if (rank == "二级")
            {
                sb.Append("and C.ID =" + ID);
            }
            else if (rank == "三级")
            {
                sb.Append("and B.ID =" + ID);
            }
            return Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, sb.ToString()));

        }

        public int 树形结构查询设备故障总数(int ID, string rank)
        {
            string sql = "";
            if (rank == "二级")
            {
                sql = string.Format("SELECT COUNT(*) as 总数 FROM c_设备故障维修表 WHERE  完成情况 = '正在进行' AND 对应单位={0}", ID);
                return Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, sql.ToString()));
            }
            else 
            {
                return 0;
            }
            //else if(rank=="三级")
            //{
            //    string cidsql = string.Format("SELECT 所属单位 FROM 部门表 where ID ={0}",ID);
            //    int cid = Convert.ToInt32(DBHelper.ExecuteScalar(DBHelper.ConnectionString, CommandType.Text, cidsql.ToString()));


            //}

        }

        public DataSet 设备名称关联备件(string sbmc, int 所属单位)
        {
            string sql = string.Format("select 成本中心 from 部门表 where ID = {0}", 所属单位);
            string 成本中心 = "";
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            while (read.Read())
            {
                成本中心 = read["成本中心"].ToString();
            }
            read.Close();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT     a.ID, a.物料号, a.备件名称, a.规格型号, a.计量单位, a.管理类别, b.成本中心, b.提报单位, SUM(b.剩余数量) AS 库存, SUM(b.剩余数量 * b.价格) ");
            sb.Append(" AS 总金额, a.备注 FROM         b_备件_信息表 AS a INNER JOIN");
            sb.Append(" b_备件_导入日志表 AS b ON a.物料号 = b.物料号 ");
            sb.Append("WHERE     (a.物料号 IN  (SELECT DISTINCT 物料号 FROM b_备件_设备关联表");
            sb.Append(" WHERE (名称 = '" + sbmc + "'))) AND (b.成本中心 = '" + 成本中心 + "')");

            sb.Append("GROUP BY a.ID, a.物料号, a.备件名称, a.规格型号, a.计量单位, a.管理类别, b.成本中心, b.提报单位, a.备注");
            sb.Append(" ORDER BY LEN(a.物料号), a.物料号");

            //sb.Append("select a.物料号, a.备件名称, a.规格型号 ,b.提报单位 from (b_备件_信息表 as a inner join b_备件_导入日志表 as b on a.物料号 = b.物料号) ");
            //sb.Append(" inner join b_备件_设备关联表 as c on a.物料号 = c.物料号 where  c.名称 = '" + sbmc + "' group by c.ID,a.物料号,a.备件名称,a.规格型号,b.提报单位 ");
            //sb.Append(" order by LEN(a.物料号), a.物料号");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }

        public DataSet 设备编号关联维修情况(string sbbh)
        {
            string sql = string.Format("SELECT * FROM dbo.c_设备故障维修表 where 设备编号='{0}' ORDER BY 故障时间 desc  ", sbbh);
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sql);
        }
        public DataSet 设备编号查询备件消耗(string sbbh)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select b.物料号,c.备件名称, sum(a.操作数量) as 操作数量 from b_备件_记录表 as a inner join b_备件_导入日志表 as b ");
            sb.Append(" on a.日志ID = b.ID and a.设备编号 = '" + sbbh + "' inner join b_备件_信息表 as c on b.物料号 =c.物料号 ");
            sb.Append("group by b.物料号, c.备件名称 order by 操作数量 desc");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }

        public string 查询所有年份()
        {
            string 年份 = "";
            //先查询投入使用时间
            string sql = "SELECT  distinct YEAR(故障时间) as 时间  FROM dbo.c_设备故障维修表  ORDER BY 时间 desc  ";
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            while (read.Read())
            {
                年份 += read["时间"] + ",";
            }
            read.Close();
            return 年份;
        }

        public string 查询设备故障年份(string sbbh)
        {
            string 年份 = "";
            string sql = string.Format("SELECT distinct YEAR(故障时间) as 时间  FROM dbo.c_设备故障维修表 where 设备编号='{0}'  ORDER BY 时间 desc ", sbbh);
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            while (read.Read())
            {
                年份 += read["时间"] + ",";
            }
            read.Close();
            return 年份;

        }

        public string 查询设备照片(string EquipmentNum)
        {
            string EquipmentNumPho="" ;
            string sql=string.Format(" select 文件名 from AC_设备图片上传 where 设备编号='{0}'", EquipmentNum);
            SqlDataReader read=DBHelper.ExecuteReader(DBHelper.ConnectionString,CommandType.Text,sql.ToString());
            while(read.Read())
            {
                EquipmentNumPho = (string)read["文件名"];
            }
            read.Close();
            return EquipmentNumPho;
        }

      


    

 

        public DataSet 模糊查询设备信息(string str)
        {
            string sql = string.Format("   SELECT 设备编号,设备名称,设备型号 from AC_设备信息表  where (设备编号 like '%{0}%' or 设备名称 like '%{0}%') ", str);
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sql);
        }


        public List<SBZC_台账_设备文件类型表> 查询设备文件类型()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * FROM dbo.AC_设备文件类型表");

                SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());

                List<SBZC_台账_设备文件类型表> list = new List<SBZC_台账_设备文件类型表>();
                SBZC_台账_设备文件类型表 modelx = new SBZC_台账_设备文件类型表();
                modelx.ID = 0;
                modelx.设备文件类型 = "全部";
                list.Add(modelx);
                while (read.Read())
                {
                    SBZC_台账_设备文件类型表 model = new SBZC_台账_设备文件类型表();
                    model.ID = Convert.ToInt32(read["ID"]);
                    model.设备文件类型 = read["设备文件类型"].ToString();
                    list.Add(model);
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HHHH一级表> 查询设备台账类型()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * FROM AC_设备类型一级表");

                SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());

                List<HHHH一级表> list = new List<HHHH一级表>();
                HHHH一级表 modelx = new HHHH一级表();
                modelx.ID = 0;
                modelx.类型 = "全部";
                list.Add(modelx);
                while (read.Read())
                {
                    HHHH一级表 model = new HHHH一级表();
                    model.ID = Convert.ToInt32(read["ID"]);
                    model.类型 = read["类型"].ToString();
                    list.Add(model);
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataSet 上传文件(string equipmentNumber, string fileName, string newFileName, string upFilePath, string nowTIme,string extension,string fileType)
        {

            StringBuilder sb = new StringBuilder();           
            sb.Append("INSERT INTO AC_上传文件表");
            sb.Append("(设备编号,原文件名,文件名,上传路径,上传时间,文件后缀,文件类型");
            sb.Append(" ) VALUES (");
            //sb.Append("" + deviceType + ",'" + fileName + "','" + newfile + "','" + nowTime + "','" + houzhuiming + "')");
            sb.Append("'" + equipmentNumber + "','" + fileName + "','" + newFileName + "','" + upFilePath + "','" + nowTIme + "','" + extension + "','"+ fileType + "')");
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }

        public DataSet 修改文件(string equipmentNumber, string fileName, string newFileName, string upFilePath, string nowTIme, string extension, string fileType)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("update AC_设备文件操作记录表 set   ");
            sb.Append("原文件名 = '"+ fileName + "',文件名 = '"+ newFileName + "',上传路径 = '"+ upFilePath + "',上传时间 = '"+ nowTIme + "',文件后缀 = '"+ extension + "',文件类型 = '"+ fileType+"'" );
            sb.Append("   where  设备编号='"+ equipmentNumber+"'" );       
            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
        }



        //ID ,设备编号,文件名 ,上传路径,上传时间,文件后缀 ,原文件名,文件类型,ID集
        //fileName, newFileName, upFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extension, DropDownList2.SelectedText
        public DataSet 上传合同文件(string fileName, string newFileName, string upFilePath, string nowTIme, string extension, string fileType, string arraySerialNumber, string IDset)
        {
            #region
            StringBuilder sb = new StringBuilder();
            //string[] result = arraySerialNumber.Split(',');
            ////DataSet data;
            // for (int i=0;i<result.Length-1;i++)
            // {
            //     string serialNumber = result[i];
            string SerialNumber = arraySerialNumber.Substring(0, arraySerialNumber.Length - 1);// 删减最后一个字符","
            sb.Append("INSERT INTO AC_上传文件表");
            sb.Append("(文件名 ,原文件名 ,上传路径,上传时间,文件后缀,文件类型,设备编号,ID集");
            sb.Append(" ) VALUES (");
            sb.Append("'" + newFileName + "','" + fileName + "','" + upFilePath + "','" + nowTIme + "','" + extension + "','" + fileType + "','" + SerialNumber + "','" + IDset + "')");
            //             //this.data = DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            // }

            return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            #endregion

            //StringBuilder sb = new StringBuilder();
            //sb.Append("INSERT INTO            ");
            //sb.Append("(文件名 ,原文件名 ,上传路径,上传时间,文件后缀,文件类型,ID集");
            //sb.Append(" ) VALUES (");
            //sb.Append("'" + newFileName + "','" + fileName + "','" + upFilePath + "','" + nowTIme + "','" + extension + "','" + fileType + "','" + result[i] + "','" + IDset + "')");
            //return DBHelper.ExecuteDataset(DBHelper.ConnectionString, CommandType.Text, sb.ToString());

        }

        public List<HHHH设备_设备信息表> 多选上传文件查询(List<int> intlist)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,SBID,SAP编号,设备编号,设备名称,设备型号,固资原值,制造商,投产时间,使用单位,厂房ID,机械,电气,属性,引进,数控,设备规格,固资净值,分类ID from AC_设备信息表");
            //sb.Append(" where 设备编号='" + intlist[0] + "'");
            sb.Append("  where  (");
            sb.Append(" ID =" + intlist[0]);
            foreach (int item in intlist)
            {
                sb.Append(" OR ID =" + item);
            }
            sb.Append(")");

            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
            List<HHHH设备_设备信息表> list = new List<HHHH设备_设备信息表>();
            while (read.Read())
            {
                HHHH设备_设备信息表 model = new HHHH设备_设备信息表();
                model.ID = Convert.ToInt32(read["ID"].ToString());
                //model.SBID = Convert.ToInt32(read["SBID"].ToString());
                model.制造商 = read["制造商"].ToString();
                model.设备编号 =read["设备编号"].ToString();
                model.设备名称 = read["设备名称"].ToString();
                //model.型号 = read["型号"].ToString();
                //model.使用方向 = read["使用方向"].ToString();
                //model.负责人 = read["负责人"].ToString();
                //model.处置方式 = 处置方式;
                list.Add(model);
            }
            return list;
        }

        public List<HHHH二级表> 查询二级类型所有()
        {
            string sql = string.Format("SELECT * from AC_设备类型二级表 ");
            SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            List<HHHH二级表> listyh = new List<HHHH二级表>();
            while (read.Read())
            {
                HHHH二级表 model = new HHHH二级表();
                model.SID = Convert.ToInt32(read["SID"].ToString());
                model.二级类型 = read["二级类型"].ToString();
                model.ID = Convert.ToInt32(read["ID"]);
                listyh.Add(model);
            }
            read.Close();
            return listyh;
        }

        public string 判断上传文件(string equipmentNumber)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from AC_上传文件表 where 设备编号 = '" + equipmentNumber + "'");
                //SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
                string permissions = "";
                SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
                while (read.Read())
                {
                    permissions = read["原文件名"].ToString();
                    
                }
                return permissions;
            }
            catch (Exception)
            {
                throw;
            }
            //StringBuilder sb = new StringBuilder();          
            //sb.Append("select * from AC_上传文件表 where 设备编号 = '" + equipmentNumber + "'");
            ////SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
            //    string permissions = "";
            //SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());

            //while (read.Read())
            //{
            //   permissions = read["原文件名"].ToString();

            //    try
            //    {
            //        if (permissions != null)
            //        {
            //            return 1;
            //        }
            //        else
            //        {

            //        }

            //    }
            //    catch (Exception)
            //    {
            //        return 0;
            //        //throw;
            //    }
            //}
            //    read.Close();

            //return 1;

        }




        public int 判断上传合同文件(string contract)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from AC_上传文件表 where 原文件名 = '" + contract + "'");
                //SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sql.ToString());
                string permissions = "";
                SqlDataReader read = DBHelper.ExecuteReader(DBHelper.ConnectionString, CommandType.Text, sb.ToString());
                while (read.Read())
                {
                    permissions = read["原文件名"].ToString();

                }
                if (contract == permissions)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
           
        }

    }
}
