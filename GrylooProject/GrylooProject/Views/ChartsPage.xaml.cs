using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;

using GrylooProject.Model;
using Rg.Plugins.Popup.Extensions;
using Newtonsoft.Json;
using GrylooProject.Repository;
using System.Globalization;

namespace GrylooProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartsPage : ContentPage
    {
        public static string winnerType = "";
        public static string LeaderwinnerType = "";
        public static string rateType = "60days";
        public static string LinerateType = "1year";
        public static string RegionCode = "";
        public static string ProvinceCode = "";


        public static bool dropdown = false;
        public static string RegionId = "";

        public static bool onappCheck = false;
        public static string leaderchartName = "";
        public List<object> leaderColor = null;
        public List<object> leadername = null;
        public List<object> leadervote = null;


        public static int CandidatureId = 0;
        public static int LeaderId = 0;

        public static string CandidaturechartName = "";
        public List<object> CandidatureColor = null;
        public List<object> Candidaturename = null;
        public List<object> Candidaturevote = null;


        public static string CandidatureOrigenchartName = "";
        public List<object> CandidatureOrigenColor = null;
        public List<object> CandidatureOrigenname = null;
        public List<object> CandidatureOrigenvote = null;
       
        public static string CandidatureDestinochartName = "";
        public List<object> CandidatureDestinoColor = null;
        public List<object> CandidatureDestinoname = null;
        public List<object> CandidatureDestinovote = null;



        public static string CandidatureDoughnutchartName = "";
        public List<object> CandidatureDoughnutColor = null;
        public List<object> CandidatureDoughnutname = null;
        public List<object> CandidatureDoughnutvote = null;

        public List<string> CandidatureEvaluationTime = null;

        public List<string> GryllooVoteTime = null;
        public List<GryllooVoteEvaluationModel> _grylloovoteEvaluationModel = new List<GryllooVoteEvaluationModel>();
       

        public List<string> GryllooUserTime = null;
        public List<GryllooVoteEvaluationModel> _gryllooUserEvaluationModel = new List<GryllooVoteEvaluationModel>();
       


        public List<string> LeaderEvaluationTime = null;

        public List<string> CandidatureGroupTime = null;
        public List<string> LeaderGroupTime = null;


        public List<VoteEvaluationModel> _voteEvaluationModel = new List<VoteEvaluationModel>();

        public List<VoteEvaluationModel> _leaderEvaluationModel = new List<VoteEvaluationModel>();

        public List<GroupChart> _voteGroupModel = new List<GroupChart>();

        public List<GroupChart> _voteLeaderGroupModel = new List<GroupChart>();

        public List<ChartWinnerData> _voteWinnerModel = new List<ChartWinnerData>();

        public List<LeaderWinner> _rateWinnerModel = new List<LeaderWinner>();




        double height = App.ScreenHeight;
        double width = App.ScreenWidth;

        public static int ChartNameId = 0;
        public static string ChartName = "";
        public static string ChartsType = "";

        public ChartsPage()
        {


            

            rateType = "60days";
            RegionCode = "";
            ProvinceCode = "";
            Device.BeginInvokeOnMainThread(async () =>
            {
                
                await LeaderChartData();
            });






        }
        protected override void OnAppearing()
        {
            Help.navigationCheck = false;
        }

        private async void Candidature_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {


                var picker = (Picker)sender;
                var hh = (CandidatureDetail)picker.SelectedItem;

                if (picker != null)
                {
                    CandidatureId = hh.CandidatureCode;

                    await CandidatureGroupPikerSelect();
                }

            }
            catch (Exception ex)
            {

            }
        }


        private async void Origen_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {


                var picker = (Picker)sender;
                var hh = (CandidatureDetail)picker.SelectedItem;

                if (picker != null)
                {
                    CandidatureId = hh.CandidatureCode;

                    await CandidatureOrigenChartData();
                }

            }
            catch (Exception ex)
            {

            }
        }
        private async void Destino_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {


                var picker = (Picker)sender;
                var hh = (CandidatureDetail)picker.SelectedItem;

                if (picker != null)
                {
                    CandidatureId = hh.CandidatureCode;

                    await CandidatureDestinoChartData();
                }

            }
            catch (Exception ex)
            {

            }
        }
        private async void Leader_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {


                var picker = (Picker)sender;
                var hh = (LeaderData)picker.SelectedItem;

                if (picker != null)
                {
                    LeaderId = hh.LeaderCode;

                    await LeaderGroupPikerSelect();
                }

            }
            catch (Exception ex)
            {

            }
        }
        private async void Winner_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                var picker = (Picker)sender;
                if (picker != null)
                {

                    if (Convert.ToString(picker.SelectedItem) == "Region" || Convert.ToString(picker.SelectedItem) == "Comunidad")
                    {
                        winnerType = "Resion";
                    }

                    else
                    {
                        winnerType = "Province";
                    }
                    await CandidatureWinnerPikerSelect();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void LeaderWinner_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                var picker = (Picker)sender;
                if (picker != null)
                {

                    if (Convert.ToString(picker.SelectedItem) == "Region" || Convert.ToString(picker.SelectedItem) == "Comunidad")
                    {
                        LeaderwinnerType = "Resion";
                    }

                    else
                    {
                        LeaderwinnerType = "Province";
                    }
                    await LeaderWinnerPikerSelect();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void pickerReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdown == false)
            {
                var picker = (Picker)sender;
                if (picker != null)
                {
                    if (Convert.ToString(picker.SelectedItem) == "Last 7 Days" || Convert.ToString(picker.SelectedItem)=="Últimos 7 días")
                    {
                        rateType = "7days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 15 Days" || Convert.ToString(picker.SelectedItem) == "Últimos 15 días")
                    {
                        rateType = "15days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 30 Days" || Convert.ToString(picker.SelectedItem) == "Últimos 30 días")
                    {
                        rateType = "30days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 60 Days" || Convert.ToString(picker.SelectedItem) == "Últimos 60 días")
                    {
                        rateType = "60days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 1 year" || Convert.ToString(picker.SelectedItem) == "Último año")
                    {
                        rateType = "1year";
                    }
                    else
                    {
                        rateType = "";
                    }

                }


                await PikerSelect();
            }
            dropdown = false;
            return;
        }


        private async void MultiLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var picker = (Picker)sender;
                if (picker != null)
                {

                    if (Convert.ToString(picker.SelectedItem) == "Last 3 month" || Convert.ToString(picker.SelectedItem) == "Últimos 3 meses")
                    {
                        LinerateType = "6month";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 2 month" || Convert.ToString(picker.SelectedItem) == "Últimos 2 meses")
                    {
                        LinerateType = "2month";
                    }
                    else
                    {
                        LinerateType = "1year";
                    }
                    await CandidatureEvaluationPikerSelect();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void MultiLineLeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var picker = (Picker)sender;
                if (picker != null)
                {

                    if (Convert.ToString(picker.SelectedItem) == "Last 3 month" || Convert.ToString(picker.SelectedItem) == "Últimos 3 meses")
                    {
                        LinerateType = "6month";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 2 month" || Convert.ToString(picker.SelectedItem) == "Últimos 2 meses")
                    {
                        LinerateType = "2month";
                    }
                    else
                    {
                        LinerateType = "1year";
                    }
                    await LeaderEvaluationPikerSelect();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void pickerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {


                var picker = (Picker)sender;
                var hh = (PostalDetail)picker.SelectedItem;

                if (picker != null)
                {
                    RegionId = hh.RegionCodeINE;
                    RegionCode = hh.RegionCodeINE;
                    if (hh.RegionCodeINE == "")
                    { pickerProvince.SelectedItem = null; pickerProvince.IsEnabled = false; }
                    else
                    {
                        pickerProvince.IsEnabled = true;
                        await ProvinceResult();
                    }


                }
                ProvinceCode = "";
                //  await LeaderChartData();
                await PikerSelect();
            }
            catch (Exception ex)
            {

            }
        }

        private async void pickerProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pickerProvince.IsEnabled = true;

                var picker = (Picker)sender;
                var hh = (PostalDetail)picker.SelectedItem;

                if (picker != null)
                {
                    ProvinceCode = hh.ProvincePostalCode;
                    await PikerSelect();
                }


            }
            catch (Exception ex)
            {

            }
        }

        public void imgStoreTapped(object sender, EventArgs e)
        {

            pickerProductCategory.Focus();

        }
        // for Candidature bar chart
        private string GetHtmlWithChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%;"">
                          <head>{chartJsScript}
{chartnpm}
{chartdatalabel}
</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetChartScript()
        {
            var chartConfig1 = GetSpendingChartConfig();

            var aa = chartConfig1.Replace(@"\", "");
            string aa1 = "" + '"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig = chartConfig2.Replace(aa2, "");

            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }


        private string GetSpendingChartConfig()
        {
            var plugins = @"yadev:{""datalabels"":{""color"": ""white"",""display"": function (context) {},""font"": {""weight"": ""bold""},}}yadav";

            var config = new
            {
                type = "horizontalBar",

                data = GetPieChartData(),
                options = new
                {



                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "top",
                        display = false,

                    },
                    animation = new
                    {
                        animateScale = true
                    },
                    plugins

                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetPieChartData()
        {
            Candidaturevote.Add("0");
            var dataPoints = Candidaturevote;// new[] { "3", "7", "8", "2", "8", "9" };
            var colors = CandidatureColor;// new[] { "#3376BB","#E00006","#C31DA6","#E97208","#1369D3","#D51617" };
            var labels = Candidaturename;// new[] { "Groceries", "Car", "Flat", "Electronics", "Entertainment", "Insurance" };
            var randomGen = new Random();

            var data = new
            {
                datasets = new[]
                {
                    new
                    {
                       label = CandidaturechartName,
                        data = dataPoints,
                        backgroundColor=colors,
                         datalabels=new{
                               align="start",
                anchor = "end"
                         }
                    }
                },
                labels
            };
            return data;
        }

        // for Candidature Doughnut chart
        private string GetHtmlWithDoughnutChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%;"">
                          <head>{chartJsScript}
{chartnpm}
{chartdatalabel}
</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetDoughnutChartScript()
        {
            var chartConfig1 = GetSpendingDoughnutChartConfig();

            var aa = chartConfig1.Replace(@"\", "");
            string aa1 = "" + '"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig = chartConfig2.Replace(aa2, "");

            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }


        private string GetSpendingDoughnutChartConfig()
        {
            var plugins = @"yadev:{""datalabels"":{""backgroundColor"": function (context) {return context.dataset.backgroundColor;},""borderColor"": ""white"",""borderRadius"": 10,""borderWidth"": 2,""color"": ""white"",""display"": function (context) {var dataset = context.dataset;var count = dataset.data.length;var value = dataset.data[context.dataIndex];return value;},""font"": {""weight"": ""bold""},""formatter"": function (value, context) {var lbl = context.chart.data.labels[context.dataIndex];var newString = lbl.replace('%','');return newString + "" "" + value + ""%"";}}}yadav";

            var config = new
            {
                type = "doughnut",

                data = GetDoughnuthartData(),
                options = new
                {



                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "top",
                        display = false,

                    },
                    animation = new
                    {
                        animateScale = true
                    },
                    plugins





                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetDoughnuthartData()
        {

            var dataPoints = CandidatureDoughnutvote;// new[] { "3", "7", "8", "2", "8", "9" };
            var colors = CandidatureDoughnutColor;// new[] { "#3376BB","#E00006","#C31DA6","#E97208","#1369D3","#D51617" };
            var labels = CandidatureDoughnutname;// new[] { "Groceries", "Car", "Flat", "Electronics", "Entertainment", "Insurance" };
            var randomGen = new Random();

            var data = new
            {
                datasets = new[]
                {
                    new
                    {
                       label = CandidatureDoughnutchartName,
                        data = dataPoints,
                        backgroundColor=colors,
                         datalabels=new{
                anchor= "center"
                         }
                    }
                },
                labels
            };
            return data;
        }
        // for Candidature Multiline chart
        private string GetHtmlWithMultiLineChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:98%;height:100%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas style=""width:98%;height:98%;"" id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:98%;height:100%"">
                          <head>{chartJsScript}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetMultiLineChartScript()
        {
            var chartConfig = GetSpendingMultiLineChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingMultiLineChartConfig()
        {
            var config = new
            {
                type = "line",

                data = GetMultiLineChartData(),
                options = new
                {
                    legend = new
                    {
                        position = "bottom",
                        display = true,
                    },
                    tooltips = new
                    {
                        mode = "label",intersect=false
                    },
                    hover = new
                    {
                        mode = "label",
                        intersect = false
                    },
                    spanGaps = true
                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetMultiLineChartData()
        {
            var aa = new
            {
                data = new[] { 10, 100, 77, 60, 60, 120, 150, 200, 700, 2000 },
                label = "Rahul",
                borderColor = "#FF0000",
                backgroundColor = "transparent"
            };




            var data = new
            {
                labels = CandidatureEvaluationTime,
                //datasets = new[]
                //{
                //    _voteEvaluationModel
                //},
                datasets = _voteEvaluationModel
            };
            return data;
        }

        // for Candidature Group chart
        private string GetHtmlWithGroupChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";

            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas style=""width:110%;height:98%;"" id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%"">
                          <head>{chartJsScript}
{chartnpm}
{chartdatalabel}
</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetGroupChartScript()
        {
            var chartConfig1 = GetSpendingGroupChartConfig();
            var aa = chartConfig1.Replace(@"\", "");
            string aa1 = "" + '"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig = chartConfig2.Replace(aa2, "");
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingGroupChartConfig()
        {
            var plugins = @"yadev:{""datalabels"":{""align"":""center"",""anchor"":""end"",""color"": ""black"",""display"": function (context) {},""font"": {""size"": ""5""}, ""formatter"": function (value, context) { return  value + '%'; }}}yadav";

           var scales = @"yadev: {xAxes: [{barThickness: 15}]}yadav";

           // var scales = @"yadev: {xAxes: [{""categoryPercentage"": 0.5,""barPercentage"": 0.5}]}yadav";


            var config = new
            {
                type = "bar",

                data = GetGroupChartData(),
                options = new
                {
                    
                    legend = new
                    {
                        position = "bottom",
                        display = true,
                    },
                    plugins ,scales
        }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetGroupChartData()
        {
            var aa = new
            {
                data = new[] { 133, 221, 783, 2478 },
                label = "Rahul",
                backgroundColor = "#9AD0F5"
            };
            var aa1 = new
            {
                data = new[] { 408, 547, 675, 734 },
                label = "Yadav",
                backgroundColor = "#F6A8B8"
            };



            var data = new
            {
                labels = CandidatureGroupTime, //new[] { "1900", "1950", "1999", "2050" },
                                               //datasets = new[]
                                               //{
                                               //    aa,aa1
                                               //},
                datasets = _voteGroupModel,
                datalabels = new
                {
                    align = "end",
                    anchor = "end"
                }
            };
            return data;
        }




        // for Candidature Winner chart
        private string GetHtmlWithWinnerChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:102%;height:100%;\"";

            var chartJsScript = "<script src=\"https://code.highcharts.com/maps/highmaps.js\"></script>";
            var chartJsScript1 = "<script src=\"https://code.highcharts.com/maps/modules/exporting.js\"></script>";
           // var chartJsScript2 = "<script src=\"https://code.highcharts.com/mapdata/countries/es/es-all.js\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <div style=""width:102%;height:100%"" id=""chart""></div>
                        </div>";
            var document = $@"<html style=""width:102%;height:100%"">
                          <head>{chartJsScript}{chartJsScript1}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetWinnerChartScript()
        {
            var chartConfig = GetSpendingWinnerChartConfig();
            var aa = chartConfig.Substring(1);
            var chartConfig1 = aa.Remove(aa.Length - 1);

            var script = $@"var config = {chartConfig1};
            window.onload = function() {{
            Highcharts.mapChart('chart', config);
        }};";
            return script;
        }


        private string GetSpendingWinnerChartConfig()
        {


           // var dd = "{ chart: {  map: 'countries/es/es-all',}, title: {text: '' }, exporting: { enabled: false },credits:{enabled: false}, mapNavigation: { enabled: false,buttonOptions: {verticalAlign: 'bottom'}}, series: [";


            var dd = "{tooltip: {pointFormat: '<br/>{point.name}:{point.value}%'},mapNavigation: {enabled: true, buttonOptions: {verticalAlign: 'bottom' }}, title: {text: '' }, exporting: { enabled: false },credits:{enabled: false}, series: [";
           
            //foreach (var item in _voteWinnerModel)
            //{
            //    dd += "{ showInLegend: false, data: [{value: '" + item.per + "',path: '" + item.PSName + "',name: '" + item.PCode + "'}], name: '" + item.Name + "', color: '" + item.CandidatureColor + "', allAreas: false, },";

            //}



            foreach (var item in _voteWinnerModel)
            {

                dd += "{name: '" + item.Name + "', color: '" + item.CandidatureColor + "', allAreas: false,data: [";
                foreach (var item1 in item.voteWinnerChart22)
                {

                    string rrr = item1.per.ToString();
                    


                    dd += "{value: '" + rrr.Replace(",",".") + "',path: '" + item1.PSName + "',name: '" + item1.PCode + "'},";
                }
                dd = dd.Remove(dd.Length - 1);
                dd += "]},";


            }

            dd += "{'color':'black',showInLegend: false,type: 'mapline',data: [{name: 'path3682',path: 'M11,-457,325,-457,325,-343'}]}] }";


            var jsonConfig = JsonConvert.SerializeObject(dd);
            return jsonConfig;
        }

        private object GetWinnerChartData()
        {
            var aa = new
            {
                data = new[] { 133, 221, 783, 2478 },
                label = "Rahul",
                backgroundColor = "#9AD0F5"
            };
            var aa1 = new
            {
                data = new[] { 408, 547, 675, 734 },
                label = "Yadav",
                backgroundColor = "#F6A8B8"
            };



            var data = new
            {
                labels = CandidatureGroupTime, //new[] { "1900", "1950", "1999", "2050" },
                                               //datasets = new[]
                                               //{
                                               //    aa,aa1
                                               //},
                datasets = _voteGroupModel
            };
            return data;
        }












        // for leader chart

        private string GetHtmlWithChartConfig1(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%;"">
                          <head>{chartJsScript}
{chartnpm}
{chartdatalabel}
</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }
        private string GetChartScript1()
        {
            var chartConfig1 = GetSpendingChartConfig1();
            var aa = chartConfig1.Replace(@"\", "");
            string aa1 = "" + '"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig = chartConfig2.Replace(aa2, "");

            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }
        private string GetSpendingChartConfig1()
        {


            var plugins = @"yadev:{""datalabels"":{""color"": ""white"",""display"": function (context) {},""font"": {""weight"": ""bold""},}}yadav";



            var config = new
            {
                type = "horizontalBar",
                data = GetPieChartData1(),
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "top",
                        display = false
                    },
                    animation = new
                    {
                        animateScale = true
                    },
                    plugins
                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetPieChartData1()
        {

            leadervote.Add("0");
            var dataPoints = leadervote;// new[] { "3", "7", "8", "2", "8", "9" };
            var colors = leaderColor;// new[] { "#3376BB","#E00006","#C31DA6","#E97208","#1369D3","#D51617" };
            var labels = leadername;// new[] { "Groceries", "Car", "Flat", "Electronics", "Entertainment", "Insurance" };
            var randomGen = new Random();

            var data = new
            {
                datasets = new[]
                {
                    new
                    {
                        label = leaderchartName,
                        data = dataPoints,
                        backgroundColor=colors,
                         datalabels=new{
                               align="start",
                anchor = "end"
                         }
                    }
                },
                labels
            };
            return data;
        }

        // for Leader Multiline chart
        private string GetHtmlWithLeaderMultiLineChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:98%;height:100%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas style=""width:98%;height:98%;"" id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:98%;height:100%"">
                          <head>{chartJsScript}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetLeaderMultiLineChartScript()
        {
            var chartConfig = GetSpendingLeaderMultiLineChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingLeaderMultiLineChartConfig()
        {
            var config = new
            {
                type = "line",

                data = GetLeaderMultiLineChartData(),
                options = new
                {
                    legend = new
                    {
                        position = "bottom",
                        display = true,
                    },
                    tooltips = new
                    {
                        mode = "label",
                        intersect = false
                    },
                    hover = new
                    {
                        mode = "label",
                        intersect = false
                    },
                    spanGaps = true
                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetLeaderMultiLineChartData()
        {
            var aa = new
            {
                data = new[] { 10, 100, 77, 60, 60, 120, 150, 200, 700, 2000 },
                label = "Rahul",
                borderColor = "#FF0000",
                backgroundColor = "transparent"
            };




            var data = new
            {
                labels = LeaderEvaluationTime,
                //datasets = new[]
                //{
                //    _voteEvaluationModel
                //},
                datasets = _leaderEvaluationModel
            };
            return data;
        }

        // for Leader Group chart
        private string GetHtmlWithLeaderGroupChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";

            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas style=""width:130%;height:98%;"" id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%"">
                          <head>{chartJsScript}
{chartnpm}
{chartdatalabel}
</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetLeaderGroupChartScript()
        {
            var chartConfig1 = GetSpendingLeaderGroupChartConfig();
            var aa = chartConfig1.Replace(@"\", "");
            string aa1 = "" + '"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig = chartConfig2.Replace(aa2, "");
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingLeaderGroupChartConfig()
        {
            var plugins = @"yadev:{""datalabels"":{""align"":""center"",""anchor"":""end"",""color"": ""black"",""display"": function (context) {},""font"": {""size"": ""5""}, ""formatter"": function (value, context) { return  value; }}}yadav";
            //var scales = @"yadev: {xAxes: [{""categoryPercentage"": 0.3,""barPercentage"": 0.3, stacked: true,barThickness: 15}]}yadav";
            var scales = @"yadev: {xAxes: [{barThickness: 15}]}yadav";
            var config = new
            {
                type = "bar",

                data = GetLeaderGroupChartData(),
                options = new
                {
                    legend = new
                    {
                        position = "bottom",
                        display = true,
                    },
                    plugins,scales

                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetLeaderGroupChartData()
        {
            var aa = new
            {
                data = new[] { 133, 221, 783, 2478 },
                label = "Rahul",
                backgroundColor = "#9AD0F5"
            };
            var aa1 = new
            {
                data = new[] { 408, 547, 675, 734 },
                label = "Yadav",
                backgroundColor = "#F6A8B8"
            };



            var data = new
            {
                labels = LeaderGroupTime, //new[] { "1900", "1950", "1999", "2050" },
                                          //datasets = new[]
                                          //{
                                          //    aa,aa1
                                          //},
                datasets = _voteLeaderGroupModel,
                datalabels = new
                {
                    align = "end",
                    anchor = "end"
                }
            };
            return data;
        }


        // for Leader Winner chart
        private string GetHtmlWithLeaderWinnerChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:102%;height:100%;\"";

            var chartJsScript = "<script src=\"https://code.highcharts.com/maps/highmaps.js\"></script>";
            var chartJsScript1 = "<script src=\"https://code.highcharts.com/maps/modules/exporting.js\"></script>";
           // var chartJsScript2 = "<script src=\"https://code.highcharts.com/mapdata/countries/es/es-all.js\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <div style=""width:102%;height:100%;"" id=""chart1""></div>
                        </div>";
            var document = $@"<html style=""width:102%;height:100%"">
                          <head>{chartJsScript}{chartJsScript1}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetLeaderWinnerChartScript()
        {
            var chartConfig = GetSpendingLeaderWinnerChartConfig();
            var aa = chartConfig.Substring(1);
            var chartConfig1 = aa.Remove(aa.Length - 1);

            var script = $@"var config = {chartConfig1};
            window.onload = function() {{
            Highcharts.mapChart('chart1', config);
        }};";
            return script;
        }


        private string GetSpendingLeaderWinnerChartConfig()
        {




            var dd = "{ mapNavigation: {enabled: true, buttonOptions: {verticalAlign: 'bottom' }},title: {text: '' }, exporting: { enabled: false },credits:{enabled: false}, series: [";
           

            foreach (var item in _rateWinnerModel)
            {

                dd += "{name: '" + item.Name + "', color: '" + item.LeaderColor + "', allAreas: false,data: [";
                foreach (var item1 in item.rateWinnerChart22)
                {
                    string rrr = item1.per.ToString();
                    dd += "{value: '" + rrr.Replace(",",".") + "',path: '" + item1.PSName + "',name: '" + item1.PCode + "'},";
                }
                dd = dd.Remove(dd.Length - 1);
                dd += "]},";


            }




            dd += "{'color':'black',showInLegend: false,type: 'mapline',data: [{name: 'path3682',path: 'M11,-457,325,-457,325,-343'}]}] }";



            var jsonConfig = JsonConvert.SerializeObject(dd);
            return jsonConfig;
        }

        private object GetLeaderWinnerChartData()
        {
            var aa = new
            {
                data = new[] { 133, 221, 783, 2478 },
                label = "Rahul",
                backgroundColor = "#9AD0F5"
            };
            var aa1 = new
            {
                data = new[] { 408, 547, 675, 734 },
                label = "Yadav",
                backgroundColor = "#F6A8B8"
            };



            var data = new
            {
                labels = CandidatureGroupTime, //new[] { "1900", "1950", "1999", "2050" },
                                               //datasets = new[]
                                               //{
                                               //    aa,aa1
                                               //},
                datasets = _voteGroupModel
            };
            return data;
        }





        // for Candidature Origen chart
        private string GetHtmlWithOrigenChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%;"">
                          <head>{chartJsScript}
{chartnpm}
{chartdatalabel}
</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetOrigenChartScript()
        {
            var chartConfig1 = GetSpendingOrigenChartConfig();

            var aa = chartConfig1.Replace(@"\", "");
            string aa1 = "" + '"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig = chartConfig2.Replace(aa2, "");

            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }


        private string GetSpendingOrigenChartConfig()
        {
            var plugins = @"yadev:{""datalabels"":{""backgroundColor"": function (context) {return context.dataset.backgroundColor;},""borderColor"": ""white"",""borderRadius"": 10,""borderWidth"": 2,""color"": ""white"",""display"": function (context) {var dataset = context.dataset;var count = dataset.data.length;var value = dataset.data[context.dataIndex];return value;},""font"": {""weight"": ""bold""},""formatter"": function (value, context) {var lbl = context.chart.data.labels[context.dataIndex];var newString = lbl.substr(2);return lbl + "" "" + value + ""%"";}}}yadav";

            var config = new
            {
                type = "doughnut",

                data = GetOrigenchartData(),
                options = new
                {



                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "top",
                        display = false,

                    },
                    animation = new
                    {
                        animateScale = true
                    },
                    plugins





                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetOrigenchartData()
        {

            var dataPoints = CandidatureOrigenvote;// new[] { "3", "7", "8", "2", "8", "9" };
            var colors = CandidatureOrigenColor;// new[] { "#3376BB","#E00006","#C31DA6","#E97208","#1369D3","#D51617" };
            var labels = CandidatureOrigenname;// new[] { "Groceries", "Car", "Flat", "Electronics", "Entertainment", "Insurance" };
            var randomGen = new Random();

            var data = new
            {
                datasets = new[]
                {
                    new
                    {
                       label = "",
                        data = dataPoints,
                        backgroundColor=colors,
                         datalabels=new{
                anchor= "center"
                         }
                    }
                },
                labels
            };
            return data;
        }



        // for Candidature Destino chart
        private string GetHtmlWithDestinoChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%;"">
                          <head>{chartJsScript}
{chartnpm}
{chartdatalabel}
</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetDestinoChartScript()
        {
            var chartConfig1 = GetSpendingDestinoChartConfig();

            var aa = chartConfig1.Replace(@"\", "");
            string aa1 = "" + '"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig = chartConfig2.Replace(aa2, "");

            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }


        private string GetSpendingDestinoChartConfig()
        {
            var plugins = @"yadev:{""datalabels"":{""backgroundColor"": function (context) {return context.dataset.backgroundColor;},""borderColor"": ""white"",""borderRadius"": 10,""borderWidth"": 2,""color"": ""white"",""display"": function (context) {var dataset = context.dataset;var count = dataset.data.length;var value = dataset.data[context.dataIndex];return value;},""font"": {""weight"": ""bold""},""formatter"": function (value, context) {var lbl = context.chart.data.labels[context.dataIndex];var newString = lbl.substr(2);return lbl + "" "" + value + ""%"";}}}yadav";

            var config = new
            {
                type = "doughnut",

                data = GetDestinochartData(),
                options = new
                {



                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "top",
                        display = false,

                    },
                    animation = new
                    {
                        animateScale = true
                    },
                    plugins





                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetDestinochartData()
        {

            var dataPoints = CandidatureDestinovote;// new[] { "3", "7", "8", "2", "8", "9" };
            var colors = CandidatureDestinoColor;// new[] { "#3376BB","#E00006","#C31DA6","#E97208","#1369D3","#D51617" };
            var labels = CandidatureDestinoname;// new[] { "Groceries", "Car", "Flat", "Electronics", "Entertainment", "Insurance" };
            var randomGen = new Random();

            var data = new
            {
                datasets = new[]
                {
                    new
                    {
                       label = "",
                        data = dataPoints,
                        backgroundColor=colors,
                         datalabels=new{
                anchor= "center"
                         }
                    }
                },
                labels
            };
            return data;
        }



        // for GryllooVote chart
        private string GetHtmlWithGryllooVoteChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:98%;height:100%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas style=""width:98%;height:98%;"" id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:98%;height:100%"">
                          <head>{chartJsScript}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetGryllooVoteChartScript()
        {
            var chartConfig = GetSpendingGryllooVoteChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingGryllooVoteChartConfig()
        {
            var config = new
            {
                type = "line",

                data = GetGryllooVoteChartData(),
                options = new
                {
                    legend = new
                    {
                        position = "bottom",
                        display = false,
                    },
                    tooltips = new
                    {
                        mode = "label",
                        intersect = false
                    },
                    hover = new
                    {
                        mode = "label",
                        intersect = false
                    },
                    spanGaps = true
                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetGryllooVoteChartData()
        {
            var aa = new
            {
                data = new[] { 10, 100, 77, 60, 60, 120, 150, 200, 700, 2000 },
                label = "Rahul",
                borderColor = "#FF0000",
                backgroundColor = "transparent"
            };




            var data = new
            {
                labels = GryllooVoteTime,
                //datasets = new[]
                //{
                //    _voteEvaluationModel
                //},
                datasets = _grylloovoteEvaluationModel
            };
            return data;
        }

        // for GryllooUser chart
        private string GetHtmlWithGryllooUserChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:98%;height:100%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas style=""width:98%;height:98%;"" id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:98%;height:100%"">
                          <head>{chartJsScript}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetGryllooUserChartScript()
        {
            var chartConfig = GetSpendingGryllooUserChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingGryllooUserChartConfig()
        {
            var config = new
            {
                type = "line",

                data = GetGryllooUserChartData(),
                options = new
                {
                    legend = new
                    {
                        position = "bottom",
                        display = false,
                    },
                    tooltips = new
                    {
                        mode = "label",
                        intersect = false
                    },
                    hover = new
                    {
                        mode = "label",
                        intersect = false
                    },
                    spanGaps = true
                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetGryllooUserChartData()
        {
            var aa = new
            {
                data = new[] { 10, 100, 77, 60, 60, 120, 150, 200, 700, 2000 },
                label = "Rahul",
                borderColor = "#FF0000",
                backgroundColor = "transparent"
            };




            var data = new
            {
                labels = GryllooUserTime,
                //datasets = new[]
                //{
                //    _voteEvaluationModel
                //},
                datasets = _gryllooUserEvaluationModel
            };
            return data;
        }











        public async Task LeaderChartData()
        {

            try
            {

                if (!CommonLib.checkconnection())

                {
                    VoteAlertPopup.textmsg = Resx.AppResources.checkInternet;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                }


                await Navigation.PushPopupAsync(new LoadPopup());



                string postData = "rateType=" + rateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;


                var result = await CommonLib.leaderChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderVoteFilterPersent?" + postData);
                if (result != null && result.Status != 0)
                {


                    leaderchartName = result.chartData.ChartName;
                    leaderColor = result.chartData.LeaderColor;
                    leadername = result.chartData.LeaderName;
                    leadervote = result.chartData.Votes;


                    //
                    InitializeComponent();
                    Title = ChartName;
                    if(ChartNameId == 1)
                    {
                        subTitle.Text = Resx.AppResources.chartOne;
                    }
                    if (ChartNameId == 2)
                    {
                        subTitle.Text = Resx.AppResources.chartTwo;
                    }
                    if (ChartNameId ==3)
                    {
                        subTitle.Text = Resx.AppResources.chartThree;
                    }
                    if (ChartNameId == 4)
                    {
                        subTitle.Text = Resx.AppResources.chartFour;
                    }
                    if (ChartNameId == 5)
                    {
                        subTitle.Text = Resx.AppResources.chartFive;
                    }
                    if (ChartNameId == 6)
                    {
                        subTitle.Text = Resx.AppResources.chartSix;
                    }
                    if (ChartNameId == 7)
                    {
                        subTitle.Text = Resx.AppResources.chartSeven;
                    }
                    if (ChartNameId == 8)
                    {
                        subTitle.Text = Resx.AppResources.chartEight;
                    }
                    if (ChartNameId == 9)
                    {
                        subTitle.Text = Resx.AppResources.chartNine;
                    }
                    if (ChartNameId == 10)
                    {
                        subTitle.Text = Resx.AppResources.chartten;
                    }
                    if (ChartNameId == 11)
                    {
                        subTitle.Text = Resx.AppResources.chartelaven;
                    }
                    if (ChartNameId == 12)
                    {
                        subTitle.Text = Resx.AppResources.chartetwalav;
                    }
                    if (ChartNameId == 13)
                    {
                        subTitle.Text = Resx.AppResources.chartethirteen;
                    }


                    partyGraph.HeightRequest = (height / 2);

                    CandidaturedoughnutGraph.HeightRequest = (height / 2);
                    CandidatureMultiLineGraph.HeightRequest = (height / 2) + 120;
                    CandidatureGroupGraph.HeightRequest = (height / 2);
                    CandidatureWinnerGraph.HeightRequest = (height / 2)+20;

                    CandidatureOrigeGraph.HeightRequest = (height / 2);
                    CandidatureDestinoGraph.HeightRequest = (height / 2);
                    GryllooVoteMultiLineGraph.HeightRequest = (height / 2);
                    GryllooUserMultiLineGraph.HeightRequest = (height / 2);
                    //

                    leaderGraph.HeightRequest = (height / 2);
                    LeaderMultiLineGraph.HeightRequest = (height / 2) + 120;
                    LeaderGroupGraph.HeightRequest = (height / 2);
                    LeaderWinnerGraph.HeightRequest = (height / 2)+20;

                    pickerProductCategory.Items.Add(Resx.AppResources.lastsevendays);
                    pickerProductCategory.Items.Add(Resx.AppResources.lastfiftendays);
                    pickerProductCategory.Items.Add(Resx.AppResources.lastthirtyndays);
                    pickerProductCategory.Items.Add(Resx.AppResources.lastsixtydays);
                    pickerProductCategory.Items.Add(Resx.AppResources.lastoneyeardays);
                    pickerProductCategory.Items.Add(Resx.AppResources.all);

                    pickerLineGraph.Items.Add(Resx.AppResources.lastoneyeardays);
                    pickerLineGraph.Items.Add(Resx.AppResources.lastsixmonth);
                   // pickerLineGraph.Items.Add(Resx.AppResources.lasttwomonth);

                    pickerLeaderLineGraph.Items.Add(Resx.AppResources.lastoneyeardays);
                    pickerLeaderLineGraph.Items.Add(Resx.AppResources.lastsixmonth);
                  //  pickerLeaderLineGraph.Items.Add(Resx.AppResources.lasttwomonth);

                    if (ChartNameId == 3)
                    {
                        if (result.chartData.LeaderColor.Count == 0)
                        {
                            leaderLabel.IsVisible = true;
                            leaderGraph.IsVisible = false;
                        }
                        else
                        {
                            leaderLabel.IsVisible = false;
                            leaderGraph.IsVisible = true;
                        }

                    }

                    dropdown = true;
                    if (rateType == "7days")
                    {
                        pickerProductCategory.SelectedIndex = 0;
                    }
                    else if (rateType == "15days")
                    {
                        pickerProductCategory.SelectedIndex = 1;
                    }
                    else if (rateType == "30days")
                    {
                        pickerProductCategory.SelectedIndex = 2;
                    }
                    else if (rateType == "60days")
                    {
                        pickerProductCategory.SelectedIndex = 3;
                    }
                    else if (rateType == "1year")
                    {
                        pickerProductCategory.SelectedIndex = 4;
                    }


                    var resultPostal = await CommonLib.postalData(CommonLib.ws_MainUrlMain + "PostalApi/Region");
                    if (resultPostal != null && resultPostal.Status != 0)
                    {
                        PostalDetail _p = new PostalDetail();
                        _p.RegionName = Resx.AppResources.totalCountry;
                        _p.RegionCodeINE = "";
                        resultPostal.data.Insert(0, _p);


                        foreach (var item in resultPostal.data)
                        {
                            pickerRegion.ItemsSource = resultPostal.data;
                        }
                    }

                    if (ChartNameId == 1)
                    {
                        await CandidatureChartData();
                        var chartConfigScript1 = GetChartScript();
                        var html1 = GetHtmlWithChartConfig(chartConfigScript1);
                        d.Html = html1;
                    }
                    if (ChartNameId == 2)
                    {
                        await CandidatureDoughnutChartData();
                        var DoughnutchartConfigScript1 = GetDoughnutChartScript();
                        var htmlDoughnut = GetHtmlWithDoughnutChartConfig(DoughnutchartConfigScript1);
                        Candidaturedoughnut.Html = htmlDoughnut;
                    }
                    if (ChartNameId == 4)
                    {
                        await CandidatureEvaluationChart();
                        var MultiLinechartConfigScript1 = GetMultiLineChartScript();
                        var htmlMultiLine = GetHtmlWithMultiLineChartConfig(MultiLinechartConfigScript1);
                        CandidatureMultiLine.Html = htmlMultiLine;
                    }

                    if (ChartNameId == 6)
                    {
                        await CandidatureWinnerChart();
                        var WinnerchartConfigScript1 = GetWinnerChartScript();
                        var htmlWinner = GetHtmlWithWinnerChartConfig(WinnerchartConfigScript1);
                        CandidatureWinner.Html = htmlWinner;
                    }
                    if (ChartNameId == 5)
                    {
                        await CandidatureGroupChart();
                        var GroupchartConfigScript1 = GetGroupChartScript();
                        var htmlGroup = GetHtmlWithGroupChartConfig(GroupchartConfigScript1);
                        CandidatureGroup.Html = htmlGroup;
                    }

                    //await LeaderEvaluationChart();
                    //await LeaderGroupChart();
                    //await LeaderWinnerChart();

                    if (ChartNameId == 7)
                    {
                        await LeaderEvaluationChart();
                        var LeaderMultiLinechartConfigScript1 = GetLeaderMultiLineChartScript();
                        var htmlLeaderMultiLine = GetHtmlWithLeaderMultiLineChartConfig(LeaderMultiLinechartConfigScript1);
                        LeaderMultiLine.Html = htmlLeaderMultiLine;
                    }
                    if (ChartNameId == 8)
                    {
                        await LeaderGroupChart();
                        var LeaderGroupchartConfigScript1 = GetLeaderGroupChartScript();
                        var htmlLeaderGroup = GetHtmlWithLeaderGroupChartConfig(LeaderGroupchartConfigScript1);
                        LeaderGroup.Html = htmlLeaderGroup;
                    }
                    if (ChartNameId == 9)
                    {
                        await LeaderWinnerChart();
                        var LeaderWinnerchartConfigScript1 = GetLeaderWinnerChartScript();
                        var htmlLeaderWinner = GetHtmlWithLeaderWinnerChartConfig(LeaderWinnerchartConfigScript1);
                        LeaderWinner.Html = htmlLeaderWinner;
                    }


                    if (ChartNameId == 3)
                    {
                        leaderGraph.IsVisible = true;
                        var chartConfigScript = GetChartScript1();
                        var html = GetHtmlWithChartConfig1(chartConfigScript);
                        e.Html = html;
                        if (Device.OS == TargetPlatform.iOS)
                        {
                            var jj = new HtmlWebViewSource();
                            jj.Html = html;
                            leaderGraph.Source = jj;
                        }
                    }

                    if (ChartNameId == 10)
                    {
                        await CandidatureOrigenChart();
                        var OrigenchartConfigScript1 = GetOrigenChartScript();
                        var htmlOrigen = GetHtmlWithOrigenChartConfig(OrigenchartConfigScript1);
                        CandidaturedOrige.Html = htmlOrigen;
                    }
                    if (ChartNameId == 11)
                    {
                        await CandidatureDestinoChart();
                      
                        var DestinochartConfigScript1 = GetDestinoChartScript();
                        var htmlDestino = GetHtmlWithDestinoChartConfig(DestinochartConfigScript1);
                        CandidaturedDestino.Html = htmlDestino;
                    
                    }
                    if (ChartNameId == 12)
                    {
                        await GryllooVoteData();
                        var GryllooVotechartConfigScript1 = GetGryllooVoteChartScript();
                        var htmlGryllooVote = GetHtmlWithGryllooVoteChartConfig(GryllooVotechartConfigScript1);
                        GryllooVoteMultiLine.Html = htmlGryllooVote;
                    }
                    if (ChartNameId == 13)
                    {
                        await GryllooUserData();
                        var GryllooUserchartConfigScript1 = GetGryllooUserChartScript();
                        var htmlGryllooUser = GetHtmlWithGryllooUserChartConfig(GryllooUserchartConfigScript1);
                        GryllooVoteMultiLine.Html = htmlGryllooUser;
                    }











                  

                    LoadPopup.CloseAllPopup();



                }
                else
                {
                    LoadPopup.CloseAllPopup();

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }
                //

                if ((ChartNameId == 1 || ChartNameId == 2 || ChartNameId == 3) && ChartsType == "N")
                {
                    
                    ambText.IsVisible = false;
                   
                }
                if ((ChartNameId == 1 || ChartNameId == 2 || ChartNameId == 3) && ChartsType == "R")
                {
                    ambText.IsVisible = true;
                    pickerRegion.IsVisible = true;
                  
                    RegionImg.IsVisible = true;
                }
                if ((ChartNameId == 1 || ChartNameId == 2 || ChartNameId == 3) && ChartsType == "P")
                {
                    ambText.IsVisible = true;
                    pickerRegion.IsVisible = true;
                    pickerProvince.IsVisible = true;
                  
                    RegionImg.IsVisible = true;
                    ProvinceImg.IsVisible = true;

                }

                if ((ChartNameId == 4 || ChartNameId == 7) && ChartsType == "N")
                {
                    ambText.IsVisible = false;
                   
                }

            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }


        }

        public async Task CandidatureDoughnutChartData()
        {

            try
            {
                string postData = "rateType=" + rateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;
                var result = await CommonLib.CandidatureVoteChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CandidatureVoteFilterPersent?" + postData);
                if (result != null && result.Status != 0)
                {
                    CandidaturedoughnutGraph.IsVisible = true;
                    if (result.chartData.CandidatureColor.Count == 0)
                    {
                        CandidaturedoughnutLabel.IsVisible = true;
                        CandidaturedoughnutGraph.IsVisible = false;
                    }
                    else
                    {
                        CandidaturedoughnutLabel.IsVisible = false;
                        CandidaturedoughnutGraph.IsVisible = true;
                    }

                    CandidatureDoughnutchartName = result.chartData.ChartName;
                    CandidatureDoughnutColor = result.chartData.CandidatureColor;
                    CandidatureDoughnutname = result.chartData.CandidatureName;
                    CandidatureDoughnutvote = result.chartData.Votes;



                    if(Device.OS==TargetPlatform.iOS){
                        
                        var DoughnutchartConfigScript1 = GetDoughnutChartScript();
                        var htmlDoughnut = GetHtmlWithDoughnutChartConfig(DoughnutchartConfigScript1);
                        Candidaturedoughnut.Html = htmlDoughnut;
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlDoughnut;
                        CandidaturedoughnutGraph.Source = jj;
                    }


                }
                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        public async Task CandidatureChartData()
        {

            try
            {
                string postData = "rateType=" + rateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;
                var result = await CommonLib.CandidatureVoteChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CandidatureVoteFilter?" + postData);
                if (result != null && result.Status != 0)
                {
                    partyGraph.IsVisible = true;
                    if (result.chartData.CandidatureColor.Count == 0)
                    {
                        partyLabel.IsVisible = true;
                        partyGraph.IsVisible = false;
                    }
                    else
                    {
                        partyLabel.IsVisible = false;
                        partyGraph.IsVisible = true;
                    }

                    CandidaturechartName = result.chartData.ChartName;
                    CandidatureColor = result.chartData.CandidatureColor;
                    Candidaturename = result.chartData.CandidatureName;
                    Candidaturevote = result.chartData.Votes;

                    if(Device.OS==TargetPlatform.iOS){
                        var chartConfigScript1 = GetChartScript();
                        var html1 = GetHtmlWithChartConfig(chartConfigScript1);
                        d.Html = html1;
                        var jj = new HtmlWebViewSource();
                        jj.Html = html1;
                        partyGraph.Source = jj;
                    }

                }
                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }


        public async Task CandidatureEvaluationChart()
        {

            try
            {
                CandidatureMultiLineFrame.IsVisible = true;
                CandidatureMultiLineGraph.IsVisible = true;
                pickerLineGraph.SelectedIndex = 1;
                //string postData = "rateType=" + LinerateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;
                //var result = await CommonLib.CandidatureVoteEvaluationChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CandidatureVoteEvaluation?" + postData);
                //if (result != null && result.Status != 0)
                //{
                //    if (result.chartData.Time == null)
                //    {
                //        CandidatureMultiLineLabel.IsVisible = true;
                //        CandidatureMultiLineGraph.IsVisible = false;
                //    }
                //    else
                //    {
                //        CandidatureMultiLineLabel.IsVisible = false;
                //        CandidatureMultiLineGraph.IsVisible = true;
                //    }

                //    CandidatureEvaluationTime = result.chartData.Time;
                //    _voteEvaluationModel.Clear();


                //    foreach (var item in result.chartData.voteEvaluation)
                //    {
                //        _voteEvaluationModel.Add(new VoteEvaluationModel { backgroundColor = "transparent", borderColor = item.Color, label = item.Name, data = item.Vote });

                //    }
                //}

                //else
                //{

                //    VoteAlertPopup.textmsg = result.msg;
                //    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                //}
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        public async Task LeaderEvaluationChart()
        {

            try
            {
                pickerLeaderLineFrame.IsVisible = true;
                pickerLeaderLineGraph.IsVisible = true;
                pickerLeaderLineGraph.SelectedIndex = 1;
                //string postData = "rateType=" + LinerateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;
                //var result = await CommonLib.LeaderVoteEvaluationChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderVoteEvaluation?" + postData);
                //if (result != null && result.Status != 0)
                //{
                //    if (result.chartData.Time == null)
                //    {
                //        LeaderMultiLineLabel.IsVisible = true;
                //        LeaderMultiLineGraph.IsVisible = false;
                //    }
                //    else
                //    {
                //        LeaderMultiLineLabel.IsVisible = false;
                //        LeaderMultiLineGraph.IsVisible = true;
                //    }

                //    LeaderEvaluationTime = result.chartData.Time;
                //    _leaderEvaluationModel.Clear();


                //    foreach (var item in result.chartData.voteEvaluation)
                //    {
                //        _leaderEvaluationModel.Add(new VoteEvaluationModel { backgroundColor = "transparent", borderColor = item.Color, label = item.Name, data = item.Vote });

                //    }
                // }

                //  else
                //  {

                //      VoteAlertPopup.textmsg = result.msg;
                //    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                // }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }


        public async Task CandidatureGroupChart()
        {

            try
            {
                ambText.IsVisible = false;
                
                CandidatureGroupFrame.IsVisible = true;
                CandidatureGroupGraph.IsVisible = true;
                var getCandidature = await CommonLib.AllCandidatures(CommonLib.ws_MainUrlMain + "CandidatureApi/GetAllCandidature");
                if (getCandidature.chartData != null)
                {
                    foreach (var item in getCandidature.chartData)
                    {
                        CandidatureId = item.CandidatureCode;
                        break;
                    }

                    pickerCandidature.ItemsSource = getCandidature.chartData;
                    pickerCandidature.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }


        public async Task CandidatureOrigenChart()
        {

            try
            {
                chartViewLayout.IsVisible = false;
                chartViewSecondLayout.IsVisible = false;
                ambText.IsVisible = false;
                CandidatureOrigenFrame.IsVisible = true;
                CandidatureOrigeGraph.IsVisible = true;

                var getCandidature = await CommonLib.AllCandidatures(CommonLib.ws_MainUrlMain + "CandidatureApi/GetAllCandidature");
                if (getCandidature.chartData != null)
                {
                    foreach (var item in getCandidature.chartData)
                    {
                        CandidatureId = item.CandidatureCode;
                        break;
                    }

                    pickerOrigenCandidature.ItemsSource = getCandidature.chartData;
                    pickerOrigenCandidature.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }
        public async Task CandidatureDestinoChart()
        {

            try
            {
                chartViewLayout.IsVisible = false;
                chartViewSecondLayout.IsVisible = false;
                ambText.IsVisible = false;
                CandidatureDestinoFrame.IsVisible = true;
                CandidatureDestinoGraph.IsVisible = true;

                var getCandidature = await CommonLib.AllCandidatures(CommonLib.ws_MainUrlMain + "CandidatureApi/GetAllCandidature");
                if (getCandidature.chartData != null)
                {
                    foreach (var item in getCandidature.chartData)
                    {
                        CandidatureId = item.CandidatureCode;
                        break;
                    }

                    pickerDestinoCandidature.ItemsSource = getCandidature.chartData;
                    pickerDestinoCandidature.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }


        public async Task LeaderGroupChart()
        {

            try
            {
                ambText.IsVisible = false;
               
                LeaderGroupFrame.IsVisible = true;
                LeaderGroupGraph.IsVisible = true;
                var getCandidature = await CommonLib.AllLeaders(CommonLib.ws_MainUrlMain + "LeaderApi/GetAllLeader");
                if (getCandidature.chartData != null)
                {
                    foreach (var item in getCandidature.chartData)
                    {
                        LeaderId = item.LeaderCode;
                        break;
                    }


                    pickerLeader.ItemsSource = getCandidature.chartData;
                    pickerLeader.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        public async Task CandidatureWinnerChart()
        {

            try
            {
                ambText.IsVisible = false;
               
                CandidatureWinnerFrame.IsVisible = true;
                CandidatureWinnerGraph.IsVisible = true;


                if (ChartsNamePage.selectedItem == "R")
                {
                    pickerWinnerGraph.Items.Add(Resx.AppResources.region);
                    CandidatureWinnerFrame.IsVisible = false;
                }
                else
                {
                    pickerWinnerGraph.Items.Add(Resx.AppResources.region);
                    pickerWinnerGraph.Items.Add(Resx.AppResources.province);
                    CandidatureWinnerFrame.IsVisible = true;
                }
                pickerWinnerGraph.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        public async Task LeaderWinnerChart()
        {

            try
            {
                ambText.IsVisible = false;

                LeaderWinnerFrame.IsVisible = true;
                LeaderWinnerGraph.IsVisible = true;

                if (ChartsNamePage.selectedItem == "R")
                {
                    pickerLeaderWinnerGraph.Items.Add(Resx.AppResources.region);
                    LeaderWinnerFrame.IsVisible = false;
                }
                
                else
                {
                    pickerLeaderWinnerGraph.Items.Add(Resx.AppResources.region);
                    pickerLeaderWinnerGraph.Items.Add(Resx.AppResources.province);
                    LeaderWinnerFrame.IsVisible = true;
                }


                pickerLeaderWinnerGraph.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        public async Task ProvinceResult()
        {
            string RegionpostData = "id=" + RegionId;
            var resultRegion = await CommonLib.postalData(CommonLib.ws_MainUrlMain + "PostalApi/Province?" + RegionpostData);
            if (resultRegion != null && resultRegion.Status != 0)
            {
                pickerProvince.ItemsSource = null;

                PostalDetail _p = new PostalDetail();
                _p.ProvinceName = "Total Region";
                _p.ProvincePostalCode = "";
                resultRegion.data.Insert(0, _p);

                foreach (var item in resultRegion.data)
                {
                    pickerProvince.ItemsSource = resultRegion.data;
                }
            }
        }


        public async Task PikerSelect()
        {

            try
            {

                if (!CommonLib.checkconnection())

                {
                    VoteAlertPopup.textmsg = "Check your internet connection.";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                }


                await Navigation.PushPopupAsync(new LoadPopup());



                string postData = "rateType=" + rateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;


                var result = await CommonLib.leaderChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderVoteFilterPersent?" + postData);
                if (result != null && result.Status != 0)
                {

                    leaderchartName = result.chartData.ChartName;
                    leaderColor = result.chartData.LeaderColor;
                    leadername = result.chartData.LeaderName;
                    leadervote = result.chartData.Votes;
                    if (ChartNameId == 3)
                    {
                        if (result.chartData.LeaderColor.Count == 0)
                        {
                            leaderLabel.IsVisible = true;
                            leaderGraph.IsVisible = false;
                        }
                        else
                        {
                            leaderLabel.IsVisible = false;
                            leaderGraph.IsVisible = true;
                        }
                    }


                    //   await CandidatureChartData();

                    if (ChartNameId == 1)
                    {
                        await CandidatureChartData();
                        var chartConfigScript1 = GetChartScript();
                        var html1 = GetHtmlWithChartConfig(chartConfigScript1);
                        d.Html = html1;
                    }
                    //  await CandidatureDoughnutChartData();
                    if (ChartNameId == 2)
                    {
                        await CandidatureDoughnutChartData();
                        var DoughnutchartConfigScript1 = GetDoughnutChartScript();
                        var htmlDoughnut = GetHtmlWithDoughnutChartConfig(DoughnutchartConfigScript1);
                        Candidaturedoughnut.Html = htmlDoughnut;
                    }
                    // await CandidatureEvaluationChart();
                    if (ChartNameId == 4)
                    {
                        await CandidatureEvaluationChart();

                        var MultiLinechartConfigScript1 = GetMultiLineChartScript();
                        var htmlMultiLine = GetHtmlWithMultiLineChartConfig(MultiLinechartConfigScript1);
                        CandidatureMultiLine.Html = htmlMultiLine;
                    }
                    //    await LeaderEvaluationChart();
                    if (ChartNameId == 7)
                    {
                        await LeaderEvaluationChart();

                        var LeaderMultiLinechartConfigScript1 = GetLeaderMultiLineChartScript();
                        var htmlLeaderMultiLine = GetHtmlWithLeaderMultiLineChartConfig(LeaderMultiLinechartConfigScript1);
                        LeaderMultiLine.Html = htmlLeaderMultiLine;
                    }

                    //var chartConfigScript1 = GetChartScript();
                    //var html1 = GetHtmlWithChartConfig(chartConfigScript1);
                    //d.Html = html1;

                    //var DoughnutchartConfigScript1 = GetDoughnutChartScript();
                    //var htmlDoughnut = GetHtmlWithDoughnutChartConfig(DoughnutchartConfigScript1);
                    //Candidaturedoughnut.Html = htmlDoughnut;


                    //var MultiLinechartConfigScript1 = GetMultiLineChartScript();
                    //var htmlMultiLine = GetHtmlWithMultiLineChartConfig(MultiLinechartConfigScript1);
                    //CandidatureMultiLine.Html = htmlMultiLine;



                    if (ChartNameId == 3)
                    {
                        var chartConfigScript = GetChartScript1();
                        var html = GetHtmlWithChartConfig1(chartConfigScript);
                        e.Html = html;
                        if (Device.OS == TargetPlatform.iOS)
                        {
                            var jj = new HtmlWebViewSource();
                            jj.Html = html;
                            leaderGraph.Source = jj;
                        }
                    }

                    //var LeaderMultiLinechartConfigScript1 = GetLeaderMultiLineChartScript();
                    //var htmlLeaderMultiLine = GetHtmlWithLeaderMultiLineChartConfig(LeaderMultiLinechartConfigScript1);
                    //LeaderMultiLine.Html = htmlLeaderMultiLine;


                    LoadPopup.CloseAllPopup();



                }
                else
                {
                    LoadPopup.CloseAllPopup();

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }


            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }


        }
        public async Task CandidatureEvaluationPikerSelect()
        {

            try
            {
                //await Navigation.PushPopupAsync(new LoadPopup());
                string postData = "rateType=" + LinerateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;
                var result = await CommonLib.CandidatureVoteEvaluationChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CandidatureVoteEvaluation?" + postData);
                if (result != null && result.Status != 0)
                {
                    if (result.chartData.Time == null)
                    {
                        CandidatureMultiLineLabel.IsVisible = true;
                        CandidatureMultiLineGraph.IsVisible = false;
                    }
                    else
                    {
                        CandidatureMultiLineLabel.IsVisible = false;
                        CandidatureMultiLineGraph.IsVisible = true;
                    }



                    CandidatureEvaluationTime = result.chartData.Time;
                    _voteEvaluationModel.Clear();


                    foreach (var item in result.chartData.voteEvaluation)
                    {
                        _voteEvaluationModel.Add(new VoteEvaluationModel {pointHoverRadius=6, backgroundColor = "transparent", borderColor = item.Color, label = item.Name, data = item.Vote });

                    }

                    var MultiLinechartConfigScript1 = GetMultiLineChartScript();
                    var htmlMultiLine = GetHtmlWithMultiLineChartConfig(MultiLinechartConfigScript1);
                    CandidatureMultiLine.Html = htmlMultiLine;
                   // LoadPopup.CloseAllPopup1();

                    if (Device.OS == TargetPlatform.iOS)
                    {
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlMultiLine;
                        CandidatureMultiLineGraph.Source = jj;
                    }

                }


                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    //LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                //LoadPopup.CloseAllPopup1();
            }
        }




        public async Task GryllooVoteData()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());
                var result = await CommonLib.GryllooVoteResult(CommonLib.ws_MainUrlMain + "CandidatureApi/GryllooVotes");
                if (result != null && result.Status != 0)
                {
                    chartViewLayout.IsVisible = false;
                    chartViewSecondLayout.IsVisible = false;
                    ambText.IsVisible = false;


                    if (result.chartData.Time == null)
                    {
                        GryllooVoteLabel.IsVisible = true;
                        GryllooVoteMultiLineGraph.IsVisible = false;
                    }
                    else
                    {
                        GryllooVoteLabel.IsVisible = false;
                        GryllooVoteMultiLineGraph.IsVisible = true;
                    }



                    GryllooVoteTime = result.chartData.Time;
                   
                    _grylloovoteEvaluationModel.Clear();


                    
                    _grylloovoteEvaluationModel.Add(new GryllooVoteEvaluationModel { pointHoverRadius = 6, backgroundColor = "transparent", borderColor = "Green", label = "Votes", data = result.chartData.data });

                     

                    var GryllooVotechartConfigScript1 = GetGryllooVoteChartScript();
                    var htmlGryllooVote = GetHtmlWithGryllooVoteChartConfig(GryllooVotechartConfigScript1);
                    GryllooVoteMultiLine.Html = htmlGryllooVote;
                    LoadPopup.CloseAllPopup1();
                }


                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                LoadPopup.CloseAllPopup1();
            }
        }
        public async Task GryllooUserData()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());
                var result = await CommonLib.GryllooVoteResult(CommonLib.ws_MainUrlMain + "CandidatureApi/GryllooUsers");
                if (result != null && result.Status != 0)
                {
                    chartViewLayout.IsVisible = false;
                    chartViewSecondLayout.IsVisible = false;
                    ambText.IsVisible = false;


                    if (result.chartData.Time == null)
                    {
                        GryllooUserLabel.IsVisible = true;
                        GryllooUserMultiLineGraph.IsVisible = false;
                    }
                    else
                    {
                        GryllooUserLabel.IsVisible = false;
                        GryllooUserMultiLineGraph.IsVisible = true;
                    }



                    GryllooUserTime = result.chartData.Time;

                    _gryllooUserEvaluationModel.Clear();



                    _gryllooUserEvaluationModel.Add(new GryllooVoteEvaluationModel { pointHoverRadius = 6, backgroundColor = "transparent", borderColor = "Green", label = "Users", data = result.chartData.data });



                    var GryllooUserchartConfigScript1 = GetGryllooUserChartScript();
                    var htmlGryllooUser = GetHtmlWithGryllooUserChartConfig(GryllooUserchartConfigScript1);
                    GryllooUserMultiLine.Html = htmlGryllooUser;
                    LoadPopup.CloseAllPopup1();
                }


                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                LoadPopup.CloseAllPopup1();
            }
        }


        public async Task LeaderEvaluationPikerSelect()
        {

            try
            {
               // await Navigation.PushPopupAsync(new LoadPopup());
                string postData = "rateType=" + LinerateType + "&RegionCode=" + RegionCode + "&ProvinceCode=" + ProvinceCode;
                var result = await CommonLib.LeaderVoteEvaluationChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderVoteEvaluation?" + postData);
                if (result != null && result.Status != 0)
                {
                    if (result.chartData.Time == null)
                    {
                        LeaderMultiLineLabel.IsVisible = true;
                        LeaderMultiLineGraph.IsVisible = false;
                    }
                    else
                    {
                        LeaderMultiLineLabel.IsVisible = false;
                        LeaderMultiLineGraph.IsVisible = true;
                    }

                    LeaderEvaluationTime = result.chartData.Time;
                    _leaderEvaluationModel.Clear();


                    foreach (var item in result.chartData.voteEvaluation)
                    {
                        _leaderEvaluationModel.Add(new VoteEvaluationModel { pointHoverRadius=6,backgroundColor = "transparent", borderColor = item.Color, label = item.Name, data = item.Vote });

                    }

                    var LeaderMultiLinechartConfigScript1 = GetLeaderMultiLineChartScript();
                    var htmlLeaderMultiLine = GetHtmlWithLeaderMultiLineChartConfig(LeaderMultiLinechartConfigScript1);
                    LeaderMultiLine.Html = htmlLeaderMultiLine;
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlLeaderMultiLine;
                        LeaderMultiLineGraph.Source = jj;
                    }
                    //LoadPopup.CloseAllPopup1();
                }


                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    //LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
               // LoadPopup.CloseAllPopup1();
            }
        }


        public async Task CandidatureGroupPikerSelect()
        {

            try
            {
                //await Navigation.PushPopupAsync(new LoadPopup());
                string postData = "CandidatureId=" + CandidatureId;
                var result = await CommonLib.CandidatureGroupChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CandidatureVoteGroup?" + postData);
                if (result != null && result.Status != 0)
                {
                    if (result.chartData.Time == null)
                    {
                        CandidatureGroupLabel.IsVisible = true;
                        CandidatureGroupGraph.IsVisible = false;
                    }
                    else
                    {
                        CandidatureGroupLabel.IsVisible = false;
                        CandidatureGroupGraph.IsVisible = true;
                    }

                    CandidatureGroupTime = result.chartData.Time;
                    _voteGroupModel.Clear();


                    foreach (var item in result.chartData.voteGroup)
                    {
                        _voteGroupModel.Add(new GroupChart { backgroundColor = item.color, label = item.sex, data = item.per });
                    }

                    var GroupchartConfigScript1 = GetGroupChartScript();
                    var htmlGroup = GetHtmlWithGroupChartConfig(GroupchartConfigScript1);
                    CandidatureGroup.Html = htmlGroup;



                    if (Device.OS == TargetPlatform.iOS)
                    {
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlGroup;
                        CandidatureGroupGraph.Source = jj;
                    }


                   // LoadPopup.CloseAllPopup1();
                }


                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    //LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
               // LoadPopup.CloseAllPopup1();
            }


        }


        public async Task CandidatureOrigenChartData()
        {

            try
            {
                string postData = "candidatureCode=" + CandidatureId;
                var result = await CommonLib.CandidatureOrigenVoteChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CadidatureOrigenVotes?" + postData);
                if (result != null && result.Status != 0)
                {
                    partyGraph.IsVisible = true;
                    if (result.chartData.CandidatureColor.Count == 0)
                    {
                        CandidatureOrigenLabel.IsVisible = true;
                        CandidatureOrigeGraph.IsVisible = false;
                    }
                    else
                    {
                        CandidatureOrigenLabel.IsVisible = false;
                        CandidatureOrigeGraph.IsVisible = true;
                    }


                    
                    CandidatureOrigenColor=result.chartData.CandidatureColor;
                    CandidatureOrigenname=result.chartData.CandidatureName;
                    CandidatureOrigenvote=result.chartData.Votes;



                    

                    var OrigenchartConfigScript1 = GetOrigenChartScript();
                    var htmlOrigen = GetHtmlWithOrigenChartConfig(OrigenchartConfigScript1);
                    CandidaturedOrige.Html = htmlOrigen;

                    if (Device.OS == TargetPlatform.iOS)
                    {
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlOrigen;
                        CandidatureOrigeGraph.Source = jj;
                    }

                }
                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }
        public async Task CandidatureDestinoChartData()
        {

            try
            {
                string postData = "candidatureCode=" + CandidatureId;
                var result = await CommonLib.CandidatureDestinoVoteChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CadidatureDestinoVotes?" + postData);
                if (result != null && result.Status != 0)
                {
                    partyGraph.IsVisible = true;
                    if (result.chartData.CandidatureColor.Count == 0)
                    {
                        CandidatureDestinoLabel.IsVisible = true;
                        CandidatureDestinoGraph.IsVisible = false;
                    }
                    else
                    {
                        CandidatureDestinoLabel.IsVisible = false;
                        CandidatureDestinoGraph.IsVisible = true;
                    }



                    CandidatureDestinoColor = result.chartData.CandidatureColor;
                    CandidatureDestinoname = result.chartData.CandidatureName;
                    CandidatureDestinovote = result.chartData.Votes;





                    var DestinochartConfigScript1 = GetDestinoChartScript();
                    var htmlDestino = GetHtmlWithDestinoChartConfig(DestinochartConfigScript1);
                    CandidaturedDestino.Html = htmlDestino;

                    if (Device.OS == TargetPlatform.iOS) { 
                    var jj = new HtmlWebViewSource();
                    jj.Html = htmlDestino;
                    CandidatureDestinoGraph.Source = jj;
                    }

                }
                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        public async Task LeaderGroupPikerSelect()
        {

            try
            {
              //  await Navigation.PushPopupAsync(new LoadPopup());
                string postData = "LeaderId=" + LeaderId;
                var result = await CommonLib.LeaderGroupChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderVoteGroup?" + postData);
                if (result != null && result.Status != 0)
                {
                    if (result.chartData.Time == null)
                    {
                        LeaderGroupLabel.IsVisible = true;
                        LeaderGroupGraph.IsVisible = false;
                    }
                    else
                    {
                        LeaderGroupLabel.IsVisible = false;
                        LeaderGroupGraph.IsVisible = true;
                    }

                    LeaderGroupTime = result.chartData.Time;
                    _voteLeaderGroupModel.Clear();


                    foreach (var item in result.chartData.voteGroup)
                    {
                        _voteLeaderGroupModel.Add(new GroupChart { backgroundColor = item.color, label = item.sex, data = item.per });
                    }

                    var LeaderGroupchartConfigScript1 = GetLeaderGroupChartScript();
                    var htmlLeaderGroup = GetHtmlWithLeaderGroupChartConfig(LeaderGroupchartConfigScript1);
                    LeaderGroup.Html = htmlLeaderGroup;

                    if (Device.OS == TargetPlatform.iOS)
                    {
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlLeaderGroup;
                        LeaderGroupGraph.Source = jj;
                    }
                   // LoadPopup.CloseAllPopup1();
                }


                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
               // LoadPopup.CloseAllPopup1();
            }


        }

        public async Task CandidatureWinnerPikerSelect()
        {

            try
            {
              //  await Navigation.PushPopupAsync(new LoadPopup());


                string postData = "type=" + winnerType;
                var result = await CommonLib.CandidatureWinnerChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CadidatureWinner?" + postData);
                if (result != null && result.Status != 0)
                {
                    _voteWinnerModel.Clear();
                  

                    //foreach (var item in result.chartData)
                    //{
                    //    string name = item.Name.Replace("'", "");
                    //    _voteWinnerModel.Add(new ChartWinnerData { CandidatureColor = item.CandidatureColor, Name = name, PCode = item.PCode, per = item.per, PSName = item.PSName });
                    //}


                    foreach (var item in result.chartData)
                    {
                        string name = item.Name.Replace("'", "");
                        _voteWinnerModel.Add(new ChartWinnerData { CandidatureColor = item.CandidatureColor, Name = name });

                        foreach (var item1 in _voteWinnerModel.Where(x => x.Name == name))
                        {
                            foreach (var item3 in item.voteWinnerChart2)
                            {
                                
                                item1.voteWinnerChart22.Add(new VoteWinnerChart22 { PCode = item3.PCode, per = item3.per, PSName = item3.PSName });

                            }
                        }


                    }








                    var WinnerchartConfigScript1 = GetWinnerChartScript();
                    var htmlWinner = GetHtmlWithWinnerChartConfig(WinnerchartConfigScript1);
                    CandidatureWinner.Html = htmlWinner;


                    if (Device.OS == TargetPlatform.iOS)
                    {
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlWinner;
                        CandidatureWinnerGraph.Source = jj;
                    }

                   // LoadPopup.CloseAllPopup1();
                }

                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                   // LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
               // LoadPopup.CloseAllPopup1();
            }


        }


        public async Task LeaderWinnerPikerSelect()
        {

            try
            {
                //await Navigation.PushPopupAsync(new LoadPopup());


                string postData = "type=" + LeaderwinnerType;
                var result = await CommonLib.LeaderWinnerChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderWinner?" + postData);
                if (result != null && result.Status != 0)
                {
                    _rateWinnerModel.Clear();
                    foreach (var item in result.chartData)
                    {
                        string name = item.Name.Replace("'", "");
                        _rateWinnerModel.Add(new LeaderWinner { LeaderColor = item.LeaderColor, Name = name});

                        foreach (var item1 in _rateWinnerModel.Where(x=>x.Name==name))
                        {
                            foreach (var item3 in item.rateWinnerChart2)
                            {
                                item1.rateWinnerChart22.Add(new RateWinnerChart22 { PCode = item3.PCode, per = item3.per, PSName = item3.PSName });

                            }
                        }


                    }

                    var LeaderWinnerchartConfigScript1 = GetLeaderWinnerChartScript();
                    var htmlLeaderWinner = GetHtmlWithLeaderWinnerChartConfig(LeaderWinnerchartConfigScript1);
                    LeaderWinner.Html = htmlLeaderWinner;

                    if (Device.OS == TargetPlatform.iOS)
                    {
                        var jj = new HtmlWebViewSource();
                        jj.Html = htmlLeaderWinner;
                        LeaderWinnerGraph.Source = jj;
                    }
                   // LoadPopup.CloseAllPopup1();
                }

                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
               // LoadPopup.CloseAllPopup1();
            }


        }


    }
}