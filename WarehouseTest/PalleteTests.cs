using Xunit;

using WarehouseApp;

namespace WarehouseTest
{
    public class PalleteTests
    {
        [Fact]
        public void BoxVolumeCalculatesCorrect()
        {
            Box box = new(10, 20, 30);
            Assert.Equal(6000, box.Volume);
        }
    }
}