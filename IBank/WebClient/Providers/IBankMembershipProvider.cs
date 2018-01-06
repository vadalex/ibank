using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using DAL;

namespace WebClient.Providers
{
    public class IBankMembershipProvider : SimpleMembershipProvider
    {
        private Repositories _repo = new Repositories();

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Argument cannot be null or empty", "username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Argument cannot be null or empty", "password");
            }

            var user = _repo.Customers.GetAll(c => (c.Login == username) && (c.Passoword == password)).ToList();
            if (user.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}