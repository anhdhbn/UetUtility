using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Models;

namespace UetGrade.Modules.MStudent
{
    public interface IStudentRepository
    {
        bool CheckExist(string MessageId);
        void CreateNewStudent(string MessageId);
        Guid GetIdFromMessageId(string MessageId);
    }

    public class StudentRepository : CommonRepository<Student>, IStudentRepository
    {
        public StudentRepository(UetContext context) : base(context)
        {
        }


        public bool CheckExist(string MessageId)
        {
            return context.Student.Where(x => x.MessageId == MessageId).FirstOrDefault() != null;
        }

        public void CreateNewStudent(string MessageId)
        {
            Student student = new Student();
            student.Id = Guid.NewGuid();
            student.MessageId = MessageId;
            context.Student.Add(student);
        }

        public Guid GetIdFromMessageId(string MessageId)
        {
            return context.Student.Where(x => x.MessageId == MessageId).FirstOrDefault().Id;
        }
    }
}
