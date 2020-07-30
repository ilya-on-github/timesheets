using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountQuery _accountQuery;

        public AccountsController(IAccountQuery accountQuery, IMapper mapper)
            : base(mapper)
        {
            _accountQuery = accountQuery ?? throw new ArgumentNullException(nameof(accountQuery));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), 200)]
        public async Task<IActionResult> GetAccounts([FromQuery] AccountFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var filter = Mapper.Map<AccountFilter>(filterDto);
            var accounts = await _accountQuery.Execute(filter, cancellationToken);

            return Ok(Mapper.Map<IEnumerable<AccountDto>>(accounts));
        }
    }
}