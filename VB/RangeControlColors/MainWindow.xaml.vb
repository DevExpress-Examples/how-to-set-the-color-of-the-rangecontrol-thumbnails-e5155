Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Core
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
		End Sub
	End Class

	Public Class AppointmentToColorConverter
		Inherits MarkupExtension
		Implements IValueConverter
		Private instance As AppointmentToColorConverter = Nothing

		Public Sub New()
		End Sub

		Private privateDefaultColor As Color
		Public Property DefaultColor() As Color
			Get
				Return privateDefaultColor
			End Get
			Set(ByVal value As Color)
				privateDefaultColor = value
			End Set
		End Property

		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			If instance Is Nothing Then
				instance = New AppointmentToColorConverter()
				instance.DefaultColor = DefaultColor
			End If
			Return instance
		End Function
		#Region "IValueConverter Members"
		Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim apt As Appointment = TryCast(value, Appointment)
			If apt.Location Is Nothing Then
				Return Brushes.Pink
			End If
			If apt.Location.Contains("Blue") Then
				Return Brushes.Blue
			End If
			If apt.Location.Contains("Brown") Then
				Return Brushes.Brown
			End If
			Return Brushes.Red
		End Function
		Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Return (CType(value, SolidColorBrush)).Color
		End Function
		#End Region
	End Class
End Namespace
