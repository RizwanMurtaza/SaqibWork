using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Serials.Core
{
    public enum SoftwareEnum
    {
        [Display(Name = "Software1")]
        Software1 =1,
        [Display(Name = "Software2")]
        Software2= 2,
        [Display(Name = "Software3")]
        Software3= 3
    }
}
