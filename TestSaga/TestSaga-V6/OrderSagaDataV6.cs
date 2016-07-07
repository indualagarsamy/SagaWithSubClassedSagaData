using System;
using System.Collections.Generic;
using NServiceBus;
using NServiceBus.Sagas;

namespace TestSaga
{
    public class OrderSagaDataV6 : ContainSagaData
    {

        public virtual Guid WorkflowId { get; set; }
        
        public virtual string Title { get; set; }

        public virtual PersonV6 Initiator { get; set; }

        public virtual IList<WorkflowTaskV6> Tasks { get; set; }

        public OrderSagaDataV6()
        {
            Initiator = new PersonV6();
            Tasks = new List<WorkflowTaskV6>();
        }
    }


    public class PersonV6
    {
        public virtual string Surname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Login { get; set; }
        public virtual string Email { get; set; }
    }

    public class WorkflowTaskV6
    {
        public virtual Guid Id { get; set; }
        public virtual Guid SomeGuid { get; set; }
    }
}