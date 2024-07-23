using AutoMapper;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using CsvHelper;
using CustomerService.Contracts.Repositories;
using CustomerService.Contracts.Services;
using CustomerService.Dtos;
using CustomerService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAgentRepository _agentRepository;

        public DiscountService(IDiscountRepository repository, IMapper mapper, HttpClient httpClient, IAgentRepository agentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpClient = httpClient;
            _agentRepository = agentRepository;
        }
        private async Task<(bool exists, string userName)> UserExistsAsync(int userId)
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

        public async Task<Result<DiscountDto, IEnumerable<string>>> CreateDiscount(DiscountDto discountDto, string agentEmail)
        {
            var agent = await _agentRepository.GetByEmail(agentEmail);
            if (agent == null)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "The agent does not exist." });
            }
            var (userExists, userName) = await UserExistsAsync(discountDto.UserId);
            if (!userExists)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "The user does not exist." });
            }

            if (_repository.ExistByCoupon(discountDto.DiscountCoupon) == true)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "The discount coupon already exists." });
            }
            if (await LimitExceded(agent) == true)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "You have reached the limit of coupons for today." });
            }

            var discount = _mapper.Map<Discount>(discountDto);
            discount.CouponStartDate = DateTime.Now;
            discount.CouponEndDate = discount.CouponStartDate.AddMonths(1);
            discount.Agent = agent;
            discount.Username = userName;
            var res = await _repository.Add(discount);
            if (!res)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "There has been an error while saving the discount!" });
            }
            
            return Result.Success<DiscountDto, IEnumerable<string>>(discountDto);
        }

        public async Task<bool> LimitExceded(Agent agent)
        {
            if( await _repository.CouponsMadeToday(agent)>=5)
            {
                return true;
            }
            return false;
                
        }

       
    }
}
