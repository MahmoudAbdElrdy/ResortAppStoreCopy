using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.API.Reports.BL;
using ResortAppStore.Services.ERPBackEnd.Application.Reports;
using StackExchange.Redis;
using Stimulsoft.Base;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using System.Xml.Serialization;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{

    public class ViewerController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration configurationManager;
      
        private readonly IReportFileRepository reportFileRepos;
        public ViewerController(IWebHostEnvironment env, IConfiguration configuration, IReportFileRepository reportFileRepos)
        {
            StimulsoftFunctions.RegisterFunctions();
            this.env = env;
            this.configurationManager = configuration;
            
            this.reportFileRepos = reportFileRepos;
           


        }
        public IActionResult Reports()
        {
            return View();
        }

        public ActionResult GetReport(long id, params string[] reportParameter)
        {
            StiLicense.Key =
              "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHmO00Jw23AL1+qtDS62NHQcK/vmoylhb1eTUS7dHjAfrX4VO+" +
"OlSSwWo+oqDnvssp1JH8lM4Et+bW594mQqiJdFwLLx6YbBFxvQZwMQo9DELFCtV+/bZAW+Xc/rYWr6MbKogneBSndG" +
"pPuKfm9slXx+QuD4iSL07JSjOOXUiEcMMqNBYqyYWrpJlz1rVdv+1VvxKN8E0f/+rGFHxJowELrGjsJDyCSHFqwGNn" +
"yLo2Zl852K/JLs90OvY4J6DwnWryMBSXf8v2ayG41GFkpRflhbElsiroEoLABVrDx32MN+y47uhweSyuvQOzR1K1KQ" +
"OdJrhWE77MKl4c1tBSTrBVHUU2o/VyZiYOcPmmjxPhH94N0iZllhzQVbVf8wBLz/pwvP1qwTzI9RNk40pAOPOtmcOZ" +
"+WI4/utxJhdvD3easLcUKjQ1+T6exGvVvNCNpWxJebkLxy8Y7MhR6j1e27dYJ3xuQds3YMW6K5RSBCjwRh01GVTAUv" +
"A6ELu4pfMY87gThXZ0XT5V2uu37CXP3hrUaU";
           // StiOptions.Viewer.RightToLeft = StiRightToLeftType.Yes;


            var reportParams = reportParameter[0].Split("\n");

            //reportFileRepos.SetDatabaseName(dbName);
            // Create the report object and load data from xml file

            var report = new StiReport();

            var reportFile = reportFileRepos.Find(id);

            
            var x = report.DataSources;
            try
            {
                report.Load(env.ContentRootPath + @"\ReportFile\" + reportFile.FileName + ".mrt");

                var dbMS_SQL = (StiSqlDatabase)report.Dictionary.Databases["MS SQL"];
                string connectionString = configurationManager.GetConnectionString("DefaultConnection"); 
                dbMS_SQL.ConnectionString = connectionString;

                string[] rptPrams;
                if (reportParams.Length > 1)
                {
                    for (int i = 0; i < reportParams.Length; i++)
                    {

                        rptPrams = reportParams[i].Split('!');

                        report.Dictionary.Variables.Add(rptPrams[0], rptPrams[1]);
                        report["@" + rptPrams[0]] = rptPrams[1];
                        Console.WriteLine("@" + rptPrams[0]);
                        //var p = new StiDataParameter("@" + rptPrams[0], rptPrams[1],(int)SqlDbType.NVarChar, 255);
                    }
                }
                else
                {
                    for (int i = 0; i < reportParameter.Length; i++)
                    {

                        rptPrams = reportParameter[i].Split('!');

                        report.Dictionary.Variables.Add(rptPrams[0], rptPrams[1]);
                        report["@" + rptPrams[0]] = rptPrams[1];
                        Console.WriteLine("@" + rptPrams[0]);
                        //var p = new StiDataParameter("@" + rptPrams[0], rptPrams[1],(int)SqlDbType.NVarChar, 255);
                    }
                }
                report.Render();
                //StiDataBand dataBand = report.GetComponentByName("DataDataSource2") as StiDataBand;
                //dataBand.AlignTo(StiAligning.Left);
                ////dataBand.G = true;
                //dataBand.RightToLeft = false;
                //var style = report.Dictionary.Styles.Default;
                //style.WritingMode = StiWritingMode.RightToLeft;

                // StiReport report = new StiReport();
                //                string cultureName = "ar";
                //report.LocalizeReport(cultureName);
                //                report.Render(false);
                //report.LocalizeReport(cultureName); report.Show();
                //  StiOptions.Viewer.RightToLeft = StiRightToLeftType.Yes;

                //
                // Required for Windows Form Designer support
                //
                //InitializeComponent();




              //  report.Render();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            //return StiMvcViewer.GetReportResult(report);
            return StiNetCoreViewer.GetReportResult(this, report);



        }

        private void Report_EndRender(object sender, EventArgs e)
        {

            var rpt = (StiReport) sender;
            foreach (StiPage page in rpt.RenderedPages)
            {
                double max = 0;
                foreach (StiComponent comp in page.GetComponents())
                {
                    if (comp.Bottom > max) max = comp.Bottom;
                }
                page.PageHeight = max + page.Margins.Top + page.Margins.Bottom;
                page.SegmentPerHeight = 1;
            }

        }

        public List<Image64Response> GetBillImage(long id, string fileName,  params string[] reportParameter)
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
            var report = new StiReport();
            report.EndRender += Report_EndRender;

            try
            {

                //report.RegData("DataSource.BillMaster", billMasterDt);
                //report.Dictionary.Synchronize();
              //  reportFileRepos.SetDatabaseName(dbName);
                var reportFile = reportFileRepos.Find(id);
                report.Load(env.ContentRootPath + @"\ReportFile\" + reportFile.FileName + ".mrt");

                var dbMS_SQL = (StiSqlDatabase)report.Dictionary.Databases["MS SQL"];
                string connectionString = configurationManager.GetConnectionString("DefaultConnection");
                //+ ";Initial Catalog=" + dbName + ";";
                dbMS_SQL.ConnectionString = connectionString;
                string[] rptPrams;
                for (int i = 0; i < reportParameter.Length; i++)
                {

                    rptPrams = reportParameter[i].Split('!');
                    report.Dictionary.Variables.Add(rptPrams[0], rptPrams[1]);
                    report["@" + rptPrams[0]] = rptPrams[1];
                }
            }
            catch (Exception)
            {

            }
            //Guid fileGuid = Guid.NewGuid();
            string filePath = env.ContentRootPath + @"\Resources\Invoices\" + Guid.NewGuid() + ".png";


            report.Render();

            report.ExportDocument(StiExportFormat.ImagePng, filePath);
            byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            List<Image64Response> lst = new List<Image64Response>();

            lst.Add(new Image64Response()
            {
                Image64 = base64ImageRepresentation
            });
            return lst;
        }


        public ActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        public ActionResult Design(long id, int reportType, int reportTypeId, string[] reportParameter)
        {
            string p = "";
            for (int i = 0; i < reportParameter.Length; i++)
            {
                p += reportParameter[i] + "\n";
            }
            return RedirectToAction("Reports", "Designer", new { id = id, reportType = reportType, reportTypeId = reportTypeId, reportParameter = p });
        }
    }
}
public class Image64Response
{
    public string Image64 { get; set; }
}
