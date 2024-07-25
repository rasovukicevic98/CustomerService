using AutoMapper;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using CsvHelper;
using CustomerService.Contracts;
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
        private readonly IAgentRepository _agentRepository;
        private readonly IUserServiceAdapter _userServiceAdapter;
        public DiscountService(IDiscountRepository repository, IMapper mapper, IAgentRepository agentRepository, IUserServiceAdapter userServiceAdapter)
        {
            _repository = repository;
            _mapper = mapper;
            _agentRepository = agentRepository;
            _userServiceAdapter = userServiceAdapter;
        }


        public async Task<Result<DiscountDto, IEnumerable<string>>> CreateDiscount(DiscountDto discountDto, string agentEmail)
        {
            var agent = await _agentRepository.GetByEmail(agentEmail);
            if (agent == null)
            {
                return Result.Failure<DiscountDto, IEnumerable<string>>(new List<string> { "The agent does not exist." });
            }
            var (userExists, userName) = await _userServiceAdapter.UserExistsAsync(discountDto.UserId);
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
