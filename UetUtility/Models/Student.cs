using System;
using System.Collections.Generic;

namespace UetGrade.Models
{
    public partial class Student
    {
        public Student()
        {
            ConnectorSg = new HashSet<ConnectorSg>();
        }

        public Guid Id { get; set; }
        public string MessageId { get; set; }

        public virtual ICollection<ConnectorSg> ConnectorSg { get; set; }
    }
}
