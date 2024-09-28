using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowImportPlugin.ViewModels
{
    public class ImportViewModel : ObservableObject
    {

        public Action CloseAction { get; set; } = () => { };

        private RelayCommand? _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ??= new(Cancel);
        public void Cancel() => CloseAction();

        public Action AcceptAction { get; set; } = () => { };

        private RelayCommand? _acceptCommand;
        public RelayCommand AcceptCommand => _acceptCommand ??= new(Accept);
        public void Accept() => AcceptAction();

        private float _interval = 2;
        public float IntervalInput
        {
            get => _interval;
            set => SetProperty(ref _interval, value);
        }
    }
}
