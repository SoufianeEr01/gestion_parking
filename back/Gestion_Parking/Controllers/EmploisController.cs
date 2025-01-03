﻿using Gestion_Parking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Gestion_Parking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploisController : ControllerBase
    {
        private readonly string connectionString;
        private readonly ILogger<EmploisController> _logger;

        public EmploisController(IConfiguration configuration, ILogger<EmploisController> logger)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? "";
            _logger = logger;
        }

        // Créer un nouvel emploi
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult CreateEmploi([FromBody] Emploi emploi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Données invalides.");
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Vérifier l'existence d'un emploi pour le même jour et groupe
                    string checkSql = "SELECT COUNT(*) FROM Emplois WHERE Jour = @Jour AND Groupe_Id = @Groupe_Id";
                    using (var checkCommand = new SqlCommand(checkSql, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Jour", emploi.Jour.ToString());
                        checkCommand.Parameters.AddWithValue("@Groupe_Id", emploi.Groupe_Id);

                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            return BadRequest("Un emploi existe déjà pour ce jour et ce groupe.");
                        }
                    }

                    // Ajouter le nouvel emploi
                    string insertSql = "INSERT INTO Emplois (Jour, DateDebut, DateFin, Groupe_Id) VALUES (@Jour, @DateDebut, @DateFin, @Groupe_Id)";
                    using (var insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Jour", emploi.Jour.ToString());
                        insertCommand.Parameters.AddWithValue("@DateDebut", emploi.DateDebut);
                        insertCommand.Parameters.AddWithValue("@DateFin", emploi.DateFin);
                        insertCommand.Parameters.AddWithValue("@Groupe_Id", emploi.Groupe_Id);

                        insertCommand.ExecuteNonQuery();
                    }
                }
                return Ok("Emploi ajouté avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'ajout de l'emploi.");
                return StatusCode(500, "Une erreur est survenue.");
            }
        }

        // Récupérer tous les emplois d'un groupe
        [Authorize(Policy = "EtudiantOuAdmin")]
        [HttpGet("group/{groupeId}")]
        public IActionResult GetEmploisByGroupe(int groupeId)
        {
            var emplois = new List<Emploi>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Requête SQL pour récupérer les emplois du groupe spécifié
                    string sql = "SELECT Jour, DateDebut, DateFin, Groupe_Id FROM Emplois WHERE Groupe_Id = @Groupe_Id";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Groupe_Id", groupeId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Ajout des logs pour vérifier les données lues
                                _logger.LogInformation("Lecture d'un emploi: Jour={Jour}, DateDebut={DateDebut}, DateFin={DateFin}, Groupe_Id={Groupe_Id}",
                                    reader["Jour"], reader["DateDebut"], reader["DateFin"], reader["Groupe_Id"]);

                                emplois.Add(new Emploi
                                {
                                    Jour = Enum.Parse<Jour>(reader.GetString(0)), // Vérifiez que "Jour" est bien une chaîne dans la base de données
                                    DateDebut = reader.GetTimeSpan(1),
                                    DateFin = reader.GetTimeSpan(2),
                                    Groupe_Id = reader.GetInt32(3)
                                });
                            }
                        }
                    }
                }

                if (emplois.Count == 0)
                {
                    _logger.LogWarning("Aucun emploi trouvé pour le groupe avec l'ID {GroupeId}", groupeId);
                    return NotFound($"Aucun emploi trouvé pour le groupe avec l'ID {groupeId}.");
                }

                return Ok(emplois);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des emplois pour le groupe avec l'ID {GroupeId}", groupeId);
                return StatusCode(500, "Une erreur est survenue lors de la récupération des emplois.");
            }
        }

        // Nouvelle fonction : Récupérer les emplois par ID d'étudiant
        [Authorize(Policy = "EtudiantOuAdmin")]
        [HttpGet("etudiant/{idEtudiant}")]
        public IActionResult GetEmploiByIdEtudiant(int idEtudiant)
        {
            var emplois = new List<Emploi>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"
        SELECT E.Id, E.Jour, E.DateDebut, E.DateFin, E.Groupe_Id 
        FROM Emplois E 
        JOIN Groupes G ON E.Groupe_Id = G.Id 
        JOIN Personnes P ON G.Id = P.GroupeId 
        WHERE P.Id = @idEtudiant";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idEtudiant", idEtudiant);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var emploi = new Emploi
                                {
                                    Id = reader.GetInt32(0), // Colonne 'Id'
                                    Jour = Enum.TryParse<Jour>(reader.GetString(1), out var jour) ? jour : throw new InvalidCastException("Jour invalide."), // Conversion sécurisée pour Enum
                                    DateDebut = reader.GetTimeSpan(2), // Colonne 'DateDebut'
                                    DateFin = reader.GetTimeSpan(3),   // Colonne 'DateFin'
                                    Groupe_Id = reader.GetInt32(4)    // Colonne 'Groupe_Id'
                                };
                                emplois.Add(emploi);
                            }
                        }
                    }
                }

                if (emplois.Count == 0)
                {
                    return NotFound("Aucun emploi trouvé pour cet étudiant.");
                }

                return Ok(emplois);
            }
            catch (SqlException ex)
            {
                // Gestion des erreurs SQL
                _logger.LogError(ex, "Erreur SQL lors de la récupération des emplois.");
                return StatusCode(500, new { erreur = $"Erreur SQL : {ex.Message}" });
            }
            catch (Exception ex)
            {
                // Gestion des erreurs générales
                _logger.LogError(ex, "Erreur lors de la récupération des emplois.");
                return StatusCode(500, new { erreur = $"Erreur : {ex.Message}" });
            }
        }

        // Modifier un emploi
        [Authorize(Policy = "Admin")]
        [HttpPut("{jour}")]
        public IActionResult UpdateEmploi(Jour jour, [FromBody] Emploi emploi)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Emplois SET DateDebut = @DateDebut, DateFin = @DateFin, Groupe_Id = @Groupe_Id WHERE Jour = @Jour";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Jour", jour.ToString());
                        command.Parameters.AddWithValue("@DateDebut", emploi.DateDebut);
                        command.Parameters.AddWithValue("@DateFin", emploi.DateFin);
                        command.Parameters.AddWithValue("@Groupe_Id", emploi.Groupe_Id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0) return NotFound("Emploi non trouvé.");
                    }
                }
                return Ok("Emploi modifié avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification.");
                return StatusCode(500, "Une erreur est survenue.");
            }
        }

        // Supprimer un emploi avec confirmation
        [Authorize(Policy = "Admin")]
        [HttpDelete("{jour}")]
        public IActionResult DeleteEmploi(Jour jour, [FromQuery] bool confirm)
        {
            if (!confirm) return BadRequest("Suppression non confirmée.");
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Emplois WHERE Jour = @Jour";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Jour", jour.ToString());

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0) return NotFound("Emploi non trouvé.");
                    }
                }
                return Ok("Emploi supprimé avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression.");
                return StatusCode(500, "Une erreur est survenue.");
            }
        }
    }
}