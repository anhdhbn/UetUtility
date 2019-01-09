using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UetGrade.UCommon
{
    public abstract class Base
    {
        public Base() { }
        public Base(Base obj)
        {
            Common<Base>.Copy(obj, this);
        }
        public abstract bool Equals(Base other);
        public abstract bool EqualsProperties(Base other);
    }
}
