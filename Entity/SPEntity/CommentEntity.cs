using SPO.CORE.DaTaAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPO.CORE.TypeCAttribute;
using SPS.MASS.MODEL.BaseEntity;
using System.Text.Json.Serialization;

namespace SPS.MASS.MODEL.SPEntity
{
    public class CommentEntity : BaseItem
    {
        public string? Content { get; set; }

        public int Privacy { get; set; }

        public List<CUserLookupValue>? UserTag { get; set; }

        public int ParentId { get; set; }

        public CFieldLookupValue? ItemLookup { get; set; }

        public string? ModuleName { get; set; }

        public string? FilesLink { get; set; }

        public CommentEntity()
        {
            UserTag = new List<CUserLookupValue>();
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class CommentEntityVm : CommentEntity
    {
        public string? CreatedTimeAgo { get; set; }
        public List<CommentEntityVm>? lstList { get; set; }

        public  object Permission { get; set; }
    }


    public class CommentEntityPost : BaseListFileAttach
    {
        public CFieldLookupValue ItemLookup { get; set; }
        public string ModuleName { get; set; }

        public string FilesLink { get; set; }

        public int Privacy { get; set; }

        public int ParentId { get; set; }

        public List<CUserLookupValue>? UserTag { get; set; }

    }



    public class CommentAddEntity : BaseAddItem
    {
        public string Content { get; set; }
        public int Privacy { get; set; } = 0;
        public int ParentId { get; set; }
        public string? FilesLink { get; set; }
        public int ItemLookup { get; set; }
        public string? ModuleName { get; set; }
        [JsonIgnore]
        public List<CUserLookupValue>? UserTag { get; set; }
        
    }
}
