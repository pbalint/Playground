<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <script src="https://code.jquery.com/jquery-3.1.1.slim.min.js" type="text/javascript"></script>
	<script src="https://code.highcharts.com/highcharts.js"></script>
	<script src="https://code.highcharts.com/modules/exporting.js"></script>

	<script>
	$(function () {
	    Highcharts.chart('container', {
	        chart: {
	            type: 'line',
	            zoomType: 'xy'
	        },
	        title: {
	            text: 'Temperature'
	        },
	        xAxis: { type: 'datetime' },
	        yAxis: {
	            title: {
	                text: 'Temperature (°C)'
	            }
	        },
	        series: [{
	            name: 'LM92',
	            data: [
	            <#list model.measurements as measurement>
 					[${measurement.timeStamp?c}, ${measurement.temperature?c}],
				</#list>
	            ],
	            lineWidth: 0.5
	        }]
	    });
	});
</script>
</head>
<body>
	<div id="container" style="height: 100%; width: 100%;"></div>
</body>
</html>