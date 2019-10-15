using Contacts.Model;
using Contacts.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        public ContactViewModel()
        {
            this.Contacts = DataManagment.LoadData<TrulyObservableCollection<Contact>>();
            if (this.Contacts == null)
            {
                this.Contacts = new TrulyObservableCollection<Contact>();
                //Replace with log
                Trace.WriteLine("Data not loaded");
            }
            this.Contacts.CollectionChanged += Contacts_CollectionChanged;
            this.SaveCommand = new RelayCommand(param =>
            {
                this.CanExecuteSave = this.CanExecuteCancel = false;
                Task.Run(() =>
                {
                    DataManagment.SaveData(this.Contacts.Where(c => string.IsNullOrEmpty(c.Error)).ToList());
                });
            }, () => CanExecuteSave);

            this.CancelCommand = new RelayCommand(param =>
            {
                this.CanExecuteSave = this.CanExecuteCancel = false;
                Task.Run(() =>
                {
                    this.Contacts = DataManagment.LoadData<TrulyObservableCollection<Contact>>();
                    this.Contacts.CollectionChanged += Contacts_CollectionChanged;
                });
            }, () => CanExecuteCancel);

            this.DeleteItemCommand = new RelayCommand(param =>
            {
                var contactToDelete = param as Contact;
                if (contactToDelete == null) return;

                this.Contacts.Remove(contactToDelete);
                //if entry is not valid then add new empty row
                if (!string.IsNullOrEmpty(contactToDelete.Error))
                {
                    this.Contacts.Add(new Contact());
                }
                this.Contacts.Remove(contactToDelete);

            });

            CanExecuteCancel = CanExecuteSave = false;
        }


        #region Properties

        public bool CanExecuteSave { get; set; }

        public bool CanExecuteCancel { get; set; }

        #region Contacts

        private TrulyObservableCollection<Contact> _contacts;

        public TrulyObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                CanExecuteCancel = CanExecuteSave = true;
                OnPropertyChanged(() => Contacts);
            }
        }

        private void Contacts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CanExecuteCancel = CanExecuteSave = true;
            OnPropertyChanged(() => Contacts);
        }

        #endregion Contacts

        #endregion Properties 

        #region Commands

        #region SaveCommand

        public RelayCommand SaveCommand { get; set; }

        #endregion SaveCommand

        #region CancelCommand

        public RelayCommand CancelCommand { get; set; }

        #endregion CancelCommand

        #region DeleteItemCommand

        //public RelayCommand DeleteItemCommand = new RelayCommand<object>(CommandWithAParameter);

        public RelayCommand _deleteItemCommand;
        /// <summary>
        /// Returns a command with a parameter
        /// </summary>
        public RelayCommand DeleteItemCommand { get; set; }

        private void CommandWithAParameter(object state)
        {
            var str = state as string;
        }

        #endregion DeleteItemCommand

        #endregion Commands
    }
}
