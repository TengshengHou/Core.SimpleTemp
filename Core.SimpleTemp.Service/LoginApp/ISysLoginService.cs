using Core.SimpleTemp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service
{
    public interface ISysLoginService
    {
        Task<bool> LoginAsync(HttpContext context, SysUser sysUser);
        Task SignOutAsync(HttpContext context);
       
    }
}
