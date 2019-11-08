using OpenCvSharp;

namespace ImageProcessingUtility.Processor
{
    public sealed class ThresholdBinary : ProcessBase
    {
        public ThresholdBinary()
        {
            var dialog = new ParameterEditDialog
            {
                Title = { Text = Description }
            };
            dialog.AddTextBox(this, () => Threshold);
            dialog.AddTextBox(this, () => MaxValue);
            EditParameterDialog = dialog;
        }

        public override string Description { get; } = "Threshold Binary";

        public int Threshold { get; set; } = 128;

        public int MaxValue { get; set; } = 255;

        public override Mat Process(Mat image)
        {
            var pre = new ConvertGrayscale();
            image = pre.Process(image);

            var result = new Mat();
            Cv2.Threshold(image, result, Threshold, MaxValue, ThresholdTypes.Binary);
            return result;
        }
    }
}