using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Contacts.View.Customs
{
    /// <summary>
    /// Focus Textbox onLoad + select text, use at DataGrid's edit cell to focus at TextBox immediately 
    /// </summary>
    public class ClickSelectTextBox : TextBox
    {
        
        public ClickSelectTextBox()
        {           
            this.Loaded += ClickSelectTextBox_Loaded;
            this.MouseLeftButtonDown += ClickSelectTextBox_Loaded;
        }

        private void ClickSelectTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            DependencyObject source = e.OriginalSource as UIElement;
            //Look for TextBox
            while (source != null && !(typeof(TextBox) != source.GetType()))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            if (source != null)
            {
                var textBox = (TextBox)source;
                if (!textBox.IsKeyboardFocusWithin)
                {
                    textBox.Focus();
                    textBox.SelectAll();
                    e.Handled = true;
                }
            }
        }
    }
}
