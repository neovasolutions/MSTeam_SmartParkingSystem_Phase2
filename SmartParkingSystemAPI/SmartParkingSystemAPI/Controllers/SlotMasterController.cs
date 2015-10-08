using SmartParkingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SmartParkingSystemAPI.Controllers
{
    public class SlotMasterController : ApiController
    {
        public List<SlotModel> GetSlots()
        {
            List<SlotModel> result = new List<SlotModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.SlotMasters.ToList();

                if (qry.Count() > 0)
                {
                    result = qry.Select(s => new SlotModel()
                    {
                        IsAcquired = s.IsAcquired,
                        IsActive = s.IsActive,
                        ParkingID = s.ParkingID,
                        SlotID = s.SlotID,
                        SlotNumber = s.SlotNumber
                    }).ToList();
                }
            }
            return result;
        }

        public List<SlotModel> GetSlots(int id)
        {
            List<SlotModel> result = new List<SlotModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.SlotMasters.Where(s => s.ParkingID == id).ToList();

                if (qry.Count() > 0)
                {
                    result = qry.Select(s => new SlotModel()
                    {
                        IsAcquired = s.IsAcquired,
                        IsActive = s.IsActive,
                        ParkingID = s.ParkingID,
                        SlotID = s.SlotID,
                        SlotNumber = s.SlotNumber
                    }).ToList();
                }
            }
            return result;
        }

        public SlotModel GetSlotByNumber(string inputId)
        {
            SlotModel result = new SlotModel();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.SlotMasters.Where(s => s.SlotNumber == inputId).SingleOrDefault();

                if (qry != null)
                {
                    result = ConvertDBToModelObject(qry);
                }
            }
            return result;
        }

        public SlotModel GetSlotByID(int inputId)
        {
            SlotModel result = new SlotModel();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.SlotMasters.Where(s => s.SlotID == inputId).SingleOrDefault();

                if (qry != null)
                {
                    result = ConvertDBToModelObject(qry);
                }
            }
            return result;
        }

        public bool PostSlotMaster(SlotModel slot)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                SlotMaster parlSlot = new SlotMaster()
                {
                    ParkingID = slot.ParkingID,
                    SlotNumber = slot.SlotNumber,
                    IsAcquired = slot.IsAcquired,
                    IsActive = slot.IsActive
                };

                context.SlotMasters.Add(parlSlot);
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool PostUpdateSlotMaster(SlotModel slot)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                SlotMaster updateSlot = context.SlotMasters.Where(s => s.ParkingID == slot.ParkingID && s.SlotID==slot.SlotID).SingleOrDefault();
                updateSlot.SlotNumber = slot.SlotNumber;
                updateSlot.IsActive = slot.IsActive;
                updateSlot.IsAcquired = slot.IsAcquired;

                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public string  PostParkUnPark(int userId, int parkingId, string slotNo, bool isAcquired)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                SlotMaster slot = context.SlotMasters.Where(s => s.ParkingID == parkingId && s.SlotNumber == slotNo).SingleOrDefault();
                slot.IsAcquired = isAcquired;

                if (isAcquired)
                {
                    ParkingSlotTransaction transaction = new ParkingSlotTransaction()
                    {
                        SlotID = slot.SlotID,
                        UserID = userId,
                        ParkingInTime = DateTime.Now,
                        VehicleNumber = null
                    };
                    context.ParkingSlotTransactions.Add(transaction);
                }
                else
                {
                    var getSolt = context.SlotMasters.Where(s => s.SlotNumber == slotNo && s.ParkingID == parkingId).SingleOrDefault();
                    if (getSolt != null)
                    {
                        ParkingSlotTransaction transaction = context.ParkingSlotTransactions.OrderByDescending(t => t.ParkingInTime).Where(t => t.SlotID == getSolt.SlotID && t.UserID == userId).FirstOrDefault();
                        transaction.ParkingOutTime = DateTime.Now;
                    }
                }

                context.SaveChanges();
                result = true;
            }
            //return result;
            return userId.ToString() + " " + parkingId.ToString() + " " + slotNo + " " + isAcquired.ToString();
        }

        public SlotModel ConvertDBToModelObject(SlotMaster slot)
        {
            return new SlotModel()
            {
                IsAcquired = slot.IsAcquired,
                IsActive = slot.IsActive,
                ParkingID = slot.ParkingID,
                SlotID = slot.SlotID,
                SlotNumber = slot.SlotNumber
            };
        }

    }
}
