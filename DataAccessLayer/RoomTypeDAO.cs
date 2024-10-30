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
    public class RoomTypeDAO 
    {
        private static RoomTypeDAO instance = null;
        private static readonly object instanceLock = new object();
        public static RoomTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }
        }
        public List<RoomType> GetAllRoomTypes()
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    return db.RoomType.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi lấy danh sách loại phòng: " + ex.Message);
                }
            }
        }

        public void CreateRoomType(RoomType roomType)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.RoomType.Add(roomType);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tạo loại phòng: " + ex.Message);
                }
            }
        }

        public void UpdateRoomType(RoomType roomType)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.Entry<RoomType>(roomType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật loại phòng: " + ex.Message);
                }
            }
        }

        public void DeleteRoomType(RoomType roomType)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    var existingRoomType = db.RoomType.FirstOrDefault(x => x.RoomTypeID == roomType.RoomTypeID);
                    if (existingRoomType != null)
                    {
                        db.RoomType.Remove(existingRoomType);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi xóa loại phòng: " + ex.Message);
                }
            }
        }

        public RoomType GetRoomTypeById(int roomTypeID)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                return db.RoomType.FirstOrDefault(r => r.RoomTypeID == roomTypeID);
            }
        }

    }
}
