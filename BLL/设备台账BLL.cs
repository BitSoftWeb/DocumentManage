using Model;
using BLL;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class 设备台账BLL
    {
        设备台账SQL sql = new 设备台账SQL();
        public DataSet 查询树结构设备台账(int ID,string rank)
        {
            return sql.查询树结构设备台账(ID,rank);
        }


        public DataSet 查询树结构设备类型台账(int ID, string rank,string sSearch)
        {
            return sql.查询树结构设备类型台账(ID, rank, sSearch);
        }

        public List<HHHH设备_设备信息表> 多选上传文件查询(List<int> intlist)
        {
            return sql.多选上传文件查询(intlist);
        }

        public DataSet 查询树结构类别台账(int ID, string rank)
        {
            return sql.查询树结构类别台账(ID, rank);
        }

        public DataSet 测试查询转向架台账数据(int ID)
        {
            return sql.测试查询转向架台账数据(ID);
        }

        public List<用户单位表> 查询二级结构(int ID) 
        {
            return sql.查询二级结构(ID);
        }

        public List<HHHH二级表> 查询二级类型结构(int 一级ID)
        {
            return sql.查询二级类型结构(一级ID);
        }

        public List<Z_一级结构表> 查询一级结构() 
        {
            return sql.查询一级结构();
        }

        public List<HHHH一级表> 一级结构()
        {
            return sql.一级结构();
        }



        public int 查询设备总数()
        {
            return sql.查询设备总数();
        }
        public int 树形结构查询设备总数(int ID, string rank)
        {
            return sql.树形结构查询设备总数(ID,rank);
        }

        public int 树形结构查询设备故障总数(int ID, string rank)
        {
            return sql.树形结构查询设备故障总数(ID, rank);
        }
        
        

        public int 查询故障设备总数()
        {
            return sql.查询故障设备总数();
        }

        public List<部门表> 查询三级结构(int 二级结构ID)
        {
            return sql.查询三级结构(二级结构ID);
        }

        public DataSet 设备名称关联备件(string sbmc,int 所属单位) 
        {
            return sql.设备名称关联备件(sbmc,所属单位);
        }
        public DataSet 设备编号关联维修情况(string sbbh) 
        {
            return sql.设备编号关联维修情况(sbbh);
        }

        public DataSet 设备编号查询备件消耗(string sbbh)
        {
            return sql.设备编号查询备件消耗(sbbh);
        }

        public string 查询所有年份() 
        {
            return sql.查询所有年份();
        }
        public string 查询设备故障年份(string sbbh) 
        {
            return sql.查询设备故障年份(sbbh);
        }
      

        public DataSet 模糊查询设备信息(string str)
        {
            return sql.模糊查询设备信息(str);
        }

        public string 查询设备照片(string EquipmentNum)
        {
            return sql.查询设备照片(EquipmentNum);
        }

        public List<SBZC_台账_设备文件类型表> 查询设备文件类型()
        {
            return sql.查询设备文件类型();
        }

        public List<HHHH一级表> 查询设备台账类型()
        {
            return sql.查询设备台账类型();
        }

        public List<HHHH二级表> 查询二级类型所有()
        {
            return sql.查询二级类型所有();
        }

        public DataSet 上传文件(string equipmentNumber, string fileName, string newFileName, string upFilePath, string nowTIme, string extension,string fileType)
        {
            return sql.上传文件(equipmentNumber, fileName, newFileName, upFilePath, nowTIme, extension, fileType);
        }

        public DataSet 修改文件(string equipmentNumber, string fileName, string newFileName, string upFilePath, string nowTIme, string extension, string fileType)
        {
            return sql.上传文件(equipmentNumber, fileName, newFileName, upFilePath, nowTIme, extension, fileType);
        }

        public DataSet 上传合同文件(string fileName, string newFileName, string upFilePath, string nowTIme, string extension, string fileType, string arraySerialNumber, string IDset)
        {
            return sql.上传合同文件(fileName, newFileName, upFilePath, nowTIme, extension, fileType, arraySerialNumber,IDset);
        }


        public string 判断上传文件(string equipmentNumber)
        {
            return sql.判断上传文件(equipmentNumber);
        }

        public int 判断上传合同文件(string contract)
        {
            return sql.判断上传合同文件(contract);
        }
    }
}
