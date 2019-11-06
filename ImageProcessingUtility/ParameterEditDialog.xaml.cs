using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ImageProcessingUtility
{
    /// <summary>
    /// ParameterEditDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class ParameterEditDialog : UserControl
    {
        public ParameterEditDialog()
        {
            InitializeComponent();
        }

        private string PropertyName<TResult>(Expression<Func<TResult>> property)
        {
            if (!(property.Body is MemberExpression memberEx))
                throw new ArgumentException();
            return memberEx.Member.Name;
        }

        public void AddTextBox<TResult>(object source, Expression<Func<TResult>> property, string hint = "", string label = "")
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                ParameterControls.Children.Add(new TextBlock
                {
                    Text = PropertyName(property),
                    Margin = new Thickness(0,5,0,0),
                });
            }
            else
            {
                ParameterControls.Children.Add(new TextBlock
                {
                    Text = label,
                });
            }

            var textBox = new TextBox();
            ParameterControls.Children.Add(textBox);
            if (!string.IsNullOrWhiteSpace(hint))
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(textBox, hint);
            }

            var bind = new Binding(PropertyName(property));
            bind.Source = source;
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, bind);
        }
    }
}
