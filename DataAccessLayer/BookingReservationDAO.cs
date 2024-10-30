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
    public class BookingReservationDAO
    {
        private static BookingReservationDAO instance = null;
        private static readonly object instanceLock = new object();
        public static BookingReservationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                    return instance;
                }
            }
        }

        public List<BookingReservation> GetAllBookingReservations()
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    return db.BookingReservation.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void CreateBookingReservation(BookingReservation b)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.BookingReservation.Add(b);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UpdateBookingReservation(BookingReservation b)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.Entry<BookingReservation>(b).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void DeleteBookingReservation(BookingReservation b)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    var reservation = db.BookingReservation.FirstOrDefault(x => x.BookingReservationID == b.BookingReservationID);
                    if (reservation != null)
                    {
                        db.BookingReservation.Remove(reservation);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public BookingReservation GetBookingReservationById(int bookingReservationID)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                return db.BookingReservation.FirstOrDefault(b => b.BookingReservationID == bookingReservationID);
            }
        }

        public List<BookingReservation> GetBookingReservationsByDateRange(DateOnly startDate, DateOnly endDate)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                return db.BookingReservation
                    .Where(r => r.BookingDate >= startDate && r.BookingDate <= endDate)
                    .ToList();
            }
        }
    }
}
