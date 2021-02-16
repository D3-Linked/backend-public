using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Linked_backend.Models;

namespace Linked_backend.Data
{
    public static class DbInitializer
    {
        
        public static void Initialize(ScheduleContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Rollen.AddRange(
                new Role { Naam = "User" },
                new Role { Naam = "PlanningManager" },
                new Role { Naam = "Admin" }
            );
            context.SaveChanges();

            context.Bedrijven.AddRange(
                new Bedrijf { Naam = "DirkCompany" },
                new Bedrijf { Naam = "JonasCompany" }
            );
            context.SaveChanges();

            context.Users.AddRange(
                new User {Naam="Dirk", Email="dirk@gmail.com", Paswoord = BCrypt.Net.BCrypt.HashPassword("Dirk123!"), RoleID = 1},
                new User { Naam = "Jonas", Email = "jonas@gmail.com", Paswoord = BCrypt.Net.BCrypt.HashPassword("Jonas123!"), RoleID = 1 }
            );
            context.SaveChanges();

            context.Schedules.AddRange(
                new Schedule { Code = 321, Datum = DateTime.Now, Opmerking = "hallo", UserID = 1},
                new Schedule { Code = 123, Datum = DateTime.Now, Opmerking = "hallo", UserID = 2 }
            );
            context.SaveChanges();
            context.Laadkades.AddRange(
                new Laadkade { Nummer= 1, IsBezet = false },
                new Laadkade { Nummer = 2, IsBezet = false }
            );
            context.SaveChanges();

            context.Leveranciers.AddRange(
                new Leverancier { BedrijfID = 1, Code = 321, Nummerplaat= "1fam998"}
            );
            context.SaveChanges();

            context.Leveringen.AddRange(
                 new Levering { Omschrijving = "Hallo", LaadkadeID = 1, ScheduleID = 1, LeverancierID = 1}
            );
            context.SaveChanges();

            context.Producten.AddRange(
                new Product { Naam="Cola", LeveringID = 1 }
            );
            context.SaveChanges();



        }
    }
}