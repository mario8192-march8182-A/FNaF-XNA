using Microsoft.Xna.Framework;

namespace FNaF_XNA.Utils
{
    /// <summary>
    /// Utility class for math operations commonly used in games.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Calculate distance between two points.
        /// </summary>
        public static float Distance(Vector2 point1, Vector2 point2)
        {
            float dx = point2.X - point1.X;
            float dy = point2.Y - point1.Y;
            return MathHelper.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Check if a point is within a rectangle.
        /// </summary>
        public static bool IsPointInRectangle(Vector2 point, Rectangle rect)
        {
            return point.X >= rect.X && point.X <= rect.X + rect.Width &&
                   point.Y >= rect.Y && point.Y <= rect.Y + rect.Height;
        }

        /// <summary>
        /// Check if two rectangles overlap.
        /// </summary>
        public static bool RectanglesOverlap(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }

        /// <summary>
        /// Clamp value between min and max.
        /// </summary>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// Linear interpolation between two values.
        /// </summary>
        public static float Lerp(float start, float end, float t)
        {
            return start + (end - start) * Clamp(t, 0f, 1f);
        }

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        public static float ToRadians(float degrees)
        {
            return degrees * MathHelper.Pi / 180f;
        }

        /// <summary>
        /// Convert radians to degrees.
        /// </summary>
        public static float ToDegrees(float radians)
        {
            return radians * 180f / MathHelper.Pi;
        }
    }
}
