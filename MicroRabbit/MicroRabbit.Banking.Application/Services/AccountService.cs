using MicroRabbit.Banking.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Banking.Domain.Interfaces;

namespace MicroRabbit.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }
    }
}
