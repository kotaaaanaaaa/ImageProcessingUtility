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

        public ChartValues<double> SourceBlueHistogram { get => IPModel.SourceBlueHistogram; }

        public ChartValues<double> SourceGreenHistogram { get => IPModel.SourceGreenHistogram; }

        public ChartValues<double> SourceRedHistogram { get => IPModel.SourceRedHistogram; }

        public ChartValues<double> SourceHistogram { get => IPModel.SourceHistogram; }

        public ChartValues<double> ResultBlueHistogram { get => IPModel.ResultBlueHistogram; }

        public ChartValues<double> ResultGreenHistogram { get => IPModel.ResultGreenHistogram; }

        public ChartValues<double> ResultRedHistogram { get => IPModel.ResultRedHistogram; }

        public ChartValues<double> ResultHistogram { get => IPModel.ResultHistogram; }

        public MainWindowViewModel()
        {
            throw new NotImplementedException();
        }

        public MainWindowViewModel(ImageProcessModel ipModel)
        {
            IPModel = ipModel;

            IPModel.CalcSourceHistogram();
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
            IPModel.CalcResultHistogram();
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