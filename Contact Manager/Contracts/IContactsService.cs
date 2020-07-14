using Models.Contact_Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact_Manager.Contracts
{
    public interface IContactsService
    {
        public List<Contact> ContactList { get; set; }
        public List<Contact> ParseContacts(string[] contacts);
        public void CreateContact(string name, string lastName, List<string> phones, string address);
        public Contact GetContactById(int id);
        public void UpdateContact(Contact contact, string name, string lastName, List<string> phones, string address);
        public void DeleteContact(Contact contact);
        public bool IsContactListEmpty();
        public bool IsPhoneUniqueGlobally(List<string> phones, string phone);
        public bool IsPhoneUniqueLocally(string phone);
        public string GetContactsData();
    }
}
