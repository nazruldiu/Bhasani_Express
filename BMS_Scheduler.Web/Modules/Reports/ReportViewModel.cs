using Microsoft.AspNetCore.Mvc.Rendering;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace BMS_Scheduler.Reports
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            //String strSql = String.Empty;

            //strSql = $@"SELECT TOP 1 SalaryMonth, SalaryYear 
            //            FROM PGM_SalaryMaster 
            //            ORDER BY CAST(SalaryYear+SalaryMonth as DATETIME) DESC";

            //using (var connection = SqlConnections.NewFor<SalaryMasterRow>())
            //{
            //    var yearMonth = connection.Query<SalaryMasterRow>(strSql).FirstOrDefault();
            //    if (yearMonth != null)
            //    {
            //        this.Year = yearMonth.SalaryYear;
            //        this.Month = yearMonth.SalaryMonth;
            //}
        }

        //Employee Type used as Job Status

        [Display(Name = "Employee Type")]
        public Int32 EmployeeTypeId { get; set; }


        [Display(Name = "Employee Type")]
        public Int32 EmployeeTypeIdAll { get; set; }

        public String ReportTitle { get; set; }

        #region Other Parameters

        [DisplayName("Include ex-employee")]
        public Boolean IncludeExEmployee { get; set; }

        [DisplayName("Show summary report")]
        public Boolean ShowSummaryReport { get; set; }

        [DisplayName("Show withheld employee register")]
        public Boolean ShowWithheldEmployeeRegister { get; set; }

        public String PayrollSummaryType { get; set; }

        /// <summary>
        /// Multiple report launch in one UI then it use as switch
        /// </summary>
        public String SelectedReport { get; set; }

        #endregion

        public class JsTreeModel
        {
            public string id { get; set; }
            public string parent { get; set; }
            public string text { get; set; }
            public string icon { get; set; }
            public string state { get; set; }
            public bool opened { get; set; }
            public bool disabled { get; set; }
            public bool selected { get; set; }
            public string li_attr { get; set; }
            public string a_attr { get; set; }
            public string type { get; set; }
        }

        public class OfficeModel
        {
            public Int64? Id { get; set; }
            public Int64? ParentId { get; set; }
            public String Name { get; set; }
            public String OrganogramType { get; set; }
            public String Code { get; set; }
        }

    }
}


