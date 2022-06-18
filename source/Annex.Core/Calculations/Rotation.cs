using Annex.Core.Data;

namespace Annex.Core.Calculations
{
    public static class Rotation
    {
        public static (float x, float y) ComputeUnits(double degrees) {
            return ((float)Math.Cos(degrees.ToRadians()), (float)Math.Sin(degrees.ToRadians()));
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

        public static (float x, float y) RotateAbout(float x, float y, double degrees, float x2, float y2) {

            float zerod_x = x - x2;
            float zerod_y = y - y2;

            (float cos, float sin) = ComputeUnits(degrees);

            float xnew = cos * zerod_x - zerod_y * sin;
            float ynew = sin * zerod_x - zerod_y * cos;
            
            // Shift it back
            float finalX = xnew + x2;
            float finalY = ynew + y2;

            return (finalX, finalY);
        }
    }
}
