using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markom2.Repository.Business;
using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Markom2.Web.Pages.Master
{
    public class MEmployeeModel : PageModel
    {
        private readonly MEmployeeService _mEmployeeService;
        private readonly ILogger _logger;

        public MEmployeeModel(MEmployeeService mEmployeeService, ILogger<MEmployeeModel> logger)
        {
            _mEmployeeService = mEmployeeService;
            _logger = logger;
        }

        public MEmployee Employee { get; set; }

        public MEmployeeViewModel EmployeeView { get; set; }

        public void OnGet()
        {
        }
    }
}
