using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class ContactDate
    {
        private string first_name;
        private string last_name;

        public ContactDate (string first_name, string last_name)
        {
            this.first_name = first_name;
            this.last_name = last_name;
        }

        public string First_name
        {
            get
            {
                return first_name;
            }
            set
            {
                first_name = value;
            }
        }

        public string Last_name
        {
            get
            {
                return last_name;
            }
            set
            {
                last_name = value;
            }
        }
    }
}
