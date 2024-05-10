using BMS_Scheduler.Administration.Entities;
using Microsoft.Data.SqlClient;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Common
{
    public interface ICommonSPCall
    {
        ICommonSPCall SetParameters(SqlParameter[] paremeters);
        ICommonSPCall SetProcedureName(string spName);

        DataTable GetDataTable();
    }

    public class CommonSPCall: ICommonSPCall
    {
        private String constr;
        private SqlParameter[] parameters = null;
        private String spName = String.Empty;
        protected ISqlConnections SqlConnections { get; }

        #region Ctor
        public CommonSPCall(ISqlConnections sqlConnections)
        {
            SqlConnections = sqlConnections ??
               throw new ArgumentNullException(nameof(sqlConnections));

            using (var con = SqlConnections.NewFor<RoleRow>())
            {
                this.constr = con.ConnectionString;
            }
        }
        #endregion


        public ICommonSPCall SetParameters(SqlParameter[] paremeters)
        {
            this.parameters = paremeters;
            return this;
        }

        public ICommonSPCall SetProcedureName(string spName)
        {
            this.spName = (spName ?? throw new ArgumentNullException(nameof(spName)));
            return this;
        }

        public DataTable GetDataTable()
        {
            var dsResult = new DataTable();
            try
            {
                using (var connection = new SqlConnection(constr))
                {
                    var command = new SqlCommand(this.spName, connection);
                    command.CommandTimeout = 0;

                    if (this.parameters == null)
                        command.CommandType = CommandType.Text;
                    else
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var item in this.parameters)
                        {
                            command.Parameters.Add(item);
                        }
                    }

                    connection.Open();

                    foreach (SqlParameter sp in command.Parameters)
                    {
                        if (sp.Direction != ParameterDirection.Output)
                        {
                            if (sp.Value == null)
                            {
                                sp.Value = DBNull.Value;
                            }
                            else
                            {
                                if (sp.Value.ToString() == DateTime.MinValue.ToString())
                                    sp.Value = DBNull.Value;
                            }
                        }
                    }

                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(dsResult);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dsResult;
        }
        public static DataTable ToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

    }
}