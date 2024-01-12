using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class DocumentCategoryAssignmentModel
    {
        public int DocumentId { get; set; }
        public int CategoryId { get; set; }

        public DocumentCategoryAssignmentModel(int documentId, int categoryId)
        {
            DocumentId = documentId;
            CategoryId = categoryId;
        }
    }

}
