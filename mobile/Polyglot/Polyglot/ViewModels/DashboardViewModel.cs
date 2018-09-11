using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using Polyglot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Polyglot.BusinessLogic.Services;
using Xamarin.Forms;

namespace Polyglot.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private ProjectViewModel _selectedProject;
        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                if (!SetProperty(ref _selectedProject, value))
                {
                    return;
                }

            }
        }


        private IEnumerable<ProjectViewModel> _projects;
        public IEnumerable<ProjectViewModel> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        public async void Initialize()
        {
            var httpClient = new HttpClient();
            var token = UserService.Token;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net/api/projects/");

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var projects = JsonConvert.DeserializeObject<List<ProjectDTO>>(content);

            Projects = projects.Select(x => new ProjectViewModel
            {
                Id=x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Description = x.Description
            }).ToList();

        }
    }
}
