using CRUDRestApi.Data_Access;
using CRUDRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUDRestApi.Controllers
{
    public class CustomerApiController : ApiController
    {
      
            CustomerRepository customerRepository = new CustomerRepository();

            // GET: api/Customer
            public HttpResponseMessage Get()
            {
                try
                {
                    var customerList = customerRepository.GetAllCustomers();
                    return Request.CreateResponse(HttpStatusCode.OK, customerList);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            // GET: api/Customer/5
            public HttpResponseMessage Get(int id)
            {
                try
                {
                    var customer = customerRepository.GetCustomersByID(id).FirstOrDefault();
                    if (customer == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, customer);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            // POST: api/Customer
            public HttpResponseMessage Post([FromBody] CustomerModel customerModel)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        bool IsInserted = customerRepository.InsertCustomerData(customerModel);
                        if (IsInserted)
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, "Customer created successfully");
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to save customer");
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            // PUT: api/Customer/5
            public HttpResponseMessage Put(int id, [FromBody] CustomerModel customerModel)
            {
                try
                {
                    customerModel.CustomerID = id; // Ensure ID is set from URL
                    bool IsUpdated = customerRepository.UpdateCustomerData(customerModel);
                    if (IsUpdated)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Customer updated successfully");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Customer not found or unable to update");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            // DELETE: api/Customer/5
            public HttpResponseMessage Delete(int id)
            {
                try
                {
                    string result = customerRepository.DeleteCustomer(id);
                    if (result.Contains("deleted"))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Customer deleted successfully");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Customer deleted successfully");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }
    }

