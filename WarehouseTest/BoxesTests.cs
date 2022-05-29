using Xunit;
using System;

using WarehouseApp;

namespace WarehouseTest
{
    public class BoxesTests
    {
        [Fact]
        public void BoxVolumeCalculatesCorrect()
        {
            Box box = new(10, 20, 30, DateTime.Now);
            Assert.Equal(6000, box.Volume);
        }
        [Fact]
        public void BoxCanStoreExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(2, 3, 4, bestBefore: today);
            Assert.Equal(today, box.ExpirationDate);
        }
        [Fact]
        public void BoxCanCalculateExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(2, 3, 4, produced: today);
            DateTime after100Days = today + TimeSpan.FromDays(100);
            Assert.Equal(after100Days, box.ExpirationDate);
        }
        [Fact]
        public void BoxCreationThrowsExceptionWithoutDates()
        {
            try
            {
                Box box = new(1, 2, 3);
                Assert.True(false, "Box creating without dates does not throws any exception");
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentNullException), ex.GetType());
            }
        }
    }
}