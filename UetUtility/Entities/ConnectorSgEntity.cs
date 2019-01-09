using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.UCommon;

namespace UetGrade.Entities
{
    public class ConnectorSgEntity : Base
    {
        public Guid Id { get; set; }
        public Guid? GradeId { get; set; }
        public Guid? StudentId { get; set; }

        public override bool Equals(Base other)
        {
            throw new NotImplementedException();
        }

        public override bool EqualsProperties(Base other)
        {
            throw new NotImplementedException();
        }
    }
}
