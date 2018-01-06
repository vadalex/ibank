using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebClient.BankService;

namespace WebClient.Models
{
    public class CookieUserInfo
    {
        public HttpCookie Cookie { get; set; }
        public LoginModel LoginModel { get; set; }
    }
}