﻿using Polyglot.BusinessLogic.DTO;
using Polyglot.ViewModels;
using Polyglot.ViewModels.TranslationsDetails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Polyglot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslationsPage : ContentPage
    {
        public int ComplexStringId { get; set; }

        public TranslationsPage(TranslationsViewModel translationsViewModel, int complexStringId, int projectId)
        {
            BindingContext = translationsViewModel;

            NavigationPage.SetHasNavigationBar(this, false);
            ComplexStringId = complexStringId;

            InitializeComponent();

            translationsViewModel.Initialize(complexStringId, projectId);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var tr = e.Item as TranslationViewModel;

            var actionSheet = await DisplayActionSheet("Title", "Cancel", null, "Comments", "History", "Optional translations");

            switch (actionSheet)
            {
                case "Cancel":
                    break;

                case "Comments":
                    var commentsPage = new CommentsPage(new CommentsViewModel(), ComplexStringId);
                    await Navigation.PushAsync(commentsPage);
                    break;

                case "History":                   
                    if (string.IsNullOrEmpty(tr.Id))
                    {
                        break;
                    }
                    var historyPage = new HistoryPage(new HistoryViewModel(), ComplexStringId, tr.Id);
                    await Navigation.PushAsync(historyPage);

                    break;

                case "Optional translations":                   
                    if (string.IsNullOrEmpty(tr.Id))
                    {
                        break;
                    }
                    var optionalPage = new OptionalTranslationsPage(new OptionalTranslationsViewModel(), ComplexStringId, tr.Id);
                    await Navigation.PushAsync(optionalPage);

                    break;
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
