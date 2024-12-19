using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CycleShare.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Address table
            string[] streets = ["Beresteiskyi avenue", "Khreshchatyk street", "Saksahanskoho street",
                                "Velyka Vasylkivska street", "Holosiivskyi avenue", "Drahomanova street",
                                "Kudriavska street", "Mezhyhirska Street", "Podilska Street",
                                "Hlybochytska Street", "Yaroslaviv Val Street", "Shota Rustaveli Street",
                                "Pankivska Street", "Reitarska Street", "Sofiivska Street",
                                "Mikhailivska Street", "Lvivska Street", "Kostiantynivska Street",
                                "Dmytrivska Street", "Dehtiarivska Street"];
            int maxBuildingNumber = 30;
            int addressesNumber = 100;
            for (int i = 0; i < addressesNumber; i++)
            {
                migrationBuilder.InsertData(
                    table: "Address",
                    columns: new[] { "Id", "City", "Street", "Building", "Note" },
                    values: new object[] { i+1, "Kyiv", streets[i % streets.Length], $"{i % maxBuildingNumber}",
                                           $"Near building {i % maxBuildingNumber}" }
                );
            }

            // User table
            migrationBuilder.InsertData(
                    table: "User",
                    columns: new[] { "Id", "FirstName", "LastName", "Gender", "Email", "PhoneNumber", "CreatedDate", "Deleted" },
                    values: new object[] { 1, "John", "Smith", "m", $"john@example.com",
                                           $"123456789", DateTime.UtcNow.AddDays(-1100 + new Random().Next(30)), null}
                );

            // Credentials table
            migrationBuilder.InsertData(
                    table: "Credentials",
                    columns: new[] { "Id", "Password", "Salt", "UserId", "Deleted" },
                    values: new object[] { 1, new byte[] { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 },
                                           new byte[] { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 }, 1, null }
                );

            // Bike table
            string[] names = [  "Specialized Stumpjumper Bike",
                                "Raleigh Skarn full suspension bike",
                                "Specialized Diverge Road Bike - Large",
                                "Fat Chance Old School Bike",
                                "KONA Private Jake Gravel CX, Medium, with suspension",
                                "Bianchi Volpe bike",
                                "Orbea Orca M20 57cm",
                                "BMC Speedfox Small bike",
                                "Specialized Enduro Size Large Bike",
                                "Specialized Status 160 Size large",
                                "Whyte T-130 Small Bike",
                                "Co-op Fender bike size large",
                                "Trek ProCaliber Cross Country Bike",
                                "Co-op ADV bike 56cm",
                                "Schwinn Women's Trailway Bike",
                                "YT Tues CF Pro carbon downhill bike",
                                "Felt Breed Single Speed Cyclocross Bike",
                                "Salsa Ti Fargo - XL",
                                "Full speed and full suspension fun",
                                "Redline d460 29er Bike",
                                "Specialized Sirrus bike, size medium"];
            string[] types = ["Road", "Hybrid", "Mountain"];
            string[] materials = ["Aluminium", "Steel", "Carbon"];
            string[] sizes = ["24 in", "26 in", "28 in", "29 in"];
            int bikesNumber = names.Length;
            for (int i = 0; i < bikesNumber; i++)
            {
                migrationBuilder.InsertData(
                    table: "Bike",
                    columns: new[] { "Id", "Name", "Type", "Material", "Wheelsize", "Description", "Price", "Rating", "Active", "AddressId", "UserId", "CreatedDate" },
                    values: new object[] { i+1, names[i], types[i % types.Length], materials[i % materials.Length],
                                           sizes[i % sizes.Length], names[i], 20 + 5 * (new Random().Next(0, 10)), (decimal)0.1 * (new Random().Next(30, 50)), true, i+1, 1,
                                           DateTime.UtcNow.AddDays(-1000 + new Random().Next(30)) }
                );
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Address\";");
            migrationBuilder.Sql("DELETE FROM \"Credentials\";");
            migrationBuilder.Sql("DELETE FROM \"User\";");
            migrationBuilder.Sql("DELETE FROM \"Bike\";");
        }
    }
}
