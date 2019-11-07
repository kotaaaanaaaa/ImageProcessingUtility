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

        public void LoadImage(string path)
        {
            Source = new Mat(path);
        }

        public void AddProcess(IProcess process)
        {
            Processes.Add(process);
        }

        public static ChartValues<double> CalcHistogram(Mat img)
        {
            if (img.Channels() != 1)
            {
                return new ChartValues<double>();
            }
            return CalcHistogramInternal(img, 0);
        }

        public static ChartValues<double> CalcHistogram(Mat img, int channel)
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
