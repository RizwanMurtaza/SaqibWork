using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Serials.Core
{
    public class SerialViewModel
    {
        private const int DefaultDeregistrationLimit = 5;
        public SerialViewModel()
        {
            SoftwareList = Enum.GetValues(typeof(SoftwareEnum)).Cast<SoftwareEnum>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = (v).ToString(),
            }).ToList();

            DeregistrationLimit = DefaultDeregistrationLimit;
            Active = true;
        }

        [Required(ErrorMessage = "Please enter Serial Number")]
        [DisplayName("Serial number")]
        public string SerialNumber { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid Email address"), Required(ErrorMessage ="Email address can't be empty")]
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Activation date")]
        public string ActivationDate { get; set; }

        [DisplayName("Machine Id")]
        public string Activation { get; set; }

        public string Info { get; set; }

        public string Language { get; set; }

        public string Notes { get; set; }

        [DisplayName("Active (status)")]
        public bool Active { get; set; }

        [DisplayName("Deregistration Limit")]
        [Range(1, 99, ErrorMessage = "Please enter a valid number between 0 - 99")]
        public int DeregistrationLimit { get; set; }

        public List<ActivationHistoryItemViewModel> ActivationHistory { get; set; }

        [DisplayName("Software")]
        public string SoftwareSelected { get; set; }

        public IEnumerable<SelectListItem> SoftwareList { get; set; }

        public InputType InputType { get; set; }

        public static SerialViewModel ToViewModel(Serials entity)
        {
           
            var viewModel = new SerialViewModel
            {
                ActivationDate = (entity.ActivationDate).UnixTimeStampToDateTime()?.ToShortDateString(),
                Email = entity.Email,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Info = entity.Info,
                SerialNumber = entity.SerialNumber,
                SoftwareSelected = entity.Software,
                Activation = entity.Activation,
                Language = entity.Language,
                Active = !entity.Disabled,
                DeregistrationLimit = entity.DeregistrationLimit?? DefaultDeregistrationLimit,
                Notes = entity.Notes,
              //  ActivationHistory = entity.ActivationHistory
            };



            viewModel.SoftwareList = Enum.GetValues(typeof(SoftwareEnum)).Cast<SoftwareEnum>().Select(v => new SelectListItem
                                    {
                                        Text = v.ToString(),
                                        Value = (v).ToString(),
                                        Selected = v.ToString().ToLower() == entity.Software?.ToLower()
                                    }).ToList();

            return viewModel;
        }

        public static Serials ToEntity(SerialViewModel model)
        {
            var entity = new Serials
            {
                ActivationDate = string.IsNullOrEmpty(model.ActivationDate)? (long?)null: DateTime.Parse(model.ActivationDate).ToUnixTime(),
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Info = model.Info,
                SerialNumber = model.SerialNumber,
                Software = model.SoftwareSelected,
                Activation = model.Activation,
                Language = model.Language,
                Disabled = !model.Active,
                Notes = model.Notes,
              //  ActivationHistory = model.ActivationHistory
            };

            if (model.DeregistrationLimit == DefaultDeregistrationLimit)
                entity.DeregistrationLimit = null;
            else
                entity.DeregistrationLimit = model.DeregistrationLimit;


            return entity;
        }
    }



    public class ActivationHistoryItemViewModel
    {
        public string Activation { get; set; }

        public string ActivationDate { get; set; }

        public string ActivationInfo { get; set; }

        public string DeactivationDate { get; set; }

        public string DeactivationInfo { get; set; }
    }


}
