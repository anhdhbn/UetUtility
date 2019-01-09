using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Entities;
using UetGrade.UCommon;

namespace UetGrade.Models
{
    public partial class Student : Base
    {
        public Student(StudentEntity StudentEntity) : base(StudentEntity) { }
        public override bool Equals(Base other)
        {
            if (other == null) return false;
            if (other is Grade Grade)
            {
                return Id.Equals(Grade.Id);
            }

            return false;
        }

        public override bool EqualsProperties(Base other)
        {
            throw new NotImplementedException();
        }
    }
}
