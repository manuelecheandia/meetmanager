﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using MeetManager.Types;

namespace DAL
{
    public class DataAccess
    {
        #region Fields

        private readonly string? connectionString;

        #endregion

        #region Constructors

        public DataAccess()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            connectionString = builder.Build().GetConnectionString("MeetMgrConnStr");
        }
        #endregion

        #region Public Methods

        public DataTable Execute(string cmdText, List<Parm>? parms = null, CommandType cmdType = CommandType.StoredProcedure)
        {
            DataTable dt = new();
            SqlCommand cmd = CreateCommand(cmdText, parms, cmdType);

            using (cmd.Connection)
            {
                SqlDataAdapter da = new(cmd);

                da.Fill(dt);
            }

            return dt;
        }

        public object ExecuteScalar(string cmdText, List<Parm>? parms = null, CommandType cmdType = CommandType.StoredProcedure)
        {
            object retVal;
            SqlCommand cmd = CreateCommand(cmdText, parms, cmdType);

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                retVal = cmd.ExecuteScalar();
            }

            return retVal;
        }

        public int ExcecuteNonQuery(string cmdText, List<Parm>? parms = null, CommandType cmdType = CommandType.StoredProcedure)
        {
            int rowsAffected = 0;
            SqlCommand cmd = CreateCommand(cmdText, parms, cmdType);

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();

                UnloadParms(parms, cmd);
            }

            return rowsAffected;
        }

        #endregion

        #region Private Methods

        private SqlCommand CreateCommand(string cmdText, List<Parm>? parms, CommandType cmdType)
        {
            SqlConnection conn = new(connectionString);

            SqlCommand cmd = new(SQLCleaner(cmdText), conn) { CommandType = cmdType };

            if (parms != null)
                foreach(Parm p in parms)
                {
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = p.Name,
                        SqlDbType = p.DataType,
                        Size = p.Size,
                        Value = p.Value,
                        Direction = p.Direction
                    });
                }

            return cmd;
        }

        private void UnloadParms(List<Parm>? parms, SqlCommand cmd)
        {
            if (parms is null) return;

            for(int i = 0; i < parms.Count; i++)
            {
                parms[i].Value = cmd.Parameters[i].Value;
            }
        }

        private string SQLCleaner(string sql)
        {
            while (sql.Contains("  "))
                sql = sql.Replace("  ", " ");

            return sql.Replace(Environment.NewLine, "");
        }

        #endregion
    }
}