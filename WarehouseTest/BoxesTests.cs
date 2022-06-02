using Xunit;
using System;
using WarehouseApp.Data;

namespace WarehouseTest
{
    public class BoxesTests
    {
        [Fact(DisplayName = "Объём короба вычисляется корректно из размеров")]
        public void BoxVolumeCalculatesCorrect()
        {
            Box box = new(3, 5, 7, 11, DateTime.Now);
            Assert.Equal(105, box.Volume);
        }
        [Fact(DisplayName = "Короб может сохранять дату срока годности из параметров")]
        public void BoxCanStoreExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, today);
            Assert.Equal(today, box.ExpirationDate);
        }
        [Fact(DisplayName = "Короб может вычислять дату срока годности из даты производства")]
        public void BoxCanCalculateExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, produced: today);
            DateTime after100Days = today + TimeSpan.FromDays(100);
            Assert.Equal(after100Days, box.ExpirationDate);
        }
        [Fact(DisplayName = "Короб берёт дату срока годности если в приоритете от даты производства")]
        public void BoxCanCalculateExpirationDateWithinProducedDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, expirationDate: today, produced: today);
            Assert.Equal(today, box.ExpirationDate);
        }
        [Fact(DisplayName = "Если в конструктор не передана ни одна дата, бросается исключение")]
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