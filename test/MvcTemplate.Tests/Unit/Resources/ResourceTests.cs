using MvcTemplate.Objects;
using MvcTemplate.Tests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Xunit;

namespace MvcTemplate.Resources.Tests
{
    public class ResourceTests
    {
        #region Set(String type)

        [Fact]
        public void Set_Same()
        {
            Object expected = Resource.Set("Test");
            Object actual = Resource.Set("Test");

            Assert.Same(expected, actual);
        }

        #endregion

        #region ForArea(String name)

        [Fact]
        public void ForArea_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/Shared", "Areas", "Administration");
            String actual = Resource.ForArea("administration");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForArea_NotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForArea("Null"));
        }

        #endregion

        #region ForAction(String name)

        [Fact]
        public void ForAction_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/Shared", "Actions", "Create");
            String actual = Resource.ForAction("create");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForAction_NotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForAction("Null"));
        }

        #endregion

        #region ForController(String name)

        [Fact]
        public void ForController_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/Shared", "Controllers", "AdministrationRoles");
            String actual = Resource.ForController("administrationroles");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForController_NotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForController("Null"));
        }

        #endregion

        #region ForLookup(String type)

        [Fact]
        public void ForLookup_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/Lookup", "Titles", "Role");
            String actual = Resource.ForLookup("role");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForLookup_NotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForLookup("Test"));
        }

        #endregion

        #region ForString(String value)

        [Fact]
        public void ForString_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/Shared", "Strings", "All");
            String actual = Resource.ForString("all");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForString_NotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForString("Null"));
        }

        #endregion

        #region ForHeader(String model)

        [Fact]
        public void ForHeader_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/Page", "Headers", "Account");
            String actual = Resource.ForHeader("account");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForHeader_NotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForHeader("Test"));
        }

        #endregion

        #region ForPage(String path)

        [Fact]
        public void ForPage_Path_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/Page", "Titles", "AdministrationRolesDetails");
            String actual = Resource.ForPage("administrationrolesdetails");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForPage_PathNotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForPage("Test"));
        }

        #endregion

        #region ForPage(IDictionary<String, Object> values)

        [Fact]
        public void ForPage_IsCaseInsensitive()
        {
            IDictionary<String, Object> values = new Dictionary<String, Object>();
            values["area"] = "administration";
            values["controller"] = "roles";
            values["action"] = "details";

            String expected = ResourceFor("Shared/Page", "Titles", "AdministrationRolesDetails");
            String actual = Resource.ForPage(values);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ForPage_WithoutArea(String area)
        {
            IDictionary<String, Object> values = new Dictionary<String, Object>();
            values["controller"] = "profile";
            values["action"] = "edit";
            values["area"] = area;

            String expected = ResourceFor("Shared/Page", "Titles", "ProfileEdit");
            String actual = Resource.ForPage(values);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForPage_NotFound_ReturnsNull()
        {
            IDictionary<String, Object> values = new Dictionary<String, Object>
            {
                ["controller"] = null,
                ["action"] = null,
                ["area"] = null
            };

            Assert.Null(Resource.ForPage(values));
        }

        #endregion

        #region ForSiteMap(String area, String controller, String action)

        [Fact]
        public void ForSiteMap_IsCaseInsensitive()
        {
            String expected = ResourceFor("Shared/SiteMap", "Titles", "AdministrationRolesIndex");
            String actual = Resource.ForSiteMap("administration", "roles", "index");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForSiteMap_WithoutControllerAndAction()
        {
            String expected = ResourceFor("Shared/SiteMap", "Titles", "Administration");
            String actual = Resource.ForSiteMap("administration", null, null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForSiteMap_NotFound_ReturnsNull()
        {
            Assert.Null(Resource.ForSiteMap("Test", "Test", "Test"));
        }

        #endregion

        #region ForProperty<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)

        [Fact]
        public void ForProperty_NotMemberLambdaExpression_ReturnNull()
        {
            Assert.Null(Resource.ForProperty<TestView, String>(view => view.ToString()));
        }

        [Fact]
        public void ForProperty_FromLambdaExpression()
        {
            String expected = ResourceFor("Views/Administration/Accounts/AccountView", "Titles", "Username");
            String actual = Resource.ForProperty<AccountView, String>(account => account.Username);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForProperty_FromLambdaExpressionRelation()
        {
            String actual = Resource.ForProperty<AccountEditView, Int32?>(account => account.RoleId);
            String expected = ResourceFor("Views/Administration/Roles/RoleView", "Titles", "Id");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForProperty_NotFoundLambdaExpression_ReturnsNull()
        {
            Assert.Null(Resource.ForProperty<AccountView, Int32>(account => account.Id));
        }

        [Fact]
        public void ForProperty_NotFoundLambdaType_ReturnsNull()
        {
            Assert.Null(Resource.ForProperty<TestView, String>(test => test.Title));
        }

        #endregion

        #region ForProperty(String view, String name)

        [Fact]
        public void ForProperty_View()
        {
            String expected = ResourceFor("Views/Administration/Accounts/AccountView", "Titles", "Username");
            String actual = Resource.ForProperty(nameof(AccountView), nameof(AccountView.Username));

            Assert.Equal(expected, actual);
        }

        #endregion

        #region ForProperty(Type view, String name)

        [Fact]
        public void ForProperty_IsCaseInsensitive()
        {
            String actual = Resource.ForProperty(typeof(AccountView), nameof(AccountView.Username).ToLower());
            String expected = ResourceFor("Views/Administration/Accounts/AccountView", "Titles", "Username");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForProperty_FromRelation()
        {
            String actual = Resource.ForProperty(typeof(Object), $"{nameof(Account)}{nameof(Account.Username)}");
            String expected = ResourceFor("Views/Administration/Accounts/AccountView", "Titles", "Username");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForProperty_FromMultipleRelations()
        {
            String actual = Resource.ForProperty(typeof(RoleView), $"{nameof(Account)}{nameof(Role)}{nameof(Account)}{nameof(Account.Username)}");
            String expected = ResourceFor("Views/Administration/Accounts/AccountView", "Titles", "Username");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForProperty_NotFoundProperty_ReturnsNull()
        {
            Assert.Null(Resource.ForProperty(typeof(AccountView), "Id"));
        }

        [Fact]
        public void ForProperty_NotFoundTypeProperty_ReturnsNull()
        {
            Assert.Null(Resource.ForProperty(typeof(TestView), "Title"));
        }

        [Fact]
        public void ForProperty_NullKey_ReturnsNull()
        {
            Assert.Null(Resource.ForProperty(typeof(RoleView), null));
        }

        #endregion

        #region ForProperty(Expression expression)

        [Fact]
        public void ForProperty_NotMemberExpression_ReturnNull()
        {
            Expression<Func<TestView, String>> lambda = (view) => view.ToString();

            Assert.Null(Resource.ForProperty(lambda.Body));
        }

        [Fact]
        public void ForProperty_FromExpression()
        {
            Expression<Func<AccountView, String>> lambda = (account) => account.Username;

            String expected = ResourceFor("Views/Administration/Accounts/AccountView", "Titles", "Username");
            String actual = Resource.ForProperty(lambda.Body);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForProperty_FromExpressionRelation()
        {
            Expression<Func<AccountEditView, Int32?>> lambda = (account) => account.RoleId;

            String expected = ResourceFor("Views/Administration/Roles/RoleView", "Titles", "Id");
            String actual = Resource.ForProperty(lambda.Body);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForProperty_NotFoundExpression_ReturnsNull()
        {
            Expression<Func<AccountView, Int32>> lambda = (account) => account.Id;

            Assert.Null(Resource.ForProperty(lambda.Body));
        }

        [Fact]
        public void ForProperty_NotFoundType_ReturnsNull()
        {
            Expression<Func<TestView, String>> lambda = (test) => test.Title;

            Assert.Null(Resource.ForProperty(lambda.Body));
        }

        #endregion

        #region Test helpers

        private String ResourceFor(String path, String group, String key)
        {
            String resource = File.ReadAllText(Path.Combine("Resources", $"{path}.json"));

            return JsonConvert.DeserializeObject<Dictionary<String, Dictionary<String, String>>>(resource)[group][key];
        }

        #endregion
    }
}
