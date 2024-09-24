﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace COMPASS.Models.Preferences
{
    public class ListLayoutPreferences : ObservableObject
    {
        private bool _showTitle = true;
        public bool ShowTitle
        {
            get => _showTitle;
            set => SetProperty(ref _showTitle, value);
        }

        public bool ShowAuthor
        {
            get => Properties.Settings.Default.ListShowAuthor;
            set
            {
                Properties.Settings.Default.ListShowAuthor = value;
                OnPropertyChanged();
            }
        }

        public bool ShowPublisher
        {
            get => Properties.Settings.Default.ListShowPublisher;
            set
            {
                Properties.Settings.Default.ListShowPublisher = value;
                OnPropertyChanged();
            }
        }

        public bool ShowReleaseDate
        {
            get => Properties.Settings.Default.ListShowRelease;
            set
            {
                Properties.Settings.Default.ListShowRelease = value;
                OnPropertyChanged();
            }
        }

        public bool ShowDateAdded
        {
            get => Properties.Settings.Default.ListShowDateAdded;
            set
            {
                Properties.Settings.Default.ListShowDateAdded = value;
                OnPropertyChanged();
            }
        }

        public bool ShowVersion
        {
            get => Properties.Settings.Default.ListShowVersion;
            set
            {
                Properties.Settings.Default.ListShowVersion = value;
                OnPropertyChanged();
            }
        }

        public bool ShowRating
        {
            get => Properties.Settings.Default.ListShowRating;
            set
            {
                Properties.Settings.Default.ListShowRating = value;
                OnPropertyChanged();
            }
        }

        public bool ShowISBN
        {
            get => Properties.Settings.Default.ListShowISBN;
            set
            {
                Properties.Settings.Default.ListShowISBN = value;
                OnPropertyChanged();
            }
        }

        public bool ShowTags
        {
            get => Properties.Settings.Default.ListShowTags;
            set
            {
                Properties.Settings.Default.ListShowTags = value;
                OnPropertyChanged();
            }
        }

        public bool ShowFileIcons
        {
            get => Properties.Settings.Default.ListShowFileIcons;
            set
            {
                Properties.Settings.Default.ListShowFileIcons = value;
                OnPropertyChanged();
            }
        }

        public bool ShowEditIcon
        {
            get => Properties.Settings.Default.ListShowEditIcon;
            set
            {
                Properties.Settings.Default.ListShowEditIcon = value;
                OnPropertyChanged();
            }
        }

        public bool ShowDescription
        {
            get => Properties.Settings.Default.ListShowDescription;
            set
            {
                Properties.Settings.Default.ListShowDescription = value;
                OnPropertyChanged();
            }
        }

        public bool ShowPath
        {
            get => Properties.Settings.Default.ListShowPath;
            set
            {
                Properties.Settings.Default.ListShowPath = value;
                OnPropertyChanged();
            }
        }
    }
}
