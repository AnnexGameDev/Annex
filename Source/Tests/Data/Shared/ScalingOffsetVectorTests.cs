using Annex.Data.Shared;
using NUnit.Framework;

namespace Tests.Data.Shared
{
    public class ScalingOffsetVectorTests
    {
        [Test]
        public void Assignment_X_ModifiesScale_AndNotOriginalOrOffset() {
            float offsetX = 2;
            float originalX = 4;
            Vector offset = Vector.Create(offsetX, 3);
            Vector original = Vector.Create(originalX, 5);
            Vector scale = Vector.Create(6, 7);
            float newScaleX = 8;
            float expectedX = (offsetX * newScaleX) + originalX;
            var source = new ScalingOffsetVector(original, offset, scale);

            source.X = newScaleX;

            Assert.AreEqual(source.Scale.X, newScaleX);
            Assert.AreEqual(source.Original.X, originalX);
            Assert.AreEqual(source.Offset.X, offsetX);
            Assert.AreEqual(source.X, expectedX);
        }

        [Test]
        public void Assignment_Y_ModifiesScale_AndNotOriginalOrOffset() {
            float offsetY = 3;
            float originalY = 5;
            Vector offset = Vector.Create(2, offsetY);
            Vector original = Vector.Create(4, originalY);
            Vector scale = Vector.Create(6, 7);
            float newScaleY = 8;
            float expectedY = (offsetY * newScaleY) + originalY;
            var source = new ScalingOffsetVector(original, offset, scale);

            source.Y = newScaleY;

            Assert.AreEqual(source.Scale.Y, newScaleY);
            Assert.AreEqual(source.Original.Y, originalY);
            Assert.AreEqual(source.Offset.Y, offsetY);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Constructor_SharedVector_SharedVector_SharedVector() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = (offsetX * scaleX) + originalX;
            float expectedY = (offsetY * scaleY) + originalY;

            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);
            Vector scale = Vector.Create(scaleX, scaleY);

            ScalingOffsetVector source = new ScalingOffsetVector(original, offset, scale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Constructor_SharedVector_SharedVector_Float_Float() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = (offsetX * scaleX) + originalX;
            float expectedY = (offsetY * scaleY) + originalY;

            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);

            ScalingOffsetVector source = new ScalingOffsetVector(original, offset, scaleX, scaleY);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void ScalingOffsetVector_NestedOriginal() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = (offsetX * scaleX) + ((offsetX * scaleX) + originalX);
            float expectedY = (offsetY * scaleY) + ((offsetY * scaleY) + originalY);

            Vector scale = Vector.Create(scaleX, scaleY);
            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);

            Vector nestedOriginal = new ScalingOffsetVector(original, offset, scale);

            ScalingOffsetVector source = new ScalingOffsetVector(nestedOriginal, offset, scale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void ScalingOffsetVector_NestedOffset() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = (((offsetX * scaleX) + originalX) * scaleX) + originalX;
            float expectedY = (((offsetY * scaleY) + originalY) * scaleY) + originalY;

            Vector scale = Vector.Create(scaleX, scaleY);
            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);

            Vector nestedOffset = new ScalingOffsetVector(original, offset, scale);

            ScalingOffsetVector source = new ScalingOffsetVector(original, nestedOffset, scale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void ScalingOffsetVector_NestedScale() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = (offsetX * ((offsetX * scaleX) + originalX)) + originalX;
            float expectedY = (offsetY * ((offsetY * scaleY) + originalY)) + originalY;

            Vector scale = Vector.Create(scaleX, scaleY);
            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);

            Vector nestedScale = new ScalingOffsetVector(original, offset, scale);

            ScalingOffsetVector source = new ScalingOffsetVector(original, offset, nestedScale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }
    }
}
