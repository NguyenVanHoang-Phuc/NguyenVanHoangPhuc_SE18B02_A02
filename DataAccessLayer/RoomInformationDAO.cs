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
    public class RoomInformationDAO 
    {
        private static RoomInformationDAO instance = null;
        private static readonly object instanceLock = new object();
        public static RoomInformationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInformationDAO();
                    }
                    return instance;
                }
            }
        }
        public List<RoomInformation> GetAllRooms()
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    return db.RoomInformation.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void CreateRoom(RoomInformation room)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.RoomInformation.Add(room);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UpdateRoom(RoomInformation room)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    db.Entry<RoomInformation>(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void DeleteRoom(RoomInformation room)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                try
                {
                    var existingRoom = db.RoomInformation.FirstOrDefault(x => x.RoomID == room.RoomID);
                    if (existingRoom != null)
                    {
                        db.RoomInformation.Remove(existingRoom);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public RoomInformation GetRoomById(int roomID)
        {
            using (var db = new FUMiniHotelManagementDBContext())
            {
                return db.RoomInformation.FirstOrDefault(r => r.RoomID == roomID);
            }
        }
    }
}
