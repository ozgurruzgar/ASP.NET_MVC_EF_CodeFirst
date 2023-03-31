using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_EF_CodeFirst.Models.Managers
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new GenerateDatabase());
        }
    }

    public class GenerateDatabase : CreateDatabaseIfNotExists<DatabaseContext>
    {
        //Initialize database veritabanı oluşturmadan önce yapmak bir işlem yapmak istersek override etmemiz gereken class
        //Seed ise veritabanı oluştuktan sonra bir işlem yapmak istersek override etmemiz gereken class
        protected override void Seed(DatabaseContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                Person person = new Person();
                person.FirstName = FakeData.NameData.GetFirstName();
                person.LastName = FakeData.NameData.GetSurname();
                person.Age = FakeData.NumberData.GetNumber(10, 90);

                context.Persons.Add(person);
            }

            context.SaveChanges();

            List<Person> GetPersons = context.Persons.ToList();

            foreach (var persons in GetPersons)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1,5); i++)
                {
                    Address address = new Address();
                    address.AddressDetail = FakeData.PlaceData.GetAddress();
                    address.Person = persons;

                    context.Addresses.Add(address);
                }
            }
            context.SaveChanges();
        }
    }
}