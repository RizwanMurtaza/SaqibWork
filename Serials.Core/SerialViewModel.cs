using System;
using System.ComponentModel.DataAnnotations;

namespace Serials.Core
{
    public class SerialViewModel
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ActivationDate { get; set; }

        public string Email { get; set; }

        public string Info { get; set; }

        [Required(ErrorMessage = "Please Enter Serial Number")]
        public string SerialNumber { get; set; }

        public string Software { get; set; }
        public InputType InputType { get; set; }

        public static SerialViewModel ToViewModel(Serials model)
        {
           
            return new SerialViewModel
            {
                ActivationDate = model.ActivationDate.UnixTimeStampToDateTime().ToShortDateString(),
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Info = model.Info,
                SerialNumber = model.SerialNumber,
                Software = model.Software
            };
        }
        public static Serials ToEntity(SerialViewModel model)
        {
            ExtensionMethods.TryConvert(int.Parse(model.Software), out SoftwareEnum software);
            return new Serials
            {
                ActivationDate = DateTime.Parse(model.ActivationDate).ToUnixTime(),
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Info = model.Info,
                SerialNumber = model.SerialNumber,
                Software = software.ToString()
            };
        }
    }

   
}
