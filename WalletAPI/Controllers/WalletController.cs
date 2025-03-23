using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using WalletAPI.Models;
using WalletAPI.Models.RequestArgs;

namespace WalletAPI.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletController : ControllerBase
    {
        private static ConcurrentDictionary<Guid, Wallet> Players = new();

        [HttpPost("register/{playerId}")]
        public IActionResult RegisterPlayer(Guid playerId)
        {
            if (!Players.TryAdd(playerId, new Wallet()))
            {
                return Conflict("Player has already been registered.");
            }
            return Ok();
        }

        [HttpPut("transaction/{playerId}")]
        public async Task<IActionResult> CreditTransaction(Guid playerId, [FromBody] TransactionRequest request)
        {
            if (!Players.TryGetValue(playerId, out var wallet))
            {
                return NotFound("The player was not found.");
            }

            bool result = await wallet.HandleTransactionAsync(request.TransactionId, request.Type, request.Amount);

            if (!result)
            {
                return BadRequest("Rejected");
            }

            return  Ok("Accepted");
        }

        [HttpGet("balance/{playerId}")]
        public IActionResult GetBalance(Guid playerId)
        {
            if (!Players.TryGetValue(playerId, out var wallet))
            {
                return NotFound("The player was not found.");
            }

            var balance = wallet.Balance;

            return Ok(new { balance });
        }

        [HttpGet("transactions/{playerId}")]
        public IActionResult GetTransactions(Guid playerId)
        {
            if (!Players.TryGetValue(playerId, out var wallet))
            {
                return NotFound("The player was not found.");
            }
            return Ok(wallet.GetTransactions());
        }
    }
}