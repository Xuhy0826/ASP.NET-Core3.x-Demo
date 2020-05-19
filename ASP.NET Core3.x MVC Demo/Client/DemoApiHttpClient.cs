using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ASP.NET_Core3.x_MVC_Demo.Extensions;
using Demo.Dto.Dtos;

namespace ASP.NET_Core3.x_MVC_Demo.Client
{
    public class DemoApiHttpClient
    {
        private readonly HttpClient _httpClient;

        public DemoApiHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(EmployeeQueryDto query)
        {
            string queryString = null;
            if (query != null)
            {
                queryString = query.QueryStringFormat();
            }
            var relativeUri = queryString == null ? $"/api/employees" : $"/api/employees?{queryString}";
            var jsonAsync = await _httpClient.GetStringAsync(relativeUri);
            return JsonSerializer.Deserialize<IEnumerable<EmployeeDto>>(jsonAsync,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true, });
        }
    }
}
