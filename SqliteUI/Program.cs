using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace SqliteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteCrud sql = new SQLiteCrud(GetConnectionString());
            

            //ReadAllContacts(sql);

            //ReadContact(sql,2);

            //CreateNewContact(sql);

            //UpdateContact(sql);

            //RemovePhoneNumberFromContact(sql, 1, 1);

            Console.WriteLine("Done Processing Sqlite");
            Console.ReadLine();
        }
        private static string GetConnectionString(string connectionStringName = "Default")
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            return config.GetConnectionString(connectionStringName);
        }
        private static void CreateNewContact(SQLiteCrud sql)
        {
            FullContactModel user = new FullContactModel
            {
                BasicInfo = new BasicContactModel
                {
                    FirstName = "Mustafa",
                    LastName = "Uzun"
                }
            };
            user.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "abc@xyz.com" });
            user.EmailAddresses.Add(new EmailAddressModel { Id = 2, EmailAddress = "me@utlumurat.com" });

            user.PhoneNumbers.Add(new PhoneNumberModel { Id = 1, PhoneNumber = "444-42231123" });
            user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-422311" });
            sql.CreateContact(user);
        }

        private static void ReadAllContacts(SQLiteCrud sql)
        {
            var rows = sql.GetAllContacts();
            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id} : {row.FirstName} {row.LastName}");
            }
        }

        private static void UpdateContact(SQLiteCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "Murti",
                LastName = "Utlu"
            };
            sql.UpdateContactName(contact);

        }
        private static void ReadContact(SQLiteCrud sql, int contactId)
        {
            var contact = sql.GetFullContactById(contactId);

            Console.WriteLine($"{contact.BasicInfo.Id} : {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");

        }

        private static void RemovePhoneNumberFromContact(SQLiteCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
        }
    }
}
