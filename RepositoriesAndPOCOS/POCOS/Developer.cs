using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesAndPOCOS.POCOS
{
    public class Developer
    {
        public int IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasAccessToPluralsight { get; set; }

        public Developer() { }

        public Developer(int idNumber, string firstName, string lastName,  bool hasAccessToPluralsight)
        {
            IdNumber = idNumber;
            FirstName = firstName;
            LastName = lastName;
            HasAccessToPluralsight = hasAccessToPluralsight;
        }
    }
}
