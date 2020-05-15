using ShuklaJi.DataManager;
using ShuklaJi.ModelLayer.Areas.Admin.Misc;
using ShuklaJi.Utilities;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace ShuklaJi.BusinessLayer.Areas.Admin
{
  public  class Miscellaneous
    {
        #region Ctor
        public Miscellaneous()
        {
            DataExecutor.ConnectionString = Utilities.AppSetting.GetConnectionString(string.Empty);
        }
        #endregion
        public List<RefLookupModel> GetReferenceLookup(string key)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@key",key)
            };
            return DataExecutor.ExecuteDataTable(UtilityConstant.Procedures.Mst_GetReferenceLookup, param).ToList<RefLookupModel>();
        }

        public string SaveReferenceLookup(RefLookupModel model)
        {
            List<string> lst = new List<string>();
            lst = model.Value.Split("||").ToList();
            foreach (var item in lst)
            {
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@key",model.Key),
                new KeyValuePair<string, object>("@Value",item),
                new KeyValuePair<string, object>("@Remark",model.Remark),
                new KeyValuePair<string, object>("@UserId",model.UserId),
            };
             DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_SaveReferenceLookup, param);
            }
            return "Saved";
        }

        public string UpdateReferenceLookup(RefLookupModel model)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@key",model.Key),
                new KeyValuePair<string, object>("@Value",model.Value),
                new KeyValuePair<string, object>("@Remark",model.Remark),
                new KeyValuePair<string, object>("@UserId",model.UserId),
                new KeyValuePair<string, object>("@Id",model.Id)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_UpdateReferenceLookup, param);
        }

        public string DeleteReferenceLookup(RefLookupModel model)
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@UserId",model.UserId),
                new KeyValuePair<string, object>("@Id",model.Id)
            };
            return DataExecutor.ExecuteScalar(UtilityConstant.Procedures.Mst_DeleteReferenceLookup, param);
        }
    }
}
