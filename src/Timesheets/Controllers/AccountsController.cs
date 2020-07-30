using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        public AccountsController(IMapper mapper, Mediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), 200)]
        public async Task<IActionResult> GetAccounts([FromQuery] AccountFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var filter = Mapper.Map<AccountFilter>(filterDto);
            var accounts = await Mediator.Send(new AccountQuery(filter), cancellationToken);

            return Ok(Mapper.Map<IEnumerable<AccountDto>>(accounts));
        }
    }
}