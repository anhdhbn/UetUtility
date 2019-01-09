using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Models;

namespace UetGrade.Modules.MGrade
{
    public interface IGradeService : ITransientService
    {
        string GetName(string code);
        void Update(ICollection<Grade> grades);
        List<Grade> GetAllGrades();
    }
    public class GradeService : CommonService, IGradeService
    {
        public GradeService(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public List<Grade> GetAllGrades()
        {
            return UnitOfWork.GradeRepository.GetAllGrades();
        }

        public string GetName(string code)
        {
            return UnitOfWork.GradeRepository.GetName(code);
        }

        public void Update(ICollection<Grade> grades)
        {
            UnitOfWork.GradeRepository.Update(grades);
            UnitOfWork.Complete();
        }
    }
}
