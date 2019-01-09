using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.UCommon;

namespace UetGrade.Entities
{
    public class GradeEntity : Base
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Link { get; set; }

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
