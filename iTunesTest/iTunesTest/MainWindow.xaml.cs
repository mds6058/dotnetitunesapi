﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using Newtonsoft.Json;

namespace iTunesTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String url = "https://itunes.apple.com/search?term=karma%20police&attribute=songTerm&entity=album";
            var json = new WebClient().DownloadString(url);

            iTunesResult deserialized = JsonConvert.DeserializeObject<iTunesResult>(json);

            //For now take the first image returned and set it in our image view
            String imageUrl = deserialized.results[0].artworkUrl100;

            setImageToUrl(imageUrl);

            TestLabel.Content = "Clicked!";

        } 

        private void setImageToUrl(String url)
        {
            BitmapImage urlImage = new BitmapImage();

            urlImage.BeginInit();
            urlImage.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
            urlImage.EndInit();

            CoverArtImage.Source = urlImage;
        }

    }

    public class iTunesResult
    {
        public string resultCount { get; set; }
        public iTunesAttribute[] results {get; set; }
    }

    public class iTunesAttribute
    {
        public String artworkUrl60 { get; set; }
        public String artworkUrl100 { get; set; }
    }


}
