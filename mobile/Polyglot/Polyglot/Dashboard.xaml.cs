using Newtonsoft.Json;
using Polyglot.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public Dashboard()
        {
           /* BindingContext= var in constructor*/
            InitializeComponent();

            var httpClient = new HttpClient();
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImEwY2ViNDY3NDJhNjNlMTk2NDIxNjNhNzI4NmRjZDQyZjc0MzYzNjYifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vcG9seWdsb3QtZGJjOWEiLCJuYW1lIjoi0JDQu9C10LrRgdCw0L3QtNGA0LAg0KTQtdC00L7RgtC-0LLQsCIsInBpY3R1cmUiOiJodHRwczovL2xoNi5nb29nbGV1c2VyY29udGVudC5jb20vLXVLcmc3cm1TODNzL0FBQUFBQUFBQUFJL0FBQUFBQUFBQ29JL3FKT1pQNGpjS29ZL3Bob3RvLmpwZyIsImF1ZCI6InBvbHlnbG90LWRiYzlhIiwiYXV0aF90aW1lIjoxNTM2MjYwMzQyLCJ1c2VyX2lkIjoibGUycU4xMWFsdU5xRXo0R3ROWnpyWnpBT1FmMiIsInN1YiI6ImxlMnFOMTFhbHVOcUV6NEd0Tlp6clp6QU9RZjIiLCJpYXQiOjE1MzYyNjAzNDIsImV4cCI6MTUzNjI2Mzk0MiwiZW1haWwiOiJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJnb29nbGUuY29tIjpbIjEwMzExNDg4MDA4MDcwOTY5NTY0MCJdLCJlbWFpbCI6WyJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJnb29nbGUuY29tIn19.ebNMGHRiwAffKCw6LEgPnyxA2wo8Eh3pFIUI_9o4Iw-vK-1dtveqTegV4LGfMpcLuGBhyVZTlIy8z8WD5FF3Hj7smNDo96296xlxx5perjtkrgGboLRcL5OXw2QaBw1GIFYfk1dapjZMu6U90D6g3lv-o-yEj1FvTjxk9h3U4Q62QYpKuJwxcHIhqJ-mTSy_mlnGCVvLz3DmerVOAOb1syXISCxSe4T3xqajWiDcaZpoqhpZP4D0MIvZ5t8x5ACiAIVYpiMGy-of6QZ19jDAGd6XV9RugLZ-7DtHEVok75P9AOGZef0Z6DsRZXg2aZHM4_3Z8h8QKFfM2BlnOOFUAg";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = httpClient.GetAsync("http://polyglotbsa.azurewebsites.net/api/projects/").ConfigureAwait(false).GetAwaiter().GetResult();

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content =  response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            var projects = JsonConvert.DeserializeObject<List<ProjectDTO>>(content);

            if (projects != null) 
            {
                MyListView.ItemsSource = projects;
            }

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
