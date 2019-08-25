using MvcTemplate.Objects;
using MvcTemplate.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace MvcTemplate.Data.Migrations.Tests
{
    public class InitialDataTests : IDisposable
    {
        private Configuration configuration;
        private TestingContext context;

        public InitialDataTests()
        {
            context = new TestingContext();
            configuration = new Configuration(context, null);
            configuration.SeedData();
        }
        public void Dispose()
        {
            configuration.Dispose();
            context.Dispose();
        }

        [Fact]
        public void RolesTable_HasSysAdmin()
        {
            Assert.Single(context.Set<Role>(), role => role.Title == "Sys_Admin");
        }

        [Fact]
        public void AccountsTable_HasAdmin()
        {
            Assert.Single(context.Set<Account>(), account => account.Username == "admin" && account.Role.Title == "Sys_Admin");
        }

        [Theory]
        [InlineData("Administration", "Accounts", "Index")]
        [InlineData("Administration", "Accounts", "Create")]
        [InlineData("Administration", "Accounts", "Details")]
        [InlineData("Administration", "Accounts", "Edit")]
        [InlineData("Administration", "Roles", "Index")]
        [InlineData("Administration", "Roles", "Create")]
        [InlineData("Administration", "Roles", "Details")]
        [InlineData("Administration", "Roles", "Edit")]
        [InlineData("Administration", "Roles", "Delete")]
        [InlineData("Membership", "Customer", "Index")]
        [InlineData("Membership", "Customer", "Create")]
        [InlineData("Membership", "Customer", "Details")]
        [InlineData("Membership", "Customer", "Edit")]
        [InlineData("Membership", "Customer", "Delete")]
        [InlineData("Common", "Address", "Index")]
        [InlineData("Common", "Address", "Create")]
        [InlineData("Common", "Address", "Details")]
        [InlineData("Common", "Address", "Edit")]
        [InlineData("Common", "Address", "Delete")]
        [InlineData("Common", "Contact", "Index")]
        [InlineData("Common", "Contact", "Create")]
        [InlineData("Common", "Contact", "Details")]
        [InlineData("Common", "Contact", "Edit")]
        [InlineData("Common", "Contact", "Delete")]
        [InlineData("Common", "ContactType", "Index")]
        [InlineData("Common", "ContactType", "Create")]
        [InlineData("Common", "ContactType", "Details")]
        [InlineData("Common", "ContactType", "Edit")]
        [InlineData("Common", "ContactType", "Delete")]
        [InlineData("Common", "AddressType", "Index")]
        [InlineData("Common", "AddressType", "Create")]
        [InlineData("Common", "AddressType", "Details")]
        [InlineData("Common", "AddressType", "Edit")]
        [InlineData("Common", "AddressType", "Delete")]
        public void PermissionsTable_HasPermission(String area, String controller, String action)
        {
            Assert.Single(context.Set<Permission>(), permission =>
                permission.Controller == controller &&
                permission.Action == action &&
                permission.Area == area);
        }

        [Fact]
        public void PermissionsTable_HasExactNumberOfPermissions()
        {
            Int32 actual = context.Set<Permission>().Count();
            Int32 expected = GetType()
                .GetMethod(nameof(PermissionsTable_HasPermission))
                .GetCustomAttributes<InlineDataAttribute>()
                .Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RolesPermissionsTable_HasAllSysAdminPermissions()
        {
            IEnumerable<Int32> expected = context
                .Set<Permission>()
                .Select(permission => permission.Id)
                .OrderBy(permissionId => permissionId);

            IEnumerable<Int32> actual = context
                .Set<RolePermission>()
                .Where(permission => permission.Role.Title == "Sys_Admin")
                .Select(rolePermission => rolePermission.PermissionId)
                .OrderBy(permissionId => permissionId);

            Assert.Equal(expected, actual);
        }
    }
}
