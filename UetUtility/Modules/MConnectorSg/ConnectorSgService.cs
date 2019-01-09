using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Models;
using UetGrade.Modules.MStudent;

namespace UetGrade.Modules.MConnectorSg
{
    public interface IConnectorSgService : ITransientService
    {
        void Update(string MessageId, List<string> classes);
        List<Student> GetStudentsFromGrade(Grade grade);
    }

    public class ConnectorSgService : CommonService, IConnectorSgService
    {
        private IStudentService studentService;
        public ConnectorSgService(IUnitOfWork UnitOfWork, IStudentService studentService) : base(UnitOfWork)
        {
            this.studentService = studentService;
        }

        public List<Student> GetStudentsFromGrade(Grade grade)
        {
            return UnitOfWork.ConnectorSgRepository.GetStudentsFromGrade(grade);
        }

        public void Update(string MessageId, List<string> classes)
        {
            if (studentService.CheckExist(MessageId))
            {
                UnitOfWork.ConnectorSgRepository.RemoveAllClass(MessageId);
            }
            else
            {
                studentService.CreateNewStudent(MessageId);
            }
            foreach (var c in classes)
            {
                UnitOfWork.ConnectorSgRepository.AddClass(c, MessageId);
            }
            
            UnitOfWork.Complete();
        }
    }
}
