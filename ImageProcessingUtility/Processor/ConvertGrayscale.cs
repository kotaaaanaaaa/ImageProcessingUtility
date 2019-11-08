using OpenCvSharp;

namespace ImageProcessingUtility.Processor
{
    public sealed class ConvertGrayscale : ProcessBase
    {
        public override string Description { get;  } = "Grayscale";

        public override Mat Process(Mat image)
        {
            if (image.Channels() == 3)
            {
                return image.CvtColor(ColorConversionCodes.BGR2GRAY);
            }
            return image;
        }
    }
}