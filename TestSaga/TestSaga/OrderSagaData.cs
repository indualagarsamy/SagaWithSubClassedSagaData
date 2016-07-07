using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using NHibernate.Transaction;
using NServiceBus.Saga;

namespace TestSaga
{
    public class OrderSagaData : ContainSagaData
    {

        [Unique]
        public virtual Guid WorkflowId { get; set; }
        
        public virtual string Title { get; set; }

        public virtual Person Initiator { get; set; }

        public virtual IList<WorkflowTask> Tasks { get; set; }

        public OrderSagaData()
        {
            Initiator = new Person();
            Tasks = new List<WorkflowTask>();
        }
    }


    public class Person
    {
        public virtual string Surname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Login { get; set; }
        public virtual string Email { get; set; }
    }

    public class WorkflowTask
    {
        public virtual Guid Id { get; set; }
        public virtual Guid WorkflowTaskId { get; set; }
    }
}