﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryViewModel vm;
        public HistoryPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as HistoryViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetPosts();
            
        }

        private void postListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;
            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}