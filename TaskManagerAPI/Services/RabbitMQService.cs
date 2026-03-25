using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManagerAPI.Services
{
    public class RabbitMQService
    {
        private readonly string _hostname = "localhost";

        public async Task SendMessage(object message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "task-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "task-queue",
                body: body
                );
        }
    }
}
