using OpenCvSharp;

namespace ImageProcessingUtility.Processor
{
    public sealed class ConvertGrayscale : ProcessBase
    {
        public override string Description { get;  } = "Grayscale";

        public override Mat Process(Mat image)
        {
            return image.CvtColor(ColorConversionCodes.BGR2GRAY);
        }

    }
}
