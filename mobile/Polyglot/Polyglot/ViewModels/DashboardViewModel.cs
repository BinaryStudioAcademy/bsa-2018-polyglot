using Android.Content.Res;
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
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBmNTVkZWZlOWU5YzU2ZmRhZTRkOGY0MDFjZjQ5Njc4YzE2N2MzYWEifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vcG9seWdsb3QtZGJjOWEiLCJuYW1lIjoi0JDQu9C10LrRgdCw0L3QtNGA0LAg0KTQtdC00L7RgtC-0LLQsCIsInBpY3R1cmUiOiJodHRwczovL2xoNi5nb29nbGV1c2VyY29udGVudC5jb20vLXVLcmc3cm1TODNzL0FBQUFBQUFBQUFJL0FBQUFBQUFBQ29JL3FKT1pQNGpjS29ZL3Bob3RvLmpwZyIsImF1ZCI6InBvbHlnbG90LWRiYzlhIiwiYXV0aF90aW1lIjoxNTM2NTIxNzIxLCJ1c2VyX2lkIjoibGUycU4xMWFsdU5xRXo0R3ROWnpyWnpBT1FmMiIsInN1YiI6ImxlMnFOMTFhbHVOcUV6NEd0Tlp6clp6QU9RZjIiLCJpYXQiOjE1MzY1MjE3MjEsImV4cCI6MTUzNjUyNTMyMSwiZW1haWwiOiJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJnb29nbGUuY29tIjpbIjEwMzExNDg4MDA4MDcwOTY5NTY0MCJdLCJlbWFpbCI6WyJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJnb29nbGUuY29tIn19.RPkDj3MbPk3Z2M0pRtFlIAS4QySMvLo1RofgBS8T_V5sF3PTw69foSnj8rw7XFdhvlpnC09TLtSbvPt0Sgk3vivCqXnw-OEzPNR9cKe-OoGIqegFbm3heKj6G4OrWYzF5ucP78jXQkPhE4-7_KLUkZbOzSOgLIbT5IArLCHuIiGFIZ9iYXN7ogHSQzKTfPrTPKDYAGPWdZkOhGSuYy8iPSKxEC6gXNLfm5Lu2gXlxItkbXIoc3WJCvHzehFr30sGcTzqTCndmv4gevMfo5ws42AbcY8-2nLrgrUo-YmPXzMXKFW-ypsoQo1GtWePIBl7US1AovbUvs8AierzKjkpOg";
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
