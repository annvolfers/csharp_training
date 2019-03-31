using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactDate : IEquatable<ContactDate>, IComparable<ContactDate>
    {
        private string first_name;
        private string last_name;

        public ContactDate (string first_name, string last_name)
        {
            this.first_name = first_name;
            this.last_name = last_name;
        }

        public bool Equals(ContactDate other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return First_name == other.First_name && Last_name == other.Last_name;
        }

        public int GetHashCodeFirst_name()
        {
            return First_name.GetHashCode();
        }

        public int GetHashCodeLast_name()
        {
            return Last_name.GetHashCode();
        }

        public override string ToString()
        {
            return "first_name=" + First_name + " and " + "last_name=" + Last_name;
        }

        public int CompareTo(ContactDate other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return First_name.CompareTo(other.First_name) + Last_name.CompareTo(other.Last_name);
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
