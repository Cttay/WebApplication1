using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.ProviderBase;

public static class SQLHelper
{
    public static DataSet ExecuteQuery(int UserId, string connString, string cmdText, bool Audit)
    {
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection conn = new SqlConnection(connString))
        {
            try
            {
                PrepareCommand(UserId, cmd, conn, CommandType.Text, cmdText, null,Audit);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                
                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
    }
    //public static DataSet ExecuteQuery(int UserId,string connString, string cmdText, SqlParameter[] cmdParms, bool Audit)
    //{
    //    SqlDataAdapter adp;
    //    DataSet ds = new DataSet();
    //    SqlCommand cmd = new SqlCommand();
        
    //    using (SqlConnection conn = new SqlConnection(connString))
    //    {
    //        try
    //        {
    //            PrepareCommand(UserId,cmd, conn, CommandType.StoredProcedure, cmdText, cmdParms,true);
      
    //            adp = new SqlDataAdapter(cmd);
    //            adp.Fill(ds);
    //            cmd.Parameters.Clear();
    //            return ds;
    //        }
    //        catch
    //        {
    //            conn.Close();
    //            throw;
    //        }
    //    }
    //}
    public static int ExecuteQueryReturnInt(int UserId, string connString, string cmdText, SqlParameter[] cmdParms,bool Audit)
    {
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        using (SqlConnection conn = new SqlConnection(connString))
        {
            try
            {
                //if (Audit)
                //{
                //    auditlog(UserId, connString, cmdText, cmdParms);
                //}

                PrepareCommand(UserId,cmd, conn, CommandType.StoredProcedure, cmdText, cmdParms,Audit);                
                adp = new SqlDataAdapter(cmd);                                
                adp.Fill(ds);
                //auditlog(UserId, connString, cmdText, cmdParms, Audit);
                
                cmd.Parameters.Clear();
                if (ds.Tables[0].Rows.Count == 0)
                    return 0;
                else
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
    }
    public static void PrepareCommand(int Userid, SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms, bool Audit)
    {

        if (conn.State != ConnectionState.Open)
            conn.Open();

        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        cmd.CommandTimeout = 36000;
        //if (trans != null)
        //    cmd.Transaction = trans;

        cmd.CommandType = cmdType;

        if (cmdParms != null)
        {
            foreach (SqlParameter parm in cmdParms)
                cmd.Parameters.Add(parm);
        }

    }
    public static void auditlog(int Userid, string constr, string cmdText, SqlParameter[] cmdParm)
    {
        try
        {
            if (cmdText[0].Equals('['))
            {
                cmdText = cmdText.Substring(1);
            }
            if (cmdText[cmdText.Length - 1].Equals(']'))
            {
                cmdText = cmdText.Substring(0, cmdText.Length - 1);
            }

            SqlParameter[] parmAudit ={
            new SqlParameter("@SYSTEMUSERID",Userid),
            new SqlParameter("@ACTIONNAME",cmdText)
        };
            int auditlogid = SQLHelper.ExecuteQueryReturnInt(0, constr, "[AUD_AUDITTRIAL_ADDAUDITTRIAL]", parmAudit, false);
            for (int a = 0; a < cmdParm.Length; a++)
            {
                SqlParameter[] parmAuditData ={
            new SqlParameter("@AUDITTRIALID",auditlogid),
            new SqlParameter("@PARAMNAME",cmdParm[a].ParameterName),
            new SqlParameter("@PARAM",cmdParm[a].Value)
            };
                SQLHelper.ExecuteQueryReturnInt(0, constr, "[AUD_AUDITTRIALPARAM_ADDAUDITTRIALPARAM]", parmAuditData, false);
            }
        }
        catch { }
    }
}

