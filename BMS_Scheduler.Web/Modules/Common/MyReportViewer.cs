
using BMS_Scheduler;
using BMS_Scheduler.Common;
//using BMS_Scheduler.Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace Common
{
    public interface IMyReportViewer
    {
        IMyReportViewer SetReportPath(string reportPath);
        IMyReportViewer SetDataSource(ListDictionary dataSources);
        IMyReportViewer SetParameters(Dictionary<string, string> paremeters);
        IMyReportViewer ExportToExcel(Boolean exportToExcel);
        IMyReportViewer SetFileDownloadName(String fileDownloadName);
        IMyReportViewer SetSubReports(List<String> subReports);
        FileContentResult OpenReport();
    }


    public class MyReportViewer : PageModel, IMyReportViewer
    {
        private IWebHostEnvironment _host;
        private string _reportPath = string.Empty;
        private ListDictionary _dataSource = null;
        private Dictionary<string, string> _parameters = null;
        private bool _exportToExcel = false;
        private string _fileDownloadName = string.Empty;
        private List<String> _subReports = null;
        private bool _downloadPdf = false;

        public MyReportViewer(IWebHostEnvironment host, bool downloadPdf = false)
        {
            this._host = (host ?? throw new ArgumentNullException(nameof(host)));
            _downloadPdf = downloadPdf;
        }


        public IMyReportViewer SetReportPath(string reportPath)
        {
            this._reportPath = (reportPath ?? throw new ArgumentNullException(nameof(reportPath)));
            return this;
        }

        public IMyReportViewer SetDataSource(ListDictionary dataSources)
        {
            this._dataSource = (dataSources ?? throw new ArgumentNullException(nameof(dataSources)));
            return this;
        }

        public IMyReportViewer SetParameters(Dictionary<string, string> paremeters)
        {
            this._parameters = (paremeters ?? throw new ArgumentNullException(nameof(paremeters)));
            return this;
        }

        public IMyReportViewer SetFileDownloadName(string fileDownloadName)
        {
            this._fileDownloadName = (fileDownloadName ?? throw new ArgumentNullException(nameof(fileDownloadName)));
            return this;
        }

        public IMyReportViewer ExportToExcel(bool exportToExcel)
        {
            this._exportToExcel = exportToExcel;
            return this;
        }

        public IMyReportViewer SetSubReports(List<string> subReports)
        {
            this._subReports = subReports;
            return this;
        }


        public FileContentResult OpenReport()
        {
            if (this._host == null) throw new ArgumentNullException(nameof(this._host));
            if (string.IsNullOrEmpty(this._reportPath)) throw new ArgumentNullException(nameof(this._reportPath));

            var reportPath = $"{this._host.ApplicationName}.Modules.Reports.{this._reportPath.Replace("/", ".")}";
            using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream(reportPath);

            var report = new LocalReport();
            report.LoadReportDefinition(rs);

           

            if (this._subReports != null && this._subReports.Count > 0)
            {
                foreach (var subReport in _subReports)
                {
                    var subreportPath = $"{ this._host.ApplicationName }.Modules.Reports.ReportHeaderRdlcFiles.{ subReport.Replace("/", ".") }.rdlc";
                    using var subReportStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(subreportPath);

                    report.LoadSubreportDefinition(subReport, subReportStream);
                }
                report.SubreportProcessing += Report_SubreportProcessing;
            }

            if (this._dataSource != null && this._dataSource.Count > 0)
            {
                foreach (DictionaryEntry ds in this._dataSource)
                {
                    report.DataSources.Add(new ReportDataSource(ds.Key.ToString(), ds.Value));
                }
            }

            if (this._parameters != null && this._parameters.Count > 0)
            {
               var parameters = new List<ReportParameter>();

                foreach (var item in this._parameters)
                {
                    parameters.Add(new ReportParameter(item.Key.ToString(), item.Value.ToString()));
                }

                report.SetParameters(parameters);
            }

            //Get TVChannel Information
            //var TVChannelInfo = OrganizationInfoUtils.GetOrganizationInfornation();
            //report.DataSources.Add(new ReportDataSource("BMSReportHeaderDataSet", TVChannelInfo));

            byte[] result;
            string mimeType = String.Empty;

            if (this._exportToExcel)
            {
                mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                result = report.Render("EXCELOPENXML");
                if (!string.IsNullOrEmpty(this._fileDownloadName))
                {
                    this._fileDownloadName = this._fileDownloadName + ".xlsx";
                }
                else
                {
                    this._fileDownloadName = "Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".xlsx";
                }
            }
            else
            {
                mimeType = "application/pdf";
                result = report.Render("PDF");
                if (!string.IsNullOrEmpty(this._fileDownloadName))
                {
                    this._fileDownloadName = "";
                }
            }

            FileContentResult fileContentResult;
                
             if(string.IsNullOrEmpty(this._fileDownloadName) && _downloadPdf == false)
                fileContentResult = File(result, mimeType);
             else 
                fileContentResult = File(result, mimeType,this._fileDownloadName);

            return fileContentResult;
        }



        private void Report_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                var dsName = string.Empty;

                switch (e.ReportPath)
                {
                    case "_LandscapeReportHeader":

                        //var TVChannelInfo =  OrganizationInfoUtils.GetOrganizationInfornation();

                        //dsName = "BMSReportHeaderDataSet";
                        //e.DataSources.Add(new ReportDataSource(dsName, TVChannelInfo));

                        break;
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message, ex.InnerException); }
        }


    }
}
