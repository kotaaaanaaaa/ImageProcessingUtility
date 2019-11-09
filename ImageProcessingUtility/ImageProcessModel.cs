using ImageProcessingUtility.Processor;
using LiveCharts;
using OpenCvSharp;
using System.Collections.ObjectModel;

namespace ImageProcessingUtility
{
    public class ImageProcessModel
    {
        public Mat Source { get; set; }

        public Mat Result { get; set; }

        public ObservableCollection<IProcess> Processes { get; } = new ObservableCollection<IProcess>();

        public ChartValues<double> SourceBlueHistogram { get; set; }

        public ChartValues<double> SourceGreenHistogram { get; set; }

        public ChartValues<double> SourceRedHistogram { get; set; }

        public ChartValues<double> SourceHistogram { get; set; }

        public ChartValues<double> ResultBlueHistogram { get; set; }

        public ChartValues<double> ResultGreenHistogram { get; set; }

        public ChartValues<double> ResultRedHistogram { get; set; }

        public ChartValues<double> ResultHistogram { get; set; }

        public void LoadImage(string path)
        {
            Source = new Mat(path);
        }

        public void AddProcess(IProcess process)
        {
            Processes.Add(process);
        }

        public void CalcSourceHistogram()
        {
            SourceBlueHistogram = CalcHistogram(Source, 0);
            SourceGreenHistogram = CalcHistogram(Source, 1);
            SourceRedHistogram = CalcHistogram(Source, 2);
            SourceHistogram = CalcHistogram(Source);
        }

        public void CalcResultHistogram()
        {
            ResultBlueHistogram = CalcHistogram(Result, 0);
            ResultGreenHistogram = CalcHistogram(Result, 1);
            ResultRedHistogram = CalcHistogram(Result, 2);
            ResultHistogram = CalcHistogram(Result);
        }

        private static ChartValues<double> CalcHistogram(Mat img)
        {
            if (img.Channels() != 1)
            {
                return new ChartValues<double>();
            }
            return CalcHistogramInternal(img, 0);
        }

        private static ChartValues<double> CalcHistogram(Mat img, int channel)
        {
            if (img.Channels() == 1)
            {
                return new ChartValues<double>();
            }
            return CalcHistogramInternal(img, channel);
        }

        private static ChartValues<double> CalcHistogramInternal(Mat img, int channel)
        {
            var result = new ChartValues<double>();

            if (channel >= img.Channels())
            {
                return result;
            }

            var histogram = new Mat();
            Cv2.CalcHist(new[] { img }, new[] { channel }, new Mat(), histogram, 1, new[] { 256 }, new[] { new float[] { 0, 256 } });


            for (var i = 0; i < histogram.Rows; i++)
            {
                result.Add(histogram.GetArray(i, 0)[0]);
            }
            return result;
        }

        public void Process()
        {
            var image = Source;
            foreach (var ip in Processes)
            {
                image = ip.Process(image);
            }
            Result = image;
        }
    }
}