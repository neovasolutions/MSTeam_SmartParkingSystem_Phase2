using SmartParkingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//using System.Data.Entity;

namespace SmartParkingSystemAPI.Controllers
{
    public class ParkingSlotController : ApiController
    {
        #region Old code implementation
        //vijay
        public List<string> GetOnlyOccupied()
        {
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                try
                {
                    var qry = (from listSlots in context.ParkingSlotMasters
                               where listSlots.IsOccupied == true
                               select listSlots.SlotName);

                    return qry.ToList();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    return new List<string>();
                }

            }


        }

        public string PutAllocationSlot(string barcodeNo, bool flag)//
        {
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                try
                {
                    ParkingSlotMaster recordRow = (from slot in context.ParkingSlotMasters
                                                   where slot.BarcodeNo == barcodeNo
                                                   select slot).SingleOrDefault();

                    recordRow.BarcodeNo = barcodeNo;
                    recordRow.IsOccupied = flag;

                    context.SaveChanges();
                    return recordRow.SlotName;
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    return null;
                }

            }


        }
        // GET api/values
        //public List<ParkingSlotMaster> Get()
        //{
        //    using (Smart_ParkingEntities1 context = new Smart_ParkingEntities1())
        //        {
        //            List<ParkingSlotMaster> list = (from ParkingSlot in context.ParkingSlotMasters
        //                       where ParkingSlot.IsActive==true
        //                        select ParkingSlot).ToList();
        //            return list;              
        //        }

        //}

        // GET api/values/5
        public ParkingSlotMaster Get(int inputId)
        {
            try
            {
                using (SmartParkingEntities context = new SmartParkingEntities())
                {
                    ParkingSlotMaster list = (from ParkingSlots in context.ParkingSlotMasters
                                              where ParkingSlots.ID == inputId
                                              select ParkingSlots).SingleOrDefault();
                    return list;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ParkingSlotMaster();
            }

        }

        // POST api/values
        //public void Post([FromBody]string value)
        //{
        //    //insert
        //}

        //PUT api/values/5
        public bool Post(ParkingSlotMaster ParkSlotObj)
        {
            try
            {
                using (SmartParkingEntities context = new SmartParkingEntities())
                {
                    //ParkingSlotMaster ParkSlotMastr = (from slot in context.ParkingSlotMasters
                    //                                   where slot.ID == ParkSlotObj.ID
                    //                                   select slot).SingleOrDefault();

                    ParkingSlotMaster ParkSlotMastr = (from slot in context.ParkingSlotMasters
                                                       where slot.BarcodeNo == ParkSlotObj.BarcodeNo
                                                       select slot).SingleOrDefault();

                    //ParkSlotMastr.ID = ParkSlotObj.ID;
                    //ParkSlotMastr.ParkSlotID = ParkSlotObj.ParkSlotID;
                    //ParkSlotMastr.SlotName = ParkSlotObj.SlotName;
                    //ParkSlotMastr.Address = ParkSlotObj.Address;
                    ParkSlotMastr.BarcodeNo = ParkSlotObj.BarcodeNo;
                    ParkSlotMastr.IsOccupied = ParkSlotObj.IsOccupied;
                    //ParkSlotMastr.OccupiedBy = ParkSlotObj.OccupiedBy;
                    //ParkSlotMastr.IsActive = ParkSlotObj.IsActive;
                    //ParkSlotMastr.ParkInTime = ParkSlotObj.ParkInTime;
                    //ParkSlotMastr.ParkOutTime = ParkSlotObj.ParkOutTime;
                    context.SaveChanges();
                    return true;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            //update
        }

        // DELETE api/values/5
        public bool Delete(int ID)
        {
            try
            {
                using (SmartParkingEntities context = new SmartParkingEntities())
                {
                    //ParkingSlotMaster OneSlot = (from slot in context.ParkingSlotMasters
                    //                             where slot.ID == ID
                    //                             select slot).FirstOrDefault();

                    //context.ParkingSlotMasters.Attach(OneSlot);
                    //context.ParkingSlotMasters.Remove(OneSlot);
                    //context.SaveChanges();
                    //return true;
                    ParkingSlotMaster OneSlot = new ParkingSlotMaster { ID = ID };
                    context.Entry(OneSlot).State = System.Data.EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            //delete
        }

        #endregion
    }
}