using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpenseManagement.Domain.AggregatesModel.Person
{
    public class Person : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Age { get; private set; }

        public Person(string name, int age)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetAge(age);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
                throw new ArgumentException("Invalid name");

            Name = name;
        }

        public void SetAge(int age)
        {
            if (age < 0)
                throw new ArgumentException("Invalid age");

            Age = age;
        }

        public bool IsMinor() => Age < 18;
    }
}
