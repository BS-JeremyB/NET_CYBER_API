using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.BLL.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(Utilisateur utilisateur);
    }
}
