using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace RabbitMQLib
{
    public class RabbitMQHelper
    {
        private IConnection conn;
        private IModel channel;
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string hostname { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public bool IsConnect { get; set; } = false;
        private Dictionary<string, string> queues;
        private EventingBasicConsumer consumer;
        public event OnEventCallBack OnEventCallBack;

        private bool isConsumer = false;
        private bool isPublishser = false;
        private string exchangeName = string.Empty;
        public RabbitMQHelper(bool isConsumer = true, bool isPublisher = true,string username = "guest", string password = "guest", string hostname = "localhost", int port = 5672)
        {
            Username = username;
            Password = password;
            this.hostname = hostname;
            Port = port;
            this.isConsumer = isConsumer;
            this.isPublishser = isPublisher;
        }

        #region: Connect
        /// <summary>
        /// <param name="exchangeName"></param>
        /// <param name="exchangeType"></param>
        /// <param name="queues">Dictionary<queueName, routeId></param>
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                conn = factory.CreateConnection();
                channel = conn.CreateModel();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Disconnect()
        {
            foreach (var data in queues)
            {
                channel?.QueueDelete(data.Key);
            }
            channel?.ExchangeDelete(exchangeName);
            channel?.Close();
            conn?.Close();
        }
        #endregion: End Connect

        #region: Publisher
        public bool StartPublisher(string exchangeName, Em_ExchangeType exchangeType, Dictionary<string, string> queues)
        {
            try
            {
                CreateExchange(exchangeName, exchangeType, true);
                foreach (var queueData in queues)
                {
                    string queueName = queueData.Key;
                    string routingKey = queueData.Value;
                    channel.QueueDeclare(queueName, true, false, false, null);
                    channel.QueueBind(queueName, exchangeName, routingKey);
                }
                this.queues = queues;
                this.exchangeName = exchangeName;
                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainExchangeName"></param>
        /// <param name="exchangeType"></param>
        /// <param name="subExchangeDefines">ExchangeName-ExchanegBindKey-QueueName-QueueKey</param>
        /// <returns></returns>
        public bool StartPublisher(string mainExchangeName,Em_ExchangeType exchangeType, List<Tuple<string,string,string, string>> subExchangeDefines)
        {
            try
            {
                this.queues = new Dictionary<string, string>();
                CreateExchange(mainExchangeName, exchangeType, true);
                foreach(var subExchangeData in subExchangeDefines)
                {
                    string subExchangeName = subExchangeData.Item1;
                    string subExchangeRoutingKey = subExchangeData.Item2;
                    string queueName = subExchangeData.Item3;
                    string queueRoutingId = subExchangeData.Item4;

                    channel.ExchangeDeclare(subExchangeName, exchangeType.ToString(), true, false, null);
                    channel.ExchangeBind(subExchangeName, mainExchangeName, subExchangeRoutingKey);

                    channel.QueueDeclare(queueName, true, false, false, null);
                    channel.QueueBind(queueName, subExchangeName, queueRoutingId);
                    this.queues.Add(queueName, queueRoutingId);
                }
                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }


        public void Publish(string exchangeName, string queueName, string message)
        {
            if (queues.ContainsKey(queueName))
            {
                byte[] payLoad = Encoding.ASCII.GetBytes(message);
                channel.BasicPublish(exchangeName, queues[queueName], null, payLoad);
            }
        }
        #endregion: End Publisher

        #region: Consumer
        public bool StartSubscriber(List<string> queueNames)
        {
            try
            {
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;

                foreach(string queueName in queueNames)
                {
                    channel.BasicConsume(queueName, true, consumer);
                }
                return true;
            }
            catch (Exception)
            {
                //Log
                consumer.Received -= Consumer_Received;
                return false;
            }
        }
        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            OnEventCallBack?.Invoke(this, Encoding.UTF8.GetString(e.Body.ToArray()));
        }
        #endregion: End Consumer

        #region: Private
        private bool CreateExchange(string exchangeName, Em_ExchangeType exchangeType, bool isDurable)
        {
            try
            {
                channel.ExchangeDeclare(
                exchangeName,
                Em_ExchangeType.fanout.ToString(),
                true,
                false,
                null
                );
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion: End Private

    }
}
