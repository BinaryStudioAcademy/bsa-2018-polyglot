using System.Collections.Generic;
using System.Threading.Tasks;
using App;
using Polyglot.BusinessLogic;
using Polyglot.BusinessLogic.DTO;

namespace Polyglot.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        private List<TeamPrevDTO> _teams;
        public List<TeamPrevDTO> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
        }

        public async Task LoadTeams()
        {
            Teams = await HttpService.GetAsync<List<TeamPrevDTO>>("teams");

            foreach (var team in Teams)
            {
                foreach (var person in team.Persons)
                {
                    person.AvatarUrl = person.AvatarUrl == "/assets/images/default-avatar.jpg" ? "http://polyglotbsa.azurewebsites.net/assets/images/default-avatar.jpg" : person.AvatarUrl;
                }

                team.ListHeight = team.Persons.Count * 50;
            }
        }
    }
}
