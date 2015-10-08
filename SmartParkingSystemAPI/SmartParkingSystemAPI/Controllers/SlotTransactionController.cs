using SmartParkingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SmartParkingSystemAPI.Controllers
{
    public class SlotTransactionController : ApiController
    {
        public bool PostTransactionLog(ParkingSlotTransaction log)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                ParkingSlotTransaction transaction = new ParkingSlotTransaction()
                {
                    SlotID = log.SlotID,
                    UserID = log.UserID,
                    ParkingInTime = DateTime.Now,
                    VehicleNumber = log.VehicleNumber
                };

                context.ParkingSlotTransactions.Add(transaction);
                context.SaveChanges();
                result = true;
            }
            return result;
        }
        public List<SlotTransactionModel> GetSlotTransaction_BySlot(int slotId)
        {
            List<SlotTransactionModel> result = new List<SlotTransactionModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                result = (from pt in context.ParkingSlotTransactions
                          join s in context.SlotMasters on pt.SlotID equals s.SlotID
                          join p in context.ParkingMasters on s.ParkingID equals p.ParkingID
                          join u in context.UserProfiles on pt.UserID equals u.UserID
                          where pt.SlotID == slotId 
                          select new SlotTransactionModel
                          {
                              TransactionID = pt.TransactionID,
                              SlotID = s.SlotID,
                              UserID = u.UserID,
                              UserName = u.FirstName + "" + u.LastName,
                              SlotName = s.SlotNumber,
                              ParkingInTime = pt.ParkingInTime,
                              ParkingName = p.ParkingName,
                              ParkingOutTime = pt.ParkingOutTime
                          }).ToList();
            }
            return result;
        }
        public List<SlotTransactionModel> GetSlotTransaction(int slotId, int userId)
        {
            List<SlotTransactionModel> result = new List<SlotTransactionModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                result = (from pt in context.ParkingSlotTransactions
                          join s in context.SlotMasters on pt.SlotID equals s.SlotID
                          join p in context.ParkingMasters on s.ParkingID equals p.ParkingID
                          join u in context.UserProfiles on pt.UserID equals u.UserID
                          where pt.SlotID == slotId && pt.UserID == userId
                          select new SlotTransactionModel
                          {
                              TransactionID = pt.TransactionID,
                              SlotID = s.SlotID,
                              UserID = u.UserID,
                              UserName = u.FirstName + "" + u.LastName,
                              SlotName = s.SlotNumber,
                              ParkingInTime = pt.ParkingInTime,
                              ParkingName = p.ParkingName,
                              ParkingOutTime = pt.ParkingOutTime
                          }).ToList();
            }
            return result;
        }


        public List<SlotTransactionModel> GetSlotTransactionAsUser(int userId)
        {
            List<SlotTransactionModel> result = new List<SlotTransactionModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                result = (from pt in context.ParkingSlotTransactions
                          join s in context.SlotMasters on pt.SlotID equals s.SlotID
                          join p in context.ParkingMasters on s.ParkingID equals p.ParkingID
                          join u in context.UserProfiles on pt.UserID equals u.UserID
                          where pt.UserID == userId
                          select new SlotTransactionModel
                          {
                              TransactionID = pt.TransactionID,
                              SlotID = s.SlotID,
                              UserID = u.UserID,
                              UserName = u.FirstName + "" + u.LastName,
                              SlotName = s.SlotNumber,
                              ParkingInTime = pt.ParkingInTime,
                              ParkingName = p.ParkingName,
                              ParkingOutTime = pt.ParkingOutTime
                          }).ToList();
            }
            return result;
        }


        public List<SlotTransactionModel> GetSlotTransaction()
        {
            List<SlotTransactionModel> result = new List<SlotTransactionModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                result = (from pt in context.ParkingSlotTransactions
                          join s in context.SlotMasters on pt.SlotID equals s.SlotID
                          join p in context.ParkingMasters on s.ParkingID equals p.ParkingID
                          join u in context.UserProfiles on pt.UserID equals u.UserID
                          select new SlotTransactionModel
                          {
                              TransactionID = pt.TransactionID,
                              SlotID = s.SlotID,
                              UserID = u.UserID,
                              UserName = u.FirstName + "" + u.LastName,
                              SlotName = s.SlotNumber,
                              ParkingInTime = pt.ParkingInTime,
                              ParkingName = p.ParkingName,
                              ParkingOutTime = pt.ParkingOutTime
                          }).ToList();

            }
            return result;
        }

        public bool PostUpdateParkingSlotTransaction(string slotNumber, int userId, int parkingId)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var getSolt = context.SlotMasters.Where(s => s.SlotNumber == slotNumber && s.ParkingID == parkingId).SingleOrDefault();
                if (getSolt != null)
                {
                    ParkingSlotTransaction transaction = context.ParkingSlotTransactions.Where(t => t.SlotID == getSolt.SlotID && t.UserID == userId).SingleOrDefault();
                    transaction.ParkingOutTime = DateTime.Now;

                    context.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public int GetLastParkingSlot(int inputId)
        {
            int result = 0;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                ParkingSlotTransaction trasaction = context.ParkingSlotTransactions.OrderByDescending(t => t.ParkingInTime).Where(t => t.UserID == inputId).FirstOrDefault();
                if (trasaction != null)
                {
                    var slot = context.SlotMasters.Where(s => s.SlotID == trasaction.SlotID).SingleOrDefault();
                    result = slot.ParkingID;
                }
            }
            return result;
        }

        public SlotTransactionModel ConvertDBToModelObject(ParkingSlotTransaction trans)
        {
            return new SlotTransactionModel()
            {
                SlotID = trans.SlotID,
                ParkingInTime = trans.ParkingInTime,
                ParkingOutTime = trans.ParkingOutTime,
                TransactionID = trans.TransactionID,
                UserID = trans.UserID,
                VehicleNumber = trans.VehicleNumber
            };
        }

    }
}
