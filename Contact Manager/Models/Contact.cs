using System.Collections.Generic;
using System.Threading;

namespace Models.Contact_Manager
{
    public class Contact
    {
        private static int contactID;
        public int ContactID { get; private set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<string> Phones { get; set; }
        public string Address { get; set; }

        public Contact(string name, string lastName, List<string> phone, string address)
        {
            Name = name;
            LastName = lastName;
            Phones = phone;
            Address = address;
            ContactID = Interlocked.Increment(ref contactID);
        }
    }
}
