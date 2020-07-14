using Contact_Manager;
using Contact_Manager.Contracts;
using Models.Contact_Manager;
using System;
using System.Collections.Generic;

namespace Services.Contact_Manager
{
    public class ConsoleUIService : IUIService
    {
        private IContactsService _contactsService;
        private IDatabaseService _databaseService;

        public ConsoleUIService(IContactsService contactsService, IDatabaseService databaseService)
        {
            _contactsService = contactsService;
            _databaseService = databaseService;
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public int GetInput()
        {
            bool success = int.TryParse(Console.ReadLine(), out int input);

            if (!success)
                PrintMessage(Constants.InvalidParameterMessage);
            return input;
        }

        public void ProcessInput(int input)
        {
            switch (input)
            {
                case 1:
                    {
                        PrintMessage(Constants.NameMessage);
                        string name = Console.ReadLine();
                        PrintMessage(Constants.LastNameMessage);
                        string lastName = Console.ReadLine();
                        List<string> phones = GetPhoneList();
                        PrintMessage(Constants.AddressMessage);
                        string address = Console.ReadLine();
                        if (address.Equals(string.Empty))
                            address = " ";

                        if (name.Equals(string.Empty) || lastName.Equals(string.Empty))
                        {
                            PrintMessage(Constants.InvalidParameterMessage);
                            break;
                        }


                        try
                        {
                            _contactsService.CreateContact(name, lastName, phones, address);
                        }
                        catch
                        {
                            Console.WriteLine(Constants.InvalidParameterMessage);
                        }

                        string data = _contactsService.GetContactsData();
                        _databaseService.UpdateFile(data);

                        break;
                    }
                case 2:
                    {
                        PrintMessage(Constants.UpdateMessage);
                        int contactId = int.Parse(Console.ReadLine());
                        Contact contact = _contactsService.GetContactById(contactId);
                        if(contact == null)
                        {
                            PrintMessage(Constants.IdNotFoundMessage);
                            break;
                        }

                        PrintMessage(Constants.NameMessage);
                        string name = Console.ReadLine();

                        PrintMessage(Constants.LastNameMessage);
                        string lastName = Console.ReadLine();

                        if (name.Equals(string.Empty) || lastName.Equals(string.Empty))
                        {
                            PrintMessage(Constants.InvalidParameterMessage);
                            break;
                        }

                        List<string> phones = GetPhoneList();

                        PrintMessage(Constants.AddressMessage);
                        string address = Console.ReadLine();

                        _contactsService.UpdateContact(contact, name, lastName, phones, address);
                        string data = _contactsService.GetContactsData();
                        _databaseService.UpdateFile(data);

                        break;
                    }
                case 3:
                    {
                        PrintMessage(Constants.DeleteMessage);
                        int contactId = int.Parse(Console.ReadLine());
                        Contact contact = _contactsService.GetContactById(contactId);
                        if (contact == null)
                        {
                            PrintMessage(Constants.IdNotFoundMessage);
                            break;
                        }

                        _contactsService.DeleteContact(contact);

                        string data = _contactsService.GetContactsData();
                        _databaseService.UpdateFile(data);
                        break;
                    }
                case 4:
                    {
                        List<Contact> contacts = _contactsService.ContactList;
                        PrintAllContacts(contacts);
                        break;
                    }
            }

        }

        public void PrintAllContacts(List<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine();
                Console.WriteLine(Constants.IdMessage + contact.ContactID);
                Console.WriteLine(Constants.NameMessage + contact.Name);
                Console.WriteLine(Constants.LastNameMessage + contact.LastName);
                foreach (var phone in contact.Phones)
                {
                    Console.WriteLine(Constants.PhoneMessage + phone);
                }
                Console.WriteLine(Constants.AddressMessage + contact.Address);
                Console.WriteLine("");
            }
        }

        private List<string> GetPhoneList()
        {
            List<string> phoneList = new List<string>();

            while (true)
            {
                PrintMessage(Constants.PhoneMessage);
                string phone = Console.ReadLine();

                if (phone.Equals(string.Empty))
                {
                    if (_contactsService.IsContactListEmpty() || phoneList.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                else
                {
                    
                    if (_contactsService.IsPhoneUniqueLocally(phone) && _contactsService.IsPhoneUniqueGlobally(phoneList, phone))
                    {
                        phoneList.Add(phone);
                    }
                    else
                    {
                        PrintMessage(Constants.DuplicatePhoneMessage);
                    }
                    continue;
                }
            }

            return phoneList;
        }
    }
}
