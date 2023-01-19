using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SemestralkaBE.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Double> Doubles { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Single> Singles { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=daniellieskovsky;Password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Double>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doubles_pk");

            entity.ToTable("doubles", "ostzpb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EncounterId).HasColumnName("encounter_id");
            entity.Property(e => e.GuestPlayerFirst).HasColumnName("guest_player_first");
            entity.Property(e => e.GuestPlayerSecond).HasColumnName("guest_player_second");
            entity.Property(e => e.GuestSetWin).HasColumnName("guest_set_win");
            entity.Property(e => e.HostPlayerFirst).HasColumnName("host_player_first");
            entity.Property(e => e.HostPlayerSecond).HasColumnName("host_player_second");
            entity.Property(e => e.HostSetWin).HasColumnName("host_set_win");

            entity.HasOne(d => d.Encounter).WithMany(p => p.Doubles)
                .HasForeignKey(d => d.EncounterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doubles_fk");

            entity.HasOne(d => d.GuestPlayerFirstNavigation).WithMany(p => p.DoubleGuestPlayerFirstNavigations)
                .HasForeignKey(d => d.GuestPlayerFirst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doubles_fk_3");

            entity.HasOne(d => d.GuestPlayerSecondNavigation).WithMany(p => p.DoubleGuestPlayerSecondNavigations)
                .HasForeignKey(d => d.GuestPlayerSecond)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doubles_fk_4");

            entity.HasOne(d => d.HostPlayerFirstNavigation).WithMany(p => p.DoubleHostPlayerFirstNavigations)
                .HasForeignKey(d => d.HostPlayerFirst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doubles_fk_1");

            entity.HasOne(d => d.HostPlayerSecondNavigation).WithMany(p => p.DoubleHostPlayerSecondNavigations)
                .HasForeignKey(d => d.HostPlayerSecond)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doubles_fk_2");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("encounter_pk");

            entity.ToTable("encounter", "ostzpb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Guest).HasColumnName("guest");
            entity.Property(e => e.GuestsWins).HasColumnName("guests_wins");
            entity.Property(e => e.Host).HasColumnName("host");
            entity.Property(e => e.HostsWins).HasColumnName("hosts_wins");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Round).HasColumnName("round");
            entity.Property(e => e.Time).HasColumnName("time");

            entity.HasOne(d => d.GuestNavigation).WithMany(p => p.EncounterGuestNavigations)
                .HasForeignKey(d => d.Guest)
                .HasConstraintName("encounter_fk_2");

            entity.HasOne(d => d.HostNavigation).WithMany(p => p.EncounterHostNavigations)
                .HasForeignKey(d => d.Host)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("encounter_fk_1");

            entity.HasOne(d => d.PlaceNavigation).WithMany(p => p.Encounters)
                .HasForeignKey(d => d.Place)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("encounter_fk");
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("league_pk");

            entity.ToTable("league", "ostzpb");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("place_pk");

            entity.ToTable("place", "ostzpb");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('ostzpb.place_place_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Team).WithMany(p => p.Places)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("place_fk");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("player_pk");

            entity.ToTable("player", "ostzpb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("newtable_fk");
        });

        modelBuilder.Entity<Single>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("singles_pk");

            entity.ToTable("singles", "ostzpb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EncounterId).HasColumnName("encounter_id");
            entity.Property(e => e.GuestPlayerId).HasColumnName("guest_player_id");
            entity.Property(e => e.GuestSetWin).HasColumnName("guest_set_win");
            entity.Property(e => e.HostPlayerId).HasColumnName("host_player_id");
            entity.Property(e => e.HostSetWin).HasColumnName("host_set_win");

            entity.HasOne(d => d.Encounter).WithMany(p => p.Singles)
                .HasForeignKey(d => d.EncounterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("newtable_fk");

            entity.HasOne(d => d.GuestPlayer).WithMany(p => p.SingleGuestPlayers)
                .HasForeignKey(d => d.GuestPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("singles_fk_1");

            entity.HasOne(d => d.HostPlayer).WithMany(p => p.SingleHostPlayers)
                .HasForeignKey(d => d.HostPlayerId)
                .HasConstraintName("singles_fk");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_pk");

            entity.ToTable("team", "ostzpb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.League).HasColumnName("league");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.LeagueNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.League)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("team_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user", "ostzpb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
