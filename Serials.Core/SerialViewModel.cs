using System;

namespace Serials.Core
{
    public class SerialViewModel
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string ActivationDate { get; set; }

        public string Email { get; set; }

        public string Info { get; set; }

        public string SerialNumber { get; set; }

        public string Software { get; set; }
        public InputType InputType { get; set; }

        public static SerialViewModel ToViewModel(Serials model)
        {
            return new SerialViewModel
            {
                ActivationDate = model.ActivationDate,
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
            return new Serials
            {
                ActivationDate = model.ActivationDate,
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Info = model.Info,
                SerialNumber = model.SerialNumber,
                Software = model.Software
            };
        }
    }

   
}
