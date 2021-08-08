using Microsoft.AspNetCore.Http;
using companyservice.Model;
using companyservice.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Diagnostics;

namespace companyservice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1.0/market/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companyService;
        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet]
        [Route("getall")]
        public ActionResult<List<Company>> Get() =>
            _companyService.Get();

        /// [HttpGet]
        // [HttpGet("{companycode:length(24)}", Name = "GetCompany")]
        [HttpGet]
        [Route("info/companycode")]
        public ActionResult<Company> Info(string companycode)
        {
            var emp = _companyService.Get(companycode);

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }




        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        [Route("register")]
        public object Register([FromBody] Company company)
        {
            Debug.WriteLine("Register Info");
            Debug.WriteLine("CompanyName :" + company.CompanyName);
            Debug.WriteLine("CompanyCEO :" + company.CompanyCEO);
            Debug.WriteLine("CompanyCode :" + company.CompanyCode);
            Debug.WriteLine("CompanyTurnover :" + company.CompanyTurnover);
            Debug.WriteLine("CompanyWebsite :" + company.CompanyWebsite);
            Debug.WriteLine("StockExchange :" + company.StockExchange);

            _companyService.Register(company);

            return new Status
            { Result = "Success", Message = "Employee Details register  Successfully" };

            //   return CreatedAtRoute("GetCompany", new { companycode = company.CompanyCode.ToString() }, company);
        }

        [HttpDelete]
        [Route("delete/{companycode}")]
        public object Delete(string companyCode)
        {
            Debug.WriteLine("Delete Info");
            Debug.WriteLine("company Code :" + companyCode);

            var company = _companyService.Get(companyCode);

            if (company == null)
            {
                return NotFound();
            }
            try
            {

                _companyService.Remove(company.CompanyCode);
                return new Status
                { Result = "Success", Message = "Employee Details Delete  Successfully" };
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return NoContent();
            }


        }
    }
}
