using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string [] args)
        {
            var facConn = new ConnectionFactory() { HostName = "localhost" };
            using (var conn = facConn.CreateConnection())
            using (var mod = conn.CreateModel())
                {
                string n;
                Console.WriteLine("Hello my name is,");
                n = Console.ReadLine();
                    mod.QueueDeclare(queue: "Name", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    string msg = "Hello my name is, " + n;
                    var body = Encoding.UTF8.GetBytes(msg);

                    mod.BasicPublish(exchange: "", routingKey: "Name", basicProperties: null, body: body);
                    Console.WriteLine("[x] sent {0}", msg);
                }
                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();            
        }
    }
}
