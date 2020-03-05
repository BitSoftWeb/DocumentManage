using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common;
using Model;
namespace SQLDAL
{
    class OfficeEquipmentService
    {
        public class UserEquipment_SQL
        {
            public 办公设备信息表 bs(办公设备信息表 bg)
            {
                string Sql = @"SELECT ID,编号,名称,类型,型号,位置,归属部门,一级类别名称,二级类别名称,三级类别名称  FROM dbo.办公设备信息表";
                DataSet equipment = DBHelper.Query(Sql);
                return equipment;
            }
            public 办公设备信息表 SelectEquipment(string secequipment)
            {
                throw new NotImplementedException();
            }

        }
    }
}
