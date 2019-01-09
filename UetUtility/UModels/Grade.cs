using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Entities;
using UetGrade.UCommon;

namespace UetGrade.Models
{
    public partial class Grade : Base
    {
        public Grade(GradeEntity GradeEntity) : base(GradeEntity) { }
        public override bool Equals(Base other)
        {
            if (other == null) return false;
            if (other is Grade Grade)
            {
                return Code.Equals(Grade.Code) && Status.Equals(Grade.Status);
            }

            return false;
        }

        public override bool EqualsProperties(Base other)
        {
            if (other == null) return false;
            if (other is Grade Grade)
            {
                return Code.Equals(Grade.Code);
            }

            return false;
        }
    }
}
