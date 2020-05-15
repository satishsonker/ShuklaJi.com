using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.BaseLayout.Layout;
using ShuklaJi.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ShuklaJi.BusinessLayer.Areas.BaseLayout.Layout
{
    public class LayoutDetails
    {
        #region Ctor
        public LayoutDetails()
        {
            int _conTimeOut = 300;
            DataExecutor.ConnectionString = AppSetting.GetConnectionString(string.Empty, ref _conTimeOut);
            DataExecutor.ConnectionTimeout = _conTimeOut;
        }
        #endregion
        #region Public Method
        public List<PageMenuModel> GetLayoutMenu()
        {
            List<PageMenuModel> pageMenuTemp = DataExecutor.ExecuteDataTable("GetLayoutMenu").ToList<PageMenuModel>();
            List<PageMenuModel> pageMenus = new List<PageMenuModel>();
            pageMenus.AddRange(pageMenuTemp.Where(x => x.ParentMenu.Equals("Parent")).OrderBy(x => x.DisplayOrder).ToList());
            foreach (var parent in pageMenus)
            {
                parent.ChildMenu = new List<PageMenuModel>();
                parent.ChildMenu.AddRange(pageMenuTemp.Where(x => x.ParentMenuName == parent.MenuName).OrderBy(x => x.DisplayOrder).ToList());
            }

            return pageMenus;
        } 
        #endregion

    }
}
