using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class Program
    {
        static byte[,] threasholding(int w, int h, byte [,] image, int threshold)
        {
            byte[,] result = new byte[h,w];
            //..////////////////////////////////
            for (int i=0; i<h; i++)
            {
                for (int j=0; j<w; j++)
                {
                    if (image[i, j] >= threshold)
                        result[i, j] = 255;
                    else
                        result[i, j] = 0;
                }
            }
            //..////////////////////////////////
            return result;
        }

        static byte[,] mirroring(int w, int h, byte[,] image)
        {
            byte[,] result = new byte[h, w];
            //..////////////////////////////////
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    result[i, w - j - 1] = image[i, j];
                }
            }
            //..////////////////////////////////
            return result;
        }

        static byte[,] zoom(int w, int h, byte[,] image, double zoomFactor)
        {
            int newW = (int)(w * zoomFactor);
            int newH = (int)(h * zoomFactor);
            byte[,] result = new byte[newH, newW];
            //..////////////////////////////////////////////////
            for (int i=0; i<newH; i++)
            {
                int oldR = (int)(i / zoomFactor);
                for (int j=0; j<newW; j++)
                {
                    int oldC = (int)(j / zoomFactor);
                    result[i, j] = image[oldR, oldC];
                }
            }
            //..////////////////////////////////////////////////
            return result;
        }

        static byte[,] rotation(int w, int h, byte[,] image, int angle)
        {
            double centerX = (w - 1) / 2.0;
            double centerY = (h - 1) / 2.0;
            byte[,] result = new byte[h, w];
            //..///////////////////////////////////////////////////////
            double dAngle = Math.PI * angle / 180.0;
            for (int i=0; i<h; i++)
            {
                double moveY = i - centerY;
                for (int j=0; j<w; j++)
                {
                    double moveX = j - centerX;
                    double oldX = Math.Round(((Math.Cos(dAngle) * moveX) - (Math.Sin(dAngle) * moveY) + centerX), 0);
                    double oldY = Math.Round(((Math.Sin(dAngle) * moveX) + (Math.Cos(dAngle) * moveY) +  + centerY), 0);
                    if (oldX < 0 || oldX >= w || oldY < 0 || oldY >= h)
                    {
                        result[i, j] = 0;
                    }
                    else
                    {
                        result[i, j] = image[(int)oldY, (int)oldX];
                    }
                }
            }
            //..///////////////////////////////////////////////////////
            return result;
        }

        static void printImage(int w, int h, byte[,] image)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Console.Write("{0:X2} ", image[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            //byte[,] input = { { 1, 2, 3, 4, 5, 6 }, { 7, 8, 9, 10, 11, 12 }, { 13, 14, 15, 16, 17, 18 }, { 19, 20, 21, 22, 23, 24 } };
            byte[,] input = { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 } };
            //byte[,] input = { { 1, 2}, { 3,4}};
            int w = input.GetLength(1);
            int h = input.GetLength(0);

            printImage(w, h, input);
            Console.WriteLine();

            byte[,] thImage = threasholding(w, h, input, 10);
            printImage(w, h, thImage);
            Console.WriteLine();

            byte[,] mrImage = mirroring(w, h, input);
            printImage(w, h, mrImage);
            Console.WriteLine();

            double zoomFactor = 3.0;
            byte[,] zmImage = zoom(w, h, input, zoomFactor); 
            printImage((int)(w * zoomFactor), (int)(h * zoomFactor), zmImage);
            Console.WriteLine();

            byte[,] roImage = rotation(w, h, input, 90);
            printImage(w, h, roImage);
        }
    }
}
