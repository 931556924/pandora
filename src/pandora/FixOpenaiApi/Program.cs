using System.Net;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

ProxyServer proxyServer = new();
proxyServer.CertificateManager.CreateRootCertificate();
proxyServer.CertificateManager.TrustRootCertificate(true);
proxyServer.BeforeRequest += OnRequest;
var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 18965, true)
{
    // Use self-issued generic certificate on all https requests
    // Optimizes performance by not creating a certificate for each https-enabled domain
    // Useful when certificate trust is not required by proxy clients
    //GenericCertificate = new X509Certificate2(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "genericcert.pfx"), "password")
};

proxyServer.AddEndPoint(explicitEndPoint);
proxyServer.Start();

foreach (var endPoint in proxyServer.ProxyEndPoints)
    Console.WriteLine("Listening on '{0}' endpoint at Ip {1} and port: {2} ",
        endPoint.GetType().Name, endPoint.IpAddress, endPoint.Port);

proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);

Console.Read();

proxyServer.BeforeRequest -= OnRequest;
proxyServer.Stop();

async Task OnRequest(object sender, SessionEventArgs e)
{
    if (e.HttpClient.Request.RequestUri.AbsoluteUri.Contains("api.openai.com"))
    {
         e.HttpClient.Request.RequestUri = new Uri("https://ai.fakeopen.com/v1/chat/completions");
    }
    if (e.HttpClient.Request.Url.Contains("https://ai.fakeopen.com/v1/chat/completions"))
    {
        Console.WriteLine($"URL: {e.HttpClient.Request.Url}");
        
        foreach (var header in e.HttpClient.Request.Headers)
        {
            Console.WriteLine($"Header: {header.Name} Value: {header.Value}");
        }

        if (e.HttpClient.Request.HasBody)
        {
            var bodyString = await e.GetRequestBodyAsString();
            Console.WriteLine($"Body: {bodyString}");
        }
    }
}
