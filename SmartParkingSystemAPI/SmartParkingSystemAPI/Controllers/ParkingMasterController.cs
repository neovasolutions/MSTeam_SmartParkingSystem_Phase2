using SmartParkingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;

namespace SmartParkingSystemAPI.Controllers
{
    public class ParkingMasterController : ApiController
    {
        public List<ParkingModel> GetParkingMasterList()
        {
            List<ParkingModel> result = new List<ParkingModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.ParkingMasters.ToList();

                if (qry.Count() > 0)
                {
                    result = qry.Select(p => new ParkingModel()
                                {
                                    IsActive = p.IsActive,
                                    NumberOfSlots = p.NumberOfSlots,
                                    ParkingAddress = p.ParkingAddress,
                                    ParkingID = p.ParkingID,
                                    ParkingName = p.ParkingName,
                                    ParkingTemplateName = p.ParkingTemplateName
                                }).ToList();
                }
                return result;
            }
        }

        public List<SelectedListModel> GetParkings()
        {
            List<SelectedListModel> result = new List<SelectedListModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                result = context.ParkingMasters.Where(s => s.IsActive == true).Select(s => new SelectedListModel { ListText = s.ParkingName, ListValue = s.ParkingID }).ToList();

                if (result.Count() > 0)
                    return result;
            }
            return result;
        }

        public ParkingModel GetParkingMaster(int inputId)
        {
            ParkingModel result = new ParkingModel();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var prk = context.ParkingMasters.Where(u => u.ParkingID == inputId).SingleOrDefault();
                if (prk != null)
                {
                    result = ConvertDBToModelObject(prk);
                }
            }
            return result;
        }

        public bool PostParking(ParkingMaster prk)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                ParkingMaster parking = new ParkingMaster()
                {
                    ParkingName = prk.ParkingName,
                    ParkingTemplateName = prk.ParkingTemplateName,
                    ParkingAddress = prk.ParkingAddress,
                    NumberOfSlots = prk.NumberOfSlots,
                    IsActive = prk.IsActive
                };

                context.ParkingMasters.Add(parking);
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool PostUpdateParking(ParkingMaster prk)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                ParkingMaster parking = context.ParkingMasters.Where(u => u.ParkingID == prk.ParkingID).SingleOrDefault();
                parking.IsActive = prk.IsActive;
                parking.NumberOfSlots = prk.NumberOfSlots;
                parking.ParkingAddress = prk.ParkingAddress;
                parking.ParkingName = prk.ParkingName;
                parking.ParkingTemplateName = prk.ParkingTemplateName;
                parking.IsActive= prk.IsActive;

                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public ParkingModel ConvertDBToModelObject(ParkingMaster prk)
        {
            return new ParkingModel()
            {
                IsActive = prk.IsActive,
                ParkingID = prk.ParkingID,
                NumberOfSlots = prk.NumberOfSlots,
                ParkingAddress = prk.ParkingAddress,
                ParkingName = prk.ParkingName,
                ParkingTemplateName = prk.ParkingTemplateName
            };
        }

        public List<ParkingModel> SearchParkingMaster(ParkingSearchModel model)
        {
            List<ParkingModel> result = new List<ParkingModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.ParkingMasters.Where(s => s.IsActive == true).ToList();

                if (model != null)
                {
                    if (model.ParkingName != null)
                        qry = qry.Where(p => p.ParkingName.Contains(model.ParkingName)).ToList();
                    if (model.ParkingAddress != null)
                        qry = qry.Where(p => p.ParkingAddress.Contains(model.ParkingAddress)).ToList();
                }

                if (qry.Count() > 0)
                {
                    result = qry.Select(p => new ParkingModel()
                    {
                        IsActive = p.IsActive,
                        NumberOfSlots = p.NumberOfSlots,
                        ParkingAddress = p.ParkingAddress,
                        ParkingID = p.ParkingID,
                        ParkingName = p.ParkingName,
                        ParkingTemplateName = p.ParkingTemplateName
                    }).ToList();
                }
                return result;
            }
        }

        public List<SelectedListModel> GetParkingByName(string inputId)
        {
            List<SelectedListModel> result = new List<SelectedListModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                result = context.ParkingMasters.Where(s => s.IsActive == true && s.ParkingName.Contains(inputId))
                         .Select(s => new SelectedListModel { ListText = s.ParkingName, ListValue = s.ParkingID })
                         .ToList();

                if (result.Count() > 0)
                    return result;
            }
            return result;
        }
    }
}
