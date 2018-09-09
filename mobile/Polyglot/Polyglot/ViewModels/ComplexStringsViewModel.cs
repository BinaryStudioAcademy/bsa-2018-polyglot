using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using Polyglot.BusinessLogic.Services;
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
    public class ComplexStringsViewModel : BaseViewModel
    {
        private ComplexStringViewModel _selectedString;
        public ComplexStringViewModel SelectedString
        {
            get => _selectedString;
            set => SetProperty(ref _selectedString, value);
        }

        private IEnumerable<ComplexStringViewModel> _complexStrings;
        public IEnumerable<ComplexStringViewModel> ComplexStrings
        {
            get => _complexStrings;
            set => SetProperty(ref _complexStrings, value);
        }

        public async void Initialize(int projectId)
        {
            var httpClient = new HttpClient();
            var token = UserService.Token;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net//api/projects/" +projectId + "/paginatedStrings?itemsOnPage=9&page=0&search=");


            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var complexStrings = JsonConvert.DeserializeObject<List<ComplexStringDTO>>(content);

            ComplexStrings = complexStrings.Select(x => new ComplexStringViewModel
            {
                Id=x.Id,
                Key=x.Key,
                OriginalValue=x.OriginalValue,
                Description=x.Description,
                ProjectId = x.ProjectId
            }).ToList();

        }
    }
}
