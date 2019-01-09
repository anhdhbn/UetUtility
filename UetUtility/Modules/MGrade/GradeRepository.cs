using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Models;
using UetGrade.UCommon;

namespace UetGrade.Modules.MGrade
{
    public interface IGradeRepository
    {
        string GetName(string code);
        void Update(ICollection<Grade> grades);
        List<Grade> GetAllGrades();
    }
    public class GradeRepository : CommonRepository<Grade>, IGradeRepository
    {
        public GradeRepository(UetContext context) : base(context)
        {
        }

        public List<Grade> GetAllGrades()
        {
            return context.Grade.ToList();
        }

        public string GetName(string code)
        {
            var temp = context.Grade.Where(x => x.Code.Replace(" ", "").ToLower() == code.Replace(" ", "").ToLower()).FirstOrDefault();
            //return temp == null ? string.Empty : (temp.Status == false ? temp.Name : $"{temp.Name} (đã có điểm)");
            if (temp == null) return string.Empty;
            else
            {
                if (temp.Status == true) return $"{temp.Name} (đã có điểm)";
                else return temp.Name;
            }
        }

        public void Update(ICollection<Grade> grades)
        {
            List<Grade> Current = context.Grade.ToList();
            List<Grade> Insert, Update, Delete, NoChange;
            Common<Grade>.Split(grades, Current, out Insert, out Update, out Delete, out NoChange);
            foreach (Grade Grade in Insert)
            {
                Grade.Id = Guid.NewGuid();
                context.Grade.Add(Grade);
            }
            foreach (Grade Grade in Update)
            {
                Grade CN = Current.Where(cn => cn.Code == Grade.Code).FirstOrDefault();
                if(CN != null)
                {
                    Guid current = CN.Id;
                    Common<Grade>.Copy(Grade, CN);
                    CN.Id = current;
                }
            }
            foreach (Grade Grade in Delete)
                context.Grade.Remove(Grade);
        }
    }
}
