﻿using Autofac;
using CommunityToolkit.Mvvm.Input;
using COMPASS.Interfaces;
using COMPASS.Models;
using COMPASS.Models.Enums;
using COMPASS.Sdk.Helpers;
using COMPASS.Services;
using COMPASS.Tools;
using COMPASS.ViewModels.Import;
using COMPASS.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace COMPASS.ViewModels
{
    public class LeftDockViewModel : ViewModelBase, IDealsWithTabControl
    {
        public LeftDockViewModel(MainViewModel mainViewModel)
        {
            _mainVM = mainViewModel;
            _preferencesService = PreferencesService.GetInstance();
        }

        private MainViewModel _mainVM;
        private PreferencesService _preferencesService;

        public MainViewModel MainVM
        {
            get => _mainVM;
            init => SetProperty(ref _mainVM, value);
        }

        public int SelectedTab
        {
            get => _preferencesService.Preferences.UIState.StartupTab;
            set
            {
                _preferencesService.Preferences.UIState.StartupTab = value;
                OnPropertyChanged();
                if (value > 0) Collapsed = false;
            }
        }

        private bool _collapsed = false;
        public bool Collapsed
        {
            get => _collapsed;
            set
            {
                SetProperty(ref _collapsed, value);
                if (value) SelectedTab = 0;
            }
        }

        #region Add Books Tab
        private AsyncRelayCommand<ImportSource>? _importCommand;
        public AsyncRelayCommand<ImportSource> ImportCommand => _importCommand ??= new(ImportViewModel.Import);

        private AsyncRelayCommand? _importBooksFromSatchelCommand;
        public AsyncRelayCommand ImportBooksFromSatchelCommand => _importBooksFromSatchelCommand ??= new(ImportBooksFromSatchel);
        public async Task ImportBooksFromSatchel()
        {
            var collectionToImport = await IOService.OpenSatchel();

            if (collectionToImport == null)
            {
                Logger.Warn("Failed to open file");
                return;
            }

            //Create importcollection ready to merge into existing collection
            //set in advanced mode as a sort of preview
            var vm = new ImportCollectionViewModel(collectionToImport)
            {
                AdvancedImport = true,
                MergeIntoCollection = true
            };

            if (!vm.ContentSelectorVM.HasCodices)
            {
                Notification noItemsFound = new("No items found", $"{collectionToImport.DirectoryName[2..]} does not contain items to import");
                App.Container.ResolveKeyed<INotificationService>(NotificationDisplayType.Windowed).Show(noItemsFound);
                return;
            }

            //setup for only codices
            vm.Steps.Clear();
            vm.Steps.Add(CollectionContentSelectorViewModel.ItemsStep);

            var w = new ImportCollectionWizard(vm);
            w.Show();
        }
        #endregion

        #region Plugin POC
        public ObservableCollection<ImportSourceItem> PluginImportSourceItems { get; set; } = new(new List<ImportSourceItem>());

        private AsyncRelayCommand<ImportSourceItem>? _importFromPluginCommand;
        public AsyncRelayCommand<ImportSourceItem> ImportFromPluginCommand => _importFromPluginCommand ??= new(ImportViewModel.ImportFromPluginAsync!);
        #endregion
    }
}
