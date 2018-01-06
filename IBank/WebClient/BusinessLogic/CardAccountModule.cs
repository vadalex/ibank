using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebClient.BusinessLogic
{
    public static class CardAccountModule
    {
        public static string ConvertCardNumberString(string cardNumber)
        {
            string newNumberStr = cardNumber.Substring(0, 4) + "********" + cardNumber.Substring(12);            
            return newNumberStr;
        }
    }
}