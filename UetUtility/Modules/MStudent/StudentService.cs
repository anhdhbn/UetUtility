using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Modules.MConnectorSg;

namespace UetGrade.Modules.MStudent
{
    public interface IStudentService : ITransientService
    {
        bool CheckExist(string MessageId);
        void CreateNewStudent(string MessageId);
        void AddAllClasses(List<string> classes, string MessageId);
    }
    public class StudentService : CommonService, IStudentService
    {
        public StudentService(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public void AddAllClasses(List<string> classes, string MessageId)
        {
            foreach (var c in classes)
            {
                UnitOfWork.ConnectorSgRepository.AddClass(c, MessageId);
            }
            UnitOfWork.Complete();
        }

        public bool CheckExist(string MessageId)
        {
            return UnitOfWork.StudentRepository.CheckExist(MessageId);
        }

        public void CreateNewStudent(string MessageId)
        {
            UnitOfWork.StudentRepository.CreateNewStudent(MessageId);
            UnitOfWork.Complete();
        }
        
    }
}
