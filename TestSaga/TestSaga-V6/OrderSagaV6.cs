using System;
using System.Threading.Tasks;
using NServiceBus;

namespace TestSaga
{
    public class OrderSagaV6 : Saga<OrderSagaDataV6>,
                          IAmStartedByMessages<StartSaga>, IHandleTimeouts<MyCustomTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaDataV6> mapper)
        {
            mapper.ConfigureMapping<StartSaga>(message => message.WorkflowId)
                    .ToSaga(sagaData => sagaData.WorkflowId);
        }


        public async Task Handle(StartSaga message, IMessageHandlerContext context)
        {
            Data.WorkflowId = message.WorkflowId;
            Data.Initiator.Email = "a@b.com";
            Data.Initiator.Lastname = "Doe";
            Data.Initiator.Surname = "Doe";
            Data.Initiator.Login = "jdoe";
            Data.Title = "Something";

            Data.Tasks.Add(new WorkflowTaskV6() { SomeGuid = Data.WorkflowId });
            await RequestTimeout<MyCustomTimeout>(context, TimeSpan.FromSeconds(3));

        }

        public Task Timeout(MyCustomTimeout state, IMessageHandlerContext context)
        {
            MarkAsComplete();
            return Task.FromResult(0);
        }
    }

    public class MyCustomTimeout
    {
    }
}
