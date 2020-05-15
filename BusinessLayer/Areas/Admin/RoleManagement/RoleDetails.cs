using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.Admin.RoleManagement;
using ShuklaJi.Utilities;
using System.Collections.Generic;
using System.Data;

namespace ShuklaJi.BusinessLayer.Areas.Admin.RoleManagement
{
    public class RoleDetails
    {
        #region CTOR
        public RoleDetails()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
        }
        #endregion
        public string AddRoles(AddRoleModel addRoleModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@roleName",addRoleModel.RoleName),
                new KeyValuePair<string, object>("@roleDisplayName",addRoleModel.RoleDisplayName),
                new KeyValuePair<string, object>("@userId",addRoleModel.UserId),

            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_AddRoles, param);
        }

        public DataTable GetRoles(int roleId=0)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@RoleId",roleId)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetRoles, param);
        }

        public string UpdateRoles(AddRoleModel addRoleModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@roleName",addRoleModel.RoleName),
                new KeyValuePair<string, object>("@displayName",addRoleModel.RoleDisplayName),
                new KeyValuePair<string, object>("@userId",addRoleModel.UserId),
                new KeyValuePair<string, object>("@roleId",addRoleModel.RoleId)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateRoles, param);
        }

        public string DeleteRoles(int roleId,int userId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@userId",userId),
                new KeyValuePair<string, object>("@roleId",roleId)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteRoles, param);
        }

    }
}
