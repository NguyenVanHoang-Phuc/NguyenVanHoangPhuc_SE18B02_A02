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
    public class BookingDetailDAO
    {
        private static BookingDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                    return instance;
                }
            }
        }
        public List<BookingDetail> GetBookingDetails()
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    return db.BookingDetail.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void CreateBookingDetail(BookingDetail b)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.BookingDetail.Add(b);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UpdateBookingDetail(BookingDetail b)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.Entry<BookingDetail>(b).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void DeleteBookingDetail(BookingDetail b)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    var bookingDetail = db.BookingDetail.FirstOrDefault(x => x.BookingReservationID == b.BookingReservationID && x.RoomID == b.RoomID);
                    if (bookingDetail != null)
                    {
                        db.BookingDetail.Remove(bookingDetail);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public BookingDetail GetBookingDetailById(int bookingReservationId, int roomId)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                return db.BookingDetail.FirstOrDefault(b => b.BookingReservationID == bookingReservationId && b.RoomID == roomId);
            }
        }
    }
}
        

