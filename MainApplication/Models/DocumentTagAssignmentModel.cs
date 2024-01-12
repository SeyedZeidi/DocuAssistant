using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class DocumentTagAssignmentModel
    {
        public int DocumentId { get; set; }
        public int TagId { get; set; }

        public DocumentTagAssignmentModel(int documentId, int tagId)
        {
            DocumentId = documentId;
            TagId = tagId;
        }
    }

}
