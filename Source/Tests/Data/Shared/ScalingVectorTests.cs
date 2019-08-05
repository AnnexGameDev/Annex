using Annex.Data.Shared;
using NUnit.Framework;

namespace Tests.Data.Shared
{
    public class ScalingVectorTests
    {
        [Test]
        public void Assignment_X_ModifiesScale_AndNotOriginal() {
            float originalX = 10;
            float originalY = 20;
            float scaleX = 30;
            float scaleY = 40;
            float newScaleX = 50;
            float expected = originalX * newScaleX;
            var original = new Vector(originalX, originalY);
            var scale = new Vector(scaleX, scaleY);
            var source = new ScalingVector(original, scale);

            source.X = newScaleX;

            Assert.AreEqual(source.Scale.X, newScaleX);
            Assert.AreEqual(source.X, expected);
            Assert.AreEqual(source.Original.X, originalX);
        }

        [Test]
        public void Assignment_Y_ModifiesScale_AndNotOriginal() {
            float originalX = 10;
            float originalY = 20;
            float scaleX = 30;
            float scaleY = 40;
            float newScaleY = 50;
            float expected = originalY * newScaleY;
            var original = new Vector(originalX, originalY);
            var scale = new Vector(scaleX, scaleY);
            var source = new ScalingVector(original, scale);

            source.Y = newScaleY;

            Assert.AreEqual(source.Scale.Y, newScaleY);
            Assert.AreEqual(source.Y, expected);
            Assert.AreEqual(source.Original.Y, originalY);
        }

        [Test]
        public void Constructor_SharedVector_Float_Float() {
            float originalX = 10;
            float originalY = 20;
            float scaleX = 30;
            float scaleY = 40;
            float expectedX = originalX * scaleX;
            float expectedY = originalY * scaleY;
            var original = new Vector(originalX, originalY);
            var source = new ScalingVector(original, scaleX, scaleY);

            Assert.AreEqual(source.Scale.X, scaleX);
            Assert.AreEqual(source.Scale.Y, scaleY);
            Assert.AreEqual(source.Original.X, originalX);
            Assert.AreEqual(source.Original.Y, originalY);
            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Constructor_SharedVector_SharedVector() {
            float originalX = 10;
            float originalY = 20;
            float scaleX = 30;
            float scaleY = 40;
            float expectedX = originalX * scaleX;
            float expectedY = originalY * scaleY;
            var original = new Vector(originalX, originalY);
            var scale = new Vector(scaleX, scaleY);
            var source = new ScalingVector(original, scale);

            Assert.AreEqual(source.Scale.X, scale.X);
            Assert.AreEqual(source.Scale.Y, scale.Y);
            Assert.AreEqual(source.Original.X, originalX);
            Assert.AreEqual(source.Original.Y, originalY);
            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void ScalingVector_NestedOriginal() {
            float originalX = 10;
            float originalY = 20;
            float scaleX = 30;
            float scaleY = 40;
            float expectedX = originalX * scaleX * scaleX;
            float expectedY = originalY * scaleY * scaleY;

            var original = new Vector(originalX, originalY);
            var scale = new Vector(scaleX, scaleY);

            ScalingVector nestedOriginal = new ScalingVector(original, scale);
            ScalingVector source = new ScalingVector(nestedOriginal, scale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void ScalingVector_NestedScale() {
            float originalX = 10;
            float originalY = 20;
            float scaleX = 30;
            float scaleY = 40;
            float expectedX = originalX * originalX * scaleX;
            float expectedY = originalY * originalY * scaleY;

            var original = new Vector(originalX, originalY);
            var scale = new Vector(scaleX, scaleY);

            ScalingVector nestedScale = new ScalingVector(original, scale);
            ScalingVector source = new ScalingVector(original, nestedScale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }
    }
}
