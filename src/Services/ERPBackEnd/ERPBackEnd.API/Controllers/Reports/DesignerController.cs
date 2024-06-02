using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.API.Reports.BL;
using ResortAppStore.Services.ERPBackEnd.Application.Reports;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System.Collections;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    public class DesignerController : Controller
    { 
        private readonly IWebHostEnvironment env;
        private readonly IReportFileRepository reportFileRepos;
        
        public DesignerController(IWebHostEnvironment env, IReportFileRepository reportFileRepos)
        {
            this.env = env;
            
            this.reportFileRepos = reportFileRepos;
            StimulsoftFunctions.RegisterFunctions();

        }
        static string saveFlag = "";       
       
        

        // GET: Designer
        public ActionResult Reports(long id, string reportParameter)
        {
            return View();
        }
        public ActionResult GetReport(long id,  string reportParameter)
        {
            StiReport report = new StiReport();
            var reportFile = reportFileRepos.Find(id);


            //reportFile.FileName
            report.Load(env.ContentRootPath + @"\ReportFile\" + reportFile.FileName + ".mrt");

            return StiNetCoreDesigner.GetReportResult(this, report);
        }
        public ActionResult PreviewReport(long id,  string reportParameter)
        {
            string[] myReportParameter = reportParameter.Split('\n');
            var reportFile = reportFileRepos.Find(id);




            StiReport report = StiNetCoreDesigner.GetActionReportObject(this);
            string[] rptPrams;
            for (int i = 0; i < myReportParameter.Length - 1; i++)
            {
                rptPrams = myReportParameter[i].Split('!');
                report.Dictionary.Variables.Add(rptPrams[0], rptPrams[1]);
                report["@" + rptPrams[0]] = rptPrams[1];

                //report[rptPrams[0]] = rptPrams[1];                
                //if (i == 0 || i==1)                //{
                //    //اول 2 باراميتر خاص بادارة عرض التقارير
                //    //الاول يخص نوع التقرير (فاتورة - سند - تقرير) والثاني يخص نوع النمط مثلا في حال الفاتورة يكون نوع النمط مبيعات ام مشتريات الخ 
                //}
                //else
                //{

                //    report["@" + rptPrams[0]] = rptPrams[1];
                //}
            }
            //report.Load(env.ContentRootPath + @"\ReportFile\" + reportFile.FileName + ".mrt");



            //foreach (ReportParameters p in reportParameter)
            //{

            //  report["@" +p.ParamName] = p.ParamValue;
            //}
            //report.RegData(data);

            return StiNetCoreDesigner.PreviewReportResult(this, report);
        }
        public ActionResult DesignerEvent()
        {
            return StiNetCoreDesigner.DesignerEventResult(this);
        }


        public ActionResult SaveReport(long id)
        {
            //string[] myReportParameter;
            //try
            //{
            //    myReportParameter = reportParameter.Split('\n');
            //}
            //catch (Exception)
            //{
                
            //}
             
            StiReport report = StiNetCoreDesigner.GetReportObject(this);
            Hashtable parameters = StiNetCoreDesigner.GetRequestParams(this).All;
            //string fileName = Convert.ToString(parameters["reportFile"]);
              var reportdata = reportFileRepos.Find(id);
            if (saveFlag == "")
            {
                //Save Report by id ==> no save as 
                report.Save(env.ContentRootPath + @"\ReportFile\" + reportdata.FileName + ".mrt");
                return StiNetCoreDesigner.SaveReportResult(this);

            }
            else if (saveFlag == "OK")
            {
                //Save Report by id ==> save as 
                if (reportdata.FileName.Contains(".mrt"))
                {
                    reportdata.FileName = reportdata.FileName.Replace(".mrt", "");
                }
                report.Save(env.ContentRootPath + @"\ReportFile\" + reportdata.FileName + ".mrt");
                return StiNetCoreDesigner.SaveReportResult(this);
            }
            else
            {
                return StiNetCoreDesigner.SaveReportResult(this, "true");
            }

        }

        public ActionResult SaveAsReport(long id,int reportType, int reportTypeId, string reportParameter)
        {
            if (reportParameter == null) 
            {
                reportParameter = "";
            }
            string[] myReportParameter = reportParameter.Split('\n');
            StiReport report = StiNetCoreDesigner.GetReportObject(this);


            //string jsonData = ((string[])(HttpContext.))[0];
            Hashtable parameters = StiNetCoreDesigner.GetRequestParams(this).All;
            string viewName = Convert.ToString(parameters["reportFile"]);
            
            if (viewName.Contains(".mrt"))
            {
                saveFlag = "اسم الملف لا يجب ان يحتوي على الامتداد .mrt";
                return StiNetCoreDesigner.SaveReportResult(this, "اسم الملف لا يجب ان يحتوي على الامتداد .mrt");
            }

            ReportFile rpt = new ReportFile();
            rpt.ReportType = Convert.ToInt32(myReportParameter[0].Split('!')[1]);
            rpt.ReportTypeId = Convert.ToInt32(myReportParameter[1].Split('!')[1]);
            rpt.ReportNameAr = viewName + Guid.NewGuid().ToString();
            rpt.FileName = viewName + Guid.NewGuid().ToString();
            rpt.IsDefault = false;
            string res = reportFileRepos.AddReport(rpt);
            
            
            if (res == "Success")
            {
                report.Save(env.ContentRootPath + @"\ReportFile\" + rpt.FileName + ".mrt");

                saveFlag = "OK";
                return StiNetCoreDesigner.SaveReportResult(this);

            }
            else if (res == "FileExist")
            {
                saveFlag = "يوجد ملف بنفس الاسم";
                return StiNetCoreDesigner.SaveReportResult(this, saveFlag);
            }
            else
            {
                saveFlag = "حدث الخطأ التالي اثناء عمية الحفظ: " + res;
                return StiNetCoreDesigner.SaveReportResult(this, saveFlag);
            }
            //return StiMvcDesigner.SaveReportResult();
        }
    }
}

