﻿using COMPASS.ViewModels;
using System.Windows;


namespace COMPASS.Windows
{
    /// <summary>
    /// Interaction logic for ImportFolderWindow.xaml
    /// </summary>
    public partial class ImportFolderWindow : Window
    {
        public ImportFolderWindow(FolderSourceViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
