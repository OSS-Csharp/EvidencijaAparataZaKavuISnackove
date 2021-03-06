// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VendingMachine.Persistence.Repository;

namespace VendingMachine.API.Migrations
{
    [DbContext(typeof(VmDbContext))]
    partial class VmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("VendingMachine.Domain.Entity.ItemEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("itemName")
                        .HasColumnType("TEXT");

                    b.Property<float>("price")
                        .HasColumnType("REAL");

                    b.Property<int>("unit")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("ItemEntities");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.JobMachineWorkerEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("finished")
                        .HasColumnType("INTEGER");

                    b.Property<int>("intworkerIdOriginal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("vendingMachineId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("workerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("vendingMachineId");

                    b.HasIndex("workerId");

                    b.ToTable("JobMachineWorkerEntities");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.VendingMachineEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("machineValidity")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("VendingMachineEntities");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.VendingMachineItemEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("amountOfItem")
                        .HasColumnType("INTEGER");

                    b.Property<int>("itemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("vendingMachineId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("itemId");

                    b.HasIndex("vendingMachineId");

                    b.ToTable("VendingMachineItemEntities");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.WorkerEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("workerIdOriginal")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("WorkerEntities");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.JobMachineWorkerEntity", b =>
                {
                    b.HasOne("VendingMachine.Domain.Entity.VendingMachineEntity", "vendingMachine")
                        .WithMany()
                        .HasForeignKey("vendingMachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VendingMachine.Domain.Entity.WorkerEntity", "worker")
                        .WithMany("jobMachineWorkerEntity")
                        .HasForeignKey("workerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("vendingMachine");

                    b.Navigation("worker");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.VendingMachineItemEntity", b =>
                {
                    b.HasOne("VendingMachine.Domain.Entity.ItemEntity", "item")
                        .WithMany("vendingMachineItemEntities")
                        .HasForeignKey("itemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VendingMachine.Domain.Entity.VendingMachineEntity", "vendingMachine")
                        .WithMany("vendingMachineItemEntities")
                        .HasForeignKey("vendingMachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("item");

                    b.Navigation("vendingMachine");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.ItemEntity", b =>
                {
                    b.Navigation("vendingMachineItemEntities");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.VendingMachineEntity", b =>
                {
                    b.Navigation("vendingMachineItemEntities");
                });

            modelBuilder.Entity("VendingMachine.Domain.Entity.WorkerEntity", b =>
                {
                    b.Navigation("jobMachineWorkerEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
