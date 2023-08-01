using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobilivaEcommerceAPI.Services
{
    public class MailSenderBackgroundService : BackgroundService
    {
        private readonly ILogger<MailSenderBackgroundService> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        //private readonly IMailSender _mailSender; // Burada mail gönderim servisinizi enjekte edin

        public MailSenderBackgroundService(ILogger<MailSenderBackgroundService> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory
            {
                // RabbitMQ bağlantı bilgilerini buraya ekleyin
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "SendMailQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Received message: {message}");

                // Mail gönderim işlemini gerçekleştir
                try
                {
                    // Burada gelen mesajı parse edebilir ve mail gönderimini gerçekleştirebilirsiniz.
                    // Örnek olarak IMailSender servisini kullanarak mail gönderimini çağırabilirsiniz.
                    // Örnek bir mail gönderim işlemi:

                    // await _mailSender.SendMailAsync(message); // message, gelen mesaj içeriğine göre düzenlenmeli

                    _channel.BasicAck(ea.DeliveryTag, false);
                    _logger.LogInformation("Mail sent successfully.");
                }
                catch (Exception ex)
                {
                    // Mail gönderimi başarısız olursa burada hata işlemleri yapabilirsiniz.
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                    _logger.LogError($"Mail sending failed: {ex.Message}");
                }
            };

            _channel.BasicConsume(queue: "SendMailQueue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}