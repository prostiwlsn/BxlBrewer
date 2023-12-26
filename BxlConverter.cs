using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

namespace BxlConverter
{
    public class BxlBrewer
    {
        private MemoryStream _ms;
        private BinaryWriter _bw;

        public BxlBrewer()
        {
            _ms = new MemoryStream();
            _bw = new BinaryWriter(_ms);
        }

        public Bitmap PNGToBitmap(string path) => new Bitmap(path);

        public uint[,] BitmapToArray(Bitmap image, bool inverting = false)
        {
            int width = image.Width;
            int height = image.Height;

            uint[,] pixels = new uint[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    uint brightness = (uint)(inverting ? ((255 - pixelColor.R) + (255-pixelColor.G) + (255-pixelColor.B)) / 3 : (pixelColor.R + pixelColor.G + pixelColor.B)/3);
                    pixels[x, y] = brightness/16;
                }
            }

            return pixels;
        }

        public byte[] ArrayToSequence(uint[,] brightness, uint capacity)
        {
            byte[] sequence = new byte[capacity/2];

            bool appending = false;
            string summingString = "";
            uint index = 0;

            foreach (uint b in brightness)
            {
                summingString += b.ToString("X");

                if (appending)
                {
                    sequence[index / 2] = Convert.ToByte(summingString, 16);

                    summingString = "";
                }

                appending = !appending;

                index++;
            }

            return sequence;
        }

        public void SequenceToBinary(byte[] sequence, string path)
        {
            _bw.Write(sequence);
            File.WriteAllBytes(path, _ms.ToArray());
        }
    }
}