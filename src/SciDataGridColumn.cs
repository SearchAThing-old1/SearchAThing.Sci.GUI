#region SearchAThing.Sci.GUI, Copyright(C) 2016 Lorenzo Delana, License under MIT
/*
* The MIT License(MIT)
* Copyright(c) 2016 Lorenzo Delana, https://searchathing.com
*
* Permission is hereby granted, free of charge, to any person obtaining a
* copy of this software and associated documentation files (the "Software"),
* to deal in the Software without restriction, including without limitation
* the rights to use, copy, modify, merge, publish, distribute, sublicense,
* and/or sell copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
* DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Media;

namespace SearchAThing.Sci.GUI
{

    public class SciDataGridColumn : DataGridColumn, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged [propce]       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SendPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public SciDataGridColumn()
        {
        }

        public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;

        /// <summary>
        /// display measure unit in textblock
        /// </summary>
        public bool DisplayMU { get; set; } = true;

        void ApplyBinding(DependencyObject depObj, DependencyProperty depProp)
        {
            if (Binding != null)
                BindingOperations.SetBinding(depObj, depProp, Binding);
            else
                BindingOperations.ClearBinding(depObj, depProp);
        }

        #region Binding [propc]
        BindingBase _Binding;
        public BindingBase Binding
        {
            get
            {
                return _Binding;
            }
            set
            {
                if (_Binding != value)
                {
                    _Binding = value;
                    SendPropertyChanged("Binding");
                }
            }
        }
        #endregion        

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var stb = new SciTextBox();

            ApplyBinding(stb, SciTextBox.ValueProperty);

            return stb;
        }        

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var stb = new SciTextBlock() { DisplayMU = DisplayMU };

            ApplyBinding(stb, SciTextBlock.ValueProperty);

            return stb;
        }

        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            var stb = editingElement as SciTextBox;

            if (stb != null)
            {
                stb.BeginEdit(editingEventArgs);
                if (editingEventArgs == null) stb.Focus(); // F2

                return stb.Value;
            }

            return null;
        }

        protected override bool CommitCellEdit(FrameworkElement editingElement)
        {
            return base.CommitCellEdit(editingElement);
        }

        protected override void CancelCellEdit(FrameworkElement editingElement, object uneditedValue)
        {
            base.CancelCellEdit(editingElement, uneditedValue);
        }

    }

    public class Cvt1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
