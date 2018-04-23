Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace RangeControlColors
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits DXWindow

        Public Sub New()
            InitializeComponent()
            AddHandler scheduler.AppointmentViewInfoCustomizing, AddressOf scheduler_AppointmentViewInfoCustomizing
            scheduler.Start = Date.Today
            CreateAppointments()
        End Sub

        Private Sub scheduler_AppointmentViewInfoCustomizing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Scheduler.AppointmentViewInfoCustomizingEventArgs)
            If e.ViewInfo.Location = String.Empty Then
                Dim label As AppointmentLabel = Me.scheduler.Storage.AppointmentStorage.Labels.CreateNewLabel("")
                label.SetColor(Colors.White)
                e.ViewInfo.Label = label
            End If
            If e.ViewInfo.Location.Contains("Orange") Then
                Dim label As AppointmentLabel = Me.scheduler.Storage.AppointmentStorage.Labels.CreateNewLabel("")
                label.SetColor(Colors.Orange)
                e.ViewInfo.Label = label
            End If
            If e.ViewInfo.Location.Contains("Green") Then
                Dim label As AppointmentLabel = Me.scheduler.Storage.AppointmentStorage.Labels.CreateNewLabel("")
                label.SetColor(Colors.Green)
                e.ViewInfo.Label = label
            End If
            If e.ViewInfo.Location.Contains("Blue") Then
                Dim label As AppointmentLabel = Me.scheduler.Storage.AppointmentStorage.Labels.CreateNewLabel("")
                label.SetColor(Colors.Blue)
                e.ViewInfo.Label = label
            End If
        End Sub

        Private Sub CreateAppointments()
            Dim keyWords() As String = { "Orange", "Green", "Blue" }
            For i As Integer = 0 To keyWords.Length - 1
                Dim apt As Appointment = Me.scheduler.Storage.CreateAppointment(AppointmentType.Normal)
                apt.Start = Date.Now.AddHours(i)
                apt.End = Date.Now.AddHours(i + 1)
                apt.Location += keyWords(i)
                scheduler.Storage.AppointmentStorage.Add(apt)
            Next i
        End Sub
    End Class
    #Region "#AppointmentToColorConverter"
    Public Class AppointmentToColorConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private instance As AppointmentToColorConverter = Nothing

        Public Sub New()
        End Sub

        Public Property DefaultColor() As Color

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            If instance Is Nothing Then
                instance = New AppointmentToColorConverter()
                instance.DefaultColor = DefaultColor
            End If
            Return instance
        End Function
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim apt As Appointment = TryCast(value, Appointment)
            If apt.Location = String.Empty Then
                Return Brushes.White
            End If
            If apt.Location.Contains("Orange") Then
                Return Brushes.Orange
            End If
            If apt.Location.Contains("Green") Then
                Return Brushes.Green
            End If
            If apt.Location.Contains("Blue") Then
                Return Brushes.Blue
            End If
            Return Brushes.Gray
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DirectCast(value, SolidColorBrush).Color
        End Function
    End Class
    #End Region ' #AppointmentToColorConverter
End Namespace