using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindCloud
{
    //надо допилить:
    //сделать не через bitmap, а через что-нибудь другое
    //минимальный интерфейс, в котором можно будет настраивать контраст (выбрать оптимальный) и удобно выбирать картинки
    //что-то придумать с контуром изображения, потому что внутри изображения пиксели могут покраситься в красный. После этого переписать программу
    //возможно, стоит делить изображения на чёрно-белые и цветные
    class FindingClouds
    {
        //const string DIRECTORY = @"C:\Users\rez\Pictures\Saburov\forQuicklooks\";
        //const string IMAGE_NAME = "L1C_T37VDH_A020410_20190520T084602";
        //const string imagePath = DIRECTORY + IMAGE_NAME + ".jpg";

        //static Bitmap image = new Bitmap(imagePath);
        //Bitmap grayImage = new Bitmap(image.Width, image.Height);
        //double cloudPercent;
        //Bitmap contrastImage = new Bitmap(image.Width, image.Height);

        //int redPixels = 0; //пиксели, которые не входят в изображение

        //const string DIRECTORY = @"C:\Users\rez\Pictures\Saburov\forQuicklooks\";
        //const string IMAGE_NAME = "L1C_T37VDH_A020410_20190520T084602";
        //const string imagePath = DIRECTORY + IMAGE_NAME + ".jpg";

        //Image img = Image.FromFile(@"D:\фотки\я2.jpg");

        //static Bitmap image = new Bitmap(imagePath);
        //Bitmap grayImage = new Bitmap(image.Width, image.Height);
        //double cloudPercent;
        //Bitmap contrastImage = new Bitmap(image.Width, image.Height);

        //int redPixels = 0; //пиксели, которые не входят в изображение

        int numOfWhitePixels;
        int otherPixels;
        public int CloudPercent { get; set; }

        ~FindingClouds()
        {

        }

        public Image Contrast(Image pic, int intensity) //принимает аргумент - величина смены контраста
        {
            numOfWhitePixels = 0;
            otherPixels = 0;
            Bitmap b = new Bitmap(pic);
            Bitmap newBmp = new Bitmap(pic.Width, pic.Height);
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        //blue = p[0];
                        //green = p[1];
                        //red = p[2];
                        if (p[0] == p[1] && p[1] == p[2] && p[2] == 0)
                        {
                            p[0] = 255;
                            otherPixels++;
                        }
                        else
                        {
                            if (((p[0] + p[1] + p[2]) / 3) <= intensity)
                            {
                                p[0] = p[1] = p[2] = 0;
                            }
                            else
                            {
                                p[0] = p[1] = p[2] = 255;
                                numOfWhitePixels++;
                            }
                        }
                        
                        //pixel = red / 255.0;
                        //pixel -= 0.5;
                        //pixel *= contrast;
                        //pixel += 0.5;
                        //pixel *= 255;
                        //if (pixel < 0) pixel = 0;
                        //if (pixel > 255) pixel = 255;
                        //p[2] = (byte)pixel;

                        //pixel = green / 255.0;
                        //pixel -= 0.5;
                        //pixel *= contrast;
                        //pixel += 0.5;
                        //pixel *= 255;
                        //if (pixel < 0) pixel = 0;
                        //if (pixel > 255) pixel = 255;
                        //p[1] = (byte)pixel;

                        //pixel = blue / 255.0;
                        //pixel -= 0.5;
                        //pixel *= contrast;
                        //pixel += 0.5;
                        //pixel *= 255;
                        //if (pixel < 0) pixel = 0;
                        //if (pixel > 255) pixel = 255;
                        //p[0] = (byte)pixel;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            CloudPercent = numOfWhitePixels * 100 / ((pic.Width * pic.Height) - otherPixels);

            b.UnlockBits(bmData);
            newBmp = new Bitmap(b);
            //newBmp.Save(@"C:\Users\rez\Pictures\Saburov\cosmoPictures\cloud.jpg");
            return newBmp;
        }
 



        //public void CreateGrayimage()
        //{
        //    for (int j = 0; j < image.Height; j++)
        //    {
        //        for (int i = 0; i < image.Width; i++)
        //        {
        //            // получаем (i, j) пиксель
        //            UInt32 pixel = (UInt32)(image.GetPixel(i, j).ToArgb());
        //            // получаем компоненты цветов пикселя
        //            float R = (float)((pixel & 0x00FF0000) >> 16); // красный
        //            float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
        //            float B = (float)(pixel & 0x000000FF); // синий
        //                                                   // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
        //            R = G = B = (R + G + B) / 3.0f;
        //            // собираем новый пиксель по частям (по каналам)
        //            UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
        //            // добавляем его в Bitmap нового изображения
        //            grayImage.SetPixel(i, j, Color.FromArgb((int)newPixel));
        //        }
        //    }
        //    grayImage.Save(DIRECTORY + IMAGE_NAME + "Gray.jpg");
        //}

        //public void ChangeContrast(int p) //p -- на сколько процентов увеличим контрастность
        //{
        //    int N = (100 / 100) * p;  //можно вместо нижней 100 поставить любое число, которое будет являться максимальным значением контраста
        //    for (int j = 0; j < grayImage.Height; j++)
        //    {
        //        for (int i = 0; i < grayImage.Width; i++)
        //        {
        //            int I = grayImage.GetPixel(i, j).R; //интенсивность пикселя от 0 до 255
        //                                                //Console.WriteLine(I);

        //            I = (I * 100 - 128 * N) / (100 - N);
        //            if (I > 255) I = 255;
        //            if (I < 0) I = 0;

        //            UInt32 newPixel = 0xFF000000 | ((UInt32)I << 16) | ((UInt32)I << 8) | ((UInt32)I);
        //            contrastImage.SetPixel(i, j, Color.FromArgb((int)newPixel));

        //        }
        //    }
        //    contrastImage.Save(DIRECTORY + IMAGE_NAME + "Contrast.jpg");
        //}

        //Bitmap blackImage = new Bitmap(image.Width, image.Height);
        //public void CheckBlackPixels()
        //{
        //    for (int j = 0; j < grayImage.Height; j++)
        //    {
        //        for (int i = 0; i < grayImage.Width; i++)
        //        {
        //            if (grayImage.GetPixel(i, j).R == 0)
        //            {
        //                UInt32 newPixel = 0xFF000000 | ((UInt32)255 << 16) | ((UInt32)0 << 8) | ((UInt32)0);
        //                blackImage.SetPixel(i, j, Color.FromArgb((int)newPixel));

        //                redPixels++; //пиксели, которые не входят в изображение

        //            }
        //            else
        //            {
        //                blackImage.SetPixel(i, j, Color.FromArgb(grayImage.GetPixel(i, j).ToArgb()));
        //            }

        //        }
        //    }
        //    blackImage.Save(DIRECTORY + IMAGE_NAME + "black.jpg");

        //}
        //public void CalculateCloudPercent()
        //{
        //    int k = 0; //считаем количество белых пикселей

        //    for (int j = 0; j < blackImage.Height; j++)
        //    {
        //        for (int i = 0; i < blackImage.Width; i++)
        //        {
        //            byte R = blackImage.GetPixel(i, j).G; //оттенок зелёного (серого)
        //            if (R > 230) //если оттенок близок к 255
        //            {
        //                k++;
        //            }
        //        }
        //    }

        //    cloudPercent = (double)k / (double)(blackImage.Width * blackImage.Height - redPixels) * 100.0d;
        //    Console.WriteLine("cloud percent = " + cloudPercent);
        //}

    }
}
