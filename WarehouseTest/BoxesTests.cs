using Xunit;
using System;
using WarehouseApp.Data;
using WarehouseApp.Data.DTO;
using FluentAssertions;

namespace WarehouseTest
{
    public class BoxesTests
    {
        [Fact(DisplayName = "Box volume calculates from dimensions")]
        public void BoxVolumeCalculatesCorrect()
        {
            Box box = new(3, 5, 7, 11, DateTime.Now);

            Assert.Equal(105, box.Volume);
        }
        [Fact(DisplayName = "Box can store expiration date from constructor parameters")]
        public void BoxCanStoreExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, today);

            Assert.Equal(today, box.ExpirationDate);
        }
        [Fact(DisplayName = "Box can calculate expiration date from production date in constructor parameters")]
        public void BoxCanCalculateExpirationDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, produced: today);
            DateTime after100Days = today + TimeSpan.FromDays(100);

            Assert.Equal(after100Days, box.ExpirationDate);
        }
        [Fact(DisplayName = "Box takes expiration date in priority with production date in constructor parameters")]
        public void BoxCanCalculateExpirationDateWithinProducedDate()
        {
            DateTime today = DateTime.Today;
            Box box = new(3, 5, 7, 11, expirationDate: today, produced: today);

            Assert.Equal(today, box.ExpirationDate);
        }
        [Fact(DisplayName = "Constructor without dates should throw exception")]
        public void BoxCreationThrowsExceptionWithoutDates()
        {
            Action action = () => new Box(3, 5, 7, 11);

            action.Should().Throw<ArgumentNullException>("Creating box without dates should throw exception");
        }
        [Fact(DisplayName = "Can convert Box to BoxDTO")]
        public void BoxCanConvertToBoxDto()
        {
            Box box = new(3, 5, 7, 11, DateTime.Today);

            BoxDto boxDto = box.ToBoxDto();

            box.Length.Should().Be(boxDto.Length);
            box.Height.Should().Be(boxDto.Height);
            box.Width.Should().Be(boxDto.Width);
            box.Weight.Should().Be(boxDto.Weight);
            box.ExpirationDate.Should().Be(boxDto.ExpirationDate);
        }
        [Fact(DisplayName = "Can convert BoxDto to Box")]
        public void BoxDtoCanConvertToBox()
        {
            BoxDto boxDto = new()
            {
                Length = 3,
                Height = 5,
                Width = 7,
                Weight = 11,
                ExpirationDate = DateTime.Today
            };

            Box box = boxDto.ToBox();

            box.Length.Should().Be(boxDto.Length);
            box.Height.Should().Be(boxDto.Height);
            box.Width.Should().Be(boxDto.Width);
            box.Weight.Should().Be(boxDto.Weight);
            box.ExpirationDate.Should().Be(boxDto.ExpirationDate);
        }
    }
}
