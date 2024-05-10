using Serenity.Data;
using System;
using System.Data;
using System.Linq;

namespace BMS_Scheduler.Web.Modules.Common.Helpers
{
    public static class GetIncrimentalSerialNumber
    {
        public static string GenerateIncrimentalSerialNumber(SqlQuery sql, string preFix, IDbConnection Connection, string delimeter = "-", int length = 4)
        {
            int newSerialNumber = 0;
            var lastUsedNumber = Connection.Query<string>(sql, commandType: CommandType.Text).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(lastUsedNumber))
            {
                if (!string.IsNullOrWhiteSpace(delimeter) && lastUsedNumber.Split(delimeter).Length > 1)
                    int.TryParse(lastUsedNumber.Split(delimeter)[1], out newSerialNumber);
                else
                    int.TryParse(lastUsedNumber.Substring(preFix.Length, lastUsedNumber.Length - preFix.Length), out newSerialNumber);
            }
            return $@"{preFix}{delimeter}{(Math.Abs(newSerialNumber) + 1).ToString().PadLeft(length, '0')}";
        }


        public static string GenerateIncrimentalSerialNumber(string sql, string preFix, IDbConnection Connection, string delimeter = "-", int length = 4)
        {
            int newSerialNumber = 0;
            var lastUsedNumber = Connection.Query<string>(sql, commandType: CommandType.Text).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(lastUsedNumber))
            {
                if (!string.IsNullOrWhiteSpace(delimeter) && lastUsedNumber.Split(delimeter).Length > 1)
                    int.TryParse(lastUsedNumber.Split(delimeter)[1], out newSerialNumber);
                else
                    int.TryParse(lastUsedNumber.Substring(preFix.Length, lastUsedNumber.Length - preFix.Length), out newSerialNumber);
            }
            return $@"{preFix}{delimeter}{(Math.Abs(newSerialNumber) + 1).ToString().PadLeft(length, '0')}";
        }
    }
}
