using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.RequestUtility;
using UetGrade.UCommon;

namespace UetGrade.Models
{
    public partial class SystemConfig : Base
    {
        public SystemConfig() { }
        public SystemConfig(SystemConfigEntity SystemConfigEntity) : base(SystemConfigEntity) { }
        public override bool Equals(Base other)
        {
            if (other == null) return false;
            if (other is SystemConfig SystemConfig)
            {
                return Id.Equals(SystemConfig.Id);
            }

            return false;
        }

        public override bool EqualsProperties(Base other)
        {
            throw new NotImplementedException();
        }
    }
}
