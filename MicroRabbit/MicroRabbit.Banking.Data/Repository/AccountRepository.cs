using MicroRabbit.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Banking.Data.Context;

namespace MicroRabbit.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDBContext _ctx;
        public AccountRepository(BankingDBContext ctx)
        {
            this._ctx = ctx;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;
        }
    }
}
