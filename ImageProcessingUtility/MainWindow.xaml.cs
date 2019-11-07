using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ImageProcessingUtility.Processor;
using MaterialDesignThemes.Wpf;
using Window = System.Windows.Window;

namespace ImageProcessingUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel Vm;
        public MainWindow()
        {
            InitializeComponent();
            var model = new ImageProcessModel();
            model.AddProcess(new ConvertGrayscale());
            model.AddProcess(new Trim());

            model.LoadImage("sample.jpg");
            Vm = new MainWindowViewModel { IPModel = model };
            DataContext = Vm;
        }

        private async void EditParameterOnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var process = button.CommandParameter as IProcess;
            if (process == null) return;

            await ShowParameterEditDialog(process.EditParameterDialog);
        }

        private async Task ShowParameterEditDialog(UserControl dialog)
        {
            var result = (bool)await DialogHost.Show(dialog);
            if (result) Vm.Refresh();
        }

        private async void AddProcessOnClick(object sender, RoutedEventArgs e)
        {
            Vm.Refresh();
        }

        private async void DeleteProcessOnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var process = button.CommandParameter as IProcess;
            if (process == null) return;

            Vm.IPModel.Processes.Remove(process);
            Vm.Refresh();
        }
    }
}
