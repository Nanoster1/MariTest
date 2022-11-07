﻿// <auto-generated />
using System;
using Mari.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mari.Migrations.Migrations
{
    [DbContext(typeof(MariDbContext))]
    partial class MariDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Mari.Domain.Comments.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<Guid>("ReleaseId")
                        .HasColumnType("uuid")
                        .HasColumnName("release_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_comments");

                    b.HasIndex("ReleaseId")
                        .HasDatabaseName("ix_comments_release_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_comments_user_id");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Mari.Domain.Releases.Entities.Platform", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_platforms");

                    b.ToTable("platforms", (string)null);
                });

            modelBuilder.Entity("Mari.Domain.Releases.Release", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CompleteDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("complete_date");

                    b.Property<int>("PlatformId")
                        .HasColumnType("integer")
                        .HasColumnName("platform_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id")
                        .HasName("pk_releases");

                    b.HasIndex("PlatformId")
                        .HasDatabaseName("ix_releases_platform_id");

                    b.ToTable("releases", (string)null);
                });

            modelBuilder.Entity("Mari.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Mari.Domain.Comments.Comment", b =>
                {
                    b.HasOne("Mari.Domain.Releases.Release", null)
                        .WithMany()
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_releases_release_temp_id1");

                    b.HasOne("Mari.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_users_user_temp_id");
                });

            modelBuilder.Entity("Mari.Domain.Releases.Release", b =>
                {
                    b.HasOne("Mari.Domain.Releases.Entities.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_releases_platforms_platform_temp_id");

                    b.OwnsMany("Mari.Domain.Releases.ValueObjects.Issue", "Issues", b1 =>
                        {
                            b1.Property<Guid>("ReleaseId")
                                .HasColumnType("uuid")
                                .HasColumnName("release_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Link")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("link");

                            b1.Property<string>("Title")
                                .HasColumnType("text")
                                .HasColumnName("title");

                            b1.HasKey("ReleaseId", "Id")
                                .HasName("pk_issue");

                            b1.ToTable("issue", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ReleaseId")
                                .HasConstraintName("fk_issue_releases_release_temp_id");
                        });

                    b.OwnsOne("Mari.Domain.Releases.ValueObjects.ReleaseVersion", "Version", b1 =>
                        {
                            b1.Property<Guid>("ReleaseId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<long>("Major")
                                .HasColumnType("bigint")
                                .HasColumnName("version_major");

                            b1.Property<long>("Minor")
                                .HasColumnType("bigint")
                                .HasColumnName("version_minor");

                            b1.Property<long>("Patch")
                                .HasColumnType("bigint")
                                .HasColumnName("version_patch");

                            b1.HasKey("ReleaseId");

                            b1.ToTable("releases");

                            b1.WithOwner()
                                .HasForeignKey("ReleaseId")
                                .HasConstraintName("fk_releases_releases_id");
                        });

                    b.Navigation("Issues");

                    b.Navigation("Platform");

                    b.Navigation("Version")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}