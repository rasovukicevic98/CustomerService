using CustomerService.Contracts;
using System.Xml;

namespace CustomerService.Services
{
    public class SoapUserServiceAdapter : IUserServiceAdapter
    {
        private readonly HttpClient _httpClient;

        public SoapUserServiceAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool exists, string userName)> UserExistsAsync(int userId)
        {
            var url = $"https://www.crcind.com/csp/samples/SOAP.Demo.cls?soap_method=FindPerson&id={userId}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();


                    if (responseString.TrimStart().StartsWith("<html>"))
                    {
                        Console.WriteLine("Received an HTML response, likely an error page.");
                        return (false, null);
                    }

                    try
                    {
                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(responseString);


                        var namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                        namespaceManager.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
                        namespaceManager.AddNamespace("tempuri", "http://tempuri.org");


                        var findPersonResponseNode = xmlDoc.SelectSingleNode("//SOAP-ENV:Body/tempuri:FindPersonResponse", namespaceManager);
                        if (findPersonResponseNode != null && findPersonResponseNode.HasChildNodes)
                        {
                            var nameNode = findPersonResponseNode.SelectSingleNode("tempuri:FindPersonResult/tempuri:Name", namespaceManager);
                            if (nameNode != null)
                            {
                                return (true, nameNode.InnerText);
                            }
                        }
                    }
                    catch (XmlException ex)
                    {

                        Console.WriteLine($"XML Parsing Error: {ex.Message}");
                        Console.WriteLine($"Response Content: {responseString}");
                    }
                }
                else
                {

                    Console.WriteLine($"Error: Service returned status code {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Request Error: {ex.Message}");
            }

            return (false, null);
        }
    }
}
