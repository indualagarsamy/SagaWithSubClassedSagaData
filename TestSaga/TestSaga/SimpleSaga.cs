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
            Data.Initiator = new Person()
            {
                Email = "a@b.com",
                Lastname = "Doe",
                Surname = "Doe",
                Login = "jdoe"
            };
            Data.Title = "Something";
            var taskList = new List<WorkflowTask>();
            taskList.Add(new WorkflowTask() { WorkflowTaskId = Data.WorkflowId});
        //    Data.Tasks = taskList;
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
