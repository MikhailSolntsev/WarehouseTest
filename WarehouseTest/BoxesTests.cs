using Xunit;
using System;
using WarehouseApp.Data;

namespace WarehouseTest
{
    public class BoxesTests
    {
        [Fact]
        public void BoxVolumeCalculatesCorrect()
        {
            Box box = new(3, 5, 7, 11, DateTime.Now);
            Assert.Equal(105, box.Volume);
        }
        [Fact]
        public void BoxCanStoreExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, today);
            Assert.Equal(today, box.ExpirationDate);
        }
        [Fact]
        public void BoxCanCalculateExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, produced: today);
            DateTime after100Days = today + TimeSpan.FromDays(100);
            Assert.Equal(after100Days, box.ExpirationDate);
        }
        [Fact]
        public void BoxCreationThrowsExceptionWithoutDates()
        {
            try
            {
                Box box = new(3, 5, 7, 11);
                Assert.True(false, "Box creating without dates does not throws any exception");
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentNullException), ex.GetType());
            }
        }
    }
}