using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using ESMS.Areas.Identity;
using ESMS.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESMS
{
    [Authorize(Policy = "Payment:Create")]
    public class CreateModel : BaseModel
    {
        public CreateModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager):base(signInManager, userManager) { }

        public void OnGet()
        {
            Input = dbContext.AspNetUsers.Select(U => new Employee
            {
                FirstName = U.FirstName,
                LastName = U.LastName,
                salary = (decimal)U.Salary,
                Deduction = 0,
                SalaryAfterDeduction = CalculateSalaryWithDeduction.CalculateSalaryWithDed(U.Salary, 0),
                EmployeePension =Convert.ToDecimal(CalculateSalaryGeneral.CalculateEmployeePension((decimal)(U.Salary),1)),
                EmployerPension= Convert.ToDecimal(CalculateSalaryGeneral.CalculateEmployeePension((decimal)(U.Salary), 1)),
                TaxableIncome= Convert.ToDecimal(CalculateSalaryGeneral.CalculateTaxableIncome((decimal)(U.Salary), 1)),
                WithholdingTax= Convert.ToDecimal(CalculateSalaryGeneral.CalculateWithHoldingTax((decimal)(U.Salary), 1)),
                NetWage= Convert.ToDecimal(CalculateSalaryGeneral.CalculateNetWage((decimal)(U.Salary), 1)),

                
            }).ToList();

            
        }

        public async Task<IActionResult> OnPost()
        {

            return Page();
        }


        [BindProperty]
        public List<Employee> Input { get; set; }

        public class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal salary { get; set; }
            public int MyProperty { get; set; }
            public decimal Deduction { get; set; }
            public decimal SalaryAfterDeduction { get; set; }
            public decimal EmployeePension { get; set; }
            public decimal EmployerPension { get; set; }
            public decimal TaxableIncome { get; set; }
            public decimal WithholdingTax { get; set; }
            public decimal NetWage { get; set; }







        }


    }
}