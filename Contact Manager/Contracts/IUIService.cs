using Models.Contact_Manager;
using System.Collections.Generic;

namespace Contact_Manager.Contracts
{
    public interface IUIService
    {
        public void PrintMessage(string message);
        public int GetInput();
        public void ProcessInput(int input);
        public void PrintAllContacts(List<Contact> contacts);
    }
}
