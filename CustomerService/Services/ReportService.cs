using AutoMapper;
using CSharpFunctionalExtensions;
using CsvHelper;
using CustomerService.Contracts.Repositories;
using CustomerService.Contracts.Services;
using CustomerService.Dtos;
using CustomerService.Repositories;
using Hangfire;
using System.Globalization;

namespace CustomerService.Services
{
    public class ReportService : IReportService
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public ReportService(IDiscountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MemoryStream> CreateCsvReportAsync(IEnumerable<CSVDto> coupons)
        {

            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream, leaveOpen: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                await csv.WriteRecordsAsync(coupons);
                await writer.FlushAsync();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<Result<MemoryStream, IEnumerable<string>>> GenerateReportAsCsvForEndpointAsync(string agentEmail)
        {
            var exist = _repository.GetCouponUsage().Where(c => c.IsUsed == true && c.Agent.Email==agentEmail);
            if (exist == null || !exist.Any())
            {
                return Result.Failure<MemoryStream, IEnumerable<string>>(new List<string> { "There is no data." });
            }
            IEnumerable<CSVDto> coupons = _mapper.Map<IEnumerable<CSVDto>>(exist);
            
            string filePath = @"C:\Users\Raso\Documents\CSVFiles\CouponUsageReport.csv";
            MemoryStream memoryStream = await CreateCsvReportAsync(coupons);
            return Result.Success<MemoryStream, IEnumerable<string>>(memoryStream);
        }
       
        public async Task<MemoryStream> GenerateReportAsCsvForEmail(string agentEmail)
        {
            var discountsUsed = _repository.GetCouponUsage().Where(c => c.IsUsed == true && c.Agent.Email == agentEmail);
           
            IEnumerable<CSVDto> coupons = _mapper.Map<IEnumerable<CSVDto>>(discountsUsed);

            string filePath = @"C:\Users\Raso\Documents\CSVFiles\CouponUsageReport.csv";
            MemoryStream memoryStream = await CreateCsvReportAsync(coupons);
            return memoryStream;
        }

        public async Task<Result<IEnumerable<CSVDto>, IEnumerable<string>>> GenerateReportForAllAgents()
        {
            var exist = _repository.GetCouponUsage();
            if (exist == null || !exist.Any())
            {
                return Result.Failure<IEnumerable<CSVDto>, IEnumerable<string>>(new List<string> { "There is no data." });
            }
            IEnumerable<CSVDto> coupons = _mapper.Map<IEnumerable<CSVDto>>(exist);

            return Result.Success<IEnumerable<CSVDto>, IEnumerable<string>>(coupons);
        }

    }
}
