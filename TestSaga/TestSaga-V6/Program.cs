using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Persistence;

namespace TestSaga
{
    class Program
    {
        private static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            var endpointConfiguration = new EndpointConfiguration("TestSaga-V6");
            endpointConfiguration.UsePersistence<NServiceBus.NHibernatePersistence>()
                .ConnectionString(@"Data Source=.\SQLEXPRESS;Initial Catalog=nservicebus;Integrated Security=SSPI;");
            endpointConfiguration.SendFailedMessagesTo("error");
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            await endpointInstance.SendLocal(new StartSaga() { WorkflowId = Guid.NewGuid() });
            
            Console.ReadLine();

            // Custom code before stop
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
