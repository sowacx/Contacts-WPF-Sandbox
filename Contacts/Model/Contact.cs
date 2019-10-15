using Contacts.ViewModel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Contacts.Model
{
    public class Contact : BaseViewModel, IDataErrorInfo
    {
        public Contact()
        {
        }


        #region Properties

        #region FirstName

        private string _firstName;

        [Required]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(() => FirstName);
            }
        }

        #endregion FirstName

        #region LastName

        private string _lastName;

        [Required]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(() => LastName);
            }
        }

        #endregion LastName      

        #region PhoneNumber

        private string _phoneNumber;

        [Required]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(() => PhoneNumber);
            }
        }

        #endregion PhoneNumber

        #region DayOfBirth

        private DateTime _dayOfBirth = DateTime.Now;

        [Required]
        public DateTime DayOfBirth
        {
            get => _dayOfBirth;
            set
            {
                _dayOfBirth = value;
                OnPropertyChanged(() => DayOfBirth);
            }
        }

        #endregion DayOfBirth

        #region Age

        public int Age
        {
            get
            {
                var diff = DateTime.Now - this.DayOfBirth;
                return (new DateTime(1, 1, 1) + diff).Year - 1;
            }
        }

        #endregion Age

        #region StreetName

        private string _streetName;

        [Required]
        public string StreetName
        {
            get => _streetName;
            set
            {
                _streetName = value;
                OnPropertyChanged(() => StreetName);
            }
        }

        #endregion StreetName


        #region HouseNumber

        private string _houseNumber;

        [Required]
        public string HouseNumber
        {
            get => _houseNumber;
            set
            {
                _houseNumber = value;
                OnPropertyChanged(() => HouseNumber);
            }
        }

        #endregion HouseNumber

        #region ApartmentNumber

        private string _apartmentNumber;
        
        public string ApartmentNumber
        {
            get => _apartmentNumber;
            set
            {
                _apartmentNumber = value;
                OnPropertyChanged(() => ApartmentNumber);
            }
        }

        #endregion ApartmentNumber

        #region PostalCode

        private string _postalCode;

        [Required]
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged(() => PostalCode);
            }
        }

        #endregion PostalCode


        #endregion Properties

        #region IDataErrorInfo

        public string Error
        {
            get; set;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                PropertyInfo pinfo = typeof(Contact).GetProperty(columnName);
                var isRequired = Attribute.IsDefined(pinfo, typeof(System.ComponentModel.DataAnnotations.RequiredAttribute));
                var value = pinfo.GetValue(this);
                if (isRequired && (value == null || string.IsNullOrEmpty(value.ToString())))
                {
                    error = "Value of that column cannot be empty";
                }
                return error;
            }

        }

        #endregion IDataErrorInfo
    }
}
