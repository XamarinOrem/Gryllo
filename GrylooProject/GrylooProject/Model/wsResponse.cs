using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrylooProject.Model
{

    /// <summary>
    /// wsLogin Model
    /// </summary>
    public class wsLogin
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public int Otp { get; set; }
    }

    /// <summary>
    /// wsRegistration Model
    /// </summary>
    public class wsRegistration
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public string Otp { get; set; }
    }

    public class Province
    {
        public string ProvinceName { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
    }

    // wsVerfication
    /// <summary>
    /// wsVerfication model
    /// </summary>

    public class User
    {
        public int id { get; set; }
        public DateTime creationDate { get; set; }
        public string phone { get; set; }
        public int birthYear { get; set; }
        public int postalCode { get; set; }
        public string gender { get; set; }
        public int typeOfUser { get; set; }
        public bool Lifeline { get; set; }
    }

    public class wsVerfication
    {
        public int Status { get; set; }

        public string SessionId { get; set; }
        
        public string msg { get; set; }

        public User User { get; set; }
    }




    //wsResendVerification
    /// <summary>
    /// ResendVerification Model
    /// </summary>

    public class wsResendVerification
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public int Otp { get; set; }
    }



    // wsEditprofile

    /// <summary>
    /// Edit profile Model
    /// </summary>
    public class Myprofile
    {
        public string phone { get; set; }
        public int birthYear { get; set; }
        public int postalCode { get; set; }
        public string province { get; set; }
    }

    public class wsEditprofile
    {
        public int Status { get; set; }
        public string SessionId { get; set; }
        public string msg { get; set; }
        public Myprofile Myprofile { get; set; }
    }

    //wsContactUs

        /// <summary>
        /// Contact Us Model
        /// </summary>
        /// 

    public class wsContactUs
    {
        public int Status { get; set; }

        public string SessionId { get; set; }
        public string msg { get; set; }
    }

    // wsLegalNote
    /// <summary>
    /// LegalNote Model
    /// </summary>
    public class Note
    {
        public int Id { get; set; }

       
        public string LegalText { get; set; }
    }

    public class wsLegalNote
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public string SessionId { get; set; }
        public Note Note { get; set; }
    }
    public class HelpNote
    {
        public int Id { get; set; }
        public string HelpText { get; set; }
    }
    public class IOSModel
    {
        public string keyName { get; set; }
        public string keyPrice { get; set; }
        public string keyTime { get; set; }
        public int Status { get; set; }
    }

    public class HelpDataText
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public HelpNote Note { get; set; }
    }

    //wsChangePostalCode
    /// <summary>
    /// Change PostalCode 
    /// </summary>

    public class wsChangePostalCode
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
    }


    //wsLeaderList

    /// <summary>
    /// Rate LeaderList  Model
    /// </summary>

    public class Leader
    {
        public int LeaderCode { get; set; }
        public string LeaderName { get; set; }
        public string Image { get; set; }
        public string LabelStatus { get; set; }
        public string RateButtonStatus { get; set; }



    }

    public class wsLeaderList
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public List<Leader> Leaders { get; set; }
        public int Count { get; set; }
    }


    //wsPartyList
    /// <summary>
    /// Party list Model
    /// </summary>
    public class Candidature
    {
        public int CandidatureCode { get; set; }
        public string CandidatureName { get; set; }
        public string CandidatureSortName { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string VotingStatus { get; set; }
        public string AgainVotingStatus { get; set; }
    }

    public class wsPartyList
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }

        public string VoteButtonStatus { get; set; }

        public string LabelStatus { get; set; }

        public List<Candidature> Candidatures { get; set; }
        public int Count { get; set; }
    }


    //wsLeaderRatePopUp


    public class wsLeaderRatePopUp
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }

    }


    //wsVotePARTY

    public class wsVotePARTY
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
    }


    public class ChartData
    {
        public string ChartName { get; set; }
        public List<object> LeaderName { get; set; }
        public List<object> LeaderColor { get; set; }
        public List<object> Votes { get; set; }
    }

    public class LeaderChart
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public ChartData chartData { get; set; }
    }

    public class CandidatureChartData
    {
        public string ChartName { get; set; }
        public List<object> CandidatureName { get; set; }
        public List<object> CandidatureColor { get; set; }
        public List<object> Votes { get; set; }
    }

    public class CandidatureChart
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public CandidatureChartData chartData { get; set; }
    }


    public class PostalDetail
    {
        public int Id { get; set; }
        public string ProvincePostalCode { get; set; }
        public string ProvinceName { get; set; }
        public string RegionCodeINE { get; set; }
        public string RegionName { get; set; }
        public bool Active { get; set; }
    }

    public class postalCls
    {
        public List<PostalDetail> data { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
    }



    //
    public class VoteEvaluation
    {
        public List<object> Vote { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int? Code { get; set; }
    }
    public class EvaluationChartData
    {
        public List<string> Time { get; set; }
        public List<VoteEvaluation> voteEvaluation { get; set; }
    }
    public class EvaluationModel
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public EvaluationChartData chartData { get; set; }
    }
    public class VoteEvaluationModel
    {
        public List<object> data { get; set; }
        public string label { get; set; }
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }

        public int pointHoverRadius { get; set; }
    }

    public class GryllooVoteEvaluationModel
    {
        public List<int> data { get; set; }
        public string label { get; set; }
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
        public int pointHoverRadius { get; set; }
    }



    //
    public class VoteGroup
    {
        public string color { get; set; }
        public string sex { get; set; }
        public List<double> per { get; set; }
    }

    public class GroupChartData
    {
        public List<string> Time { get; set; }
        public List<VoteGroup> voteGroup { get; set; }
    }

    public class GroupChartModel
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public GroupChartData chartData { get; set; }
    }

    public class GroupChart
    {
        public List<double> data { get; set; }
        public string label { get; set; }
        public string backgroundColor { get; set; }
    }
    //
    public class CandidatureDetail
    {
        public int Id { get; set; }
        public int CandidatureCode { get; set; }
        public int TypeOfElection { get; set; }
        public string CandidatureLogoLink { get; set; }
        public string CandidatureLogo { get; set; }
        public string CandidatureColor { get; set; }
        public string CandidatureAcronyms { get; set; }
        public string CandidatureName { get; set; }
        public bool Active { get; set; }
        public string Wikipedia { get; set; }
    }

    public class CandidatureData
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public List<CandidatureDetail> chartData { get; set; }
    }




    public class ChartWinnerData
    {
        public string CandidatureColor { get; set; }
        public string Name { get; set; }
        public List<VoteWinnerChart22> voteWinnerChart22 { get; set; }
        public ChartWinnerData()
        {
            voteWinnerChart22 = new List<VoteWinnerChart22>();
        }
    }
    public class VoteWinnerChart22
    {
        public string PCode { get; set; }
        public string PSName { get; set; }
        public double per { get; set; }
    }



    //public class ChartWinnerData
    //{
    //    public string PCode { get; set; }
    //    public string PSName { get; set; }
    //    public string CandidatureColor { get; set; }
    //    public string Name { get; set; }
    //    public double per { get; set; }
    //}

    //public class WinnerData
    //{
    //    public string SessionId { get; set; }
    //    public int Status { get; set; }
    //    public string msg { get; set; }
    //    public List<ChartWinnerData> chartData { get; set; }
    //}
    public class VoteWinnerChart2
    {
        public string PCode { get; set; }
        public string PSName { get; set; }
        public double per { get; set; }
    }

    public class MapChartData
    {
        public string CandidatureColor { get; set; }
        public string Name { get; set; }
        public List<VoteWinnerChart2> voteWinnerChart2 { get; set; }
    }

    public class WinnerData
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public List<MapChartData> chartData { get; set; }
    }








    public class LeaderData
    {
        public int Id { get; set; }
        public int LeaderCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime CancelDate { get; set; }
        public string LeaderName { get; set; }
        public string LeaderSurname { get; set; }
        public object LeaderMaidenName { get; set; }
        public string LeaderPhoto { get; set; }
        public string LeaderPhotoLink { get; set; }
        public string LeaderColor { get; set; }
        public bool Active { get; set; }
        public object Rating { get; set; }
        public string Wikipedia { get; set; }
    }

    public class LeaderDrop
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public List<LeaderData> chartData { get; set; }
    }


    //public class LeaderWinner
    //{
    //    public string PCode { get; set; }
    //    public string PSName { get; set; }
    //    public string LeaderColor { get; set; }
    //    public string Name { get; set; }
    //    public double per { get; set; }
    //}

    public class LeaderWinner
    {
        public string LeaderColor { get; set; }
        public string Name { get; set; }
        public List<RateWinnerChart22> rateWinnerChart22 { get; set; }
        public LeaderWinner()
        {
            rateWinnerChart22 = new List<RateWinnerChart22>();
        }
    }
    public class RateWinnerChart22
    {
        public string PCode { get; set; }
        public string PSName { get; set; }
        public double per { get; set; }
    }




    //public class LeaderWinnerData
    //{
    //    public string SessionId { get; set; }
    //    public int Status { get; set; }
    //    public string msg { get; set; }
    //    public List<LeaderWinner> chartData { get; set; }
    //}


    //
    public class RateWinnerChart2
    {
        public string PCode { get; set; }
        public string PSName { get; set; }
        public double per { get; set; }
    }

    public class ChartDataMap
    {
        public string LeaderColor { get; set; }
        public string Name { get; set; }
        public List<RateWinnerChart2> rateWinnerChart2 { get; set; }
    }

    public class LeaderWinnerData
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public List<ChartDataMap> chartData { get; set; }
    }











    public class CategoryData
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string chartId { get; set; }
        public string CategoryColor { get; set; }
    }

    public class CategoryList
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public List<CategoryData> CategoryData { get; set; }
    }


    public class ResultData1
    {
        public int Id { get; set; }
        public string ChartName { get; set; }
        public string img { get; set; }
        public bool NGust { get; set; }
        public bool NRegister { get; set; }
        public bool NPremium { get; set; }
        public bool RGust { get; set; }
        public bool RRegister { get; set; }
        public bool RPremium { get; set; }
        public bool PGust { get; set; }
        public bool PRegister { get; set; }
        public bool PPremium { get; set; }
        public int ChartId { get; set; }
        public int CategoryId { get; set; }
    }
    public class ResultData
    {
        public int Id { get; set; }
        public string ChartName { get; set; }
        public bool NGust { get; set; }
        public bool NRegister { get; set; }
        public bool NPremium { get; set; }
        public bool RGust { get; set; }
        public bool RRegister { get; set; }
        public bool RPremium { get; set; }
        public bool PGust { get; set; }
        public bool PRegister { get; set; }
        public bool PPremium { get; set; }
        public int ChartId { get; set; }
        public int CategoryId { get; set; }
    }

    public class ChartNameList
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public List<ResultData> resultData { get; set; }
    }
    public class UpdateDeviceToken
    {
        public int Status { get; set; }
        public string msg { get; set; }
    }




    public class PackageDetails
    {
        public int Id { get; set; }
        public string PackageNumber { get; set; }
        public string PackagePrice { get; set; }
        public string PackageTime { get; set; }
    }

    public class PackageModel
    {
        public PackageDetails details { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
    }


    public class UserResultType
    {
        public int Id1 { get; set; }
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int PhonePrefix { get; set; }
        public string Phone { get; set; }
        public int BirthYear { get; set; }
        public string PostalCode { get; set; }
        public string Sex { get; set; }
        public int TypeOfUser { get; set; }
        public string NumberOfLicence { get; set; }
        public object NumberOfConnection { get; set; }
        public DateTime? DateOfLastConnection { get; set; }
        public DateTime? LastVoteDate { get; set; }
        public object MobileModel { get; set; }
        public bool Active { get; set; }
        public bool Lifeline { get; set; }
        public int Otp { get; set; }
        public object DeviceToken { get; set; }
        public string DeviceType { get; set; }
        public string Guid { get; set; }
        public DateTime? LastRateDate { get; set; }
    }

    public class UserTypeModel
    {
        public UserResultType UserResult { get; set; }
        public int Status { get; set; }
        public string msg { get; set; }
        public string expDate { get; set; }
    }


    //

    public class postalDob
    {
        public string code { get; set; }
        public int dob { get; set; }
        public int Status { get; set; }
    }

    public class OrigenChartData
    {
        public object ChartName { get; set; }
        public List<object> CandidatureName { get; set; }
        public List<object> CandidatureColor { get; set; }
        public List<object> Votes { get; set; }
    }

    public class ChartOrigen
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public OrigenChartData chartData { get; set; }
    }


    public class GryllooVoteChartData
    {
        public List<string> Time { get; set; }
        public List<int> data { get; set; }
    }

    public class GryllooVoteModel
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public GryllooVoteChartData chartData { get; set; }
    }





    public class Profile
    {
        public int Id1 { get; set; }
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int PhonePrefix { get; set; }
        public string Phone { get; set; }
        public int BirthYear { get; set; }
        public string PostalCode { get; set; }
        public string Sex { get; set; }
        public int TypeOfUser { get; set; }
        public string NumberOfLicence { get; set; }
        public object NumberOfConnection { get; set; }
        public DateTime? DateOfLastConnection { get; set; }
        public DateTime? LastVoteDate { get; set; }
        public object MobileModel { get; set; }
        public bool Active { get; set; }
        public bool Lifeline { get; set; }
        public int Otp { get; set; }
        public object DeviceToken { get; set; }
        public string DeviceType { get; set; }
        public string Guid { get; set; }
        public DateTime? LastRateDate { get; set; }
    }

    public class profileDetailsModel
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public Profile profile { get; set; }
    }
}
