using ModelLayer.Areas.Admin.MenuManagement;
using ShuklaJi.DataManager;
using ShuklaJi.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ShuklaJi.BusinessLayer.Areas.Admin.MenuManagement
{
    public class MenuDetails
    {
        #region CTOR
        public MenuDetails()
        {
            int _conTimeOut = 300;
            DataExecutor.ConnectionString =Utilities.AppSetting.GetConnectionString(string.Empty, ref _conTimeOut);
            DataExecutor.ConnectionTimeout = _conTimeOut;
        }
        #endregion

        #region Public Methods
        public List<MenuModel> GetMenu(int menuId)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@MenuId",menuId)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.GetLayoutMenu, param).ToList<MenuModel>();
        }

        public string AddMenu(MenuModel menuModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@menuName",menuModel.MenuName),
                new KeyValuePair<string, object>("@menuDisplayName",menuModel.DisplayName),
                new KeyValuePair<string, object>("@position",menuModel.Position),
                new KeyValuePair<string, object>("@url",menuModel.MenuPath),
                new KeyValuePair<string, object>("@iconColor",menuModel.IconColor),
                new KeyValuePair<string, object>("@iconClass",menuModel.IconClass),
                new KeyValuePair<string, object>("@displayOrder",menuModel.DisplayOrder),
                new KeyValuePair<string, object>("@parentMenuId",menuModel.ParentMenuId??0),
                new KeyValuePair<string, object>("@accessBy",menuModel.AccessBy),
                new KeyValuePair<string, object>("@Badge",menuModel.Badge),
                new KeyValuePair<string, object>("@userId",menuModel.UserId)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_AddMenu, param);
        }

        public string UpdateMenu(MenuModel menuModel)
        {
            try
            {
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@menuName",menuModel.MenuName),
                new KeyValuePair<string, object>("@menuDisplayName",menuModel.DisplayName),
                new KeyValuePair<string, object>("@position",menuModel.Position),
                new KeyValuePair<string, object>("@url",menuModel.MenuPath??string.Empty),
                new KeyValuePair<string, object>("@iconColor",menuModel.IconColor),
                new KeyValuePair<string, object>("@iconClass",menuModel.IconClass),
                new KeyValuePair<string, object>("@displayOrder",menuModel.DisplayOrder),
                new KeyValuePair<string, object>("@parentMenuId",menuModel.ParentMenuId),
                new KeyValuePair<string, object>("@userId",menuModel.UserId),
                new KeyValuePair<string, object>("@Badge",menuModel.Badge),
                new KeyValuePair<string, object>("@accessBy",menuModel.AccessBy),
                 new KeyValuePair<string, object>("@menuId",menuModel.MenuId)
            };
                return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateMenu, param);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string DeleteMenu(MenuModel menuModel)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>("@userId",menuModel.UserId),
                 new KeyValuePair<string, object>("@menuId",menuModel.MenuId)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteMenu, param);
        }
        #endregion
    }
}
