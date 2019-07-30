namespace Issue726Test
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;

    using MQTTnet;
    using MQTTnet.Server;

    /// <summary>
    ///     A test program.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     A test method.
        /// </summary>
        /// <param name="args">Some unused arguments.</param>
        public static void Main(string[] args)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var certificate = new X509Certificate2(
                Path.Combine(currentPath, "certificate.pfx"),
                "test",
                X509KeyStorageFlags.Exportable);
            var optionsBuilder = new MqttServerOptionsBuilder().WithEncryptedEndpoint().WithEncryptedEndpointPort(8883)
                .WithEncryptionCertificate(certificate.Export(X509ContentType.Pfx))
                .WithEncryptionSslProtocol(SslProtocols.Tls12);
            var mqttServer = new MqttFactory().CreateMqttServer();
            mqttServer.StartAsync(optionsBuilder.Build());
            Console.ReadLine();
        }
    }
}