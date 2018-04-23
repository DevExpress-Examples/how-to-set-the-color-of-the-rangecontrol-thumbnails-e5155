# How to set the color of the RangeControl thumbnails


<p>This example demonstrates how you can specify the color of the <a href="http://help.devexpress.com/#WPF/CustomDocument15026">RangeControl</a> thumbnails in code. <br /> The background color of the thumbnail is determined by the ThumbnailControl template. The Border.Background property of the thumbnail is bound to the appointment it represents and uses a custom AppointmentToColorConverter. The converter specifies the color according to words encountered in the Appointment.Location string.<br /><br /><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-set-the-color-of-the-rangecontrol-thumbnails-e5155/15.2.4+/media/253d6038-f3fb-11e4-80bf-00155d62480c.png"></p>

<br/>


