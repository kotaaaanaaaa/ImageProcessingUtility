using OpenCvSharp;

namespace ImageProcessingUtility.Processor
{
    public sealed class Sobel : ProcessBase
    {
        public Sobel()
        {
            var dialog = new ParameterEditDialog
            {
                Title = { Text = Description }
            };
            dialog.AddTextBox(this, () => XOrder);
            dialog.AddTextBox(this, () => YOrder);
            EditParameterDialog = dialog;
        }

        public override string Description { get; } = "Sobel";

        public int XOrder { get; set; } = 1;

        public int YOrder { get; set; } = 1;

        public override Mat Process(Mat image)
        {
            var result = new Mat();
            Cv2.Sobel(image, result, MatType.CV_8U, XOrder, YOrder);
            return result;
        }
    }
}