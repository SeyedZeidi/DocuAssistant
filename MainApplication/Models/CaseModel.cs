using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class CaseModel
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public CaseModel(int caseId, string caseNumber, DateTime startDate, DateTime? endDate = null, string? description = null)
        {
            CaseId = caseId;
            CaseNumber = caseNumber;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

}
