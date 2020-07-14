
namespace Contact_Manager.Contracts
{
    public interface IDatabaseService
    {
        public string[] ReadContacts();
        public void UpdateFile(string data);
    }
}
