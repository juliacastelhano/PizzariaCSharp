﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoPizzaria.Data;

#nullable disable

namespace ProjetoPizzaria.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20231005035551_TablePedido")]
    partial class TablePedido
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("ProjetoPizzaria.Models.Atendente", b =>
                {
                    b.Property<int>("AtendenteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("AtendenteId");

                    b.ToTable("Atendentes");
                });

            modelBuilder.Entity("ProjetoPizzaria.Models.Cardapio", b =>
                {
                    b.Property<int>("CardapioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<double>("Preco")
                        .HasColumnType("REAL");

                    b.Property<string>("Sabor")
                        .HasColumnType("TEXT");

                    b.HasKey("CardapioId");

                    b.ToTable("Cardapios");
                });

            modelBuilder.Entity("ProjetoPizzaria.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int>("Telefone")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ProjetoPizzaria.Models.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AtendenteId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CardapioId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("SaborCardapio")
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalPedido")
                        .HasColumnType("REAL");

                    b.HasKey("PedidoId");

                    b.HasIndex("AtendenteId");

                    b.HasIndex("CardapioId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ProjetoPizzaria.Models.Pedido", b =>
                {
                    b.HasOne("ProjetoPizzaria.Models.Atendente", "Atendente")
                        .WithMany()
                        .HasForeignKey("AtendenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoPizzaria.Models.Cardapio", "Cardapio")
                        .WithMany()
                        .HasForeignKey("CardapioId");

                    b.HasOne("ProjetoPizzaria.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendente");

                    b.Navigation("Cardapio");

                    b.Navigation("Cliente");
                });
#pragma warning restore 612, 618
        }
    }
}
