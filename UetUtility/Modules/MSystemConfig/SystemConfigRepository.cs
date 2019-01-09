using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Models;

namespace UetGrade.Modules.MSystemConfig
{
    public interface ISystemConfigRepository
    {
        DateTime GetCurrentTime();
        void ChangeTime(DateTime time);
    }
    public class SystemConfigRepository : CommonRepository<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(UetContext context) : base(context)
        {
        }

        public void ChangeTime(DateTime time)
        {
            context.SystemConfig.FirstOrDefault().CurrentTimeGrade = time;
        }

        public DateTime GetCurrentTime()
        {
            return (DateTime)context.SystemConfig.FirstOrDefault().CurrentTimeGrade;
        }
    }
}
