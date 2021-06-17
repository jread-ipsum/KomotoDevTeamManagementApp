using RepositoriesAndPOCOS.POCOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesAndPOCOS.Repository
{
    public class DevTeamRepo
    {
        private List<DevTeam> _listOfTeams = new List<DevTeam>();
        //Create
        public void AddTeamToList(DevTeam team)
        {
            _listOfTeams.Add(team);
        }

        //Read
        public  List<DevTeam> GetTeamList()
        {
            return _listOfTeams;
        }

        //Update
        public bool UpdateExisitingTeam(int originalTeamId, DevTeam newDevTeam)
        {
            DevTeam oldTeam = GetDevTeamById(originalTeamId);
            
            if(oldTeam != null)
            {
                oldTeam.TeamId = newDevTeam.TeamId;
                oldTeam.TeamName = newDevTeam.TeamName;
                oldTeam.Developers = newDevTeam.Developers;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete
        public bool RemoveTeamFromList(int teamId)
        {
            DevTeam devTeam = GetDevTeamById(teamId);

            if(devTeam == null)
            {
                return false;
            }
            int initialCount = _listOfTeams.Count;
            _listOfTeams.Remove(devTeam);

            if(initialCount > _listOfTeams.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Helpermethod
        public DevTeam GetDevTeamById(int id)
        {
            foreach(DevTeam devTeam in _listOfTeams)
            {
                if(devTeam.TeamId == id)
                {
                    return devTeam;
                }
            }
            return null;
        }

    }
}
