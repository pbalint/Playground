<html>
<head>
<script src="https://code.jquery.com/jquery-3.1.1.slim.min.js"
    type="text/javascript"></script>
<script src="https://code.highcharts.com/highcharts.js"></script>
<script>
    window.onload = function() {
        window.configuration = {};
        window.configuration.historySize = 20;
        window.configuration.refreshInterval = 1000;

        Highcharts.setOptions({
            global : {
                useUTC : false
            }
        });
        window.chart = Highcharts.chart('container', {
            chart : {
                type : 'line',
                animation : false,
                zoomType : 'xy',
                events : {
                    load : function() {
                        var xhr = new XMLHttpRequest();
                        xhr.open("GET", "/configuration", true);
                        xhr.onload = function() {
                            if (xhr.status === 200) {
                                window.configuration = JSON.parse(xhr.responseText);
                                refresh(window.chart.series)
                            }
                        };
                        xhr.send();
                    }
                }
            },
            title : {
                text : 'Temperature'
            },
            xAxis : {
                type : 'datetime'
            },
            yAxis : {
                title : {
                    text : 'Temperature (°C)'
                }
            },
            series : [ {
                name : 'LM92',
                data : [],
                lineWidth : 0.5
            } ]
        });
    };
    this.latestTs = 0;
    refresh = function(series) {
        for (i = 0; i < series.length; i++) {
            let length = series[i].data.length;
            if (length > 0) {
                this.latestTs = Math.max(this.latestTs, series[i].data[length - 1].x);
            }
        }

        this.getNewPoints(this.latestTs, function(response) {
            response = JSON.parse(response);
            if (series[0].data.length === 0) {
                series[0].setData(response);
            }
            else {
                removeOldPoints = series[0].data.length > window.configuration.historySize;
                for (i = 0; i < response.length; i++) {
                    series[0].addPoint(response[i], false, removeOldPoints);
                }
            }
            window.chart.redraw();
            
            setTimeout(function() { refresh(window.chart.series) }, window.configuration.refreshInterval);
        },
        function(response) {
            setTimeout(function() { refresh(window.chart.series) }, window.configuration.refreshInterval);
        });
    };

    getNewPoints = function(startTs, onSuccess, onFailure) {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", "/points?startTs=" + startTs, true);
        xhr.onload = function() {
            if (xhr.status === 200) {
                onSuccess(xhr.responseText);
            }
            else {
                onFailure(xhr.responseText);
            }
        };
        xhr.onerror = function() {
            onFailure(xhr.responseText);
        }
        xhr.send();
    }
</script>
</head>
<body>
    <div id="container" style="height: 100%; width: 100%;"></div>
</body>
</html>