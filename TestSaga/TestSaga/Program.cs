using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;
using NServiceBus.Persistence;

namespace TestSaga
{
    class Program
    {
        static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.UsePersistence<NServiceBus.NHibernatePersistence>()
                .ConnectionString(@"Data Source=.\SQLEXPRESS;Initial Catalog=nservicebus;Integrated Security=SSPI;");
            //Apply configuration
            var startableBus = Bus.Create(busConfiguration);
            var bus = startableBus.Start();
            bus.SendLocal(new StartSaga() {WorkflowId = Guid.NewGuid()});

            Console.ReadLine();
        }
    }

    class ProvideConfiguration : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public  MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error"
            };
        }
    }
}
