using Microsoft.AspNetCore.Hosting;
using System;
using System.Data;

using Serenity;
using Serenity.Data;
using BMS_Scheduler.Administration.Entities;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Net.Http;
using Serenity.Services;
using BMS_Scheduler.Web.Modules.Common;
using BMS_Scheduler.Common;

namespace BMS_Scheduler
{

    public static class my
    {
        public static String GetContentRootPath()
        {
            string baseDirectory = String.Empty;
            //var hostingEnvironment = Dependency.TryResolve<IWebHostEnvironment>();
            //if (hostingEnvironment != null)
            //    baseDirectory = hostingEnvironment.ContentRootPath;
            //else
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return baseDirectory;
        }

        public static ImageResult GetImage64(String imageName)
        {
            ImageResult img = new ImageResult();

            if (!String.IsNullOrEmpty(imageName))
            {
                var strSMP = "";//SSO.HrisURL + "upload/" + imageName;
                var imagePath = new Uri(strSMP).AbsoluteUri;

                if (!String.IsNullOrEmpty(imagePath))
                {
                    using (var webClient = new HttpClient())
                    {
                        try
                        {
                            img.Image64Byte = webClient.GetByteArrayAsync(imagePath).Result;

                            string base64String = Convert.ToBase64String(img.Image64Byte, 0, img.Image64Byte.Length);
                            img.ImageBase64 = "data:image/png;base64," + base64String;
                        }
                        catch (Exception) { }
                    }
                }
            }

            return img;
        }

        public static int TVChannelId(IRequestContext context)
        {
            return Convert.ToInt32(IdentityHelper.GetClaimTypeByName(IdentityClaimType.TvChannelId.ToDescriptionString(), context));
        }
    }



    public class my1
    {
        protected ISqlConnections SqlConnections { get; }
        protected IWebHostEnvironment HostEnvironment { get; }

        public my1(ISqlConnections sqlConnections, IWebHostEnvironment hostEnvironment)
        {
            SqlConnections = sqlConnections ??
                throw new ArgumentNullException(nameof(sqlConnections));
            HostEnvironment = hostEnvironment ??
                throw new ArgumentNullException(nameof(hostEnvironment));
        }



        public String GetContentRootPath(IWebHostEnvironment hostEnvironment)
        {
            string baseDirectory;
            
            if (this.HostEnvironment != null)
                baseDirectory = hostEnvironment.ContentRootPath;
            else
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return baseDirectory;
        }


        public OrganizationInfoViewModel _organizationInfoModel { get; set; }

        public DataTable GetOrganizationInfornation()
        {

            var constr = "";
            using (var connection = SqlConnections.NewFor<RoleRow>())
            {
                constr = connection.ConnectionString;
            }

            var con = new SqlConnection(constr);

            var param = new Dapper.DynamicParameters();
            con.Open();
            var data = con.Query<OrganizationInfoViewModel>(
                "HR_SP_GetReportHeaderOfCareBangladesh"
                , param
                , commandTimeout: 0
                , commandType: CommandType.StoredProcedure).ToList();
            con.Close();

            if (data != null && data.Count() > 0 && data.FirstOrDefault().Logo != null)
            {
                var imgName = data.FirstOrDefault().Logo;

                ImageResult img = my.GetImage64(imgName);
                if (img != null)
                {
                    data.FirstOrDefault().Image64Byte = img.Image64Byte;
                    data.FirstOrDefault().Logo = img.ImageBase64;
                }
            }

            _organizationInfoModel = data.FirstOrDefault();

            var obj = new ObjToDT();
            return obj.objToDataTable(_organizationInfoModel);
        }
    }





    public class ObjToDT
    {
        public DataTable objToDataTable(OrganizationInfoViewModel obj)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Address", typeof(String));
            dt.Columns.Add("Phone", typeof(String));
            dt.Columns.Add("Image64Byte", typeof(byte[]));

            DataRow dr = dt.NewRow();
            dr["Name"] = obj.Name;
            dr["Address"] = obj.Address;
            dr["Phone"] = obj.Phone;
            dr["Image64Byte"] = obj.Image64Byte;

            dt.Rows.Add(dr);

            return dt;
        }
    }

    public class ImageResult
    {
        public byte[] Image64Byte { get; set; }
        public String ImageBase64 { get; set; }
    }

    public class OrganizationInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public String Logo { get; set; }

        public byte[] Image64Byte { get; set; }
    }




}
