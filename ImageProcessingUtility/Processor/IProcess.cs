using OpenCvSharp;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ImageProcessingUtility.Processor
{
    public interface IProcess
    {
        string Description { get; }

        Mat Process(Mat image);

        UserControl EditParameterDialog { get; }

        bool CanEditParameter { get; }

        Visibility EditParameterVisibility { get; }
    }
}
