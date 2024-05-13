using System.Drawing;

namespace Twitter_Trends.Implementations.Services
{
    public class ColorService
    {
        public static Color GetColor(float averageSentiment)
        {
            if (averageSentiment == 0)
            {
                return Color.White;
            }

            int alpha = 255;
            int red = 0;
            int green = 0;
            int blue = 0;

            if (averageSentiment < 0)
            {
                blue = 255;
                green = (int)(255 - 255 * Math.Abs(averageSentiment));
            }
            else
            {
                green = (int)(255 - 255 * averageSentiment);
                red = 255;
            }

            return Color.FromArgb(alpha, red, green, blue);
        }
    }
}
