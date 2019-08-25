using Microsoft.EntityFrameworkCore;
using MvcTemplate.Data.Core;
using MvcTemplate.Data.Logging;
using MvcTemplate.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTemplate.Data.Migrations
{
    public sealed class Configuration : IDisposable
    {
        private IUnitOfWork UnitOfWork { get; }
        private DbContext Context { get; }

        public Configuration(DbContext context, DbContext audit)
        {
            UnitOfWork = new UnitOfWork(context, audit == null ? null : new AuditLogger(audit, 0));
            Context = context;
        }

        public void UpdateDatabase()
        {
            Context.Database.Migrate();

            SeedData();
        }
        public void SeedData()
        {
            SeedPermissions();
            SeedRoles();

            SeedAccounts();
        }

        private void SeedPermissions()
        {
            List<Permission> permissions = new List<Permission>
            {
                new Permission { Area = "Administration", Controller = "Accounts", Action = "Index" },
                new Permission { Area = "Administration", Controller = "Accounts", Action = "Create" },
                new Permission { Area = "Administration", Controller = "Accounts", Action = "Details" },
                new Permission { Area = "Administration", Controller = "Accounts", Action = "Edit" },

                new Permission { Area = "Administration", Controller = "Roles", Action = "Index" },
                new Permission { Area = "Administration", Controller = "Roles", Action = "Create" },
                new Permission { Area = "Administration", Controller = "Roles", Action = "Details" },
                new Permission { Area = "Administration", Controller = "Roles", Action = "Edit" },
                new Permission { Area = "Administration", Controller = "Roles", Action = "Delete" },

                new Permission { Area = "Membership", Controller = "Customer", Action = "Index" },
                new Permission { Area = "Membership", Controller = "Customer", Action = "Create" },
                new Permission { Area = "Membership", Controller = "Customer", Action = "Edit" },
                new Permission { Area = "Membership", Controller = "Customer", Action = "Details" },
                new Permission { Area = "Membership", Controller = "Customer", Action = "Delete" },

                new Permission { Area = "Common", Controller = "Address", Action = "Index" },
                new Permission { Area = "Common", Controller = "Address", Action = "Create" },
                new Permission { Area = "Common", Controller = "Address", Action = "Edit" },
                new Permission { Area = "Common", Controller = "Address", Action = "Details" },
                new Permission { Area = "Common", Controller = "Address", Action = "Delete" },

                new Permission { Area = "Common", Controller = "Contact", Action = "Index" },
                new Permission { Area = "Common", Controller = "Contact", Action = "Create" },
                new Permission { Area = "Common", Controller = "Contact", Action = "Edit" },
                new Permission { Area = "Common", Controller = "Contact", Action = "Details" },
                new Permission { Area = "Common", Controller = "Contact", Action = "Delete" },

                new Permission { Area = "Common", Controller = "ContactType", Action = "Index" },
                new Permission { Area = "Common", Controller = "ContactType", Action = "Create" },
                new Permission { Area = "Common", Controller = "ContactType", Action = "Edit" },
                new Permission { Area = "Common", Controller = "ContactType", Action = "Details" },
                new Permission { Area = "Common", Controller = "ContactType", Action = "Delete" },

                new Permission { Area = "Common", Controller = "AddressType", Action = "Index" },
                new Permission { Area = "Common", Controller = "AddressType", Action = "Create" },
                new Permission { Area = "Common", Controller = "AddressType", Action = "Edit" },
                new Permission { Area = "Common", Controller = "AddressType", Action = "Details" },
                new Permission { Area = "Common", Controller = "AddressType", Action = "Delete" }
            };

            foreach (Permission permission in UnitOfWork.Select<Permission>().ToArray())
                if (permissions.RemoveAll(p => p.Area == permission.Area && p.Controller == permission.Controller && p.Action == permission.Action) == 0)
                {
                    UnitOfWork.DeleteRange(UnitOfWork.Select<RolePermission>().Where(role => role.PermissionId == permission.Id));
                    UnitOfWork.Delete(permission);
                }

            UnitOfWork.InsertRange(permissions);
            UnitOfWork.SaveChanges();
        }

        private void SeedRoles()
        {
            if (!UnitOfWork.Select<Role>().Any(role => role.Title == "Sys_Admin"))
            {
                UnitOfWork.Insert(new Role { Title = "Sys_Admin", Permissions = new List<RolePermission>() });
                UnitOfWork.SaveChanges();
            }

            Role admin = UnitOfWork.Select<Role>().Single(role => role.Title == "Sys_Admin");
            Int32[] permissions = admin.Permissions.Select(role => role.PermissionId).ToArray();

            foreach (Permission permission in UnitOfWork.Select<Permission>())
                if (!permissions.Contains(permission.Id))
                    UnitOfWork.Insert(new RolePermission { RoleId = admin.Id, PermissionId = permission.Id });

            UnitOfWork.SaveChanges();
        }

        private void SeedAccounts()
        {
            Account[] accounts =
            {
                new Account
                {
                    Username = "admin",
                    Passhash = "$2b$13$ouxA6L7QZ/eSeVZD8lawSOEwtRn/hOoRY67Pwaj/WJaZe7S4.cHJC", // Will be generated on project rename
                    Email = "admin@test.domains.com",
                    IsLocked = false,

                    RoleId = UnitOfWork.Select<Role>().Single(role => role.Title == "Sys_Admin").Id
                }
            };

            foreach (Account account in accounts)
            {
                if (UnitOfWork.Select<Account>().FirstOrDefault(model => model.Username == account.Username) is Account currentAccount)
                {
                    currentAccount.IsLocked = account.IsLocked;
                    currentAccount.RoleId = account.RoleId;

                    UnitOfWork.Update(currentAccount);
                }
                else
                {
                    UnitOfWork.Insert(account);
                }
            }

            UnitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
            Context.Dispose();
        }
    }
}
