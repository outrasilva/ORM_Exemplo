using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ORM_Exemplo
{
    public class Genero
    {

        [Key]
        public int Id { get; set; }
        public required string Descricao { get; set; }
        public required ICollection<Filme> Filme { get; set; }

    }

    public class Filme
    {
        [Key]
        public int Id { get; set; }
        public string? Titulo { get; set;}
        public int GeneroId { get; set;}
        [ForeignKey("GeneroId")]
        public required Genero Genero { get; set; }
    }

    public class ApplicationContext: DbContext
    {
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Filme> Filme { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=orm;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                var genero = new Genero()
                {
                    Descricao = "Fantasia"
                };

                context.Genero.Add(genero);
                context.SaveChanges();

                var filme = new Filme()
                {
                    Titulo = "De volta para o futuro",
                    GeneroId = genero.Id
                };

                context.Filme.Add(filme);
                context.SaveChanges();
            }
        }
    }
}