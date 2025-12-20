using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class IlkKurulum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminTelefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

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
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciTelefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciSifre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciSifreTekrar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciRolu = table.Column<int>(type: "int", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "UzmanlikAlanlari",
                columns: table => new
                {
                    UzmanlikAlaniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzmanlikAlaniAdi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzmanlikAlanlari", x => x.UzmanlikAlaniId);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalisanSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalisanTelefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalisanBaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: true),
                    CalisanBitisSaati = table.Column<TimeSpan>(type: "time", nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: true),
                    BerberId = table.Column<int>(type: "int", nullable: true)
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
                name: "Islemler",
                columns: table => new
                {
                    IslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IslemAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslemFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UzmanlikAlaniId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandevuTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RandevuSaati = table.Column<TimeSpan>(type: "time", nullable: true),
                    BaslangicSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RandevuUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IslemId = table.Column<int>(type: "int", nullable: false),
                    SecilenIslemler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BerberId = table.Column<int>(type: "int", nullable: false),
                    CalisanId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuId);
                    table.ForeignKey(
                        name: "FK_Randevular_Berberler_BerberId",
                        column: x => x.BerberId,
                        principalTable: "Berberler",
                        principalColumn: "BerberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Islemler_IslemId",
                        column: x => x.IslemId,
                        principalTable: "Islemler",
                        principalColumn: "IslemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_BerberId",
                table: "Randevular",
                column: "BerberId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_IslemId",
                table: "Randevular",
                column: "IslemId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "CalisanUzmanlikAlani");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropTable(
                name: "Berberler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "UzmanlikAlanlari");
        }
    }
}
