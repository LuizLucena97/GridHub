﻿// <auto-generated />
using System;
using GridHub.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace GridHub.Database.Migrations
{
    [DbContext(typeof(FIAPDBContext))]
    [Migration("20241121140814_vAdicionando_Tb_Usuario_Espaco")]
    partial class vAdicionando_Tb_Usuario_Espaco
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GridHub.Database.Models.Espaco", b =>
                {
                    b.Property<int>("EspacoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EspacoId"));

                    b.Property<double>("AreaTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<string>("DirecaoVento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<string>("FonteEnergia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("FotoEspaco")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<double>("MediaSolar")
                        .HasPrecision(18, 2)
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<string>("NomeEspaco")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("OrientacaoSolar")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Topografia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("VelocidadeVento")
                        .HasPrecision(18, 2)
                        .HasColumnType("BINARY_DOUBLE");

                    b.HasKey("EspacoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("GRIDHUB_ESPACOS", (string)null);
                });

            modelBuilder.Entity("GridHub.Database.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("UsuarioId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("FotoPerfil")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR2(256)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("UsuarioId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("GRIDHUB_USUARIOS", (string)null);
                });

            modelBuilder.Entity("GridHub.Database.Models.Espaco", b =>
                {
                    b.HasOne("GridHub.Database.Models.Usuario", "Usuario")
                        .WithMany("Espacos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GridHub.Database.Models.Usuario", b =>
                {
                    b.Navigation("Espacos");
                });
#pragma warning restore 612, 618
        }
    }
}
