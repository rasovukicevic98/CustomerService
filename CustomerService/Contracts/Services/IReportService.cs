using CSharpFunctionalExtensions;
using CustomerService.Dtos;

namespace CustomerService.Contracts.Services
{
    public interface IReportService
    {
        Task<MemoryStream> GenerateReportAsCsvForEmail(string agentEmail);
        Task<Result<MemoryStream, IEnumerable<string>>> GenerateReportAsCsvForEndpointAsync(string agentEmail);
        Task<Result<IEnumerable<CSVDto>, IEnumerable<string>>> GenerateReportForAllAgents();
    }
}
