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
    public partial class CandidatureCharts : ContentPage
    {
        public static int ChartNameId = 0;

        public static string RegionId = "";
        public static string CandidaturechartName = "";
        public List<object> CandidatureColor = null;
        public List<object> Candidaturename = null;
        public List<object> Candidaturevote = null;

        public static string CandidatureDoughnutchartName = "";
        public List<object> CandidatureDoughnutColor = null;
        public List<object> CandidatureDoughnutname = null;
        public List<object> CandidatureDoughnutvote = null;

        public List<string> CandidatureEvaluationTime = null;
        public List<VoteEvaluationModel> _voteEvaluationModel = new List<VoteEvaluationModel>();

        public static string winnerType = "";
        public List<ChartWinnerData> _voteWinnerModel = new List<ChartWinnerData>();
        public List<GroupChart> _voteGroupModel = new List<GroupChart>();

        public List<string> CandidatureGroupTime = null;

        public static int CandidatureId = 0;

        public static string LinerateType = "1year";

        public static bool dropdown = false;
        public static string rateType = "60days";
        public static string RegionCode = "";
        public static string ProvinceCode = "";
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public CandidatureCharts()
        {
            rateType = "60days";
            RegionCode = "";
            ProvinceCode = "";
            Device.BeginInvokeOnMainThread(async () =>
            {
                await ChartData();
            });

        }


        // for Candidature bar chart
        private string GetHtmlWithChartConfig(string chartConfig)
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

        private string GetChartScript()
        {
            var chartConfig = GetSpendingChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }


        private string GetSpendingChartConfig()
        {
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
                        backgroundColor=colors
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

        private string GetDoughnutChartScript()
        {
            var chartConfig = GetSpendingDoughnutChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }


        private string GetSpendingDoughnutChartConfig()
        {
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
                        backgroundColor=colors
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
                        mode = 'x'
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

                datasets = _voteEvaluationModel
            };
            return data;
        }

        // for Candidature Group chart
        private string GetHtmlWithGroupChartConfig(string chartConfig)
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

        private string GetGroupChartScript()
        {
            var chartConfig = GetSpendingGroupChartConfig();
            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
        }};";
            return script;
        }


        private string GetSpendingGroupChartConfig()
        {
            var config = new
            {
                type = "bar",

                data = GetGroupChartData(),
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
                datasets = _voteGroupModel
            };
            return data;
        }




        // for Candidature Winner chart
        private string GetHtmlWithWinnerChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:97%;height:97%;\"";

            var chartJsScript = "<script src=\"https://code.highcharts.com/maps/highmaps.js\"></script>";
            var chartJsScript1 = "<script src=\"https://code.highcharts.com/maps/modules/exporting.js\"></script>";
            var chartJsScript2 = "<script src=\"https://code.highcharts.com/mapdata/countries/es/es-all.js\"></script>";

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                          <div style=""width:98%;height:98%;"" id=""chart""></div>
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


            //var dd = "{ chart: {  map: 'countries/es/es-all',}, title: {text: '' }, mapNavigation: { enabled: true,buttonOptions: {verticalAlign: 'bottom'}}, series: [{ showInLegend: false, data: [['es-va', 1]], name: 'pp', color: 'blue', allAreas: false, }, ] }";


            var dd = "{ chart: {  map: 'countries/es/es-all',}, title: {text: '' }, exporting: { enabled: false },credits:{enabled: false}, mapNavigation: { enabled: false,buttonOptions: {verticalAlign: 'bottom'}}, series: [";
            foreach (var item in _voteWinnerModel)
            {
               // dd += "{ showInLegend: false, data: [['" + item.PSName + "', " + item.per + "]], name: '" + item.Name + "', color: '" + item.CandidatureColor + "', allAreas: false, },";

            }
            dd += "] }";



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
                labels = CandidatureGroupTime, 
                                               
                datasets = _voteGroupModel
            };
            return data;
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


                LoadPopup.CloseAllPopup();



               


            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }


        }
        public void imgStoreTapped(object sender, EventArgs e)
        {

            pickerProductCategory.Focus();

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
        public async Task ChartData()
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




                    //
                    InitializeComponent();
                    Title = "Candidature Charts";

                    partyGraph.HeightRequest = (height / 2);
                    CandidaturedoughnutGraph.HeightRequest = (height / 2);
                    CandidatureMultiLineGraph.HeightRequest = (height / 2) + 120;
                    CandidatureGroupGraph.HeightRequest = (height / 2);
                    CandidatureWinnerGraph.HeightRequest = (height / 2);
                    //


                    pickerProductCategory.Items.Add("Last 7 Days");
                    pickerProductCategory.Items.Add("Last 15 Days");
                    pickerProductCategory.Items.Add("Last 30 Days");
                    pickerProductCategory.Items.Add("Last 60 Days");
                    pickerProductCategory.Items.Add("Last 1 year");
                    pickerProductCategory.Items.Add("All");

                    pickerLineGraph.Items.Add("Last 1 year");
                    pickerLineGraph.Items.Add("Last 6 month");
                    pickerLineGraph.Items.Add("Last 2 month");




              

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
                if (ChartNameId == 5)
                {
                    await CandidatureGroupChart();
                    var GroupchartConfigScript1 = GetGroupChartScript();
                    var htmlGroup = GetHtmlWithGroupChartConfig(GroupchartConfigScript1);
                    CandidatureGroup.Html = htmlGroup;
                }
                if (ChartNameId == 6)
                {
                    await CandidatureWinnerChart();

                    var WinnerchartConfigScript1 = GetWinnerChartScript();
                    var htmlWinner = GetHtmlWithWinnerChartConfig(WinnerchartConfigScript1);
                    CandidatureWinner.Html = htmlWinner;
                }






                 
                 



                   






                  

                    LoadPopup.CloseAllPopup();



               


            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
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

                pickerLineGraph.SelectedIndex = 2;
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        private async void MultiLine_SelectedIndexChanged(object sender, EventArgs e)
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
                    await CandidatureEvaluationPikerSelect();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task CandidatureEvaluationPikerSelect()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());
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
                        _voteEvaluationModel.Add(new VoteEvaluationModel { backgroundColor = "transparent", borderColor = item.Color, label = item.Name, data = item.Vote });

                    }

                    var MultiLinechartConfigScript1 = GetMultiLineChartScript();
                    var htmlMultiLine = GetHtmlWithMultiLineChartConfig(MultiLinechartConfigScript1);
                    CandidatureMultiLine.Html = htmlMultiLine;
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


        public async Task CandidatureWinnerChart()
        {

            try
            {
                filterLayout.IsVisible = false;
                CandidatureWinnerFrame.IsVisible = true;
                CandidatureWinnerGraph.IsVisible = true;

                pickerWinnerGraph.Items.Add("Region");
                pickerWinnerGraph.Items.Add("Province");
                pickerWinnerGraph.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        private async void Winner_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                var picker = (Picker)sender;
                if (picker != null)
                {

                    if (Convert.ToString(picker.SelectedItem) == "Region")
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
        public async Task CandidatureWinnerPikerSelect()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());


                string postData = "type=" + winnerType;
                var result = await CommonLib.CandidatureWinnerChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CadidatureWinner?" + postData);
                if (result != null && result.Status != 0)
                {
                    _voteWinnerModel.Clear();
                    foreach (var item in result.chartData)
                    {
                        string name = item.Name.Replace("'", "");
                      //  _voteWinnerModel.Add(new ChartWinnerData { CandidatureColor = item.CandidatureColor, Name = name, PCode = item.PCode, per = item.per, PSName = item.PSName });
                    }

                    var WinnerchartConfigScript1 = GetWinnerChartScript();
                    var htmlWinner = GetHtmlWithWinnerChartConfig(WinnerchartConfigScript1);
                    CandidatureWinner.Html = htmlWinner;


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

        public async Task CandidatureGroupChart()
        {

            try
            {
                filterLayout.IsVisible = false;
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
        public async Task CandidatureGroupPikerSelect()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());
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
    }
}
