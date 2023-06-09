using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClientDashboardAPI.DTOs;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace ClientDashboardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string connString;
        private readonly ILogger<ClientController> _logger;
        public ClientController(IConfiguration configuration, ILogger<ClientController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var host = _configuration["DBHOST"] ?? "localhost";
            var port = _configuration["DBPORT"] ?? "3306";
            var password = _configuration["MYSQL_PASSWORD"] ?? _configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = _configuration["MYSQL_USER"] ?? _configuration.GetConnectionString("MYSQL_USER");
            var clientDashboardDatabase = _configuration["MYSQL_DATABASE"] ?? _configuration.GetConnectionString("MYSQL_DATABASE");

            connString = $"server={host}; userid={userid};pwd={password};port={port};database={clientDashboardDatabase}";
        }

        [HttpGet("api/clients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                IEnumerable<Client> result = null;
                result = await conn.QueryAsync<Client, PhoneNumber, Client>(
                    "SELECT c.*, pn.* " +
                    "FROM clients c " +
                    "LEFT JOIN clients_phone_numbers cpn ON c.id = cpn.client_id " +
                    "LEFT JOIN phone_numbers pn ON cpn.phone_number_id = pn.id " +
                    "WHERE c.archived = false",
                    (client, phoneNumber) =>
                    {
                        if (phoneNumber != null)
                        {
                            if (result == null) {
                                result = new List<Client>();
                            }

                            var existingClient = result.FirstOrDefault(c => c.id == client.id);

                            if (existingClient == null)
                            {
                                client.phone_numbers = new List<PhoneNumber> { phoneNumber };
                                return client;
                            }
                            existingClient.phone_numbers.Add(phoneNumber);
                        }
                        return null;
                    },
                    splitOn: "id");
                return result.Where(c => c != null).ToList();
            }
        }

        [HttpPost("api/clients")]
        public async Task<ActionResult<Client>> CreateClient(Client client)
        {
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var query = "INSERT INTO clients (first_name, last_name) VALUES (@FirstName, @LastName); SELECT LAST_INSERT_ID();";
                var id = await conn.ExecuteScalarAsync<int>(query, new { FirstName = client.first_name, LastName = client.last_name });

                client.id = id;

                if (client.phone_numbers != null && client.phone_numbers.Any())
                {
                    foreach (var phoneNumber in client.phone_numbers)
                    {
                        query = "INSERT INTO phone_numbers (phone_number) VALUES (@Number); SELECT LAST_INSERT_ID();";
                        var phoneNumberId = await conn.ExecuteScalarAsync<int>(query, new { Number = phoneNumber.phone_number });

                        await conn.ExecuteAsync("INSERT INTO clients_phone_numbers (client_id, phone_number_id) VALUES (@ClientId, @PhoneNumberId);",
                            new { ClientId = client.id, PhoneNumberId = phoneNumberId });
                    }
                }

                return CreatedAtAction(nameof(GetClient), new { id = client.id }, client);
            }
        }

        [HttpPut("api/clients")]
        public async Task<IActionResult> UpdateClient(Client updatedClient)
        {
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var query = "UPDATE clients SET first_name = @first_name, last_name = @last_name WHERE id = @id;";
                await conn.ExecuteAsync(query, updatedClient);

                await conn.ExecuteAsync("DELETE FROM clients_phone_numbers WHERE client_id = @ClientId", new { ClientId = updatedClient.id });

                if (updatedClient.phone_numbers != null && updatedClient.phone_numbers.Any())
                {
                    query = "INSERT INTO phone_numbers (phone_number) VALUES (@PhoneNumber); SELECT LAST_INSERT_ID();";
                    foreach (var phoneNumber in updatedClient.phone_numbers)
                    {
                        var phoneNumberId = await conn.ExecuteScalarAsync<int>(query, new { PhoneNumber = phoneNumber.phone_number });

                        await conn.ExecuteAsync("INSERT INTO clients_phone_numbers (client_id, phone_number_id) VALUES (@ClientId, @PhoneNumberId);",
                            new { ClientId = updatedClient.id, PhoneNumberId = phoneNumberId });
                    }
                }

                return NoContent();
            }
        }

        [HttpPost("api/clients/{id}/archive")]
        public async Task<IActionResult> ArchiveClient(int id)
        {
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var query = "UPDATE clients SET archived = true WHERE id = @Id;";
                await conn.ExecuteAsync(query, new { Id = id });
                return NoContent();
            }
        }

        [HttpGet("api/clients/{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var clientQuery = "SELECT * FROM clients WHERE id = @Id";
                var result = await conn.QueryAsync<Client>(clientQuery, new { Id = id });

                if (!result.Any())
                {
                    return NotFound();
                }

                var phoneNumberQuery = "SELECT * FROM phone_numbers pn " +
                    "INNER JOIN clients_phone_numbers cpn ON cpn.phone_number_id = pn.id " +
                    "WHERE cpn.client_id = @Id";

                var phoneNumbers = await conn.QueryAsync<PhoneNumber>(phoneNumberQuery, new { Id = id });

                var client = result.Single();
                client.phone_numbers = phoneNumbers.ToList();

                return client;
            }
        }
    }
}