using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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

        public async void Initialize(string projectId)
        {
            var httpClient = new HttpClient();
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBmNTVkZWZlOWU5YzU2ZmRhZTRkOGY0MDFjZjQ5Njc4YzE2N2MzYWEifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vcG9seWdsb3QtZGJjOWEiLCJuYW1lIjoi0JDQu9C10LrRgdCw0L3QtNGA0LAg0KTQtdC00L7RgtC-0LLQsCIsInBpY3R1cmUiOiJodHRwczovL2xoNi5nb29nbGV1c2VyY29udGVudC5jb20vLXVLcmc3cm1TODNzL0FBQUFBQUFBQUFJL0FBQUFBQUFBQ29JL3FKT1pQNGpjS29ZL3Bob3RvLmpwZyIsImF1ZCI6InBvbHlnbG90LWRiYzlhIiwiYXV0aF90aW1lIjoxNTM2MzE2OTk1LCJ1c2VyX2lkIjoibGUycU4xMWFsdU5xRXo0R3ROWnpyWnpBT1FmMiIsInN1YiI6ImxlMnFOMTFhbHVOcUV6NEd0Tlp6clp6QU9RZjIiLCJpYXQiOjE1MzYzMTY5OTUsImV4cCI6MTUzNjMyMDU5NSwiZW1haWwiOiJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJnb29nbGUuY29tIjpbIjEwMzExNDg4MDA4MDcwOTY5NTY0MCJdLCJlbWFpbCI6WyJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJnb29nbGUuY29tIn19.Ln6HGUmHyn1rS9Aduso8aZoyjJlZPFI_WVlz4XvDlc9k1LqeqvphGZUmq9PpcaWnwX5BQHT2hLUK5Y3YEzzFQWVb2Pyp-TD_Wt8qugQGWZ0B32L9hJcE0TlwIrUux3UgWyvVGhar62t4cNerJRvK90EjqJRbhHENyDt_iIGPuqkeyeMwxY6qkt3ryqVtZLGUcOYVv9CmSe6SR9zJB7BszUOzrlpEJj_07V1z6RXXRRxAMcifiD1obe9W77xqTQ5rty1GWdjj6-ZJbnhk_nE2veSUlepoimHsGu67VVU2ocQmIhUSKh292cZMj5E_-fNrG5CHWEKSj3W8aO6CkTwVNA";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("http://polyglotbsa.azurewebsites.net/api/workspace/"+projectId);

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var complexStrings = JsonConvert.DeserializeObject<List<ComplexStringDTO>>(content);

            ComplexStrings = complexStrings.Select(x => new ComplexStringViewModel
            {
                Id=x.Id,
                Key=x.Key,
                OriginalValue=x.OriginalValue,
                Description=x.Description
            }).ToList();

        }
    }
}
