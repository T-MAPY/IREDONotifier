using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace IREDONotifier.View
{
    /// <summary>
    /// My DataGrid implementing multiselected rows see
    /// http://stackoverflow.com/questions/22868445/wpf-binding-selecteditems-in-mvvm
    /// </summary>
    public class MyDataGrid : DataGrid
    {
        public MyDataGrid()
        {
            this.SelectionChanged += MyDataGrid_SelectionChanged;
        }

        void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = this.SelectedItems;
        }

        #region SelectedItemsList

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
                DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(MyDataGrid), new PropertyMetadata(null));

        #endregion
    }
}
