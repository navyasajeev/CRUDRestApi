using CRUDRestApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUDRestApi.Data_Access
{
    public class CustomerRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
        //Get all customer details
        public List<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> customerList = new List<CustomerModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "SP_GetCustomerDetails";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
                customerList = (from DataRow dr in dataTable.Rows

                                select new CustomerModel()
                                {
                                    CustomerID = Convert.ToInt32(dr["CustomerID"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    Address = Convert.ToString(dr["Address"]),
                                    City = Convert.ToString(dr["City"]),
                                    State = Convert.ToString(dr["State"]),
                                }).ToList();
            }
            return customerList;
        }
        public bool InsertCustomerData(CustomerModel customer)

        {
            int id = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_AddCustomerDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", customer.Name);
                sqlCommand.Parameters.AddWithValue("@Address", customer.Address);
                sqlCommand.Parameters.AddWithValue("@City", customer.City);
                sqlCommand.Parameters.AddWithValue("@State", customer.State);
                sqlConnection.Open();
                id = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            if (id > 0)
            {

                return true;

            }
            else
            {

                return false;
            }

        }

        //Get customer details by CustomerId
        public List<CustomerModel> GetCustomersByID(int CustomerID)
        {
            List<CustomerModel> customerList = new List<CustomerModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "SP_GetProductByID";
                sqlCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
                customerList = (from DataRow dr in dataTable.Rows

                                select new CustomerModel()
                                {
                                    CustomerID = Convert.ToInt32(dr["CustomerID"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    Address = Convert.ToString(dr["Address"]),
                                    City = Convert.ToString(dr["City"]),
                                    State = Convert.ToString(dr["State"]),
                                }).ToList();
            }
            return customerList;
        }
        //Update Customer
        public bool UpdateCustomerData(CustomerModel customerModel)
        {
            int i = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_UpdateCustomerDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CustomerID", customerModel.CustomerID);
                sqlCommand.Parameters.AddWithValue("@Name", customerModel.Name);
                sqlCommand.Parameters.AddWithValue("@Address", customerModel.Address);
                sqlCommand.Parameters.AddWithValue("@City", customerModel.City);
                sqlCommand.Parameters.AddWithValue("@State", customerModel.State);
                sqlConnection.Open();
                i = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            if (i > 0)
            {

                return true;

            }
            else
            {

                return false;
            }

        }
        //Delete Customer details
        public string DeleteCustomer(int CustomerID)
        {
            string result = "";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_DeleteCustomerDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                result = sqlCommand.Parameters.ToString();
                sqlConnection.Close();
            }
            return result;
        }
    }
}