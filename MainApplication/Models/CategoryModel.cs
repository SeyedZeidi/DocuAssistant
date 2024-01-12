using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }

        public CategoryModel(int categoryId, string categoryName, string? description = null)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description;
        }
    }

}
