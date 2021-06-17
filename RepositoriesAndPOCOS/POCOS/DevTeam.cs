using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesAndPOCOS.POCOS
{
    public class DevTeam
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public List<Developer> Developers { get; set; }
        

        public DevTeam() { }

        public DevTeam(int teamId, string teamName, List<Developer> developers)
        {
            TeamId = teamId;
            TeamName = teamName;
            Developers = developers;
        }
    }
}
