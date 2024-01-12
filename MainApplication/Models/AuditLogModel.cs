using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class AuditLogModel
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }

        public AuditLogModel(int logId, int userId, string action, DateTime timestamp, string? details = null)
        {
            LogId = logId;
            UserId = userId;
            Action = action;
            Timestamp = timestamp;
            Details = details;
        }
    }

}
