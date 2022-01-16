using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Application.DTOs.Logs
{
    public class AuditLogResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string Values { get; set; }
        public string PrimaryKey { get; set; }
    }
}
