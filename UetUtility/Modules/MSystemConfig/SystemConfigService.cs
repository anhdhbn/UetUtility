using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UetGrade.Modules.MSystemConfig
{
    public interface ISystemConfigService : ITransientService
    {
        DateTime GetCurrentTime();
        void ChangeTime(DateTime time);
    }
    public class SystemConfigService : CommonService, ISystemConfigService
    {
        public SystemConfigService(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public void ChangeTime(DateTime time)
        {
            UnitOfWork.SystemConfigRepository.ChangeTime(time);
            UnitOfWork.Complete();
        }

        public DateTime GetCurrentTime()
        {
            return UnitOfWork.SystemConfigRepository.GetCurrentTime();
        }
    }
}
