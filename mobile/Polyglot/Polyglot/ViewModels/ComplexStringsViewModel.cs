using App;
using Newtonsoft.Json;
using Polyglot.BusinessLogic;
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
        public bool IsEmpty => ComplexStrings == null || !ComplexStrings.Any();

        private IEnumerable<ComplexStringViewModel> _complexStrings;
        public IEnumerable<ComplexStringViewModel> ComplexStrings
        {
            get => _complexStrings;
            set
            {
                if (!SetProperty(ref _complexStrings, value))
                {
                    return;
                }

                RaisePropertyChanged(nameof(IsEmpty));

            }
        }

        public async void Initialize(int projectId)
        {
            var url = "projects/" + projectId + "/paginatedStrings?itemsOnPage=9&page=0&search=";
            var httpService = new HttpService();
            var complexStrings = await httpService.GetAsync<List<ComplexStringDTO>>(url);

            ComplexStrings = complexStrings.Select(x => new ComplexStringViewModel
            {
                Id = x.Id,
                Key = x.Key,
                OriginalValue = x.OriginalValue,
                Description = x.Description,
                ProjectId = x.ProjectId
            }).ToList();
        }
    }
}
