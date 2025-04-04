using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    UnitType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    IdNumber = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Managers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AvgRating = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Staffs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LeaveStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LeaveEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LeaveReasons = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BookingDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceUnitAmount = table.Column<int>(type: "integer", nullable: true),
                    TotalPrice = table.Column<float>(type: "real", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompletionReports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReportText = table.Column<string>(type: "text", nullable: true),
                    ReportDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletionReports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_CompletionReports_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentPaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: true),
                    RatingStar = table.Column<int>(type: "integer", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ReviewDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportImages",
                columns: table => new
                {
                    ReportImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportImages", x => x.ReportImageId);
                    table.ForeignKey(
                        name: "FK_ReportImages_CompletionReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "CompletionReports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    RefundId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: true),
                    RefundAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    RefundReason = table.Column<string>(type: "text", nullable: true),
                    RefundDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_Refunds_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName", "UnitPrice", "UnitType" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "Professional home cleaning service", "Cleaning", 90000m, "hour" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "Childcare service for one child", "Child care (1 child)", 150000m, "hour" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "Childcare service for two children", "Child care (2 children)", 200000m, "hour" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "Professional cooking service", "Cooking", 80000m, "dish" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "Email", "FullName", "Gender", "IdNumber", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@monkey5.com", "Admin User", "Male", "123456789", "7Trozvev7mZfiH4yPNWQ3g==.EeWONvf9XNaeuCSMVSCYJjS8TCgtcxGZTLPc/chQtMo=", "0123456789", "Manager" },
                    { new Guid("40000000-0000-0000-0000-000000000001"), new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvana@example.com", "Nguyen Van A", "Male", "123456789012", "wFtFzMGFPfPu3yIQtY1XRg==.ndXmscRKdJHHWo/2w1hiYFBTte5EcpK+3zt4nvD5tiY=", "0123456781", "Customer" },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new DateTime(1990, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), "tranthib@example.com", "Tran Thi B", "Female", "234567890123", "o9U9hbUaBzcb+dWatboRxw==.8+Cx1LWUEY942DKPkgakS/JWC5Osh/+CDgZi2EtecpA=", "0123456782", "Customer" },
                    { new Guid("40000000-0000-0000-0000-000000000003"), new DateTime(1988, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "levanc@example.com", "Le Van C", "Male", "345678901234", "QhriILb+9JwlA6bu1zXHLQ==.xYDzkIAI9Y5WGN9J8n/yF1Vt2LTqsqy1dGV5yNQXv24=", "0123456783", "Customer" },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(1992, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "phamthid@example.com", "Pham Thi D", "Female", "456789012345", "vn3Y0Jf0D6mKfyPh3uqL2A==.3BH55NZDBzSTuD/KaSqE1QWEhFV4m0kOtm7iK4kZuvc=", "0234567891", "Staff" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(1991, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), "hoangvane@example.com", "Hoang Van E", "Male", "567890123456", "poAttC5XrQyWxaGpk0pH1Q==.zE4UfEcjdq/HmKJ8k5rvomOHABg6SF8pB4rLskqtOB0=", "0234567892", "Staff" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new DateTime(1989, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthif@example.com", "Nguyen Thi F", "Female", "678901234567", "IKFsjJQcHE7GatboiKWlJA==.o4WuejYQH1oxumkiTDrid+hgWiVD7p5bV8FhFWy7EEA=", "0234567893", "Staff" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(1993, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "tranvang@example.com", "Tran Van G", "Male", "789012345678", "+VEJWg4cOOri1wCLf+Wq6w==.KNezYBTe6v/h1ipl6Jvba13EAO9yn5AghAUrfOC21OI=", "0234567894", "Staff" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(1987, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), "lethih@example.com", "Le Thi H", "Female", "890123456789", "Epoi0VQZT6tlazmJUGZfDA==.I2fsRs5lFgG0GJk7UbzBF3FKzr3zvObtN6HLkt4Sd74=", "0234567895", "Staff" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new DateTime(1990, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), "phamvani@example.com", "Pham Van I", "Male", "901234567890", "CUsgA6CBKZlyZ4uni6sdDA==.T8BZKWZwjKrd4UtP8xdpWQC+gkvzce/MHIBcIu6UTBo=", "0234567896", "Staff" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "UserId", "RegistrationDate" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40000000-0000-0000-0000-000000000003"), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "UserId",
                value: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "UserId", "AvgRating", "Status" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), 4.5, 0 },
                    { new Guid("50000000-0000-0000-0000-000000000002"), 4.2000000000000002, 0 },
                    { new Guid("50000000-0000-0000-0000-000000000003"), 4.7999999999999998, 0 },
                    { new Guid("50000000-0000-0000-0000-000000000004"), 4.2999999999999998, 0 },
                    { new Guid("50000000-0000-0000-0000-000000000005"), 4.9000000000000004, 0 },
                    { new Guid("50000000-0000-0000-0000-000000000006"), 4.5999999999999996, 0 }
                });

            migrationBuilder.InsertData(
                table: "LeaveRequests",
                columns: new[] { "RequestId", "LeaveEnd", "LeaveReasons", "LeaveStart", "RequestDate", "StaffId" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Family vacation planned months in advance.", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001") },
                    { new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Medical appointment and recovery.", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000003") },
                    { new Guid("c0000000-0000-0000-0000-000000000003"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Professional development course on advanced culinary techniques.", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000005") }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Address", "City", "Country", "CustomerId", "PostalCode" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "123 Nguyen Hue Street", "Ho Chi Minh City", "Vietnam", new Guid("40000000-0000-0000-0000-000000000001"), "70000" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "456 Le Loi Street", "Hanoi", "Vietnam", new Guid("40000000-0000-0000-0000-000000000001"), "10000" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "789 Tran Hung Dao Street", "Da Nang", "Vietnam", new Guid("40000000-0000-0000-0000-000000000002"), "50000" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "101 Ba Trieu Street", "Hai Phong", "Vietnam", new Guid("40000000-0000-0000-0000-000000000002"), "18000" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "202 Le Duan Street", "Can Tho", "Vietnam", new Guid("40000000-0000-0000-0000-000000000003"), "90000" },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "303 Quang Trung Street", "Nha Trang", "Vietnam", new Guid("40000000-0000-0000-0000-000000000003"), "65000" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookingDateTime", "CustomerId", "LocationId", "Note", "ServiceEndTime", "ServiceId", "ServiceStartTime", "ServiceUnitAmount", "StaffId", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001"), "Please focus on kitchen and bathroom cleaning. Customer has a cat, so be careful when entering.", new DateTime(2025, 3, 3, 13, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2025, 3, 3, 10, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("50000000-0000-0000-0000-000000000001"), 3, 270000f },
                    { new Guid("60000000-0000-0000-0000-000000000002"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000003"), "Child is 5 years old and has homework to complete. Allergic to peanuts.", new DateTime(2025, 3, 4, 18, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2025, 3, 4, 14, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("50000000-0000-0000-0000-000000000003"), 3, 600000f },
                    { new Guid("60000000-0000-0000-0000-000000000003"), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000005"), "Family prefers vegetarian dishes. Please use less spicy ingredients.", new DateTime(2025, 3, 5, 18, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), new DateTime(2025, 3, 5, 15, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("50000000-0000-0000-0000-000000000005"), 3, 400000f },
                    { new Guid("60000000-0000-0000-0000-000000000004"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000002"), "Deep cleaning needed for living room. Customer will provide special cleaning products for wooden furniture.", new DateTime(2025, 3, 6, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2025, 3, 6, 7, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("50000000-0000-0000-0000-000000000002"), 1, 360000f },
                    { new Guid("60000000-0000-0000-0000-000000000005"), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000004"), "Two children ages 3 and 6. The older child has online classes from 9-10 AM. Both children need lunch prepared.", new DateTime(2025, 3, 7, 13, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), new DateTime(2025, 3, 7, 8, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("50000000-0000-0000-0000-000000000004"), 1, 1000000f }
                });

            migrationBuilder.InsertData(
                table: "CompletionReports",
                columns: new[] { "ReportId", "BookingId", "ReportDateTime", "ReportText" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001"), new DateTime(2025, 3, 3, 13, 15, 0, 0, DateTimeKind.Utc), "Cleaned living room, kitchen, 2 bathrooms, and 3 bedrooms. All surfaces dusted and floors mopped." },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), new Guid("60000000-0000-0000-0000-000000000002"), new DateTime(2025, 3, 4, 18, 20, 0, 0, DateTimeKind.Utc), "Took care of the child, prepared lunch, helped with homework, and played educational games." },
                    { new Guid("a0000000-0000-0000-0000-000000000003"), new Guid("60000000-0000-0000-0000-000000000003"), new DateTime(2025, 3, 5, 18, 10, 0, 0, DateTimeKind.Utc), "Prepared 5 dishes: spring rolls, pho, grilled chicken, stir-fried vegetables, and mango sticky rice for dessert." }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "BookingId", "PaymentCreatedAt", "PaymentMethod", "PaymentPaidAt", "PaymentStatus" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), 270000m, new Guid("60000000-0000-0000-0000-000000000001"), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2025, 3, 2, 1, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("70000000-0000-0000-0000-000000000002"), 600000m, new Guid("60000000-0000-0000-0000-000000000002"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2025, 3, 4, 18, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("70000000-0000-0000-0000-000000000003"), 400000m, new Guid("60000000-0000-0000-0000-000000000003"), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2025, 3, 4, 2, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("70000000-0000-0000-0000-000000000004"), 360000m, new Guid("60000000-0000-0000-0000-000000000004"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, null, 1 },
                    { new Guid("70000000-0000-0000-0000-000000000005"), 1000000m, new Guid("60000000-0000-0000-0000-000000000005"), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "BookingId", "Comment", "RatingStar", "ReviewDateTime" },
                values: new object[,]
                {
                    { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001"), "Excellent cleaning service, very thorough and professional!", 5, new DateTime(2025, 3, 3, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80000000-0000-0000-0000-000000000002"), new Guid("60000000-0000-0000-0000-000000000002"), "Great childcare service, my child was very happy.", 4, new DateTime(2025, 3, 4, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80000000-0000-0000-0000-000000000003"), new Guid("60000000-0000-0000-0000-000000000003"), "Amazing cooking! The dishes were delicious and beautifully presented.", 5, new DateTime(2025, 3, 5, 19, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Refunds",
                columns: new[] { "RefundId", "PaymentId", "RefundAmount", "RefundDateTime", "RefundReason" },
                values: new object[] { new Guid("90000000-0000-0000-0000-000000000001"), new Guid("70000000-0000-0000-0000-000000000003"), 100000m, new DateTime(2025, 3, 5, 2, 0, 0, 0, DateTimeKind.Utc), "One dish was not prepared as requested" });

            migrationBuilder.InsertData(
                table: "ReportImages",
                columns: new[] { "ReportImageId", "ImagePath", "ReportId" },
                values: new object[,]
                {
                    { new Guid("b0000000-0000-0000-0000-000000000001"), "cleaning_living_room.jpg", new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("b0000000-0000-0000-0000-000000000002"), "cleaning_kitchen.jpg", new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("b0000000-0000-0000-0000-000000000003"), "childcare_lunch.jpg", new Guid("a0000000-0000-0000-0000-000000000002") },
                    { new Guid("b0000000-0000-0000-0000-000000000004"), "cooking_spring_rolls.jpg", new Guid("a0000000-0000-0000-0000-000000000003") },
                    { new Guid("b0000000-0000-0000-0000-000000000005"), "cooking_pho.jpg", new Guid("a0000000-0000-0000-0000-000000000003") },
                    { new Guid("b0000000-0000-0000-0000-000000000006"), "cooking_dessert.jpg", new Guid("a0000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LocationId",
                table: "Bookings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ServiceId",
                table: "Bookings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StaffId",
                table: "Bookings",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionReports_BookingId",
                table: "CompletionReports",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_StaffId",
                table: "LeaveRequests",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CustomerId",
                table: "Locations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_PaymentId",
                table: "Refunds",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportImages_ReportId",
                table: "ReportImages",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropTable(
                name: "ReportImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "CompletionReports");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
