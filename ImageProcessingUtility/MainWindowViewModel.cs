using System;
using System.ComponentModel;
using System.Timers;
using ImageProcessingUtility.Processor;
using OpenCvSharp.Extensions;
using System.Windows.Media.Imaging;

namespace ImageProcessingUtility
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ImageProcessModel IPModel { get; set; }

        public BitmapSource SourceImage { get => IPModel.Source.ToBitmap().ToSource(); }

        public BitmapSource ResultImage { get => IPModel.Process().ToBitmap().ToSource(); }

        public void Refresh()
        {
            PropertyChanged.Raise(() => ResultImage);
        }
    }

    internal class MainWindowViewModelSample : MainWindowViewModel
    {
        public MainWindowViewModelSample()
        {
            IPModel = new ImageProcessModel();
            IPModel.AddProcess(new ConvertGrayscale());
            IPModel.AddProcess(new Trim());
        }
    }
}
