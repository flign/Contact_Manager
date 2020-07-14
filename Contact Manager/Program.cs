using Contact_Manager.Contracts;
using Services.Contact_Manager;

namespace Contact_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseService fileDatabaseService = new FileDatabaseService();
            IContactsService contactsService = new ContactsService();
            IUIService consoleUIService = new ConsoleUIService(contactsService, fileDatabaseService);

            string[] contactsData = fileDatabaseService.ReadContacts();
            if (contactsData != null)
                contactsService.ContactList = contactsService.ParseContacts(contactsData);

            while (true)
            {
                consoleUIService.PrintMessage(Constants.MenuMessage);
                int input = consoleUIService.GetInput();
                consoleUIService.ProcessInput(input);
            }
        }
    }
}
