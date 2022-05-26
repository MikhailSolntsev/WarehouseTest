using Xunit;

using WarehouseApp;

namespace WarehouseTest
{
    public class BoxesTests
    {
        [Fact]
        public void BoxesCreatesCorrect()
        {
            Box box = new();
        }
    }
}