﻿using System;
using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using ImageMagick;
using COMPASS.ViewModels;

namespace COMPASS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel = new MainViewModel("DnD");

        public MainWindow()
        {
            InitializeComponent();

            MagickNET.SetGhostscriptDirectory(@"C:\Users\pauld\Documents\COMPASS\COMPASS\Libraries");

            //set Itemsources for databinding
            DataContext = MainViewModel;
            //temp
            MainViewModel.CurrentFileViewModel = new FileListViewModel() { ActiveFiles = MainViewModel.FilterHandler.ActiveFiles};
            //

            //ViewsGrid.DataContext = MainViewModel.CurrentData;
            TagTree.DataContext = MainViewModel.CurrentData.RootTags;
            ParentSelectionTree.DataContext = MainViewModel.CurrentData.RootTags;
            ParentSelectionPanel.DataContext = ParentSelectionTree.SelectedItem as Tag;

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        // is true if we hold left mouse button on windows tilebar
        private bool DragWindow = false;

        public void RefreshTreeViews()
        {
            //redraws treeviews
            TagTree.DataContext = null;
            ParentSelectionTree.DataContext = null;
            TagTree.DataContext = MainViewModel.CurrentData.RootTags;
            ParentSelectionTree.DataContext = MainViewModel.CurrentData.RootTags;
        }

        #region Clears Selection From TreeView
        public static void ClearTreeViewSelection(TreeView tv)
        {
            if (tv != null)
                ClearTreeViewItemsControlSelection(tv.Items, tv.ItemContainerGenerator);
        }
        private static void ClearTreeViewItemsControlSelection(ItemCollection ic, ItemContainerGenerator icg)
        {
            if ((ic != null) && (icg != null))
                for (int i = 0; i < ic.Count; i++)
                {
                    if (icg.ContainerFromIndex(i) is TreeViewItem tvi)
                    {
                        ClearTreeViewItemsControlSelection(tvi.Items, tvi.ItemContainerGenerator);
                        tvi.IsSelected = false;
                    }
                }
        }
        #endregion

        //Opens tag creation popup
        private void Addtag_Click(object sender, RoutedEventArgs e)
        {
            if (TagCreation.Visibility == Visibility.Collapsed) TagCreation.Visibility = Visibility.Visible;
            else TagCreation.Visibility = Visibility.Collapsed;
        }

        //Deselects when you click away
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Focus();
        }

        //removes tag from filter list when clicked
        private void ActiveTag_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.FilterHandler.RemoveTagFilter((Tag)CurrentTagList.SelectedItem);
        }

        //import files
        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                AddExtension = false,
                Multiselect = true   
            };
            if (openFileDialog.ShowDialog() == true)
            {
                foreach(string path in openFileDialog.FileNames)
                {
                    if(MainViewModel.CurrentData.AllFiles.All(p => p.Path != path))
                    {
                    MyFile pdf = new MyFile(MainViewModel) { Path = path, Title = System.IO.Path.GetFileNameWithoutExtension(path)};
                        MainViewModel.CurrentData.AllFiles.Add(pdf);
                        CoverArtGenerator.ConvertPDF(pdf, MainViewModel.CurrentData.Folder);
                    }
                }
                MainViewModel.Reset();
            }         
        }

        private void TagNameTextBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return && TagCreation.Visibility == Visibility.Visible)
            {
                CreateTagBtn_Click(sender,e);
            }
        }

        private void Searchbox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Searchbox.Text = "";
        }

        //EDIT File
        private void Edit_File_Btn(object sender, RoutedEventArgs e)
        {
            //MainViewModel.CurrentData.EditedFile = FileListView.SelectedItem as MyFile;
            //FilePropWindow fpw = new FilePropWindow(MainViewModel);
            //fpw.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MainViewModel.CurrentData.SaveFilesToFile();
            MainViewModel.CurrentData.SaveTagsToFile();
        }

        private void CreateTagBtn_Click(object sender, RoutedEventArgs e)
        {
            Tag newtag = new Tag(MainViewModel) { Content = TagNameTextBlock.Text, BackgroundColor = (Color)ColorSelector.SelectedColor};
            if (ParentSelectionTree.SelectedItem != null)
            {
                Tag Parent = ParentSelectionTree.SelectedItem as Tag;
                Parent.Expanded = true;
                Parent.Items.Add(newtag);
                newtag.ParentID = Parent.ID;
            }
            else
            {
                MainViewModel.CurrentData.RootTags.Add(newtag);
            }

            MainViewModel.CurrentData.AllTags.Add(newtag);
            TagNameTextBlock.Text = "";
            TagCreation.Visibility = Visibility.Collapsed;
        }

        private void ShowParentSelection_Click(object sender, RoutedEventArgs e)
        {
            if (ParentSelection.Visibility == Visibility.Collapsed && sender.GetType().Name == "Button")
            {
                ParentSelection.Visibility = Visibility.Visible;
                TagCreationMain.Visibility = Visibility.Collapsed;
            }
            else
            {
                ParentSelectionPanel.DataContext = null;
                ParentSelectionPanel.DataContext = ParentSelectionTree.SelectedItem as Tag;
                ParentSelection.Visibility = Visibility.Collapsed;
                TagCreationMain.Visibility = Visibility.Visible;
            }  
        }

        private void ShowColorSelection_Click(object sender, RoutedEventArgs e)
        {
            if (ColorSelection.Visibility == Visibility.Collapsed)
            {
                ColorSelection.Visibility = Visibility.Visible;
                TagCreationMain.Visibility = Visibility.Collapsed;
            }
            else
            {                
                ColorSelection.Visibility = Visibility.Collapsed;
                TagCreationMain.Visibility = Visibility.Visible;
            }
        }

        #region Windows Tile Bar Buttons
                private void MinimizeWindow(object sender, RoutedEventArgs e)
                {
                    App.Current.MainWindow.WindowState = WindowState.Minimized;
                }
                private void MaximizeClick(object sender, RoutedEventArgs e)
                {
                    if (App.Current.MainWindow.WindowState == WindowState.Maximized)
                    {
                        App.Current.MainWindow.WindowState = WindowState.Normal;
                        Maximizeimage.Visibility = Visibility.Visible;
                        NotMaximizeimage.Visibility = Visibility.Collapsed;

                    }
                    else
                    {
                        App.Current.MainWindow.WindowState = WindowState.Maximized;
                        Maximizeimage.Visibility = Visibility.Collapsed;
                        NotMaximizeimage.Visibility = Visibility.Visible;
                    }
                }
                private void WindowsBar_MouseDown(object sender, MouseButtonEventArgs e)
                {
                    if (e.ClickCount == 2)
                    {
                        MaximizeClick(sender, e);
                        DragWindow = false;
                    }

                    else
                    {
                        App.Current.MainWindow.DragMove();
                        if (App.Current.MainWindow.WindowState == WindowState.Maximized)
                        {
                            DragWindow = WindowState == WindowState.Maximized;
                        }
                    }


                }
                private void OnMouseMove(object sender, MouseEventArgs e)
                {
                    if (DragWindow)
                    {
                        DragWindow = false;

                        var point = PointToScreen(e.MouseDevice.GetPosition(this));

                        Left = point.X - (RestoreBounds.Width * 0.5);
                        Top = point.Y;

                        WindowState = WindowState.Normal;

                        Maximizeimage.Visibility = Visibility.Visible;
                        NotMaximizeimage.Visibility = Visibility.Collapsed;

                        try
                        {
                            DragMove();
                        }

                        catch (InvalidOperationException)
                        {
                            MaximizeClick(sender, e);
                        }
                   
                    }
                }
                private void CloseButton_Click(object sender, RoutedEventArgs e)
                {
                    this.Close();
                }
        #endregion

        #region Context Menu Tags
        private void TagTree_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }
        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }

        private void EditTag(object sender, RoutedEventArgs e)
        {
            MainViewModel.CurrentData.EditedTag = TagTree.SelectedItem as Tag;
            ClearTreeViewSelection(TagTree);
            if (MainViewModel.CurrentData.EditedTag != null)
            {
                TagPropWindow tpw = new TagPropWindow(MainViewModel);
                tpw.Closed += EditTagClosedHandler;
                tpw.Show();
            }
        }

        public void EditTagClosedHandler(object sender, EventArgs e)
        {
            RefreshTreeViews();
        }

        //Delete Tag, event funtion and recursive help function
        private void DeleteTag(object sender, RoutedEventArgs e)
        {
            var todel = TagTree.SelectedItem as Tag;
            MainViewModel.CurrentData.DeleteTag(todel);
            //Go over all files and refresh tags list
            foreach (var f in MainViewModel.CurrentData.AllFiles)
            {
                int i = 0;
                //iterate over all the tags in the file
                while (i<f.Tags.Count)
                {
                    Tag currenttag = f.Tags[i];
                    //try to find the tag in alltags, if found, increase i to go to next tag
                    try
                    {
                        MainViewModel.CurrentData.AllTags.First(tag => tag.ID == currenttag.ID);
                        i++;
                    }
                    //if the tag in not found in alltags, delete it
                    catch (System.InvalidOperationException)
                    {
                        f.Tags.Remove(currenttag);
                    }                  
                }
            }
            MainViewModel.Reset();
            ClearTreeViewSelection(TagTree);
        }
        #endregion

        private void ClearParent_Click(object sender, RoutedEventArgs e)
        {
            ClearTreeViewSelection(ParentSelectionTree);
        }

        //makes scrolwheel work in parent selection tree
        private void ParentSelectionScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void TagTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Tag selectedtag = (Tag)e.NewValue;
            if (selectedtag == null) return;
            MainViewModel.FilterHandler.AddTagFilter(selectedtag);
            ClearTreeViewSelection(TagTree);
        }
    }

}


