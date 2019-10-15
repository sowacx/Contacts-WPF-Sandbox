using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contacts.View.Customs
{
    public class CustomDataGrid : DataGrid
    {
        protected override void OnCanExecuteBeginEdit(System.Windows.Input.CanExecuteRoutedEventArgs e)
        {

            bool hasCellValidationError = false;
            bool hasRowValidationError = false;
            BindingFlags bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Instance;
            //Current cell
            PropertyInfo cellErrorInfo = this.GetType().BaseType.GetProperty("HasCellValidationError", bindingFlags);
            //Grid level
            PropertyInfo rowErrorInfo = this.GetType().BaseType.GetProperty("HasRowValidationError", bindingFlags);

            if (cellErrorInfo != null) hasCellValidationError = (bool)cellErrorInfo.GetValue(this, null);
            if (rowErrorInfo != null) hasRowValidationError = (bool)rowErrorInfo.GetValue(this, null);

            base.OnCanExecuteBeginEdit(e);
            if (!e.CanExecute && !hasCellValidationError && hasRowValidationError)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }
    }
}
