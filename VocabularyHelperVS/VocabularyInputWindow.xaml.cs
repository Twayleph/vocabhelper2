using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VocabularyHelperVS
{
    /// <summary>
    /// Interaction logic for VocabularyInputWindow.xaml
    /// </summary>
    public partial class VocabularyInputWindow : Window
    {
        public VocabularyInputWindow()
        {
            InitializeComponent();
            Loaded += VocabularyInputWindow_Loaded;
            dataGrid.AddingNewItem += DataGrid_AddingNewItem;
        }

        private void VocabularyInputWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SelectDefaultCell();
        }

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            SelectDefaultCell();
        }

        private void SelectDefaultCell()
        {
            dataGrid.Focus();
            dataGrid.CurrentCell = new DataGridCellInfo(
                dataGrid.Items[dataGrid.Items.Count - 1], dataGrid.Columns[0]
            );
        }
    }
}
