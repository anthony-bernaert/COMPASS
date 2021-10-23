﻿using COMPASS.Models;
using COMPASS.ViewModels.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.ViewModels
{
    public class FileEditViewModel : BaseEditViewModel
    {
        public FileEditViewModel(MainViewModel vm, Codex ToEdit) : base(vm)
        {
            EditedFile = ToEdit;
            TempFile = new Codex(MVM.CurrentCollection);
            if(!CreateNewFile) TempFile.Copy(EditedFile);

            //Apply right checkboxes in Alltags
            foreach (TreeViewNode t in AllTreeViewNodes)
            {
                t.Expanded = false;
                t.Selected = TempFile.Tags.Contains(t.Tag) ? true : false;
                if (t.Children.Any(node => TempFile.Tags.Contains(node.Tag))) t.Expanded = true;
            }

            //Commands
            BrowsePathCommand = new BasicCommand(BrowsePath);
            TagCheckCommand = new BasicCommand(Update_Taglist);
            DeleteFileCommand = new BasicCommand(DeleteFile);
            BrowseURLCommand = new BasicCommand(BrowseURL);
            RegenArtCommand = new BasicCommand(RegenArt);
            SelectArtCommand = new BasicCommand(SelectArt);
        }

        #region Properties

        readonly Codex EditedFile;

        private bool CreateNewFile
        {
            get { return EditedFile == null; }
        }

        private Codex _tempFile;
        public Codex TempFile
        {
            get { return _tempFile; }
            set { SetProperty(ref _tempFile, value); }
        }

        #endregion

        #region Funtions and Commands

        public BasicCommand BrowsePathCommand { get; private set; }
        private void BrowsePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                AddExtension = false,
                Filter = "PDF (*.pdf) | *.pdf"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                TempFile.Path = openFileDialog.FileName;
            }
        }

        public override void OKBtn()
        {
            //Copy changes into Codex
            if(!CreateNewFile) EditedFile.Copy(TempFile);
            else
            {
                Codex ToAdd = new Codex();
                ToAdd.Copy(TempFile);
                MVM.CurrentCollection.AllFiles.Add(ToAdd);
            }
            //Add new Author and Publishers to lists
            if(TempFile.Author != "" && !MVM.CurrentCollection.AuthorList.Contains(TempFile.Author)) MVM.CurrentCollection.AuthorList.Add(TempFile.Author);
            if(TempFile.Publisher != "" && !MVM.CurrentCollection.PublisherList.Contains(TempFile.Publisher)) MVM.CurrentCollection.PublisherList.Add(TempFile.Publisher);

            MVM.Reset();
            CloseAction();
        }

        public override void Cancel()
        {
            CloseAction();
        }

        public BasicCommand TagCheckCommand { get; private set; }
        public void Update_Taglist()
        {
            TempFile.Tags.Clear();
            foreach (TreeViewNode t in AllTreeViewNodes)
            {
                if (t.Selected)
                {
                    TempFile.Tags.Add(t.Tag);
                }
            }
        }

        public BasicCommand DeleteFileCommand { get; private set; }
        private void DeleteFile()
        {
            if (!CreateNewFile)
            {
                MVM.CurrentCollection.DeleteFile(EditedFile);
                MVM.FilterHandler.RemoveFile(EditedFile);
            }
            CloseAction();
        }

        public BasicCommand BrowseURLCommand { get; private set; }
        private void BrowseURL()
        {
            if (TempFile.SourceURL == "") return;
            System.Diagnostics.Process.Start(TempFile.SourceURL);
        }

        public BasicCommand RegenArtCommand { get; private set; }
        private void RegenArt()
        {
            CoverArtGenerator.ConvertPDF(TempFile, MVM.CurrentCollection.Folder);
            //force refresh
            string CovArt = TempFile.CoverArt; 
            TempFile.CoverArt = null;
            TempFile.CoverArt = CovArt;
        }

        public BasicCommand SelectArtCommand { get; private set; }
        private void SelectArt()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                AddExtension = false,
                Multiselect = false,
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
        };
            if (openFileDialog.ShowDialog() == true)
            {
                CoverArtGenerator.SaveImageAsCover(openFileDialog.FileName, TempFile);
            }
            //force refresh
            string CovArt = TempFile.CoverArt;
            TempFile.CoverArt = null;
            TempFile.CoverArt = CovArt;
        }
        #endregion
    }
}

