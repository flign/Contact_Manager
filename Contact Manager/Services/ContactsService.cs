using Contact_Manager.Contracts;
using Models.Contact_Manager;
using System.Collections.Generic;
using System.Linq;

namespace Services.Contact_Manager
{
    public class ContactsService : IContactsService
    {
        public List<Contact> ContactList { get; set; }

        public ContactsService()
        {
            ContactList = new List<Contact>();
        }

        public List<Contact> ParseContacts(string[] contacts)
        {
            List<Contact> contactList = new List<Contact>();

            foreach(string contact in contacts)
            {
                string[] contactData = contact.Split(",");
                contactData = contactData.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                if (contactData.Length >= 3)
                {
                    List<string> contactPhones = new List<string>(contactData.Skip(3).Take(contactData.Length - 4).ToArray());

                    Contact newContact = new Contact
                    (
                        contactData[1],
                        contactData[2],
                        contactPhones,
                        contactData[contactData.Length - 1]
                    );

                    contactList.Add(newContact);
                }
            }

            return contactList;
        }

        public void CreateContact(string name, string lastName, List<string> phones, string address)
        {
            Contact contact = new Contact(name, lastName, phones, address);
            ContactList.Add(contact);
        }

        public Contact GetContactById(int id) => ContactList.Where(c => c.ContactID == id).FirstOrDefault();

        public void UpdateContact(Contact contact, string name, string lastName, List<string> phones, string address)
        {
            if(!(name.Equals(string.Empty)))
                contact.Name = name;
            if (!(lastName.Equals(string.Empty)))
                contact.LastName = lastName;
            if (!(phones.Count == 0) && DoesPhoneListContainsDuplicateNumbers(phones))
                contact.Phones = phones;
            contact.Address = address;
        }

        public void DeleteContact(Contact contact)
        {
            ContactList.Remove(contact);
        }

        public bool IsContactListEmpty() => ContactList.DefaultIfEmpty() == null ? true : false;

        public bool IsPhoneUniqueGlobally(List<string> phones, string phone) => phones.FindIndex(c => c.Contains(phone)) >= 0 ? false : true;

        public bool IsPhoneUniqueLocally(string phone) => ContactList.FindIndex(c => c.Phones.Contains(phone)) >= 0 ? false : true;

        public bool DoesPhoneListContainsDuplicateNumbers(List<string> phones)
        {
            foreach (string phone in phones)
            {
                if (IsPhoneUniqueLocally(phone))
                    return true;
            }
            return false;
        }

        public string GetContactsData()
        {
            string contactsData = string.Empty;

            foreach (Contact contact in ContactList)
            {
                string phones = string.Empty;
                foreach (string phone in contact.Phones)
                {
                    phones += phone;
                    phones += ",";
                }

                contactsData += string.Format($"{contact.ContactID},{contact.Name},{contact.LastName},{phones}{contact.Address}\n");
            }

            return contactsData;
        }
    }
}
