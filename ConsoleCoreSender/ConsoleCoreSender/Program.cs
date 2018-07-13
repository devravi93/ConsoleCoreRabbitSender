using RabbitMQ.Client;
using System;
using System.Text;

namespace ConsoleCoreSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello this is the sender application!");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine("Enter message to send");
                var msg = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(msg);
                channel.BasicPublish(exchange: "",
                                     routingKey: "msgKey",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", msg);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
