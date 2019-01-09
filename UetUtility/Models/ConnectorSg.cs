using System;
using System.Collections.Generic;

namespace UetGrade.Models
{
    public partial class ConnectorSg
    {
        public Guid Id { get; set; }
        public Guid GradeId { get; set; }
        public Guid StudentId { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Student Student { get; set; }
    }
}
