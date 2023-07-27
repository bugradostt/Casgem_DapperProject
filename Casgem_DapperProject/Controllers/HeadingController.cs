using Casgem_DapperProject.Dal.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casgem_DapperProject.Controllers
{
    public class HeadingController : Controller
    {
        readonly string _connectionString = "Server=BUDOTEKNO\\SQLEXPRESS; initial Catalog=CasgemDapperDb;integrated Security=true";
        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connectionString);
            var values = await connection.QueryAsync<Headings>("Select * from TblHeading");
            return View(values);
        }

        [HttpGet]
        public IActionResult AddHeading()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHeading(Headings p) 
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"INSERT INTO TblHeading ( HeadingName, HeadingStatus) VALUES ('{p.HeadingName}', 'True') ";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($"DELETE FROM TblHeading WHERE HeadingId = '{id}'");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
           var foundId = await connection.QueryFirstAsync<Headings>($"SELECT * FROM TblHeading WHERE HeadingId = '{id}'");
            return View(foundId);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHeading(Headings p)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($"UPDATE TblHeading SET HeadingName='{p.HeadingName}', HeadingStatus='{p.HeadingStatus}' where = HeadingId = '{p.HeadingId}' ");
            return RedirectToAction("Index");
        }
    }
}
