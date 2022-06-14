using Xunit;
using System;
using WarehouseApp.Data;
using WarehouseApp.Data.DTO;
using FluentAssertions;

namespace WarehouseTest
{
    public class PalleteTests
    {
        [Fact(DisplayName = "Pallet's volume calculates from pallet's own and boxes")]
        public void PalleteVolumeCalculatesCorrect()
        {
            // Assign
            Pallet pallet = new(10, 10, 10);
            Box box1 = new(2, 3, 4, 0, DateTime.Now);
            Box box2 = new(4, 5, 6, 0, DateTime.Now);
            int volume = 24 + 120 + 1000;

            // Act
            pallet.AddBox(box1);
            pallet.AddBox(box2);

            // Assert
            Assert.Equal(volume, pallet.Volume);
        }
        [Fact(DisplayName = "Pallet's weight calculates from pallet's 30 and boxes")]
        public void PalleteWeightCalculatesCorrect()
        {
            // Assign
            Pallet pallet = new(10, 10, 10);
            int ownWeight = 30;
            int Weight = 5 + 7 + ownWeight;
            Box box1 = new(2, 3, 4, 5, DateTime.Now);
            Box box2 = new(4, 5, 6, 7, DateTime.Now);

            // Act
            pallet.AddBox(box1);
            pallet.AddBox(box2);

            // Assert
            Assert.Equal(Weight, pallet.Weight);
        }
        [Fact(DisplayName = "Pallet's expiration date should be DateTime.MinValue if pallet is empty")]
        public void EmptyPalletExpirationDateShouldBeMinValue()
        {
            Pallet pallet = new(10, 10, 10);

            Assert.Equal(DateTime.MinValue, pallet.ExpirationDate);
        }
        [Fact(DisplayName = "Pallet's expiration date should calculate MIN from boxes")]
        public void PalleteExpirationDateCalculatesFromBestBefore()
        {
            // Assign
            Pallet pallet = new(10, 10, 10);
            DateTime day1 = DateTime.Today + TimeSpan.FromDays(-1);
            DateTime day2 = DateTime.Today + TimeSpan.FromDays(-2);
            DateTime day3 = DateTime.Today + TimeSpan.FromDays(-3);
            DateTime day4 = DateTime.Today + TimeSpan.FromDays(-4);
            Box box1 = new(2, 3, 4, 0, day1);
            Box box2 = new(4, 5, 6, 0, day2);
            Box box3 = new(4, 5, 6, 0, day3);
            Box box4 = new(4, 5, 6, 0, day4);

            // Act
            pallet.AddBox(box2);
            pallet.AddBox(box4);
            pallet.AddBox(box3);
            pallet.AddBox(box1);

            // Assert
            Assert.Equal(day4, pallet.ExpirationDate);
        }
        [Fact(DisplayName = "Adding pallet with large height throws exception")]
        public void PalleteAddBoxLagreHeight()
        {
            // Assign
            Pallet pallet = new(10, 10, 10);
            Box box = new(11, 3, 5, 7, DateTime.Today);

            // Act
            Action action = () => pallet.AddBox(box);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>("Does not allow to add too large pallet");
        }
        [Fact(DisplayName = "Adding pallet with large width throws exception")]
        public void PalleteAddBoxLagreWidth()
        {
            // Assign
            Pallet pallet = new(10, 10, 10);
            Box box = new(3, 11, 5, 7, DateTime.Today);

            // Act
            Action action = () => pallet.AddBox(box);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>("Does not allow to add too large pallet");
        }
        [Fact(DisplayName = "Adding pallet with large length throws exception")]
        public void PalleteAddBoxLagreLength()
        {
            // Assign
            Pallet pallet = new(10, 10, 10);
            Box box = new(3, 5, 11, 7, DateTime.Today);

            // Act
            Action action = () => pallet.AddBox(box);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>("Does not allow to add too large pallet");
        }
        [Fact(DisplayName = "Can convert Pallet to PalletDTO")]
        public void PalletCanConvertToPalletDto()
        {
            Pallet pallet = new(3, 5, 7);

            Box box = new(3, 5, 7, 11, DateTime.Today);
            pallet.AddBox(box);

            PalletDto palletDto = pallet.ToPalletDto();

            pallet.Length.Should().Be(palletDto.Length);
            pallet.Height.Should().Be(palletDto.Height);
            pallet.Width.Should().Be(palletDto.Width);
            palletDto.Boxes.Count.Should().Be(0);
        }
        [Fact(DisplayName = "Can convert palletDto to Pallet")]
        public void PalletDtoCanConvertToPallet()
        {
            PalletDto palletDto = new()
            {
                Length = 3,
                Height = 5,
                Width = 7
            };

            BoxDto boxDto = new()
            {
                Length = 3,
                Height = 5,
                Width = 7,
                Weight = 9,
                ExpirationDate = DateTime.Today
            };

            palletDto.Boxes.Add(boxDto);

            Pallet pallet = palletDto.ToPallet();

            pallet.Length.Should().Be(palletDto.Length);
            pallet.Height.Should().Be(palletDto.Height);
            pallet.Width.Should().Be(palletDto.Width);
            pallet.Boxes.Count.Should().Be(0);
        }
    }
}
