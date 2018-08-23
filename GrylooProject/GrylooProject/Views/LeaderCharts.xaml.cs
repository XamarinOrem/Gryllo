using GrylooProject.Model;
using GrylooProject.Repository;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GrylooProject.Views
{
    public partial class LeaderCharts : ContentPage
    {
        public static int ChartNameId = 0;
        public List<GroupChart> _voteGroupModel = new List<GroupChart>();
        public List<string> CandidatureGroupTime = null;
        public static string RegionId = "";
        public List<LeaderWinner> _rateWinnerModel = new List<LeaderWinner>();
        public static string LeaderwinnerType = "";
        public List<GroupChart> _voteLeaderGroupModel = new List<GroupChart>();
        public List<string> LeaderGroupTime = null;
        public static int LeaderId = 0;
        public static bool dropdown = false;
        public static string rateType = "60days";
        public static string RegionCode = "";
        public static string ProvinceCode = "";
        public static string leaderchartName = "";
        public List<object> leaderColor = null;
        public List<object> leadername = null;
        public List<object> leadervote = null;
        public static string LinerateType = "1year";
        public List<string> LeaderEvaluationTime = null;
        public List<VoteEvaluationModel> _leaderEvaluationModel = new List<VoteEvaluationModel>();
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public LeaderCharts()
        {
            rateType = "60days";
            RegionCode = "";
            ProvinceCode = "";
            Device.BeginInvokeOnMainThread(async () =>
            {
                await LeaderChartData();
            });
        }


        // for leader chart

        private string GetHtmlWithChartConfig1(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%;"">
                          <head>{chartJsScript}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }
        private string GetChartScript1()
        {
            var chartConfig = GetSpendingChartConfig1();


            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }
        private string GetSpendingChartConfig1()
        {





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
                    }
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
                        backgroundColor=colors
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
                        mode = 'x'
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
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <canvas style=""width:98%;height:98%;"" id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:97%;height:97%"">
                          <head>{chartJsScript}</head>
                          <body {inlineStyle}>
                            {chartContent}
                            {chartConfigJsScript}
                          </body>
                        </html>";
            return document;
        }

        private string GetLeaderGroupChartScript()
        {
            var chartConfig = GetSpendingLeaderGroupChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingLeaderGroupChartConfig()
        {
            var config = new
            {
                type = "bar",

                data = GetLeaderGroupChartData(),
                options = new
                {
                    legend = new
                    {
                        position = "bottom",
                        display = false,
                    }

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
                datasets = _voteLeaderGroupModel
            };
            return data;
        }


        // for Leader Winner chart
        private string GetHtmlWithLeaderWinnerChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";

            var chartJsScript = "<script src=\"https://code.highcharts.com/maps/highmaps.js\"></script>";
            var chartJsScript1 = "<script src=\"https://code.highcharts.com/maps/modules/exporting.js\"></script>";
            var chartJsScript2 = "<script src=\"https://code.highcharts.com/mapdata/countries/es/es-all.js\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <div style=""width:98%;height:98%;"" id=""chart1""></div>
                        </div>";
            var document = $@"<html style=""width:97%;height:97%"">
                          <head>{chartJsScript}{chartJsScript1}{chartJsScript2}</head>
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


            //var dd = "{ chart: {  map: 'countries/es/es-all',}, title: {text: '' }, mapNavigation: { enabled: true,buttonOptions: {verticalAlign: 'bottom'}}, series: [{ showInLegend: false, data: [['es-va', 1]], name: 'pp', color: 'blue', allAreas: false, }, ] }";


            var dd = "{ chart: {  map: 'countries/es/es-all',}, title: {text: '' }, exporting: { enabled: false },credits:{enabled: false}, mapNavigation: { enabled: false,buttonOptions: {verticalAlign: 'bottom'}}, series: [";
            foreach (var item in _rateWinnerModel)
            {
               // dd += "{ showInLegend: false, data: [['" + item.PSName + "', " + item.per + "]], name: '" + item.Name + "', color: '" + item.LeaderColor + "', allAreas: false, },";

            }
            dd += "] }";



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


        public async Task LeaderChartData()
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


                    //
                    InitializeComponent();
                    Title = "Leader Charts";


                    if (ChartNameId == 3)
                    {
                        leaderGraph.HeightRequest = (height / 2);
                    }
                    LeaderMultiLineGraph.HeightRequest = (height / 2) + 120;
                    LeaderGroupGraph.HeightRequest = (height / 2);
                    LeaderWinnerGraph.HeightRequest = (height / 2);

                    pickerProductCategory.Items.Add("Last 7 Days");
                    pickerProductCategory.Items.Add("Last 15 Days");
                    pickerProductCategory.Items.Add("Last 30 Days");
                    pickerProductCategory.Items.Add("Last 60 Days");
                    pickerProductCategory.Items.Add("Last 1 year");
                    pickerProductCategory.Items.Add("All");

                  

                    pickerLeaderLineGraph.Items.Add("Last 1 year");
                    pickerLeaderLineGraph.Items.Add("Last 6 month");
                    pickerLeaderLineGraph.Items.Add("Last 2 month");

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
                        _p.RegionName = "Total country";
                        _p.RegionCodeINE = "";
                        resultPostal.data.Insert(0, _p);


                        foreach (var item in resultPostal.data)
                        {
                            pickerRegion.ItemsSource = resultPostal.data;
                        }
                    }

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
                    }
                


                

                 
                    //

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
        public async Task LeaderEvaluationChart()
        {

            try
            {
                pickerLeaderLineFrame.IsVisible = true;
                pickerLeaderLineGraph.IsVisible = true;
                pickerLeaderLineGraph.SelectedIndex = 2;
                 
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }
        private async void MultiLineLeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var picker = (Picker)sender;
                if (picker != null)
                {

                    if (Convert.ToString(picker.SelectedItem) == "Last 6 month")
                    {
                        LinerateType = "6month";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 2 month")
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
        public async Task LeaderEvaluationPikerSelect()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());
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
                        _leaderEvaluationModel.Add(new VoteEvaluationModel { backgroundColor = "transparent", borderColor = item.Color, label = item.Name, data = item.Vote });

                    }

                    var LeaderMultiLinechartConfigScript1 = GetLeaderMultiLineChartScript();
                    var htmlLeaderMultiLine = GetHtmlWithLeaderMultiLineChartConfig(LeaderMultiLinechartConfigScript1);
                    LeaderMultiLine.Html = htmlLeaderMultiLine;

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

        public async Task LeaderGroupChart()
        {

            try
            {
                filter.IsVisible = false;
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

        public async Task LeaderGroupPikerSelect()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());
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

        public async Task LeaderWinnerChart()
        {

            try
            {
                filter.IsVisible = false;
                LeaderWinnerFrame.IsVisible = true;
                LeaderWinnerGraph.IsVisible = true;
                pickerLeaderWinnerGraph.Items.Add("Region");
                pickerLeaderWinnerGraph.Items.Add("Province");
                pickerLeaderWinnerGraph.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        private async void LeaderWinner_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                var picker = (Picker)sender;
                if (picker != null)
                {

                    if (Convert.ToString(picker.SelectedItem) == "Region")
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

        public async Task LeaderWinnerPikerSelect()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());


                string postData = "type=" + LeaderwinnerType;
                var result = await CommonLib.LeaderWinnerChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderWinner?" + postData);
                if (result != null && result.Status != 0)
                {
                    _rateWinnerModel.Clear();
                    foreach (var item in result.chartData)
                    {
                        string name = item.Name.Replace("'", "");
                       // _rateWinnerModel.Add(new LeaderWinner { LeaderColor = item.LeaderColor, Name = name, PCode = item.PCode, per = item.per, PSName = item.PSName });
                    }

                    var LeaderWinnerchartConfigScript1 = GetLeaderWinnerChartScript();
                    var htmlLeaderWinner = GetHtmlWithLeaderWinnerChartConfig(LeaderWinnerchartConfigScript1);
                    LeaderWinner.Html = htmlLeaderWinner;


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

        private async void pickerReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdown == false)
            {
                var picker = (Picker)sender;
                if (picker != null)
                {
                    if (Convert.ToString(picker.SelectedItem) == "Last 7 Days")
                    {
                        rateType = "7days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 15 Days")
                    {
                        rateType = "15days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 30 Days")
                    {
                        rateType = "30days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 60 Days")
                    {
                        rateType = "60days";
                    }
                    else if (Convert.ToString(picker.SelectedItem) == "Last 1 year")
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

                    if (ChartNameId == 7)
                    {
                        await LeaderEvaluationChart();

                        var LeaderMultiLinechartConfigScript1 = GetLeaderMultiLineChartScript();
                        var htmlLeaderMultiLine = GetHtmlWithLeaderMultiLineChartConfig(LeaderMultiLinechartConfigScript1);
                        LeaderMultiLine.Html = htmlLeaderMultiLine;
                    }




                    if (ChartNameId == 3)
                    {

                        var chartConfigScript = GetChartScript1();
                        var html = GetHtmlWithChartConfig1(chartConfigScript);
                        e.Html = html;
                    }

                    //

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
    }
}
