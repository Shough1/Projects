using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient; // SQL Data
using System.Web.UI.WebControls; // ASP Controls
using System.Configuration; // WebConfig access

/// <summary>
/// Summary description for PAC
///
/// </summary>
public static class PAC
{
  // sFilter will allow restrictions on title names
  public static SqlDataReader FillListBox(string sFilter)
  {
    SqlDataReader reader = null;
    SqlConnection connection =
      new SqlConnection(ConfigurationManager.ConnectionStrings["herbv_test_pubsCS"].ConnectionString);
    connection.Open();
    using (SqlCommand command = new SqlCommand())
    {
      command.Connection = connection;
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "spPubsTitles"; // Stored Proc name
      SqlParameter pFilter = new SqlParameter("@filter", SqlDbType.VarChar, 24);
      pFilter.Value = sFilter;
      pFilter.Direction = ParameterDirection.Input;
      // DONT FOGET TO ADD THE PARAMETER!!!!!!!!!!
      command.Parameters.Add(pFilter);
      reader = command.ExecuteReader(CommandBehavior.CloseConnection);
    }
    return reader;
  }
  // Retrieve summary rollup of titles by store sales
  public static SqlDataReader GetSummary(string sTitleId, out int iRows)
  {
    SqlDataReader reader = null;
    SqlConnection connection =
      new SqlConnection(ConfigurationManager.ConnectionStrings["herbv_test_pubsCS"].ConnectionString);
    connection.Open();
    using (SqlCommand command = new SqlCommand())
    {
      command.Connection = connection;
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "spSalesSummaryByTitle"; // Stored Proc name

      SqlParameter pTitleID = new SqlParameter("@title_id", SqlDbType.VarChar, 6);
      pTitleID.Value = sTitleId;
      pTitleID.Direction = ParameterDirection.Input;
      // DONT FOGET TO ADD THE PARAMETER!!!!!!!!!!
      command.Parameters.Add(pTitleID);

      // This is how it would work, BUT we can't use out/return parameters here, since
      // we pass off the reader object. Parameters do not complete and populate with their output/return
      // values until the connection is closed, in our case this won't occur until the reader has 
      // been consumed by the gridview... so not this time, but this is how it works.
      SqlParameter pReturn = new SqlParameter("@Wictor", SqlDbType.Int);
      pReturn.Direction = ParameterDirection.ReturnValue;
      command.Parameters.Add(pReturn);

      reader = command.ExecuteReader(CommandBehavior.CloseConnection);
      iRows = 0;
      //iRows = (int)pReturn.Value; // if connection could be closed.
    }
    return reader;
  }
  // update stored procedure
  /// <summary>
  /// OnSale - update by howMuch all title types that match sType, return string and int status
  /// </summary>
  /// <param name="sType">what type of title to adjust</param>
  /// <param name="howMuch">fixed increment/decrement amount to adjust Price</param>
  /// <param name="sStatus">string message as to success</param>
  /// <param name="iReturn">@@ROWCOUNT of stored proc</param>
  public static void OnSale(string sType, decimal howMuch, out string sStatus, out int iReturn)
  {
    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["herbv_test_pubsCS"].ConnectionString))
    {
      connection.Open();
      using (SqlCommand command = new SqlCommand())
      {
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "spUpdateForSale"; // Stored Proc name

        SqlParameter pHowMuch = new SqlParameter("@howMuch", SqlDbType.Money);
        pHowMuch.Value = howMuch;
        pHowMuch.Direction = ParameterDirection.Input;
        command.Parameters.Add(pHowMuch);

        SqlParameter ptitleType = new SqlParameter("@titleType", SqlDbType.Char, 12);
        ptitleType.Value = sType;
        ptitleType.Direction = ParameterDirection.Input;
        command.Parameters.Add(ptitleType);

        SqlParameter pStatus = new SqlParameter("@status", SqlDbType.VarChar, 64);
        //pStatus.Value = sStatus; Its output !
        pStatus.Direction = ParameterDirection.Output; // OUTPUT
        command.Parameters.Add(pStatus);

        SqlParameter pReturn = new SqlParameter("@KelseyReturns", SqlDbType.Int);
        pReturn.Direction = ParameterDirection.ReturnValue; // RETURN VALUE  !!!!
        command.Parameters.Add(pReturn);

        int iUselessValue = command.ExecuteNonQuery(); // no args
        // All Input/OUtput, Output and Return values are now updated with stored proc data
        // our job to retrieve the values back out... all must be cast - Pick appropriately

        sStatus = (string)pStatus.Value;
        iReturn = (int)pReturn.Value;
      }
    }
  }
}