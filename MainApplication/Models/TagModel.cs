using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class TagModel
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string? Description { get; set; }

        public TagModel(int tagId, string tagName, string? description = null)
        {
            TagId = tagId;
            TagName = tagName;
            Description = description;
        }
    }

}
