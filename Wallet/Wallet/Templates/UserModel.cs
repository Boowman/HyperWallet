using System;
using System.Collections.Generic;
using System.Text;

namespace Wallet
{
    public class UserModel
    {
        private string firstName;
        private string lastName;

        private string email;
        private string username;
        private string password;

        public string FirstName     { get => firstName; set => firstName = value; }
        public string LastName      { get => lastName; set => lastName = value; }
        public string Email         { get => email; set => email = value; }
        public string Username      { get => username; set => username = value; }
        public string Password      { get => password; set => password = value; }

        public UserModel(string firstN, string lastN, string userN, string passW, string eM)
        {
            this.firstName  = firstN;
            this.lastName   = lastN;
            this.username   = userN;
            this.password   = passW;
            this.email      = eM;
        }

        public UserModel(string userN, string passW, string eM)
        {
            this.username   = userN;
            this.password   = passW;
            this.email      = eM;
        }

        /// <summary>
        /// Check if the email provided is valid.
        /// </summary>
        public static bool EmailValidity(string email)
        {
            if (email.Contains("@"))
                return true;
            else
                return false;
        }
    }
}
