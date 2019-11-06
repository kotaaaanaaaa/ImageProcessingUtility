using ImageProcessingUtility.Processor;
using OpenCvSharp;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ImageProcessingUtility
{
    public class ImageProcessModel
    {
        public Mat Source;//TODO Notify

        public ObservableCollection<IProcess> Processes { get; } = new ObservableCollection<IProcess>();

        public void LoadImage(string path)
        {
            Source = new Mat(path);
        }

        public void AddProcess(IProcess process)
        {
            Processes.Add(process);
        }

        public Mat Process()
        {
            var image = Source;
            foreach (var ip in Processes)
            {
                image = ip.Process(image);
            }
            return image;
        }
    }
}
