using System;
using System.Collections.Generic;

namespace UetGrade.Models
{
    public partial class Grade
    {
        public Grade()
        {
            ConnectorSg = new HashSet<ConnectorSg>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Link { get; set; }
        public DateTime? Time { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ConnectorSg> ConnectorSg { get; set; }
    }
}
