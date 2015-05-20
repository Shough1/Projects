using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for PAC
/// </summary>
public static class NorthwindAccess
{
    public static SqlDataSource GetSuppliersSDS(string sFilter)
    {

        string sQuery = "select CompanyName, SupplierID from Suppliers";
        if (sFilter != "")
        {
            sQuery += " Where CompanyName like \'%" + sFilter + "%\'";
        }
        SqlDataSource sds = new SqlDataSource(
            ConfigurationManager.ConnectionStrings["shough1_NorthwindConnectionString"].ConnectionString, sQuery);

        return sds;
    }

    public static List<List<string>> GetProducts(string sSupplierID)
    {
        List<List<string>> productList = new List<List<string>>();
        List<string> productHeader = new List<string>();
        List<string> productInfo = new List<string>();
        string sQuery = "select ProductName, QuantityPerUnit, UnitsInStock from Products";
        sQuery += " where SupplierID = " + sSupplierID+" Order by ProductName ASC";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shough1_NorthwindConnectionString"].ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(sQuery, conn))
            {
                conn.Open();
                SqlDataReader sdsReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (!sdsReader.HasRows)
                    return productList;
                for(int i = 0; i < sdsReader.FieldCount; i++)
                {
                   productHeader.Add(sdsReader.GetName(i));
                }
                productList.Add(productHeader);
                while(sdsReader.Read())
                {
                    productInfo.Add(sdsReader["ProductName"].ToString());
                    productInfo.Add(sdsReader["QuantityPerUnit"].ToString());
                    productInfo.Add(sdsReader["UnitsInStock"].ToString());
                }
                productList.Add(productInfo);
            }
        }
        return productList;
    }

    public static  SqlDataReader SQLDataReader(string sCompanyName)
    {
        SqlDataReader sdr = null;
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["shough1_NorthwindConnectionString"].ConnectionString);
        sqlConn.Open();
       
        using (SqlCommand sqlComm = new SqlCommand())
        {
            sqlComm.Connection = sqlConn;
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.CommandText = "GetCustomers";
            SqlParameter pCustomer = new SqlParameter("@companyName", SqlDbType.VarChar, 25);
            pCustomer.Value = sCompanyName;
            pCustomer.Direction = ParameterDirection.Input;
            sqlComm.Parameters.Add(pCustomer);

            //SqlParameter pReturn = new SqlParameter("@companyReturn", SqlDbType.VarChar);
            //pReturn.Direction = ParameterDirection.ReturnValue;
            //sqlComm.Parameters.Add(pReturn);

            sdr = sqlComm.ExecuteReader(CommandBehavior.CloseConnection);
           
        }
        return sdr;
    }

    public static SqlDataReader CustomerCategorySummary(string sCompanyID)
    {
        SqlDataReader sdr = null;
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["shough1_NorthwindConnectionString"].ConnectionString);
        sqlConn.Open();

        using (SqlCommand sqlComm = new SqlCommand())
        {
            sqlComm.Connection = sqlConn;
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.CommandText = "CustCatSummary";
            SqlParameter pCustomerID = new SqlParameter("@customerID", SqlDbType.NChar, 5);
            pCustomerID.Value = sCompanyID;
            pCustomerID.Direction = ParameterDirection.Input;
            sqlComm.Parameters.Add(pCustomerID);

            //SqlParameter pReturn = new SqlParameter("@DataReturn", SqlDbType.VarChar);
            //pReturn.Direction = ParameterDirection.ReturnValue;
            //sqlComm.Parameters.Add(pReturn);

            sdr = sqlComm.ExecuteReader(CommandBehavior.CloseConnection);

        }
        return sdr;
    }

    public static string DeleteOrderDetails(int orderID, int prodID)
    {
        string messageOutput;
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shough1_NorthwindConnectionString"].ConnectionString))
        { 
            conn.Open();
            using (SqlCommand sqlComm = new SqlCommand())
            {

                sqlComm.Connection = conn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "DeleteOrderDetails";

                SqlParameter pOrderID = new SqlParameter("@orderID", SqlDbType.Int);
                pOrderID.Value = orderID;
                pOrderID.Direction = ParameterDirection.Input;
                sqlComm.Parameters.Add(pOrderID);

                SqlParameter pProductID = new SqlParameter("@productID", SqlDbType.Int);
                pProductID.Value = prodID;
                pProductID.Direction = ParameterDirection.Input;
                sqlComm.Parameters.Add(pProductID);

                SqlParameter pMessageID = new SqlParameter("@message", SqlDbType.VarChar, 80);

                pMessageID.Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(pMessageID);

                int notNed = sqlComm.ExecuteNonQuery();
                messageOutput = (string)pMessageID.Value;

            }
        }
        return messageOutput;
    }
    public static string InsertOrderDetails(int iOrderID, int iProductID, short sQuantity)
    {
        string returnMessage = "";
        int iReturnValue;
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shough1_NorthwindConnectionString"].ConnectionString))
        {
            conn.Open();
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = conn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "InsertOrderDetails";

                SqlParameter pOrderID = new SqlParameter("@orderID", SqlDbType.Int);
                pOrderID.Value = iOrderID;
                pOrderID.Direction = ParameterDirection.Input;
                sqlComm.Parameters.Add(pOrderID);

                SqlParameter pProductID = new SqlParameter("@productID", SqlDbType.Int);
                pProductID.Value = iProductID;
                pProductID.Direction = ParameterDirection.Input;
                sqlComm.Parameters.Add(pProductID);

                SqlParameter pQuantity = new SqlParameter("@qnty", SqlDbType.SmallInt);
                pQuantity.Value = sQuantity;
                pQuantity.Direction = ParameterDirection.Input;
                sqlComm.Parameters.Add(pQuantity);

                SqlParameter pReturnValue = new SqlParameter("@Return", SqlDbType.Int);
                pReturnValue.Direction = ParameterDirection.ReturnValue;
                sqlComm.Parameters.Add(pReturnValue);

                int yolo = sqlComm.ExecuteNonQuery();
                iReturnValue = (int)pReturnValue.Value;
                if(iReturnValue == -1)
                {
                    returnMessage = "Status: Error: ProductID does not exist";
                }
                else if(iReturnValue == -2)
                {
                    returnMessage = "Status: Error: OrderID does not exist";
                }
                else if(iReturnValue == -3)
                {
                    returnMessage = "Status: Error: Order Detail already exists";
                }
                else
                {
                    returnMessage = "Status: Inserted: " + iReturnValue + " rows";
                }

            }
        }
        return returnMessage;
    }
}