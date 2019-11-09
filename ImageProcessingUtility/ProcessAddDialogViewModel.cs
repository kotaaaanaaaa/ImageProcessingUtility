using ImageProcessingUtility.Processor;
using System.Collections.Generic;
using System.ComponentModel;

namespace ImageProcessingUtility
{
    public class ProcessAddDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<IProcess> Processes { get; set; } = new List<IProcess>();

        public IProcess _selected;

        public IProcess Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged.Raise(()=>Selected);
            }
        }

        public ProcessAddDialogViewModel()
        {
            Processes.Add(new Trim());
            Processes.Add(new ConvertGrayscale());
            Processes.Add(new ThresholdBinary());
            Processes.Add(new Sobel());
        }
    }
}