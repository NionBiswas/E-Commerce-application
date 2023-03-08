using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Models.ViewModel
{
    public class ProductVM
    {
        public Product Products { get; set; }
        //Category DropDownlist
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        //covertype Dropdownlist
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
