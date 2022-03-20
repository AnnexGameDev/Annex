using Annex.Core.Data;

namespace Annex.Core.Calculations
{
    public static class Rotation
    {
        public static (float x, float y) ComputeUnits(double rotation) {
            return ((float)Math.Cos(rotation.ToRadians()), (float)Math.Sin(rotation.ToRadians()));
        }

        public static float ComputeRotation(float x1, float y1, float x2, float y2) {
            float dx = x2 - x1;
            float dy = y2 - y1;
            return (float)Math.Atan2(dy, dx).ToDegrees();
        }

        public static double ComputeRotation(IVector2<float> position, IVector2<float> target) {
            float dx = target.X - position.X;
            float dy = target.Y - position.Y;
            return Math.Atan2(dy, dx).ToDegrees();
        }

        public static (float x, float y) ComputeUnits(IVector2<float> position, IVector2<float> target) {
            var rotation = ComputeRotation(position, target);
            return ComputeUnits(rotation);
        }
    }
}
