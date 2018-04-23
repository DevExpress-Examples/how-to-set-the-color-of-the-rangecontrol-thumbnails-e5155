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
            scheduler.AppointmentViewInfoCustomizing += scheduler_AppointmentViewInfoCustomizing;
            scheduler.Start = DateTime.Today;
            CreateAppointments();
        }

        void scheduler_AppointmentViewInfoCustomizing(object sender, DevExpress.Xpf.Scheduler.AppointmentViewInfoCustomizingEventArgs e)
        {
            if (e.ViewInfo.Location == String.Empty)
                e.ViewInfo.Label = new DevExpress.Xpf.Scheduler.AppointmentLabel(Colors.White, "");
            if (e.ViewInfo.Location.Contains("Orange"))
            e.ViewInfo.Label = new DevExpress.Xpf.Scheduler.AppointmentLabel(Colors.Orange, "");
            if (e.ViewInfo.Location.Contains("Green"))
            e.ViewInfo.Label = new DevExpress.Xpf.Scheduler.AppointmentLabel(Colors.Green, "");
            if (e.ViewInfo.Location.Contains("Blue"))
            e.ViewInfo.Label = new DevExpress.Xpf.Scheduler.AppointmentLabel(Colors.Blue, "");
        }

        private void CreateAppointments()
        {
            string[] keyWords = new string[] { "Orange", "Green", "Blue" };
            for (int i = 0; i < keyWords.Length; i++)
            {
                Appointment apt = new Appointment();
                apt.Start = DateTime.Now.AddHours(i);
                apt.End = DateTime.Now.AddHours(i + 1);
                apt.Location += keyWords[i];
                scheduler.Storage.AppointmentStorage.Add(apt);
            }
        }
    }
    #region #AppointmentToColorConverter
    public class AppointmentToColorConverter : MarkupExtension, IValueConverter
    {
        AppointmentToColorConverter instance = null;

        public AppointmentToColorConverter(){}

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
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Appointment apt = value as Appointment;
            if (apt.Location == String.Empty)
                return Brushes.White;
            if (apt.Location.Contains("Orange"))
                return Brushes.Orange;
            if (apt.Location.Contains("Green"))
                return Brushes.Green;
            if (apt.Location.Contains("Blue"))
                return Brushes.Blue;
            return Brushes.Gray;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SolidColorBrush)value).Color;
        }
    }
    #endregion #AppointmentToColorConverter
}