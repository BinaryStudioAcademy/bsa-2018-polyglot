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
            InitializeComponent();

            var httpClient = new HttpClient();
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImEwY2ViNDY3NDJhNjNlMTk2NDIxNjNhNzI4NmRjZDQyZjc0MzYzNjYifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vcG9seWdsb3QtZGJjOWEiLCJuYW1lIjoi0JDQu9C10LrRgdCw0L3QtNGA0LAg0KTQtdC00L7RgtC-0LLQsCIsInBpY3R1cmUiOiJodHRwczovL2xoNi5nb29nbGV1c2VyY29udGVudC5jb20vLXVLcmc3cm1TODNzL0FBQUFBQUFBQUFJL0FBQUFBQUFBQ29JL3FKT1pQNGpjS29ZL3Bob3RvLmpwZyIsImF1ZCI6InBvbHlnbG90LWRiYzlhIiwiYXV0aF90aW1lIjoxNTM2MjUxNTUzLCJ1c2VyX2lkIjoibGUycU4xMWFsdU5xRXo0R3ROWnpyWnpBT1FmMiIsInN1YiI6ImxlMnFOMTFhbHVOcUV6NEd0Tlp6clp6QU9RZjIiLCJpYXQiOjE1MzYyNTE1NTMsImV4cCI6MTUzNjI1NTE1MywiZW1haWwiOiJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJnb29nbGUuY29tIjpbIjEwMzExNDg4MDA4MDcwOTY5NTY0MCJdLCJlbWFpbCI6WyJmZXlhc2FzaGExOTExMTk5NkBnbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJnb29nbGUuY29tIn19.HLlagYjffOlQsPapDScE34rh2d_6LEN-ehfeJRz7nQ0gG5ErS940K1bXLFjmncz3EEhCJnVAf6JUX2vb45g6WvTrDEBXIc9-lPgEBkpRHhYsyKMiR_5FBORqdo_ysD3_n7vLH16Dx3FB9qAUpF6vOS2n-etGPNGiykcfQyyv8YOy7WoNha2k6okAI5jpgvXDaxidKIXhoFi5iAzXihTjij0sYFz3YHeQEqfCgVGmew7d2ccuLdt1rVtWycY8za0bbu5PWycgj6BFXx3hcE5Rt_F1FD8hF0nNoo7yfEZHkMcbrB7svqO8tG5vWAZ6B-kGdb1_KzGmNN2ulAVPZcFNXA";
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
