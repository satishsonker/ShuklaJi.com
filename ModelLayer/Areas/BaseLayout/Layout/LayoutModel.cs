using System.Collections.Generic;

namespace ShuklaJi.ModelLayer.Areas.BaseLayout.Layout
{
    public class PageMenuModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Badge { get; set; }
        public string MenuPath { get; set; }
        public string IconClass { get; set; }
        public string IconColor { get; set; }
        public string DisplayName { get; set; }
        public string AccessBy { get; set; }
        public string ParentMenu { get; set; }
        public string ParentMenuName { get; set; }
        public string Position { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public List<PageMenuModel> ChildMenu { get; set; }
    }
}
