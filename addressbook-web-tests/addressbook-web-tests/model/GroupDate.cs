using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]   
    public class GroupDate : IEquatable<GroupDate>, IComparable<GroupDate>
    {
        public GroupDate()
        {
        }

        public GroupDate (string name)
        {
            Name = name;
        }

        public bool Equals(GroupDate other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "\nname=" + Name + "\nheader=" + Header + "\nfooter=" + Footer;
        }

        public int CompareTo(GroupDate other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);  
        }

        [Column (Name = "group_name")]
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; }

        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<GroupDate> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactDate> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }

        public List<ContactDate> GetContactsNotInGroups()
        {
            //select * from addressbook where addressbook.deprecated = '0000-00-00 00:00:00' 
            //and not exists (select * from address_in_groups where address_in_groups.id = addressbook.id)
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(c => c.Deprecated == "0000-00-00 00:00:00")
                        where !db.GCR.Any(g => (g.ContactId == c.Id))
                        select c).Distinct().ToList();
            }
        }
    }
}
