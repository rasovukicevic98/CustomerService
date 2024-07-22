using AutoMapper;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using CsvHelper;
using CustomerService.Contracts.Repositories;
using CustomerService.Contracts.Services;
using CustomerService.Dtos;
using CustomerService.Entities;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace CustomerService.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public DiscountService(IDiscountRepository repository, IMapper mapper, HttpClient httpClient )
        {
            _repository = repository;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        private async Task<bool> UserExistsAsync(int userId)
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
                        return false;
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
                            return nameNode != null;
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

            return false;
        }

        public async Task<Result<DiscountDto, IEnumerable<string>>> CreateDiscount(DiscountDto discountDto)
        {
            if (!await UserExistsAsync(discountDto.UserId))
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "The user does not exist." });
            }

            if (_repository.ExistByCoupon(discountDto.DiscountCoupon) == true)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "The discount coupon already exists." });
            }
            var discount = _mapper.Map<Discount>(discountDto);
            discount.CouponStartDate = DateTime.Now;
            discount.CouponEndDate = discount.CouponStartDate.AddMonths(1);
            var res = await _repository.Add(discount);
            if (!res)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "There has been an error while saving the discount!" });
            }
            return Result.Success<DiscountDto, IEnumerable<string>>(discountDto);
        }

        
        
    }
}
