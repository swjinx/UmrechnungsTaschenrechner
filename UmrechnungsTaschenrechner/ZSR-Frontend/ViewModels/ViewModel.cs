using ZSRFrontend.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UmrechnungTaschenrechner.Calculators;
using System.Windows;

namespace ZSR_Frontend.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _term;
        public string TermString
        {
            get
            {
                return _term;
            }
            set
            {
                _term = value;
                NotifyPropertyChanged(nameof(TermString));
            }
        }

        private string _dezimal;

        public string Dezimal
        {
            get { return _dezimal; }
            set { _dezimal = value;
                NotifyPropertyChanged(nameof(Dezimal));
            }
        }

        private string _binär;

        public string Binär
        {
            get { return _binär; }
            set
            {
                _binär = value;
                NotifyPropertyChanged(nameof(Binär));
            }
        }

        private string _octal;

        public string Octal
        {
            get { return _octal; }
            set
            {
                _octal = value;
                NotifyPropertyChanged(nameof(Octal));
            }
        }

        private string _hexadecimal;

        public string Hexadecimal
        {
            get { return _hexadecimal; }
            set
            {
                _hexadecimal = value;
                NotifyPropertyChanged(nameof(Hexadecimal));
            }
        }

        private ObservableCollection<string> _history;

        public ObservableCollection<string> History
        {
            get { return _history; }
            set { _history = value;
                NotifyPropertyChanged(nameof(History));
            }
        }

        public ICommand CalcCommand { get; set; }

        public ViewModel()
        {
            History = new ObservableCollection<string>();
            CalcCommand = new DelegateCommand(Calculate);
        }

        private void Calculate(object param)
        {
            try
            {
                TermString = TermString.Replace('.', ',');
                var res = Term.DeriveTerm(TermString).Substring(1);
                Dezimal = UmrechnungTaschenrechner.Converters.Converter.DecimalToString(res, 10);
                Binär = UmrechnungTaschenrechner.Converters.Converter.DecimalToString(res, 2);
                Octal = UmrechnungTaschenrechner.Converters.Converter.DecimalToString(res, 8);
                Hexadecimal = UmrechnungTaschenrechner.Converters.Converter.DecimalToString(res, 16);
                History.Add(TermString + " = d" + res);
                NotifyPropertyChanged(nameof(History));
                TermString = string.Empty;
            }
            catch (Exception)
            {
                MessageBox.Show("Der eingegebene Term ist nicht korrekt.\nBitte korrigieren Sie die Eingabe.","An Error occured!", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
