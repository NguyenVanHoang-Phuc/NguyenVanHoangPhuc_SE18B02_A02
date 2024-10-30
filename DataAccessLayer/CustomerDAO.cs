using BusinessOjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Customer> GetAllCustomers()
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    return db.Customer.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
       
        public void CreateCustomer(Customer customer)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.Customer.Add(customer);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    var existingCustomer = db.Customer.FirstOrDefault(x => x.CustomerID == customer.CustomerID);
                    if (existingCustomer != null)
                    {
                        db.Customer.Remove(existingCustomer);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Customer GetCustomerById(int customerID)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                return db.Customer.FirstOrDefault(c => c.CustomerID == customerID);
            }
        }

        public Customer GetCustomerByEmail(string email)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                return db.Customer.FirstOrDefault(c => c.EmailAddress == email);
            }
        }

    }
}
