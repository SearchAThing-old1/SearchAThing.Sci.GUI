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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SearchAThing.Sci.GUI
{

    public class SciTextBox : TextBox
    {

        public SciTextBox()
        {
        }

        #region Value [dppc]
        public static readonly DependencyProperty ValueProperty =
          DependencyProperty.Register("Value", typeof(Measure), typeof(SciTextBox),
              new FrameworkPropertyMetadata(null, OnValueChanged));

        public Measure Value
        {
            get
            {
                return (Measure)GetValue(ValueProperty);
            }
            set
            {                
                SetValue(ValueProperty, value);
            }
        }

        static void OnValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var obj = (SciTextBox)source;

            if (obj.Value != null)
                obj.Text = obj.Value.ToString();
        }
        #endregion

        static Brush RedBrush = new SolidColorBrush(Colors.Red);

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            var measure = Sci.Measure.TryParse(Text, Value.MU.PhysicalQuantity);

            if (measure != null)
            {
                if (!Value.ConvertTo(measure.MU).Value.EqualsAutoTol(measure.Value) ||
                    Foreground == RedBrush)
                {
                    var curs = CaretIndex;

                    Value = measure;
                    CaretIndex = curs;

                    Foreground = (Brush)ForegroundProperty.DefaultMetadata.DefaultValue;
                }
            }
            else
                Foreground = RedBrush;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            SelectAll();
        }
    }

}
