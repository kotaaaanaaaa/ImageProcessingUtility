using System;
using ImageProcessingUtility.Processor;
using LiveCharts;
using OpenCvSharp.Extensions;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace ImageProcessingUtility
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapSource SourceImage => IPModel.Source.ToBitmap().ToSource();

        public BitmapSource ResultImage => IPModel.Result.ToBitmap().ToSource();

        public ImageProcessModel IPModel { get; set; }

        public ChartValues<double> SourceBlueHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Source, 0); }

        public ChartValues<double> SourceGreenHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Source, 1); }

        public ChartValues<double> SourceRedHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Source, 2); }

        public ChartValues<double> SourceHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Source); }

        public ChartValues<double> ResultBlueHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Result, 0); }

        public ChartValues<double> ResultGreenHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Result, 1); }

        public ChartValues<double> ResultRedHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Result, 2); }

        public ChartValues<double> ResultHistogram { get => ImageProcessModel.CalcHistogram(IPModel.Result); }

        public MainWindowViewModel()
        {
            throw new NotImplementedException();
        }

        public MainWindowViewModel(ImageProcessModel ipModel)
        {
            IPModel = ipModel;

            PropertyChanged.Raise(() => SourceBlueHistogram);
            PropertyChanged.Raise(() => SourceGreenHistogram);
            PropertyChanged.Raise(() => SourceRedHistogram);
            PropertyChanged.Raise(() => SourceHistogram);

            Refresh();
        }

        public void Refresh()
        {
            IPModel.Process();
            PropertyChanged.Raise(() => ResultImage);
            Analyze();
        }

        public void Analyze()
        {
            PropertyChanged.Raise(() => ResultBlueHistogram);
            PropertyChanged.Raise(() => ResultGreenHistogram);
            PropertyChanged.Raise(() => ResultRedHistogram);
            PropertyChanged.Raise(() => ResultHistogram);
        }
    }

    internal class MainWindowViewModelSample : MainWindowViewModel
    {
        public MainWindowViewModelSample()
        {
            IPModel.AddProcess(new ConvertGrayscale());
            IPModel.AddProcess(new Trim());
        }
    }
}