using RepositoriesAndPOCOS.POCOS;
using RepositoriesAndPOCOS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoDevTeamManagementApp
{
    public class AppUI
    {
        private DeveloperRepo _developerRepo = new DeveloperRepo();
        private DevTeamRepo _devTeamRepo = new DevTeamRepo(); 
        public void Run()
        {
            SeedDeveloperList();
            Menu();
        }

        //Menu
        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Select a menu option:\n" +
                    "1. Add New Developer\n" +
                    "2. View Developer Directory\n" +
                    "3. Update Existing Developers\n" +
                    "4. Delete Existing Developers\n" +
                    "5. Add New Developer Team\n" +
                    "6. View Developer Teams Directory\n" +
                    "7. Update Existing Developer Teams\n" +
                    "8. Exit\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateNewDeveloper();
                        break;
                    case "2":
                        DisplayAllDevelopers();
                        break;
                    case "3":
                        UpdateExistingDeveloper();
                        break;
                    case "4":
                        DeleteExistingDeveloper();
                        break;
                    case "5":
                        CreateNewTeam();
                        break;
                    case "6":
                        DisplayTeamDirectory();
                        break;
                    case "7":
                        UpdateExistingTeam();
                        break;
                    case "8":
                        Console.WriteLine("Goodbye");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }

                Console.WriteLine("Please press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        private void CreateNewDeveloper()
        {
            Console.Clear();

            Developer newDeveloper = new Developer();

            Console.WriteLine("Enter an Id number");
            string idAsString = Console.ReadLine();
            int idAsInt = int.Parse(idAsString);
            newDeveloper.IdNumber = idAsInt;                                                        //try having user assign id number then have a check to see if id number already exists if exists enter new number.

            Console.WriteLine("Enter the developers first name");
            newDeveloper.FirstName = Console.ReadLine();

            Console.WriteLine("Enter the developers last name");
            newDeveloper.LastName = Console.ReadLine();

            Console.WriteLine("Does the developer have access to Pluralsight? Enter yes or no");
            string pluralsight = Console.ReadLine().ToLower();

            if (pluralsight == "yes")
            {
                newDeveloper.HasAccessToPluralsight = true;
            }
            else
            {
                newDeveloper.HasAccessToPluralsight = false;
            }
            _developerRepo.AddDevelopersToList(newDeveloper);
        }

        private void DisplayAllDevelopers()
        {
            Console.Clear();
            List<Developer> listOfDevelopers = _developerRepo.GetDevelopersList();
            foreach(Developer developer in listOfDevelopers)
            {
                Console.WriteLine($"Id Number: {developer.IdNumber}\n" +
                    $" Name: {developer.FirstName} {developer.LastName}\n" +
                    $" Access to Pluralsight: {developer.HasAccessToPluralsight}\n" +
                    $"--------------------------");
            }
        }

        private void UpdateExistingDeveloper()
        {
            DisplayAllDevelopers();

            Console.WriteLine("\nEnter the Id number of the developer you would like to update:\n");
            string oldIdAsString = Console.ReadLine();
            int oldIdAsInt = int.Parse(oldIdAsString);                                                  //should I create a catch if the developer I want to update does not already exist, or is it fine to just create a new developer from this method
            Developer developer = _developerRepo.GetDeveloperById(oldIdAsInt);

            if (developer == null)
            {
                Console.WriteLine("Sorry, no developer found with that Id. ");
            }
            else
            {
                bool keepRunning = true;
                while (keepRunning)
                {
                    Console.Clear();

                    Console.WriteLine($"{developer.IdNumber}\n" +
                        $" {developer.FirstName} {developer.LastName}\n" +
                        $"Access to Pluralsight: {developer.HasAccessToPluralsight}\n" +
                        $"-------------------------------------");

                    Console.WriteLine("Select option:\n" +
                        "1. Update Name\n" +
                        "2. Update Access to Pluralsight\n" +
                        "3. Delete this developer\n" +
                        "4. Finished updating.\n");

                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":

                            Console.WriteLine("Enter developers first name");
                            developer.FirstName = Console.ReadLine();

                            Console.WriteLine("\nEnter developers last name");
                            developer.LastName = Console.ReadLine();

                            bool nameUpdated = _developerRepo.UpdateExistingDevelopers(oldIdAsInt, developer);

                            if (nameUpdated)
                            {
                                Console.WriteLine("\nDeveloper was successfully updated!");
                            }
                            else
                            {
                                Console.WriteLine("\nCould not update developer.");
                            }
                            break;

                        case "2":

                            Console.WriteLine("Does the developer have access to Pluralsight? Enter yes or no");
                            string pluralsight = Console.ReadLine().ToLower();

                            if (pluralsight == "yes")
                            {
                                developer.HasAccessToPluralsight = true;
                            }
                            else
                            {
                                developer.HasAccessToPluralsight = false;
                            }

                            bool pluralsightUpdated = _developerRepo.UpdateExistingDevelopers(oldIdAsInt, developer);

                            if (pluralsightUpdated)
                            {
                                Console.WriteLine("\nDeveloper was successfully updated!");
                            }
                            else
                            {
                                Console.WriteLine("\nCould not update developer.");
                            }
                            break;

                        case "3":

                            bool wasDeleted = _developerRepo.RemoveDeveloperFromList(oldIdAsInt);

                            if (wasDeleted)
                            {
                                Console.WriteLine("\nThe developer was successfully deleted.");
                            }
                            else
                            {
                                Console.WriteLine("\nThe developer could not be deleted or does not exist");
                            }
                            break;

                        case "4":
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Please enter a valid number.");
                            break;
                    }
                }
            }
        }

        private void DeleteExistingDeveloper()
        {
            DisplayAllDevelopers();

            Console.WriteLine("\nEnter the Id number of the developer you would like to delete:");
            string idAsString = Console.ReadLine();
            int idAsInt = int.Parse(idAsString);

            bool wasDeleted = _developerRepo.RemoveDeveloperFromList(idAsInt);

            if (wasDeleted)
            {
                Console.WriteLine("\nThe developer was successfully deleted.");
            }
            else
            {
                Console.WriteLine("\nThe developer could not be deleted or does not exist");
            }
        }

        private void CreateNewTeam()
        {
            Console.Clear();
            DevTeam newDevTeam = new DevTeam();
            newDevTeam.Developers = new List<Developer>();

            Console.WriteLine("Enter an Id number for the new team");      
            string idAsString = Console.ReadLine();
            int idAsInt = int.Parse(idAsString);
            newDevTeam.TeamId = idAsInt;

            Console.WriteLine("Enter a name for the new team");
            newDevTeam.TeamName = Console.ReadLine();

            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Add developers to team? Enter Yes or No.");
                 string inputAdd = Console.ReadLine().ToLower();

                switch (inputAdd)
                {
                    case "yes":

                        Console.WriteLine("List of all developers:\n");

                        DisplayAllDevelopers();

                        Console.WriteLine("\nEnter developer Id number");
                        string developerIdAsString = Console.ReadLine();
                        int developerIdAsInt = int.Parse(developerIdAsString);

                        Developer developer = _developerRepo.GetDeveloperById(developerIdAsInt);
                        if (developer != null)
                        {
                            newDevTeam.Developers.Add(developer);
                            Console.WriteLine("Developer has been added to team.\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Sorry, no developer with that Id.\n");
                        }
                        break;

                    case "no":
                        Console.WriteLine("Team has been created.");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter Yes or No.");
                        break;
                }
            }
            _devTeamRepo.AddTeamToList(newDevTeam);                          
            
        }

        private void DisplayTeamDirectory()
        {
            Console.Clear();
            List<DevTeam> listOfDevTeams = _devTeamRepo.GetTeamList();
            foreach(DevTeam devTeam in listOfDevTeams)
            {
                Console.WriteLine($"{devTeam.TeamId} {devTeam.TeamName}");

                if (devTeam.Developers != null)
                {
                    foreach (Developer developer in devTeam.Developers)
                    {
                        Console.WriteLine($" Developer:   {developer.IdNumber} {developer.FirstName} {developer.LastName}");
                    }
                    Console.WriteLine("-----------------------------");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        private void UpdateExistingTeam()
        {
            DisplayTeamDirectory();

            Console.WriteLine("\nEnter the Id number for team you would like to update.\n");                                                                                         
            string oldIdAsString = Console.ReadLine();
            int oldIdAsInt = int.Parse(oldIdAsString);

            DevTeam devTeam = _devTeamRepo.GetDevTeamById(oldIdAsInt);

            if (devTeam == null)
            {
                Console.WriteLine("Sorry, no team with that Id.");
            }
            else
            {
                bool keepRunning = true;
                while (keepRunning)
                {
                    Console.WriteLine("\nSelect an option:\n" +
                        "1. Change team name\n" +
                        "2. Add developers to team.\n" +
                        "3. Remove developers from team.\n" +
                        "4. Finished.\n");

                    string inputUpdate = Console.ReadLine();

                    switch (inputUpdate)
                    {
                        case "1":

                            Console.WriteLine("Enter a new team name");
                            devTeam.TeamName = Console.ReadLine();
                            break;

                        case "2":

                            Console.WriteLine("List of all developers:\n");

                            DisplayAllDevelopers();

                            Console.WriteLine("Enter developer Id number");
                            string developerIdAsString = Console.ReadLine();
                            int developerIdAsInt = int.Parse(developerIdAsString);

                            Developer developer = _developerRepo.GetDeveloperById(developerIdAsInt);
                            if (developer != null)
                            {
                                devTeam.Developers.Add(developer);
                                Console.WriteLine("Developer has been added to team.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Sorry, no developer with that Id");
                                Console.ReadKey();
                            }

                            Console.Clear();
                            break;

                        case "3":

                            DisplayAllDevelopers();                                                             //how do I display just developers in called team?

                            Console.WriteLine("Enter developer Id number");
                            string deleteDeveloperIdAsString = Console.ReadLine();
                            int deleteDeveloperIdAsInt = int.Parse(deleteDeveloperIdAsString);

                            Developer deleteDeveloper = _developerRepo.GetDeveloperById(deleteDeveloperIdAsInt);
                            if (deleteDeveloper != null)
                            {
                                devTeam.Developers.Remove(deleteDeveloper);
                                Console.WriteLine("Developer has been removed from team");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Sorry, no developer with that Id");
                            }

                            Console.Clear();
                            break;

                        case "4":
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Please enter a valid number.");
                            break;

                    }
                }
                bool wasUpdated = _devTeamRepo.UpdateExisitingTeam(oldIdAsInt, devTeam);

                if (wasUpdated)
                {
                    Console.WriteLine("Team was successfully updated!");
                }
                else
                {
                    Console.WriteLine("Could not update team.");
                }
            }
        }


        //Seed method
        private void SeedDeveloperList()
        {
            Developer developer1 = new Developer(33, "Jordan", "Read", true);
            Developer developer2 = new Developer(123, "Nidya", "Sanchez", false);
            Developer developer3 = new Developer(543, "Collin", "Meck", true);

            _developerRepo.AddDevelopersToList(developer1);
            _developerRepo.AddDevelopersToList(developer2);
            _developerRepo.AddDevelopersToList(developer3);
        }

        /*private void SeedDevTeamList()
        {
            DevTeam devTeam1 = new DevTeam(11, "DreamTeam",)       //?? how do I seed the 'List<Developer> parameter?
        }*/
    }
}
