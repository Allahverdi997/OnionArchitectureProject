using RusMProjectApplication.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProject.Persistance.Settings
{
    public class UserSession:IUserSessionAble
    {
        public string SecretKey { get; set; }
    }
}
