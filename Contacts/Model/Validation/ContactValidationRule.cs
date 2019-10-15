using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Windows.Data;

namespace Contacts.Model.Validation
{

    public class ContactValidationRule : ValidationRule
    {
        /// <summary>
        /// Allows to edit row when Validation blocks it,
        /// reference: https://stackoverflow.com/questions/23517251/how-i-can-edit-next-cell-in-datagrid-after-the-error
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override System.Windows.Controls.ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            if ((value as BindingGroup).Items.Count == 0)
            {
                return System.Windows.Controls.ValidationResult.ValidResult;
            }
            Contact contact = (value as BindingGroup).Items[0] as Contact;

            var props = typeof(Contact).GetProperties();
            foreach (var prop in props)
            {
                var attrs = prop.Attributes;
                var isRequired = Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.RequiredAttribute));
                if (isRequired && (prop.GetValue(contact) == null || string.IsNullOrEmpty(prop.GetValue(contact).ToString())))
                {
                    string errMsg = $"Value of {prop.Name} cannot be empty";
                    contact.Error = errMsg;
                    return new System.Windows.Controls.ValidationResult(false, errMsg);
                }
            }
            contact.Error = string.Empty;
            return System.Windows.Controls.ValidationResult.ValidResult;

        }
    }

}
