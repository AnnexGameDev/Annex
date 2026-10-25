namespace Annex.Core.Calculations;

public static class Rotation
{
    private static (float x, float y) GetDirectionalUnitVector(double degrees)
    {
        double radians = degrees.ToRadians();
        return ((float)Math.Cos(radians), (float)Math.Sin(radians));
    }

    public static float FindAngle(float x1, float y1, float x2, float y2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;
        float degrees = (float)Math.Atan2(dy, dx).ToDegrees();

        while (degrees < 0)
        {
            degrees += 360;
        }
        return (360 - (degrees % 360)) % 360;
    }

    public static (float x, float y) RotatePointAround(float x, float y, double degrees, float length)
    {
        var rotatedPoint = RotatePointAround(length, 0, degrees, 0, 0);
        return (x + rotatedPoint.x, y + rotatedPoint.y);
    }

    public static (float x, float y) RotatePointAround(float pointX, float pointY, double degrees, float fixedX, float fixedY)
    {
        float zerod_x = pointX - fixedX;
        float zerod_y = pointY - fixedY;

        (float cos, float sin) = GetDirectionalUnitVector(degrees);

        float xnew = cos * zerod_x + sin * zerod_y;
        float ynew = -sin * zerod_x + cos * zerod_y;

        return (xnew + fixedX, ynew + fixedY);
    }
}