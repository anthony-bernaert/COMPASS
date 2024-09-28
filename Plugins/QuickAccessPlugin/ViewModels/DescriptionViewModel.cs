using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessPlugin.ViewModels
{
    public class DescriptionViewModel : ObservableObject
    {
        public DescriptionViewModel(string description)
        {
            _descriptionInput = description;
        }


        public Action CloseAction { get; set; } = () => { };

        private RelayCommand? _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ??= new(Cancel);
        public void Cancel() => CloseAction();

        public Action AcceptAction { get; set; } = () => { };

        private RelayCommand? _acceptCommand;
        public RelayCommand AcceptCommand => _acceptCommand ??= new(Accept);
        public void Accept() => AcceptAction();

        private string _descriptionInput;
        public string DescriptionInput
        {
            get => _descriptionInput;
            set => SetProperty(ref _descriptionInput, value);
        }
    }
}
