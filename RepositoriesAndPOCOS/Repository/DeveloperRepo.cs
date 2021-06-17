using RepositoriesAndPOCOS.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesAndPOCOS.Repository
{
    public class DeveloperRepo
    {
        private List<Developer> _listOfDevelopers = new List<Developer>();               

        //Create
        public void AddDevelopersToList(Developer developer)
        {
            _listOfDevelopers.Add(developer);
        }
        
        //Read
        public List<Developer> GetDevelopersList()
        {
            return _listOfDevelopers;
        }

        //Update
        public bool UpdateExistingDevelopers(int originalId, Developer newDeveloper)
        {
            Developer oldDeveloper = GetDeveloperById(originalId); 

            if(oldDeveloper != null)                                                           
            {
                oldDeveloper.IdNumber = newDeveloper.IdNumber;
                oldDeveloper.FirstName = newDeveloper.FirstName;
                oldDeveloper.LastName = newDeveloper.LastName;
                oldDeveloper.HasAccessToPluralsight = newDeveloper.HasAccessToPluralsight;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete
        public bool RemoveDeveloperFromList(int idNumber)
        {
            Developer developer = GetDeveloperById(idNumber);

            if (developer == null)         
            {
                return false;
            }

            int initialCount = _listOfDevelopers.Count;
            _listOfDevelopers.Remove(developer);

            if(initialCount > _listOfDevelopers.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Helper
        public Developer GetDeveloperById(int idNumber)
        {
            foreach(Developer developer in _listOfDevelopers)
            {
                if (developer.IdNumber == idNumber)
                {
                    return developer;
                }
            }
            return null;
        }
    }
}
