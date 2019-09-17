﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TickettingSystem.Data;

namespace TickettingSystem.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TickettingSystem.Core.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<DateTime>("JoinedDate");

                    b.Property<string>("KycLevel");

                    b.Property<string>("Language");

                    b.Property<string>("Name");

                    b.Property<string>("Nationality");

                    b.Property<string>("RefUrl");

                    b.Property<string>("ReferredBy");

                    b.Property<string>("Surname");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TickettingSystem.Core.ClientNote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Notes");

                    b.HasKey("ID");

                    b.ToTable("ClientNotes");
                });

            modelBuilder.Entity("TickettingSystem.Core.Exchange", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("APIsEntered");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateEnabled");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("ExchangeName");

                    b.Property<int>("ExchangeUserId");

                    b.HasKey("ID");

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("TickettingSystem.Core.Trade", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrencyCode");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Exchange");

                    b.Property<string>("Operation");

                    b.Property<double>("Price");

                    b.Property<long>("UserId");

                    b.HasKey("ID");

                    b.ToTable("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}