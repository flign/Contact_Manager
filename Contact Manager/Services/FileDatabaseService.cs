using Contact_Manager;
using Contact_Manager.Contracts;
using System;
using System.IO;

namespace Services.Contact_Manager
{
    public class FileDatabaseService : IDatabaseService
    {
        private string _filePath;
        public FileDatabaseService()
        {
            _filePath = Environment.CurrentDirectory + @"\" + Constants.FileName;
        }
        public string[] ReadContacts()
        {
            if (File.Exists(_filePath))
                return File.ReadAllLines(_filePath);
            else
                return null;
        }

        public void UpdateFile(string data)
        {
            File.WriteAllText(_filePath, data);
        }
    }
}
