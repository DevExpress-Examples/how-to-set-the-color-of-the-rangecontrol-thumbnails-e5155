using DevExpress.Xpf.Core;
using DevExpress.XtraScheduler;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace RangeControlColors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class AppointmentToColorConverter : MarkupExtension, IValueConverter
    {
        AppointmentToColorConverter instance = null;

        public AppointmentToColorConverter()
        {
        }

        public Color DefaultColor { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (instance == null)
            {
                instance = new AppointmentToColorConverter();
                instance.DefaultColor = DefaultColor;
            }
            return instance;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Appointment apt = value as Appointment;
            if (apt.Location == null)
                return Brushes.Pink;
            if (apt.Location.Contains("Blue"))
                return Brushes.Blue;
            if (apt.Location.Contains("Brown"))
                return Brushes.Brown;
            return Brushes.Red;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SolidColorBrush)value).Color;
        }
        #endregion
    }
}
