using GrylooProject.Model;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GrylooProject.Repository
{
    public class CommonLib
    {
        //public static string ws_MainUrl = "http://175.176.184.119:8080/api/UserApi/";

        //public static string ws_MainUrlMain = "http://175.176.184.119:8080/api/";

        public static string ws_MainUrl = "http://grylloo.com/grylloo/api/UserApi/";

        public static string ws_MainUrlMain = "http://grylloo.com/grylloo/api/";



        public static bool checkconnection()
        {
            var con = CrossConnectivity.Current.IsConnected;
            return con == true ? true : false;
        }



        //#wsLogin

        /// <summary>
        /// This is for login view
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<wsLogin> LoginUser(string url)
        {
            wsLogin objData = new wsLogin();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsLogin>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }




        //#wsRegistration
        /// <summary>
        /// this is for registration user
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsRegistration> RegisterUser(string url)
        {
            wsRegistration objData = new wsRegistration();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsRegistration>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<Province> GetProvince(string url)
        {
            Province objData = new Province();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<Province>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsVerfication
        /// <summary>
        /// This is for verfication page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsVerfication> VerificationCode(string url)
        {
            wsVerfication objData = new wsVerfication();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsVerfication>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsResendVerification
        /// <summary>
        /// this is for resend verification on mobile
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsResendVerification> ResendVerificationCode(string url)
        {
            wsResendVerification objData = new wsResendVerification();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsResendVerification>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsEditprofile
        /// <summary>
        /// This is for my profile page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>



        public static async Task<wsEditprofile>EditMyProfile(string url)
        {
            wsEditprofile objData = new wsEditprofile();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsEditprofile>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }

        public static async Task<IOSModel> IOSPaymentData(string url)
        {
            IOSModel objData = new IOSModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<IOSModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


       



        public static async Task<LeaderChart> leaderChart(string url)
        {
            LeaderChart objData = new LeaderChart();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<LeaderChart>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<postalCls> postalData(string url)
        {
            postalCls objData = new postalCls();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<postalCls>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<CandidatureChart> CandidatureVoteChart(string url)
        {
            CandidatureChart objData = new CandidatureChart();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CandidatureChart>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        public static async Task<ChartOrigen> CandidatureOrigenVoteChart(string url)
        {
            ChartOrigen objData = new ChartOrigen();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ChartOrigen>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<ChartOrigen> CandidatureDestinoVoteChart(string url)
        {
            ChartOrigen objData = new ChartOrigen();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ChartOrigen>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        public static async Task<EvaluationModel> CandidatureVoteEvaluationChart(string url)
        {
            EvaluationModel objData = new EvaluationModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<EvaluationModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }

        public static async Task<GryllooVoteModel> GryllooVoteResult(string url)
        {
            GryllooVoteModel objData = new GryllooVoteModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<GryllooVoteModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }

        public static async Task<EvaluationModel> LeaderVoteEvaluationChart(string url)
        {
            EvaluationModel objData = new EvaluationModel();
            try
            {
                using (var client = new HttpClient())
                {
                    //TimeSpan time = new TimeSpan(0, 0, 60);
                    //client.Timeout = time;
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<EvaluationModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<GroupChartModel> CandidatureGroupChart(string url)
        {
            GroupChartModel objData = new GroupChartModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<GroupChartModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<GroupChartModel> LeaderGroupChart(string url)
        {
            GroupChartModel objData = new GroupChartModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<GroupChartModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<WinnerData> CandidatureWinnerChart(string url)
        {
            WinnerData objData = new WinnerData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<WinnerData>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<LeaderWinnerData> LeaderWinnerChart(string url)
        {
            LeaderWinnerData objData = new LeaderWinnerData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<LeaderWinnerData>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<CategoryList> GetCategory(string url)
        {
            CategoryList objData = new CategoryList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CategoryList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<ChartNameList> GetChartName(string url)
        {
            ChartNameList objData = new ChartNameList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ChartNameList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<PackageModel> GetPackage(string url)
        {
            PackageModel objData = new PackageModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<PackageModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<UpdateDeviceToken> UpdateToken(string url)
        {
            UpdateDeviceToken objData = new UpdateDeviceToken();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<UpdateDeviceToken>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }

        public static async Task<profileDetailsModel> UpdateAccountDetails(string url)
        {
            profileDetailsModel objData = new profileDetailsModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<profileDetailsModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        public static async Task<CandidatureData> AllCandidatures(string url)
        {
            CandidatureData objData = new CandidatureData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CandidatureData>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<LeaderDrop> AllLeaders(string url)
        {
            LeaderDrop objData = new LeaderDrop();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<LeaderDrop>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        //wsConatctUs
        /// <summary>
        /// This is ContactUs View
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<wsContactUs> ContactUs(string url)
        {
            wsContactUs objData = new wsContactUs();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsContactUs>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsLegalNote
        /// <summary>
        /// This is for LegalNote View
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsLegalNote> LegalNote(string url)
        {
            wsLegalNote objData = new wsLegalNote();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsLegalNote>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
        public static async Task<HelpDataText> HelpResult(string url)
        {
            HelpDataText objData = new HelpDataText();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<HelpDataText>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }



        //wsChangePostalCode

        /// <summary>
        /// This is for change postal code popUp View
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>


        public static async Task<wsChangePostalCode> ChangePostalCode(string url)
        {
            wsChangePostalCode objData = new wsChangePostalCode();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsChangePostalCode>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsLeaderList
        /// <summary>
        /// This is for rate Leader list view
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        
        public static async Task<wsLeaderList> LeaderList(string url)
        {
            wsLeaderList objData = new wsLeaderList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsLeaderList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsPartyList
        /// <summary>
        /// This is for Vote parties page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsPartyList> PartyList(string url)
        {
            wsPartyList objData = new wsPartyList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsPartyList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }



        //wsLeaderRatePopUp
        /// <summary>
        /// This is for Rate Leader popUp View
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsLeaderRatePopUp> leaderRate(string url)
        {
            wsLeaderRatePopUp objData = new wsLeaderRatePopUp();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsLeaderRatePopUp>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsVotePARTY

        public static async Task<wsVotePARTY> PartyVote(string url)
        {
            wsVotePARTY objData = new wsVotePARTY();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsVotePARTY>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }

        public static async Task<UserTypeModel> UserTypeResult(string url)
        {
            UserTypeModel objData = new UserTypeModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<UserTypeModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        public static async Task<postalDob> GetpostalCodeDob(string url)
        {
            postalDob objData = new postalDob();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<postalDob>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }
    }
}
