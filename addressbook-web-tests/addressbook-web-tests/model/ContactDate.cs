using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactDate : IEquatable<ContactDate>, IComparable<ContactDate>
    {
        private string allPhones;
        private string allEmails;

        public ContactDate (string first_name, string last_name)
        {
            First_name = first_name;
            Last_name = last_name;
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
            //return First_name.CompareTo(other.First_name) + Last_name.CompareTo(other.Last_name);
            if (First_name.CompareTo(other.First_name) == 1)
            {
                return Last_name.CompareTo(other.Last_name);
            }
            else return 0;
        }

        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email_1 { get; set; }
        public string Email_2 { get; set; }
        public string Email_3 { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmails(Email_1) + CleanUpEmails(Email_2) + CleanUpEmails(Email_3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            //return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
        }

        private string CleanUpEmails(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }
    }
}
