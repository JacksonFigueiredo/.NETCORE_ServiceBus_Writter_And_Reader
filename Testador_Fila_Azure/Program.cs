using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.Azure.Amqp.Serialization.SerializableType;

namespace Testador_Fila_Azure
{
    class Program
    {
        private const string QUEUE_NAME = "drm_automation";
        private static string ConnectionStringQueueBus = "Endpoint=sb://orchestratorservicebus-dev.servicebus.windows.net/;SharedAccessKeyName=SendAndReceivePolicy;SharedAccessKey=8mnFCDOCyftw0cAPQ0TZTSLo9hqDUu7cMkdBELUDClw=;EntityPath=drm_automation";
        private static ServiceBusClient _client;
        private static ServiceBusSender _clientSender;


        public class Pessoa
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string Sobrenome { get; set; } = string.Empty;

            public Pessoa()
            { }

            public Pessoa(int id, string nome, string sobrenome)
            {
                Id = id;
                Nome = nome;
                Sobrenome = sobrenome;
            }
        }


        static async Task Main(string[] args)
        {
            _client = new ServiceBusClient(ConnectionStringQueueBus);

            _clientSender = _client.CreateSender(QUEUE_NAME);

            var listaPessoas = new List<Pessoa>()
            {
                new Pessoa { Id=1,Nome="Jackson",Sobrenome="Figueiredo"},
                new Pessoa { Id=2,Nome="Ricardo",Sobrenome="Tosin"}
            };


            string messagePayload = JsonSerializer.Serialize(listaPessoas);
            ServiceBusMessage message = new ServiceBusMessage(messagePayload);
            await _clientSender.SendMessageAsync(message).ConfigureAwait(false);
        }





    }
}
