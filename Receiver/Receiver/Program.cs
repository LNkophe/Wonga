using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Receiver
{
    class Program
    {
        static void Main(string [] args)
        {
            var facConn = new ConnectionFactory() { HostName = "localhost" };
            using (var conn = facConn.CreateConnection())
            {
                using (var mod = conn.CreateModel())
                {
                    mod.QueueDeclare(queue: "Name", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var rec = new EventingBasicConsumer(mod);
                    rec.Received += (model, ea) =>
                      {
                          var body = ea.Body;
                          var msg = Encoding.UTF8.GetString(body);
                          Console.WriteLine("Hello" + msg + ", I am your father!", msg);
                      };
                    mod.BasicConsume(queue: "Name", autoAck: true, consumer: rec);
                    Console.WriteLine("Press [enter] to exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
