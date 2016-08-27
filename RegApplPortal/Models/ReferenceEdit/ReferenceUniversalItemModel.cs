using System;
using System.Collections.Generic;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace RegApplPortal.WebApps.Models
{
    public class ReferenceUniversalItemModel
    {
        public static List<ReferenceUniversalItemModel> ListReferenceUniversalItem = new List<ReferenceUniversalItemModel>();
        public static string ReferenceDisplayName { get; set; }

        [DisplayName("Название справочника")]
        public string ReferenceName { get; set; }

        [Required]
        [Display(Name = "Id", Prompt = "Id в таблице не должны дублироваться!")]
        public long? Id { get; set; }

        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Код")]
        public string Code { get; set; }

        [DisplayName("Отображаемое название")]
        public string DisplayName { get; set; }

         /// <summary>
         /// ФИО добавлено на будущее - 146% пригодится
         /// </summary>
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        [DisplayName("Фамилия")]
        public string Lastname { get; set; }

        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        [DisplayName("Имя")]
        public string Firstname { get; set; }

        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        [DisplayName("Отчество")]
        public string Secondname { get; set; }


        public List<SelectListItem> DeliveryCenters { get; set; }
        public List<SelectListItem> BSOResponsibles { get; set; }


        public static void FillListReferenceUniversal(List<ReferenceUniversalItem> reference)
        {
            ListReferenceUniversalItem = new List<ReferenceUniversalItemModel>();
            foreach (var item in reference)
            {
                ReferenceUniversalItemModel r = new ReferenceUniversalItemModel(item);
                ListReferenceUniversalItem.Add(r);
            }
        }

        public ReferenceUniversalItemModel()
        {
        }

        public ReferenceUniversalItemModel(ReferenceUniversalItem item)
        {
            ReferenceName = item.ReferenceName;
            Id = item.Id;
            Name = item.Name;
            DisplayName = item.DisplayName;
            Code = item.Code;
            
            //Lastname = item.Lastname;
            //Firstname = item.Firstname;
            //Secondname = item.Secondname;
        }

        //TODO не понятно зачем
        public void FillReferenceForView()
        {
            DeliveryCenters = ReferencesProvider.GetReferences(Constants.DeliveryCenterRef, null, true);
            //BSOResponsibles = StatusBSOProvider.GetListBSOResponsibles(true);
        }
    }
}