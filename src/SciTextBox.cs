using SearchAThing.Sci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SearchAThing.Sci;

namespace SearchAThing.Sci.GUI
{

    public class SciTextBox : TextBox
    {

        void DisplayMeasure()
        {
            var s = Value.ToString();

            Text = s;
        }

        Measure TryParseMeasureFromText()
        {
            return Sci.Measure.TryParse(Value.MU.PhysicalQuantity, Text);
        }

        public static readonly DependencyProperty MeasureProperty = DependencyProperty.Register(
            "Measure",
            typeof(Measure),
            typeof(SciTextBox),
            new PropertyMetadata(null, new PropertyChangedCallback(OnMeasureChanged)));

        public static void OnMeasureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sciTextBox = d as SciTextBox;

            var textMeasure = sciTextBox.TryParseMeasureFromText();

            if (textMeasure == null
                ||
                !textMeasure.ConvertTo(sciTextBox.Value.MU).Value.EqualsAutoTol(sciTextBox.Value.Value))
                sciTextBox.DisplayMeasure();
        }

        public delegate void ValueChangedDelegate(SciTextBox sender, Measure measure);
        public event ValueChangedDelegate ValueChanged;

        public Measure Value
        {
            get { return (Measure)GetValue(MeasureProperty); }
            set
            {
                SetValue(MeasureProperty, value);
                ValueChanged?.Invoke(this, value);
            }
        }

        public SciTextBox()
        {

        }        

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            var measure = TryParseMeasureFromText();

            if (measure != null)
            {
                var curs = CaretIndex;
                Value = measure;
                CaretIndex = curs;

                Foreground = (Brush)ForegroundProperty.DefaultMetadata.DefaultValue;
            }
            else
                Foreground = new SolidColorBrush(Colors.Red);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            SelectAll();
        }

    }

}
