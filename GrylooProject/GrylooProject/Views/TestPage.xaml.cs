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
using Xamarin.Forms.Xaml;



namespace GrylooProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public static string leaderchartName = "";
        public List<object> leaderColor = null;
        public List<object> leadername = null;
        public List<object> leadervote = null;
        public static string rateType = "30days";

        public static string CandidaturechartName = "";
        public List<object> CandidatureColor = null;
        public List<object> Candidaturename = null;
        public List<object> Candidaturevote = null;

        public static bool onappCheck = false;

        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public TestPage()
        {
            onappCheck = false;
             LeaderChartData();
           
        }

        public async void iosCheck()
        {
            //ios check
            try
            {

                if (Device.OS == TargetPlatform.iOS)
                {
                    var result = await CommonLib.GetpostalCodeDob(CommonLib.ws_MainUrl + "GetDobAndPostal?" + "Id=" + LoginDetails.userId);
                    if (result.dob == 0 || string.IsNullOrEmpty(result.code))
                    {
                        DobPostalUpdatePopup popup = new DobPostalUpdatePopup();
                        await App.Current.MainPage.Navigation.PushPopupAsync(popup);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        protected async override void OnAppearing()
        {
             
           
        }

        // for Candidature chart
        private string GetHtmlWithChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:99%;height:98%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            //
            var chartnpm = "<script src=\"https://cdn.jsdelivr.net/npm/chart.js@2.7.0/dist/Chart.min.js\"></script>";
            var chartdatalabel = "<script src=\"https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.3.0\"></script>";
            //

            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div  id=""chart-container"" {inlineStyle}>
                          <canvas  id=""chart"" />
                        </div>";
            var document = $@"<html style=""width:99%;height:98%;"">
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
            var aa = chartConfig1.Replace(@"\","");
            string aa1 = ""+'"';
            aa1 += "yadev:";
            var chartConfig2 = aa.Replace(aa1, "");
            string aa2 = "yadav";
            aa2 += "" + '"';
            var chartConfig= chartConfig2.Replace(aa2, "");


            var script = $@"var config = {chartConfig};
            window.onload = function() {{
              var canvasContext = document.getElementById(""chart"").getContext(""2d"");
              new Chart(canvasContext, config);
            }};";
            return script;
        }


        private string GetSpendingChartConfig()
        {
            var plugins =  @"yadev:{""datalabels"":{""backgroundColor"": function (context) {return context.dataset.backgroundColor;},""borderColor"": ""white"",""borderRadius"": 10,""borderWidth"": 2,""color"": ""white"",""display"": function (context) {var dataset = context.dataset;var count = dataset.data.length;var value = dataset.data[context.dataIndex];return value;},""font"": {""weight"": ""bold""},""formatter"": function (value, context) {var lbl = context.chart.data.labels[context.dataIndex];var newString = lbl.replace('%','');return newString + "" "" + value + ""%"";}}}yadav";
          
           
            var config = new
            {
                type = "doughnut",
                data = GetPieChartData(),
                options = new
                {

                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "bottom",
                        display = false,
                        onClick = false
                    },
                    animation = new
                    {
                        animateScale = true
                    },
                    plugins


                    //
                }
            };
           
            var jsonConfig = JsonConvert.SerializeObject(config);
           
            return jsonConfig;
        }

        private object GetPieChartData()
        {

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
                        data =  dataPoints ,
                        backgroundColor=colors,
                         datalabels=new{
                            anchor= "center",
                         }
                    }
                },
                labels
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



            leadervote.Add("0.2");
            var dataPoints = leadervote; 
            var colors = leaderColor; 
            var labels = leadername; 



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





                string postData = "rateType=60days&RegionCode=&ProvinceCode=";
                var result = await CommonLib.leaderChart(CommonLib.ws_MainUrlMain + "LeaderApi/LeaderVoteFilterPersent?" + postData);
                if (result != null && result.Status != 0)
                {


                    leaderchartName = result.chartData.ChartName;
                    leaderColor = result.chartData.LeaderColor;
                    leadername = result.chartData.LeaderName;
                    leadervote = result.chartData.Votes;
                    await CandidatureChartData();





                    InitializeComponent();

                    Title = "Grylloo";


                   


                    CandituresButton.WidthRequest = (width / 2) - 10;

                    leadersButton.WidthRequest = (width / 2) - 10;

                    CandituresButton.BackgroundColor = Color.FromHex("#fcbf49");
                    leadersButton.BackgroundColor = Color.White;


                   


                    leaderGraph.IsVisible = false;
                    partyGraph.IsVisible = true;




                    var chartHeight = (height / 6) - 10;
                    partyGraph.HeightRequest = chartHeight;

                    var chartConfigScript1 = GetChartScript();
                    var html1 = GetHtmlWithChartConfig(chartConfigScript1);
                    d.Html = html1;


                     




                    var chartConfigScript = GetChartScript1();
                    var html = GetHtmlWithChartConfig1(chartConfigScript);
                    e.Html = html;


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

            //iosCheck();
        }


        public async Task CandidatureChartData()
        {

            try
            {
                string postData = "rateType=60days&RegionCode=&ProvinceCode=";
                var result = await CommonLib.CandidatureVoteChart(CommonLib.ws_MainUrlMain + "CandidatureApi/CandidatureVoteFilterPersent?" + postData);
                if (result != null && result.Status != 0)
                {


                    CandidaturechartName = result.chartData.ChartName;
                    CandidatureColor = result.chartData.CandidatureColor;
                    Candidaturename = result.chartData.CandidatureName;
                    Candidaturevote = result.chartData.Votes;

                    rateType = "30days";

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



        private void canditureClicked(EventArgs e)
        {
            if (CandituresButton.BackgroundColor == Color.White)
            {
                leadersButton.BackgroundColor = Color.White;
                CandituresButton.BackgroundColor = Color.FromHex("#fcbf49");



            }
            leaderGraph.IsVisible = false;
            partyGraph.IsVisible = true;



        }


        private void leadersClicked(EventArgs e)
        {
            if (leadersButton.BackgroundColor == Color.White)
            {
                CandituresButton.BackgroundColor = Color.White;
                leadersButton.BackgroundColor = Color.FromHex("#fcbf49");

            }
            leaderGraph.IsVisible = true;
            partyGraph.IsVisible = false;



        }


    }
}