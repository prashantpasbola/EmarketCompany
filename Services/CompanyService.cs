using companyservice.DbSetting;
using companyservice.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace companyservice.Services
{
    public class CompanyService
    {
        private readonly ILogger<CompanyService> _logger;
        private readonly IMongoCollection<Company> _company;
        public CompanyService(IEStockMarketDatabaseSetting settings, ILogger<CompanyService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _company = database.GetCollection<Company>(settings.CompanyCollectionName);
            _logger= logger;
        }

        public List<Company> Get()
        {
            _logger.LogInformation(message: "Get company list");
            List<Company> company;
            company = _company.Find(com => true).ToList();
            return company;
        }

        public Company Get(string companyCode) =>
            _company.Find<Company>(com => com.CompanyCode == companyCode).FirstOrDefault();

        public Company Register(Company company)
        {

            _logger.LogInformation(message: "Register company");
            _company.InsertOne(company);
            return company;
        }

        public void Remove(string companyCode) =>
            _company.DeleteOne(Company => Company.CompanyCode == companyCode);

    }
}
