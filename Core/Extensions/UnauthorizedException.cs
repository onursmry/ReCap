using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public class UnauthorizedException : Exception
    {
        public override string Message => "Devam etmek için sisteme giriş yapmalısınız";
    }
}
