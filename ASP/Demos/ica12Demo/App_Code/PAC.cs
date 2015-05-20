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
/// </summary>
public static class PAC
{
  public static SqlDataSource GetTitles(string sFilter)
  {
    string sQuery = "select title_id, title from titles";
    sQuery += " where 1 = 1 ";
    //if( sFilter.Length != 0 ) // add "where ... "
    SqlDataSource sds = new SqlDataSource(
      ConfigurationManager.ConnectionStrings["csPubs"].ConnectionString,
      sQuery );
    return sds;
  }
  public static List<string> GetSalesForTitle( string sTitleID )
  {
    List<string> retList = new List<string>(); // return list
    // Make our query... maybe in sql enterprise...
    string sQuery = "select stor_id as 'Store ID', qty, ord_date";
    sQuery += " from sales s";
    sQuery += " where s.title_id like '" + sTitleID + "'"; // make quotes
    // Query Done, make connection
    using ( SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["csPubs"].ConnectionString))
    {
      using( SqlCommand command = new SqlCommand( sQuery, conn ))
      {
        conn.Open(); // Open, make active
        // Get data, close door, shut off lights when done
        SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        // Watcha Get ??
        //reader.FieldCount // property for # of columns/fields in result
        //reader.GetName(0) // Get column Name of 0 based index
        if (!reader.HasRows) // Got Milk ??
          return retList;
        while(reader.Read())
        { // reader, with valid read, acts as dictionary
          string tmp = "";
          tmp += reader["Store ID"] + " : "; // dictionary like access
          tmp += reader["qty"] + " => ";
          tmp += reader["ord_date"];
          retList.Add(tmp);
        } // until no more rows
      }
    }
    // Done all DB access, outside usings()
    return retList;
  }
}