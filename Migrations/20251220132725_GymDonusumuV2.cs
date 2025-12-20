using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class GymDonusumuV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Berberler_BerberId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Islemler_IslemId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropTable(
                name: "CalisanUzmanlikAlani");

            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Berberler");

            migrationBuilder.DropColumn(
                name: "BaslangicSaati",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "BitisSaati",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "RandevuSaati",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "RandevuUcret",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "SecilenIslemler",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "RandevuTarih",
                table: "Randevular",
                newName: "Tarih");

            migrationBuilder.RenameColumn(
                name: "IslemId",
                table: "Randevular",
                newName: "SalonId");

            migrationBuilder.RenameColumn(
                name: "CalisanId",
                table: "Randevular",
                newName: "HizmetId");

            migrationBuilder.RenameColumn(
                name: "BerberId",
                table: "Randevular",
                newName: "AntrenorId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_IslemId",
                table: "Randevular",
                newName: "IX_Randevular_SalonId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular",
                newName: "IX_Randevular_HizmetId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_BerberId",
                table: "Randevular",
                newName: "IX_Randevular_AntrenorId");

            migrationBuilder.AlterColumn<string>(
                name: "UzmanlikAlaniAdi",
                table: "UzmanlikAlanlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnaylandiMi",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Saat",
                table: "Randevular",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "Hizmetler",
                columns: table => new
                {
                    HizmetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sure = table.Column<int>(type: "int", nullable: false),
                    UzmanlikAlaniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetler", x => x.HizmetId);
                    table.ForeignKey(
                        name: "FK_Hizmetler_UzmanlikAlanlari_UzmanlikAlaniId",
                        column: x => x.UzmanlikAlaniId,
                        principalTable: "UzmanlikAlanlari",
                        principalColumn: "UzmanlikAlaniId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Salonlar",
                columns: table => new
                {
                    SalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sehir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ilce = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salonlar", x => x.SalonId);
                });

            migrationBuilder.CreateTable(
                name: "Antrenorler",
                columns: table => new
                {
                    AntrenorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AntrenorAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AntrenorSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AntrenorTelefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    SalonId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antrenorler", x => x.AntrenorId);
                    table.ForeignKey(
                        name: "FK_Antrenorler_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId");
                    table.ForeignKey(
                        name: "FK_Antrenorler_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "SalonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntrenorHizmet",
                columns: table => new
                {
                    AntrenorId = table.Column<int>(type: "int", nullable: false),
                    HizmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntrenorHizmet", x => new { x.AntrenorId, x.HizmetId });
                    table.ForeignKey(
                        name: "FK_AntrenorHizmet_Antrenorler_AntrenorId",
                        column: x => x.AntrenorId,
                        principalTable: "Antrenorler",
                        principalColumn: "AntrenorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntrenorHizmet_Hizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmetler",
                        principalColumn: "HizmetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntrenorUzmanlik",
                columns: table => new
                {
                    AntrenorId = table.Column<int>(type: "int", nullable: false),
                    UzmanlikAlaniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntrenorUzmanlik", x => new { x.AntrenorId, x.UzmanlikAlaniId });
                    table.ForeignKey(
                        name: "FK_AntrenorUzmanlik_Antrenorler_AntrenorId",
                        column: x => x.AntrenorId,
                        principalTable: "Antrenorler",
                        principalColumn: "AntrenorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntrenorUzmanlik_UzmanlikAlanlari_UzmanlikAlaniId",
                        column: x => x.UzmanlikAlaniId,
                        principalTable: "UzmanlikAlanlari",
                        principalColumn: "UzmanlikAlaniId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorHizmet_HizmetId",
                table: "AntrenorHizmet",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Antrenorler_KullaniciId",
                table: "Antrenorler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Antrenorler_SalonId",
                table: "Antrenorler",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorUzmanlik_UzmanlikAlaniId",
                table: "AntrenorUzmanlik",
                column: "UzmanlikAlaniId");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_UzmanlikAlaniId",
                table: "Hizmetler",
                column: "UzmanlikAlaniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Antrenorler_AntrenorId",
                table: "Randevular",
                column: "AntrenorId",
                principalTable: "Antrenorler",
                principalColumn: "AntrenorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Hizmetler_HizmetId",
                table: "Randevular",
                column: "HizmetId",
                principalTable: "Hizmetler",
                principalColumn: "HizmetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Salonlar_SalonId",
                table: "Randevular",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Antrenorler_AntrenorId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Hizmetler_HizmetId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Salonlar_SalonId",
                table: "Randevular");

            migrationBuilder.DropTable(
                name: "AntrenorHizmet");

            migrationBuilder.DropTable(
                name: "AntrenorUzmanlik");

            migrationBuilder.DropTable(
                name: "Hizmetler");

            migrationBuilder.DropTable(
                name: "Antrenorler");

            migrationBuilder.DropTable(
                name: "Salonlar");

            migrationBuilder.DropColumn(
                name: "OnaylandiMi",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "Saat",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "Tarih",
                table: "Randevular",
                newName: "RandevuTarih");

            migrationBuilder.RenameColumn(
                name: "SalonId",
                table: "Randevular",
                newName: "IslemId");

            migrationBuilder.RenameColumn(
                name: "HizmetId",
                table: "Randevular",
                newName: "CalisanId");

            migrationBuilder.RenameColumn(
                name: "AntrenorId",
                table: "Randevular",
                newName: "BerberId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_SalonId",
                table: "Randevular",
                newName: "IX_Randevular_IslemId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_HizmetId",
                table: "Randevular",
                newName: "IX_Randevular_CalisanId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_AntrenorId",
                table: "Randevular",
                newName: "IX_Randevular_BerberId");

            migrationBuilder.AlterColumn<string>(
                name: "UzmanlikAlaniAdi",
                table: "UzmanlikAlanlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "BaslangicSaati",
                table: "Randevular",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BitisSaati",
                table: "Randevular",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RandevuSaati",
                table: "Randevular",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RandevuUcret",
                table: "Randevular",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SecilenIslemler",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Berberler",
                columns: table => new
                {
                    BerberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BerberAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResimYolu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToplamKazanc = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berberler", x => x.BerberId);
                });

            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    IslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzmanlikAlaniId = table.Column<int>(type: "int", nullable: false),
                    IslemAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslemFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.IslemId);
                    table.ForeignKey(
                        name: "FK_Islemler_UzmanlikAlanlari_UzmanlikAlaniId",
                        column: x => x.UzmanlikAlaniId,
                        principalTable: "UzmanlikAlanlari",
                        principalColumn: "UzmanlikAlaniId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BerberId = table.Column<int>(type: "int", nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: true),
                    CalisanAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalisanBaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: true),
                    CalisanBitisSaati = table.Column<TimeSpan>(type: "time", nullable: true),
                    CalisanSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalisanTelefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.CalisanId);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Berberler_BerberId",
                        column: x => x.BerberId,
                        principalTable: "Berberler",
                        principalColumn: "BerberId");
                    table.ForeignKey(
                        name: "FK_Calisanlar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId");
                });

            migrationBuilder.CreateTable(
                name: "CalisanUzmanlikAlani",
                columns: table => new
                {
                    CalisanId = table.Column<int>(type: "int", nullable: false),
                    UzmanlikAlaniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanUzmanlikAlani", x => new { x.CalisanId, x.UzmanlikAlaniId });
                    table.ForeignKey(
                        name: "FK_CalisanUzmanlikAlani_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanUzmanlikAlani_UzmanlikAlanlari_UzmanlikAlaniId",
                        column: x => x.UzmanlikAlaniId,
                        principalTable: "UzmanlikAlanlari",
                        principalColumn: "UzmanlikAlaniId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_BerberId",
                table: "Calisanlar",
                column: "BerberId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlikAlani_UzmanlikAlaniId",
                table: "CalisanUzmanlikAlani",
                column: "UzmanlikAlaniId");

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_UzmanlikAlaniId",
                table: "Islemler",
                column: "UzmanlikAlaniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Berberler_BerberId",
                table: "Randevular",
                column: "BerberId",
                principalTable: "Berberler",
                principalColumn: "BerberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Islemler_IslemId",
                table: "Randevular",
                column: "IslemId",
                principalTable: "Islemler",
                principalColumn: "IslemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
