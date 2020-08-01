using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models;
using Timesheets.Services.Commands.Accounts;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        public AccountsController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), 200)]
        public async Task<IActionResult> GetAccounts([FromQuery] AccountFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var query = new AccountQuery(Mapper.Map<AccountFilter>(filterDto));
            var accounts = await Mediator.Send(query, cancellationToken);

            return Ok(Mapper.Map<IEnumerable<AccountDto>>(accounts));
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccountDto), 200)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto,
            CancellationToken cancellationToken)
        {
            var command = new CreateAccountCommand(createAccountDto.Name);
            var account = await Mediator.Send(command, cancellationToken);

            return Ok(Mapper.Map<AccountDto>(account));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AccountDto), 200)]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id,
            [FromBody] UpdateAccountDto updateAccountDto, CancellationToken cancellationToken)
        {
            var command = new UpdateAccountCommand(id, updateAccountDto.Name);
            var account = await Mediator.Send(command, cancellationToken);

            return Ok(Mapper.Map<AccountDto>(account));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteAccountCommand(id);
            await Mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}