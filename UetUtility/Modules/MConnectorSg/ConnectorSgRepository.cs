using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Models;

namespace UetGrade.Modules.MConnectorSg
{
    public interface IConnectorSgRepository
    {
        void AddClass(string classes, string MessageId);

        void RemoveAllClass(string MessageId);

        List<Student> GetStudentsFromGrade(Grade grade);
    }
    public class ConnectorSgRepository : CommonRepository<ConnectorSg>, IConnectorSgRepository
    {
        public ConnectorSgRepository(UetContext context) : base(context)
        {
        }

        public void AddClass(string classes, string MessageId)
        {
            ConnectorSg connectorSg = new ConnectorSg();
            connectorSg.Id = Guid.NewGuid();
            connectorSg.StudentId = GetIdFromMessageId(MessageId);
            connectorSg.GradeId = GetIdFromCode(classes);
            context.ConnectorSg.Add(connectorSg);
        }

        public Guid GetIdFromMessageId(string MessageId)
        {
            return context.Student.Where(x => x.MessageId == MessageId).FirstOrDefault().Id;
        }

        public Guid GetIdFromCode(string classes)
        {
            return context.Grade.Where(x => x.Code.ToLower().Replace(" ", "") == classes.ToLower().Replace(" ", "")).FirstOrDefault().Id;
        }

        public void RemoveAllClass(string MessageId)
        {
            Guid Id = GetIdFromMessageId(MessageId);
            var listConnect = context.ConnectorSg.Where(x => x.StudentId == Id);
            context.ConnectorSg.RemoveRange(listConnect);
        }

        public List<Student> GetStudentsFromGrade(Grade grade)
        {
            List<Student> students = new List<Student>();
            string code = grade.Code;
            Guid idCode = GetIdFromCode(code);
            var listConnectors = context.ConnectorSg.Where(x => x.GradeId.Equals(idCode)).ToList();
            foreach (var conn in listConnectors)
            {
                Student student = context.Student.Where(x => x.Id.Equals(conn.StudentId)).FirstOrDefault();
                students.Add(student);
            }
            return students;
        }
    }
}
