using Xunit;
using System;
using WarehouseApp;

namespace WarehouseTest
{
    public class PalleteTests
    {
        [Fact]
        public void PalleteVolumeCalculatesCorrect()
        {
            Pallet pallet = new(10, 10, 10);

            Box box1 = new(2, 3, 4, DateTime.Now);
            Box box2 = new(4, 5, 6, DateTime.Now);
            
            pallet.AddBox(box1);
            pallet.AddBox(box2);

            int volume = 24 + 120 + 1000;

            Assert.Equal(volume, pallet.Volume);
        }
        [Fact]
        public void PalleteWeightCalculatesCorrect()
        {
            Pallet pallet = new(10, 10, 10);

            Box box1 = new(2, 3, 4, DateTime.Now) { Weight = 5 };
            Box box2 = new(4, 5, 6, DateTime.Now) { Weight = 7 };

            pallet.AddBox(box1);
            pallet.AddBox(box2);

            int Weight = 5 + 7 + Pallet.OwnWeight;

            Assert.Equal(Weight, pallet.Weight);
        }
        [Fact]
        public void PalleteExpirationDateCalculatesFromBestBefore()
        {
            Pallet pallet = new(10, 10, 10);

            DateTime yesterday = DateTime.Today + TimeSpan.FromDays(-1);

            Box box1 = new(2, 3, 4, DateTime.Today);
            Box box2 = new(4, 5, 6, yesterday);

            pallet.AddBox(box1);
            pallet.AddBox(box2);

            Assert.Equal(yesterday, pallet.ExpirationDate);

            pallet = new(10, 10, 10);
            pallet.AddBox(box2);
            pallet.AddBox(box1);

            Assert.Equal(yesterday, pallet.ExpirationDate);

            pallet = new(10, 10, 10);
            Assert.Equal(DateTime.MinValue, pallet.ExpirationDate);
        }
        [Fact]
        public void PalleteAddBoxLagreHeight()
        {
            Pallet pallet = new(10, 10, 10);

            Box box = new(11, 3, 5, DateTime.Today);

            try
            {
                pallet.AddBox(box);
                Assert.True(false, "Adding box with larger Height should throw exception");
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }
        [Fact]
        public void PalleteAddBoxLagreWidth()
        {
            Pallet pallet = new(10, 10, 10);

            Box box = new(3, 11, 5, DateTime.Today);

            try
            {
                pallet.AddBox(box);
                Assert.True(false, "Adding box with larger Width should throw exception");
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }
        [Fact]
        public void PalleteAddBoxLagreLength()
        {
            Pallet pallet = new(10, 10, 10);

            Box box = new(3, 5, 11, DateTime.Today);

            try
            {
                pallet.AddBox(box);
                Assert.True(false, "Adding box with larger Length should throw exception");
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }
    }
}