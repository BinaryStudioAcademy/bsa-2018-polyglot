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
using Polyglot.BusinessLogic;

namespace Polyglot.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public bool IsEmpty => Projects == null || !Projects.Any();

        private IEnumerable<ProjectViewModel> _projects;
        public IEnumerable<ProjectViewModel> Projects
        {
            get => _projects;
            set
            {
                if (!SetProperty(ref _projects, value))
                {
                    return;
                }

                RaisePropertyChanged(nameof(IsEmpty));

            }
        }

        public async void Initialize()
        {
            var url = "projects";
            var httpService = new HttpService();
            var projects = await httpService.GetAsync<List<ProjectDTO>>(url);

            Projects = projects.Select(x => new ProjectViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Description = x.Description
            }).ToList();

        }
    }
}
