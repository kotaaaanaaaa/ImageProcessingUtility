using System.Windows.Controls;
using OpenCvSharp;

namespace ImageProcessingUtility.Processor
{
    public sealed class Trim : ProcessBase
    {
        public Trim()
        {
            var dialog = new ParameterEditDialog
            {
                Title = {Text = Description}
            };
            dialog.AddTextBox(this, () => X);
            dialog.AddTextBox(this, () => Y);
            dialog.AddTextBox(this, () => Width);
            dialog.AddTextBox(this, () => Height);
            EditParameterDialog = dialog;
        }

        public override string Description { get; } = "Triming";

        public int X { get; set; } = 100;
        public int Y { get; set; } = 100;
        public int Width { get; set; } = 200;
        public int Height { get; set; } = 150;

        public override Mat Process(Mat image)
        {
            return image.Clone(new Rect(X, Y, Width, Height));
        }
    }
}
