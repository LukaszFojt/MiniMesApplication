using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

/**
 * Kontekst bazy danych dla systemu MiniMes.
 * Zawiera definicje tabel oraz relacji między nimi.
 */
public class MiniMesDbContext : DbContext
{
    /**
     * Tabele przechowująca dane.
     */
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Processes> Processes { get; set; }
    public DbSet<Parameters> Parameters { get; set; }
    public DbSet<ProcessParameters> ProcessParameters { get; set; }
    public DbSet<MachineParameter> MachineParameter { get; set; }

    /**
     * Konstruktor klasy MiniMesDbContext.
     * Inicjalizuje nową instancję kontekstu bazy danych.
     * @param options Opcje konfiguracyjne kontekstu bazy danych.
     */
    public MiniMesDbContext(DbContextOptions<MiniMesDbContext> options) : base(options)
    {

    }

    /**
     * Metoda konfigurująca relacje między tabelami w bazie danych.
     * @param modelBuilder Obiekt umożliwiający konfigurację modelu bazy danych.
     */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracja relacji jeden-do-wielu między Zamówieniem a Maszyną
        modelBuilder.Entity<Order>()
            .HasOne<Machine>()
            .WithMany(m => m.Orders)
            .HasForeignKey(o => o.MachineId);

        // Konfiguracja relacji jeden-do-wielu między Zamówieniem a Produktem
        modelBuilder.Entity<Order>()
            .HasOne<Product>()
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId);

        // Konfiguracja relacji jeden-do-wielu między Procesem a Zamówieniem
        modelBuilder.Entity<Processes>()
            .HasOne<Order>()
            .WithMany(o => o.Processes)
            .HasForeignKey(p => p.OrderId);

        // Konfiguracja relacji jeden-do-wielu między Parametrami Procesu a Procesem
        modelBuilder.Entity<ProcessParameters>()
            .HasOne<Processes>()
            .WithMany(p => p.ProcessParameters)
            .HasForeignKey(pp => pp.ProcessId);

        // Konfiguracja relacji jeden-do-wielu między Parametrami Procesu a Parametrami
        modelBuilder.Entity<ProcessParameters>()
            .HasOne<Parameters>()
            .WithMany(p => p.ProcessParameters)
            .HasForeignKey(pp => pp.ParameterId);

        // Konfiguracja relacji jeden-do-wielu między Parametrami Maszyn a Maszynami
        modelBuilder.Entity<MachineParameter>()
            .HasOne<Machine>()
            .WithMany(m => m.MachineParameter)
            .HasForeignKey(m => m.MachineId);

        // Konfiguracja relacji jeden-do-wielu między Parametrami Maszyn a Parametrami
        modelBuilder.Entity<MachineParameter>()
            .HasOne<Parameters>()
            .WithMany(m => m.MachineParameter)
            .HasForeignKey(m => m.ParameterId);
    }
}

/**
 * Klasa reprezentująca maszynę.
 */
public class Machine : IMachine
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    /**
     * Kolekcja zamówień przypisanych do maszyny.
     */
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<MachineParameter> MachineParameter { get; set; } = new List<MachineParameter>();
}

/**
 * Klasa reprezentująca produkt.
 */
public class Product : IProduct
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    /**
     * Kolekcja zamówień zawierających ten produkt.
     */
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

/**
 * Klasa reprezentująca zamówienie.
 */
public class Order : IOrder
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    [ForeignKey("Machine")]
    public int MachineId { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    /**
     * Kolekcja procesów związanych z zamówieniem.
     */
    public ICollection<Processes> Processes { get; set; } = new List<Processes>();
}

/**
 * Klasa reprezentująca proces.
 */
public class Processes : IProcesses
{
    [Key]
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public string Status { get; set; }
    public DateTime DateTime { get; set; }
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    /**
     * Kolekcja parametrów procesu.
     */
    public ICollection<ProcessParameters> ProcessParameters { get; set; } = new List<ProcessParameters>();
}

/**
 * Klasa reprezentująca parametry procesu.
 */
public class Parameters : IParameters
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }

    /**
     * Kolekcja procesów, do których przypisany jest ten parametr.
     */
    public ICollection<ProcessParameters> ProcessParameters { get; set; } = new List<ProcessParameters>();
    public ICollection<MachineParameter> MachineParameter { get; set; } = new List<MachineParameter>();
}

/**
 * Klasa reprezentująca parametry procesu.
 */
public class ProcessParameters : IProcessParameters
{
    [Key]
    public int Id { get; set; }
    public double Value { get; set; }
    [ForeignKey("Processes")]
    public int ProcessId { get; set; }
    [ForeignKey("Parameters")]
    public int ParameterId { get; set; }
}

public class MachineParameter : IMachineParameter
{
    [Key]
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    [ForeignKey("Machine")]
    public int MachineId { get; set; }
    [ForeignKey("Parameters")]
    public int ParameterId { get; set; }
}
