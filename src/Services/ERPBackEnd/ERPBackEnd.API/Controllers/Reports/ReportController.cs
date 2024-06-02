using AutoMapper;
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ResortAppStore.Services.ERPBackEnd.Application.Reports;
using ResortAppStore.Services.ERPBackEnd.Application.Reports.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Reports.ViewModel;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.Runtime.Serialization.Json;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]

    public class ReportController : MainController<ReportFile, ReportFileDto, long>
    {
        #region property

        private readonly IReportFileRepository _reportFileService;
        private readonly IConfiguration _configuration;


        #endregion

        public ReportController(GMappRepository<ReportFile, ReportFileDto, long> mainRepos, IMapper mapper, IReportFileRepository reportFileService, IConfiguration configuration) : base(mainRepos)
        {
            _reportFileService = reportFileService;
            _configuration = configuration;
        }

        //[HttpGet]
        //[Route("getReportList")]
        [HttpGet("getReportList")]

        public List<ReportFile> getReportList(int reportType, int reportTypeID)
        {
            List<ReportFile> lst = _reportFileService.GetReportList(reportType, reportTypeID);
            return lst;
        }

        [HttpPost]
        [Route("addReport")]
        public void addReport([FromBody] ReportFile rpt)
        {
            _reportFileService.AddReport(rpt);
        }

        [HttpGet]
        [Route("reportDelete")]
        public bool deleteReport(long reportID)
        {
            try
            {
                _reportFileService.DeleteByID(reportID);
                return true;
            }
            catch
            {
                return false;

            }
        }

        [HttpGet]
        [Route("setDefaultReport")]
        public bool setDefaultReport(long reportId)
        {


            bool stat = _reportFileService.UpdateReportDefault(reportId);
            if (stat)
            {
                return true;
                //return "OK";
            }
            else
            {
                return false;
                //return "Error";
            }

        }
        //[HttpGet]
        //[Route("cancelDefaultReport")]
        [HttpGet("cancelDefaultReport")]

        public void cancelDefaultReport(int reportType, int reportTypeId)
        {
            _reportFileService.CancelDefaultReportStatus(reportType, reportTypeId);
        }

        [HttpGet]
        [Route("getReportGeneric")]
        public string getReportGeneric(string id, string[] reportParams)
        {
            StiReport report = new StiReport();
            // var path = StiNetCoreHelper.MapPath(this, "ClientApp/src/assets/Reports/" + id + ".mrt");
            var path = "";
            report.Load(path);

            string[] rptParams;
            for (int i = 0; i < reportParams.Length; i++)
            {
                rptParams = reportParams[i].Split('!');

                report["@" + rptParams[0]] = rptParams[1];
                report[rptParams[0]] = rptParams[1];
                // }

            }
            var dbMS_SQL = (StiSqlDatabase)report.Dictionary.Databases["MS SQL"];
            // dbMS_SQL.ConnectionString = _settings.Value.conn;
            //string cons = configuration.GetConnectionString("akarConnection");
            dbMS_SQL.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            report.Render(false);
            return report.SaveDocumentJsonToString();
        }
        //=End Mosfet===

        [HttpPost]
        [Route("getReport")]
        public string getReport([FromBody] JObject data)
        {

            string reportpath = data.GetValue("path") != null ? data.GetValue("path").ToString() : "";
            string fromdate = data.GetValue("fromdate") != null ? data.GetValue("fromdate").ToString() : "";
            string langcode = data.GetValue("langcode") != null ? data.GetValue("langcode").ToString() : "";
            string todate = data.GetValue("todate") != null ? data.GetValue("todate").ToString() : "";
            string customerid = data.GetValue("customerid") != null ? data.GetValue("customerid").ToString() : "";
            string buildingid = data.GetValue("buildingid") != null ? data.GetValue("buildingid").ToString() : "";
            string typeid = data.GetValue("typeid") != null ? data.GetValue("typeid").ToString() : "";
            string floorid = data.GetValue("floorid") != null ? data.GetValue("floorid").ToString() : "";
            string tenentid = data.GetValue("tenentid") != null ? data.GetValue("tenentid").ToString() : "";
            string unitid = data.GetValue("unitid") != null ? data.GetValue("unitid").ToString() : "";
            string responsibleid = data.GetValue("responsibleid") != null ? data.GetValue("responsibleid").ToString() : "";
            string equipmentid = data.GetValue("equipmentid") != null ? data.GetValue("equipmentid").ToString() : "";
            string ownerid = data.GetValue("ownerid") != null ? data.GetValue("ownerid").ToString() : "";
            string contractid = data.GetValue("contractid") != null ? data.GetValue("contractid").ToString() : "";
            string regionid = data.GetValue("regionid") != null ? data.GetValue("regionid").ToString() : "";
            string realestateid = data.GetValue("realestateid") != null ? data.GetValue("realestateid").ToString() : "";
            ////////////////////////////


            StiReport report = new StiReport();
            //var path = StiNetCoreHelper.MapPath(this, "ClientApp/src/assets/Reports/" + reportpath + ".mrt");
            var path = "";
            report.Load(path);
            if (!string.IsNullOrEmpty(langcode))
                report["@langcode"] = langcode;

            if (!string.IsNullOrEmpty(fromdate))
            {
                report["@fromdate"] = fromdate;
                report["fromdate"] = fromdate;
            }

            if (!string.IsNullOrEmpty(todate))
            {
                report["@todate"] = todate;
                report["todate"] = todate;
            }

            if (!string.IsNullOrEmpty(customerid))
            {
                report["@customerid"] = customerid;
                report["customerid"] = customerid;
            }

            if (!string.IsNullOrEmpty(buildingid))
                report["@buildingid"] = buildingid;
            if (!string.IsNullOrEmpty(typeid))
                report["@typeid"] = typeid;
            if (!string.IsNullOrEmpty(floorid))
                report["@floorid"] = floorid;
            if (!string.IsNullOrEmpty(tenentid))
                report["@tenantid"] = tenentid;
            if (!string.IsNullOrEmpty(unitid))
                report["@unitid"] = unitid;
            if (!string.IsNullOrEmpty(responsibleid))
                report["@responsibleid"] = responsibleid;
            if (!string.IsNullOrEmpty(equipmentid))
                report["@equipmentid"] = equipmentid;
            if (!string.IsNullOrEmpty(ownerid))
                report["@ownerid"] = ownerid;
            if (!string.IsNullOrEmpty(contractid))
            {
                report["@contractid"] = contractid;
                report["contractid"] = contractid;
            }
            if (!string.IsNullOrEmpty(regionid))
                report["@regionid"] = regionid;

            if (!string.IsNullOrEmpty(realestateid))
                report["@realestateid"] = realestateid;


            var dbMS_SQL = (StiSqlDatabase)report.Dictionary.Databases["MS SQL"];
            // dbMS_SQL.ConnectionString = _settings.Value.conn;


            dbMS_SQL.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            report.Render(false);
            return report.SaveDocumentJsonToString();
        }

        [HttpPost]
        [Route("GetDataSource")]
        public void GetDataSource(string id, string[] reportParams)
        {

            try
            {
                var command = (CommandJson)new DataContractJsonSerializer(typeof(CommandJson)).ReadObject(HttpContext.Request.Body);

                Result result = new Result();

                if (command.Database == "MS SQL")
                {

                    string[] rptParams;
                    string paramString = "";
                    for (int i = 0; i < reportParams.Length; i++)
                    {
                        //Index 0 Parameter Name;
                        //Index 1 Parameter Value;
                        //index 2 Parameter Type;
                        rptParams = reportParams[i].Split('!');
                        //paramString += "Declare @"+rptParams[0]+" "+rptParams[2]+" set @"+rptParams[0]+"=" +rptParams[1]+" \n";
                        if (rptParams[2] == "datetime")
                        {
                            paramString += "Declare @" + rptParams[0] + " " + rptParams[2] + "='" + Convert.ToDateTime(rptParams[1]).ToString("yyyy-MM-dd") + "' \n ";
                        }
                        else
                        {
                            paramString += "Declare @" + rptParams[0] + " " + rptParams[2] + "='" + rptParams[1] + "' \n ";
                        }

                    }

                    command.QueryString = paramString + command.QueryString;


                    //------------------------end my code
                    result = MSSQLAdapter.Process(command);
                }
                var serializer = new DataContractJsonSerializer(typeof(Result));
                serializer.WriteObject(HttpContext.Response.Body, result);
                HttpContext.Response.Body.Flush();


            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }



        [HttpGet]
        [Route("getReportForDesigner")]

        public string getReportForDesigner(string id, string[] reportParams)
        {
            StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHn79zfqs2HRkip7S88L/ioVbV3Kbick2FjtZBpXgagQPyheR5"
+
"t/HzfgGilltld2PNHDnDnxikxvqgeyZBSslS8B6Vzl4sEc4XRVRet9M4Iv0n+y7wdpIv8Y4xSY9Dhsrw8M2Ix0IoK2"
+
"O7Yw0aj7wDVn2HNNqmeVmJiGdsRPpo7SNcP7p1eHCRmzDPTsjUgKBRJ9z01EHsqBO5c1CFrqjUtCl0ruZfYF7eW4J2"
+
"o4FmeZLHcXxaxDA851FfV93tPjgWqbfICWQ1s9VqWFKgwoVDMvNXpPNCGaNKf1UQC0Gxe78MV4Vdao/1FKL0C8A6ZP"
+
"gWbZzSWSikpk34447D6t6zeA3uJfyvMhIi7t++PzoDpC/Pwj3TBrWbKyVIGkZAJY7F3++ZxPFWpabuGmFgg8/efNFQ"
+

"W0yPoU5eBXWH8AcBC6RgEYzRWYj7T5c4ubIWulVLsOIU70cPFht4lB2aaW82vvuTXsQRJc+lHJJNtGLHM7x0/8uBxE"
+
"hbXaeuPS0uSgFkksG43O4WeU+a8KfaIIjW7+";
            //var path = StiNetCoreHelper.MapPath(this, "ClientApp/src/assets/Reports/" + id + ".mrt");
            var path = "";
            StreamReader rd = new StreamReader(path);
            string data = rd.ReadToEnd();
            rd.Close();
            return data;
        }

        [HttpPost]
        [Route("savefile")]
        public string savefile([FromBody] DemoDataVM jsonString)
        {
            try
            {
                if (_reportFileService.CheckExistByName(jsonString.FileName) && jsonString.IsSaveAs == true)
                {
                    //Report Name is Exist
                    return "اسم التقرير مسجل من قبل";
                }
                else
                {
                    //var path = StiNetCoreHelper.MapPath(this, "ClientApp/src/assets/Reports/" + jsonString.fileName + ".mrt");
                    var path = "";
                    StreamWriter wr = new StreamWriter(path);
                    wr.Write(jsonString.Data);
                    wr.Close();
                    return "تم حفظ البيانات بنجاح";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


    }
}
