using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Timesheets.Jira.Models;

namespace Timesheets.Jira
{
    public class Jira
    {
        public readonly TimeSpan TimeOffset;
        private readonly IRestClient _client;

        public Jira(IRestClient client, TimeSpan timeOffset)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            TimeOffset = timeOffset;
        }

        public async Task<List<Worklog>> FindWorklogs(DateTime fromDate, DateTime toDate,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest("/rest/tempo-timesheets/4/worklogs/search", Method.POST);

            request.AddJsonBody(new
            {
                from = $"{fromDate.Date:yyyy-MM-dd}",
                to = $"{toDate.Date:yyyy-MM-dd}"
            });

            var response = await _client.ExecuteAsync<List<Worklog>>(request, cancellationToken);

            return Result(response);
        }

        public async Task<List<Account>> GetAccounts(CancellationToken cancellationToken)
        {
            var request = new RestRequest("/rest/tempo-accounts/1/account", Method.GET);

            var response = await _client.ExecuteAsync<List<Account>>(request, cancellationToken);

            return Result(response);
        }

        private static T Result<T>(IRestResponse<T> response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new NotImplementedException(
                    new StringBuilder("Unexpected response from Jira.")
                        .AppendLine(
                            $"Request method: '{response.Request.Method}', resource: '{response.Request.Resource}'.")
                        .AppendLine($"Response code: '{response.StatusCode}', content: '{response.Content}'.")
                        .ToString(),
                    response.ErrorException);
            }

            return response.Data;
        }
    }
}