using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.WebApps.Models;
using RegApplPortal.WebApps.Security;
using RegApplPortal.Interfaces;
using RegApplPortal.BusinessLogic;

namespace RegApplPortal.WebApps.Controllers
{
    [AuthorizeUser]
    [AuthorizeUser(Roles = "Administrator")]
    public class ReferenceController : RegApplPortalController
    {
        IReferenceBusinessLogic referenceBusinessLogic;

        public ReferenceController()
        {
            referenceBusinessLogic = new ReferenceBusinessLogic();
        }
        
        public ActionResult Index()
        {
            ReferenceChoiceModel model = new ReferenceChoiceModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ReferenceChoiceModel model)
        {
            ReferenceUniversalItemModel.ReferenceDisplayName = model.ReferencesDisplayName.Where(a => a.Value == model.ReferenceId.ToString()).Select(b => b.Text).FirstOrDefault();
            model.ReferenceName = model.References.Where(a => a.Value == model.ReferenceId.ToString()).Select(b => b.Text).FirstOrDefault();
            return RedirectToAction(model.ReferenceName);
        }

        //Delete
        public ActionResult Delete(long? id, string refname)
        {
            if (id != null && !string.IsNullOrEmpty(refname))
            {
                referenceBusinessLogic.DeleteReferenceItem((long)id, refname);
                return RedirectToAction(refname);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// ReferenceRef
        /// </summary>
        /// <returns></returns>
        public ActionResult ReferenceRef()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.ReferenceRef);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult ReferenceRefEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.ReferenceRef;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult ReferenceRefEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefExpertise
        /// </summary>
        /// <returns></returns>
        public ActionResult RefExpertise()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefExpertise);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefExpertiseEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefExpertise;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefExpertiseEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefIncomingChannel
        /// </summary>
        /// <returns></returns>
        public ActionResult RefIncomingChannel()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefIncomingChannel);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefIncomingChannelEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefIncomingChannel;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefIncomingChannelEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefLocality
        /// </summary>
        /// <returns></returns>
        public ActionResult RefLocality()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefLocality);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefLocalityEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefLocality;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefLocalityEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefMedDocumentType
        /// </summary>
        /// <returns></returns>
        public ActionResult RefMedDocumentType()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefMedDocumentType);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefMedDocumentTypeEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefMedDocumentType;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefMedDocumentTypeEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefNomination
        /// </summary>
        /// <returns></returns>
        public ActionResult RefNomination()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefNomination);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefNominationEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefNomination;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefNominationEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefReason
        /// </summary>
        /// <returns></returns>
        public ActionResult RefReason()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefReason);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefReasonEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefReason;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefReasonEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefMedDocumentType
        /// </summary>
        /// <returns></returns>
        public ActionResult RefStatementType()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefStatementType);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefStatementTypeEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefStatementType;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefStatementTypeEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefStatus
        /// </summary>
        /// <returns></returns>
        public ActionResult RefStatus()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefStatus);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefStatusEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefStatus;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefStatusEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }

        /// <summary>
        ///  RefMedDocumentType
        /// </summary>
        /// <returns></returns>
        public ActionResult RefSubjectInsurance()
        {
            List<ReferenceUniversalItem> reference = referenceBusinessLogic.GetUniversalList(Constants.RefSubjectInsurance);
            ReferenceUniversalItemModel.FillListReferenceUniversal(reference);
            return View(ReferenceUniversalItemModel.ListReferenceUniversalItem);
        }
        [HttpGet]
        public ActionResult RefSubjectInsuranceEdit(long? id)
        {
            if (id == null)
            {
                ReferenceUniversalItemModel refItem = new ReferenceUniversalItemModel();
                refItem.ReferenceName = Constants.RefSubjectInsurance;
                return View(refItem);
            }
            else
            {
                return View(ReferenceUniversalItemModel.ListReferenceUniversalItem.Find(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult RefSubjectInsuranceEdit(long? id, ReferenceUniversalItem model)
        {
            if (ReferenceUniversalItemModel.ListReferenceUniversalItem.Where(a => a.Id == id).Count() > 0)
            {
                //Update
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, false);
            }
            else
            {
                //Insert
                referenceBusinessLogic.SaveUniversalReferenceItem(model, model.ReferenceName, true);
            }
            return RedirectToAction(model.ReferenceName);
        }
    }
}