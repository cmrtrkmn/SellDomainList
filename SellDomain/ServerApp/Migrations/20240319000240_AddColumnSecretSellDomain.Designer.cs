﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerApp.Data;

namespace ServerApp.Migrations
{
    [DbContext(typeof(SellDomainContext))]
    [Migration("20240319000240_AddColumnSecretSellDomain")]
    partial class AddColumnSecretSellDomain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("ServerApp.Models.SellDomain", b =>
                {
                    b.Property<int>("DomainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AvalibilityStatus")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfExpire")
                        .HasColumnType("TEXT");

                    b.Property<string>("DomainName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastDateOfCheck")
                        .HasColumnType("TEXT");

                    b.Property<string>("Secret")
                        .HasColumnType("TEXT");

                    b.HasKey("DomainId");

                    b.ToTable("SellDomains");
                });
#pragma warning restore 612, 618
        }
    }
}
