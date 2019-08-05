using Annex.Data.Shared;
using NUnit.Framework;

namespace Tests.Data.Shared
{
    public class ScalingIntTests
    {
        [Test]
        public void Constructor_SharedInt_SharedInt() {
            Int original = 3;
            Int scale = 2;
            int expected = original.Value * scale.Value;

            Int source = new ScalingInt(original, scale);

            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void ScalingInts_NestedScale() {
            Int original = 3;
            Int scale = 2;
            int expected = original.Value * original.Value * scale.Value;

            Int nestedScale = new ScalingInt(original, scale);
            Int source = new ScalingInt(original, nestedScale);

            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void ScalingInts_NestedOriginal() {
            Int original = 3;
            Int scale = 2;
            int expected = original.Value * scale.Value * scale.Value;

            Int nestedOriginal = new ScalingInt(original, scale);
            Int source = new ScalingInt(nestedOriginal, scale);

            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void Assignment_Value_ModifiesScale_AndNotOriginal() {
            int original = 3;
            int scale = 4;
            int newScale = 5;
            int expected = original * newScale;
            var source = new ScalingInt(original, scale);

            source.Value = newScale;

            Assert.AreEqual(source.Scale.Value, newScale);
            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(source.Original.Value, original);
        }
    }
}
