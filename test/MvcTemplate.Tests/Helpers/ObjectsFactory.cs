using MvcTemplate.Objects;
using System;
using System.Collections.Generic;

namespace MvcTemplate.Tests
{
    public static class ObjectsFactory
    {
        public static Account CreateAccount(Int32 id = 0)
        {
            return new Account
            {
                Id = id,

                Username = $"Username{id}",
                Passhash = $"Passhash{id}",

                Email = $"{id}@tests.com",

                IsLocked = false,

                RecoveryToken = $"Token{id}",
                RecoveryTokenExpirationDate = DateTime.Now.AddMinutes(5),

                Role = CreateRole(id)
            };
        }
        public static AccountView CreateAccountView(Int32 id = 0)
        {
            return new AccountView
            {
                Id = id,

                Username = $"Username{id}",
                Email = $"{id}@tests.com",

                IsLocked = true,

                RoleTitle = $"Title{id}"
            };
        }
        public static AccountEditView CreateAccountEditView(Int32 id = 0)
        {
            return new AccountEditView
            {
                Id = id,

                Username = $"Username{id}",
                Email = $"{id}@tests.com",

                IsLocked = true,

                RoleId = id
            };
        }
        public static AccountCreateView CreateAccountCreateView(Int32 id = 0)
        {
            return new AccountCreateView
            {
                Id = id,

                Username = $"Username{id}",
                Password = $"Password{id}",

                Email = $"{id}@tests.com",

                RoleId = id
            };
        }

        public static AccountLoginView CreateAccountLoginView(Int32 id = 0)
        {
            return new AccountLoginView
            {
                Id = id,

                Username = $"Username{id}",
                Password = $"Password{id}"
            };
        }
        public static AccountResetView CreateAccountResetView(Int32 id = 0)
        {
            return new AccountResetView
            {
                Id = id,

                Token = $"Token{id}",
                NewPassword = $"NewPassword{id}"
            };
        }
        public static AccountRecoveryView CreateAccountRecoveryView(Int32 id = 0)
        {
            return new AccountRecoveryView
            {
                Id = id,

                Email = $"{id}@tests.com"
            };
        }

        public static ProfileEditView CreateProfileEditView(Int32 id = 0)
        {
            return new ProfileEditView
            {
                Id = id,

                Email = $"{id}@tests.com",
                Username = $"Username{id}",

                Password = $"Password{id}",
                NewPassword = $"NewPassword{id}"

            };
        }
        public static ProfileDeleteView CreateProfileDeleteView(Int32 id = 0)
        {
            return new ProfileDeleteView
            {
                Id = id,

                Password = $"Password{id}"
            };
        }

        public static Role CreateRole(Int32 id = 0)
        {
            return new Role
            {
                Id = id,

                Title = $"Title{id}",

                Accounts = new List<Account>(),
                Permissions = new List<RolePermission>()
            };
        }
        public static RoleView CreateRoleView(Int32 id = 0)
        {
            return new RoleView
            {
                Id = id,

                Title = $"Title{id}"
            };
        }

        public static Permission CreatePermission(Int32 id = 0)
        {
            return new Permission
            {
                Id = id,

                Area = $"Area{id}",
                Action = $"Action{id}",
                Controller = $"Controller{id}"
            };
        }
        public static RolePermission CreateRolePermission(Int32 id = 0)
        {
            return new RolePermission
            {
                Id = id,

                RoleId = id,
                Role = CreateRole(id),

                PermissionId = id,
                Permission = CreatePermission(id)
            };
        }

        public static TestModel CreateTestModel(Int32 id = 0)
        {
            return new TestModel
            {
                Id = id,

                Title = $"Title{id}"
            };
        }

        public static Customer CreateCustomer(Int32 id = 0)
        {
            return new Customer
            {
                 Id = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }
        public static CustomerView CreateCustomerView(Int32 id = 0)
        {
            return new CustomerView
            {
                 Id = id,
                 CreatedBy = id,
                 ModifiedBy = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }

        public static Address CreateAddress(Int32 id = 0)
        {
            return new Address
            {
                 Id = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }
        public static AddressView CreateAddressView(Int32 id = 0)
        {
            return new AddressView
            {
                 Id = id,
                 CreatedBy = id,
                 ModifiedBy = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }

        public static Contact CreateContact(Int32 id = 0)
        {
            return new Contact
            {
                 Id = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }
        public static ContactView CreateContactView(Int32 id = 0)
        {
            return new ContactView
            {
                 Id = id,
                 CreatedBy = id,
                 ModifiedBy = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }

        public static ContactType CreateContactType(Int32 id = 0)
        {
            return new ContactType
            {
                 Id = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }
        public static ContactTypeView CreateContactTypeView(Int32 id = 0)
        {
            return new ContactTypeView
            {
                 Id = id,
                 CreatedBy = id,
                 ModifiedBy = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }

        public static AddressType CreateAddressType(Int32 id = 0)
        {
            return new AddressType
            {
                 Id = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }
        public static AddressTypeView CreateAddressTypeView(Int32 id = 0)
        {
            return new AddressTypeView
            {
                 Id = id,
                 CreatedBy = id,
                 ModifiedBy = id,
                 ModificationDate = DateTime.Now.AddDays(id),
                 CreatedByAccountId = id,
                 ModifiedByAccountId = id
            };
        }
    }
}
