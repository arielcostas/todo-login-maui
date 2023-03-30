﻿// <auto-generated />
using Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230330103213_NuevoNombreId")]
    partial class NuevoNombreId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Api.Models.Usuario", b =>
                {
                    b.OwnsMany("Api.Models.Tarea", "Tareas", b1 =>
                        {
                            b1.Property<string>("CreadorId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<bool>("Completada")
                                .HasColumnType("bit");

                            b1.Property<string>("Titulo")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CreadorId", "Id");

                            b1.ToTable("Tareas");

                            b1.WithOwner("Creador")
                                .HasForeignKey("CreadorId");

                            b1.Navigation("Creador");
                        });

                    b.Navigation("Tareas");
                });
#pragma warning restore 612, 618
        }
    }
}
