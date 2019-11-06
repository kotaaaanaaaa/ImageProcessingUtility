using OpenCvSharp;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ImageProcessingUtility.Processor
{
    public abstract class ProcessBase : IProcess
    {
        public virtual string Description => throw new NotImplementedException();

        public virtual Mat Process(Mat image) => throw new NotImplementedException();

        public virtual UserControl EditParameterDialog { get; set; }

        public bool CanEditParameter
        {
            get
            {
                if (EditParameterDialog == null)
                    return false;

                return true;
            }
        }

        public Visibility EditParameterVisibility
        {
            get
            {
                if (EditParameterDialog == null)
                    return Visibility.Hidden;

                return Visibility.Visible;
            }
        }
    }
}
