using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Models;
using UetGrade.Modules.MConnectorSg;
using UetGrade.Modules.MGrade;
using UetGrade.Modules.MStudent;
using UetGrade.Modules.MSystemConfig;

namespace UetGrade.Modules
{
    public interface IUnitOfWork : ITransientService, IDisposable
    {
        void Complete();
        ConnectorSgRepository ConnectorSgRepository { get; }
        GradeRepository GradeRepository { get; }
        StudentRepository StudentRepository { get; }
        SystemConfigRepository SystemConfigRepository { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private UetContext context;
        private IDbContextTransaction _transaction;
        private ConnectorSgRepository _connectorSRepositoryg;
        private GradeRepository _gradeRepository;
        private StudentRepository _studentRepository;
        private SystemConfigRepository _systemConfigRepository;

        public ConnectorSgRepository ConnectorSgRepository
        {
            get
            {
                InitTransaction();
                if (_connectorSRepositoryg == null) _connectorSRepositoryg = new ConnectorSgRepository(context);
                return _connectorSRepositoryg;
            }
        }

        public GradeRepository GradeRepository
        {
            get
            {
                InitTransaction();
                if (_gradeRepository == null) _gradeRepository = new GradeRepository(context);
                return _gradeRepository;
            }
        }

        public StudentRepository StudentRepository
        {
            get
            {
                InitTransaction();
                if (_studentRepository == null) _studentRepository = new StudentRepository(context);
                return _studentRepository;
            }
        }

        public SystemConfigRepository SystemConfigRepository
        {
            get
            {
                InitTransaction();
                if (_systemConfigRepository == null) _systemConfigRepository = new SystemConfigRepository(context);
                return _systemConfigRepository;
            }
        }

        private void InitTransaction()
        {
            if (_transaction == null)
                _transaction = context.Database.BeginTransaction();
        }

        public UnitOfWork()
        {

            this.context = new UetContext();
        }

        public UnitOfWork(UetContext context)
        {
            this.context = context;
        }

        ~UnitOfWork()
        {
            context.Dispose();
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public void Complete()
        {
            try
            {
                context.SaveChanges();
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
