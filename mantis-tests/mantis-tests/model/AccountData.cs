﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class AccountData
    {
        private string username;
        private string password;
        //private string email;

        public AccountData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public AccountData()
        {
        }

        public string Name
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
