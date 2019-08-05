using Xunit;

namespace MvcTemplate.Components.Extensions.Tests
{
    public class MvcTreeNodeTests
    {
        [Fact]
        public void MvcTreeNode_SetsTitle()
        {
            MvcTreeNode actual = new MvcTreeNode("Title");

            Assert.Equal("Title", actual.Title);
            Assert.Empty(actual.Children);
            Assert.Null(actual.Id);
        }

        [Fact]
        public void MvcTreeNode_SetsIdAndTitle()
        {
            MvcTreeNode actual = new MvcTreeNode(1, "Title");

            Assert.Equal("Title", actual.Title);
            Assert.Empty(actual.Children);
            Assert.Equal(1, actual.Id);
        }
    }
}
