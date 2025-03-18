using System;
using System.Drawing;
using System.Windows.Forms;
using KMB.BLL;

namespace PixelBot
{
    public class PixelBot
    {
        private KMBLL kmb;
        private Bitmap screen;
        private int[][] pixels;

        public PixelBot()
        {
            kmb = new KMBLL();
            screen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            pixels = new int[screen.Width][];
            for (int i = 0; i < screen.Width; i++)
            {
                pixels[i] = new int[screen.Height];
            }
        }

        public void Start()
        {
            while (true)
            {
                screen = TakeScreenshot();
                GetPixels();
                ProcessPixels();
                // Add delay or use a timer to control the loop frequency
                System.Threading.Thread.Sleep(100);
            }
        }

        private Bitmap TakeScreenshot()
        {
            using (Graphics gfx = Graphics.FromImage(screen))
            {
                gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                    Screen.PrimaryScreen.Bounds.Y,
                                    0, 0,
                                    screen.Size,
                                    CopyPixelOperation.SourceCopy);
            }
            return screen;
        }

        private void GetPixels()
        {
            for (int x = 0; x < screen.Width; x++)
            {
                for (int y = 0; y < screen.Height; y++)
                {
                    Color pixelColor = screen.GetPixel(x, y);
                    pixels[x][y] = pixelColor.ToArgb();
                }
            }
        }

        private void ProcessPixels()
        {
            // Process the pixel data and perform actions based on the pixel colors
            // Example: Move the mouse cursor to a specific position
            for (int x = 0; x < screen.Width; x++)
            {
                for (int y = 0; y < screen.Height; y++)
                {
                    if (pixels[x][y] == Color.Red.ToArgb())
                    {
                        Cursor.Position = new Point(x, y);
                    }
                }
            }
        }
    }
}
