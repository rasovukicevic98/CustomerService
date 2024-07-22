using CSharpFunctionalExtensions;
using CustomerService.Dtos;

namespace CustomerService.Contracts.Services
{
    public interface IReportService
    {
        Task<Result<MemoryStream, IEnumerable<string>>> GenerateReportAsCsv();
        Task<Result<IEnumerable<CSVDto>, IEnumerable<string>>> GenerateReportForAllAgents();
    }
}
