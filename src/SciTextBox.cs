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

using SearchAThing;
using SearchAThing.Sci;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.FormattableString;

namespace SearchAThing.Sci.GUI
{

    public class SciTextBox : TextBox
    {

        static KeyConverter kc = new KeyConverter();

        public SciTextBox()
        {
            TextAlignment = TextAlignment.Right;
        }

        public void BeginEdit(RoutedEventArgs e)
        {
            if (e is KeyEventArgs)
            {
                var ke = (KeyEventArgs)e;

                var ch = kc.ConvertToInvariantString(ke.Key);

                Text = ch.ToString();

                Focus();
            }
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
            {
                if (obj.Value.MU.Equals(MUCollection.Adimensional.adim))
                {
                    var str = obj.Value.ToString(CultureInfo.InvariantCulture, includePQ: false);

                    obj.Text = str;
                }
                else
                    obj.Text = obj.Value.ToString();
            }
            else
                obj.Text = "";
        }
        #endregion

        static Brush RedBrush = new SolidColorBrush(Colors.Red);

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            
            ParseText(Text);
        }

        void ParseText(string text)
        {
            if (Value == null) return;

            var measure = Sci.Measure.TryParse(text, Value.MU.PhysicalQuantity);

            if (measure == null)
            {
                var tval = text;
                if (Value.MU != MUCollection.Adimensional.adim)
                    tval = text + Value.MU.ToString();
                measure = Sci.Measure.TryParse(text + Value.MU.ToString(), Value.MU.PhysicalQuantity);
            }

            if (measure != null)
            {
                var changed = !Value.ConvertTo(measure.MU).Value.EqualsAutoTol(measure.Value);
                if (changed || Foreground == RedBrush)
                {
                    var curs = CaretIndex;



                    var len_before = Text.Length;
                    if (changed)
                    {
                        Value = measure;
                        var len_after = Text.Length;
                        CaretIndex = curs + (len_after - len_before);
                    }

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
