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

namespace OLDSearchAThing.Sci.GUI
{

    public class MeasureVar : DependencyObject
    {

        #region Measure [dppc]
        public static readonly DependencyProperty MeasureProperty =
          DependencyProperty.Register("Measure", typeof(Measure), typeof(MeasureVar), new FrameworkPropertyMetadata(null, OnMeasureChanged));

        public Measure Measure
        {
            get
            {
                return (Measure)GetValue(MeasureProperty);
            }
            set
            {
                SetValue(MeasureProperty, value);
            }
        }

        static void OnMeasureChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MeasureVar)source;
        }
        #endregion

        public MeasureVar(Measure _measure)
        {
            Measure = _measure;
        }

    }

}