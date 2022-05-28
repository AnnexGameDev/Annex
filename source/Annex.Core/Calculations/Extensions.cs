namespace Annex_Old.Core.Calculations
{
    public static class Extensions
    {
        public static double ToRadians(this double degrees) {
            return degrees * Math.PI / 180;
        }

        public static double ToDegrees(this double radians) {
            return radians * 180 / Math.PI;
        }

        public static float ToRadians(this float degrees) {
            return degrees * (float)Math.PI / 180;
        }

        public static float ToDegrees(this float radians) {
            return radians * 180 / (float)Math.PI;
        }
    }
}
