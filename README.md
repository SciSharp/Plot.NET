# Plot.NET
Plot data as HTML

[![Join the chat at https://gitter.im/publiclab/publiclab](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/sci-sharp/community) [![NuGet](https://img.shields.io/nuget/dt/PlotNET.svg)](https://www.nuget.org/packages/PlotNET)

Convert data into `plotly.js` compatible format and generate an HTML showing in any browser.

The API keep same as `matplotlib`.

For example:

```csharp
var trace1 = new 
{
	x: new int[]{1, 2, 3, 4},
    y: new int[]{10, 15, 13, 17},
    type: 'scatter'
};

var trace2 = new 
{
    x: new int[]{1, 2, 3, 4},
    y: new int[]{16, 5, 11, 9},
    type: 'scatter'
};
```

```html
<head>
  <!-- Plotly.js -->
  <script src="https://cdn.plot.ly/plotly-latest.min.js"></script>
</head>

<!-- Plots go in blank <div> elements. 
    You can size them in the plot layout,
    or give the div a size as shown here.
-->
<div id="tester" style="width:90%;height:250px;"></div>

<script>
    TESTER = document.getElementById('tester');

    Plotly.plot( TESTER, [{
        x: [1, 2, 3, 4, 5],
        y: [1, 2, 4, 8, 16] }], { 
        margin: { t: 0 } }, {showSendToCloud:true} );
</script>
```

