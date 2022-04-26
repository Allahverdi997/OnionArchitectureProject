using RusMProjectApplication.Abstraction;
using RusMProjectApplication.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProject.Infrastructure.Services
{
    public class FilterManager:IFilterServiceAble
    {
        public  IUserSessionAble UserSessionAble { get; set; }
        public FilterManager(IUserSessionAble userSessionAble)
        {
            UserSessionAble = userSessionAble;
        }

        public void SetSecretKey(string secretKey)
        {
            if (!string.IsNullOrEmpty(secretKey))
            {
                UserSessionAble.SecretKey = secretKey;
            }
        }
    }
}
