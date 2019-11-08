using System.Windows.Controls;

namespace ImageProcessingUtility
{
    /// <summary>
    /// ProcessAddDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class ProcessAddDialog : UserControl
    {
        ProcessAddDialogViewModel vm = new ProcessAddDialogViewModel();

        public ProcessAddDialog()
        {
            InitializeComponent();
            DataContext = vm;
            if (vm.Processes.Count != 0)
            {
                vm.Selected = vm.Processes[0];
            }
        }
    }
}