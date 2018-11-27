using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp
{
    public class MemoryCacheTicketStore : ITicketStore
    {
        Task ITicketStore.RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        Task ITicketStore.RenewAsync(string key, AuthenticationTicket ticket)
        {
            throw new NotImplementedException();
        }

        Task<AuthenticationTicket> ITicketStore.RetrieveAsync(string key)
        {
            throw new NotImplementedException();
        }

        Task<string> ITicketStore.StoreAsync(AuthenticationTicket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
