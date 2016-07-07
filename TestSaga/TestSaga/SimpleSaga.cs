using System;
using System.Collections.Generic;
using NServiceBus.Saga;

namespace TestSaga
{
    public class OrderSaga : Saga<OrderSagaData>,
                          IAmStartedByMessages<StartSaga>, IHandleTimeouts<MyCustomTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper.ConfigureMapping<StartSaga>(message => message.WorkflowId)
                    .ToSaga(sagaData => sagaData.WorkflowId);
        }

        public void Handle(StartSaga message)
        {
            Data.WorkflowId = message.WorkflowId;
            Data.Initiator.Email = "a@b.com";
            Data.Initiator.Lastname = "Doe";
            Data.Initiator.Surname = "Doe";
            Data.Initiator.Login = "jdoe";
            Data.Title = "Something";
            
            Data.Tasks.Add(new WorkflowTask() { WorkflowTaskId = Data.WorkflowId });
            RequestTimeout<MyCustomTimeout>(TimeSpan.FromSeconds(3));
        }

        public void Timeout(MyCustomTimeout state)
        {
         MarkAsComplete();
        }
    }

    public class MyCustomTimeout
    {
    }
}
