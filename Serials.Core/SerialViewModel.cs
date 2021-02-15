using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Serials.Core
{
    public class SerialViewModel
    {
        public SerialViewModel()
        {
            SoftwareList = Enum.GetValues(typeof(SoftwareEnum)).Cast<SoftwareEnum>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = (v).ToString(),
            }).ToList();
        }

        [Required(ErrorMessage = "Please enter Serial Number")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Please enter an Email address")]
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ActivationDate { get; set; }

        

        public string Info { get; set; }

        

        public string SoftwareSelected { get; set; }

        public IEnumerable<SelectListItem> SoftwareList { get; set; }

        public InputType InputType { get; set; }

        public static SerialViewModel ToViewModel(Serials model)
        {
           
            var viewModel = new SerialViewModel
            {
                ActivationDate = model.ActivationDate.UnixTimeStampToDateTime().ToShortDateString(),
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Info = model.Info,
                SerialNumber = model.SerialNumber,
                SoftwareSelected = model.Software
            };

            viewModel.SoftwareList = Enum.GetValues(typeof(SoftwareEnum)).Cast<SoftwareEnum>().Select(v => new SelectListItem
                                    {
                                        Text = v.ToString(),
                                        Value = (v).ToString(),
                                        Selected = v.ToString().ToLower() == model.Software?.ToLower()
                                    }).ToList();

            return viewModel;
        }
        public static Serials ToEntity(SerialViewModel model)
        {
            //ExtensionMethods.TryConvert(int.Parse(model.Software), out SoftwareEnum software);
            return new Serials
            {
                ActivationDate = DateTime.Parse(model.ActivationDate).ToUnixTime(),
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Info = model.Info,
                SerialNumber = model.SerialNumber,
                //Software = software.ToString()
                Software = model.SoftwareSelected
            };
        }
    }

   
}
