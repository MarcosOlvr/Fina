using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandlerAsync)
            .WithName("Transactions: Get by period")
            .WithSummary("Recupera transações feitas dentro de uma data")
            .WithDescription("Recupera transação feitas dentro de uma data")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>>>();

        private static async Task<IResult> HandlerAsync(
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionByPeriodRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetByPeriodAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
