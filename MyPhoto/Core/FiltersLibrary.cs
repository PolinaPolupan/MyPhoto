using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyPhoto.Utils;

namespace MyPhoto.Core
{
    public static class FiltersLibrary
    {
        public enum Filter
        {
            BRIGHTNESS,
            CONTRAST,
            SATURATION,
            RED_CHANNEL,
            GREEN_CHANNEL,
            BLUE_CHANNEL,
            SEPIA,
            GRAYSCALE,
            NEGATIVE,
            TRANSPARENCY,
            DARK,
            BLUE,
            PURPLE,
            HUE
        }

        public static float[][] GetBrightnessMatrix(int brightness)
        {
            float[][] matrix = MathUtils.Identity5x5;

            float brightnessVal = (brightness / 100f);

            matrix[4][0] = brightnessVal;
            matrix[4][1] = brightnessVal;
            matrix[4][2] = brightnessVal;

            return matrix;
        }

        public static float[][] GetContrastMatrix(int contrast)
        {
            float[][] matrix = MathUtils.Identity5x5;

            float contrastVal = (contrast / 100f);

            matrix[0][0] = contrastVal;
            matrix[1][1] = contrastVal;
            matrix[2][2] = contrastVal;

            return matrix;
        }

        public static float[][] GetSaturationMatrix(int sat)
        {
            float[][] matrix = MathUtils.Identity5x5;

            // Luminance vector for linear RGB
            const float rwgt = 0.3086f;
            const float gwgt = 0.6094f;
            const float bwgt = 0.0820f;

            float saturation = sat * 0.01f;
            float baseSat = 1f - saturation;

            matrix[0][0] = baseSat * rwgt + saturation;
            matrix[0][1] = baseSat * rwgt;
            matrix[0][2] = baseSat * rwgt;
            matrix[1][0] = baseSat * gwgt;
            matrix[1][1] = baseSat * gwgt + saturation;
            matrix[1][2] = baseSat * gwgt;
            matrix[2][0] = baseSat * bwgt;
            matrix[2][1] = baseSat * bwgt;
            matrix[2][2] = baseSat * bwgt + saturation;

            return matrix;
        }

        public static float[][] GetRedChannelMatrix(int red)
        {
            float[][] matrix = MathUtils.Identity5x5;

            float redVal = (red / 100f);

            matrix[0][0] = redVal;

            return matrix;
        }

        public static float[][] GetGreenChannelMatrix(int green)
        {
            float[][] matrix = MathUtils.Identity5x5;

            float greenVal = (green / 100f);

            matrix[1][1] = greenVal;

            return matrix;
        }

        public static float[][] GetBlueChannelMatrix(int blue)
        {
            float[][] matrix = MathUtils.Identity5x5;

            float blueVal = (blue / 100f);

            matrix[2][2] = blueVal;

            return matrix;
        }

        public static float[][] GetSepiaMatrix(int value)
        {
            float[][] matrix = new float[][]
            {
                new float[]{0.393f, 0.349f, 0.272f, 0f, 0f},
                new float[]{0.769f, 0.686f, 0.534f, 0f, 0f},
                new float[]{0.189f, 0.168f, 0.131f, 0f, 0f},
                new float[]{0f,     0f,     0f,     1f, 0f},
                new float[]{0f,     0f,     0f,     0f, 1f}
            };

            matrix = MathUtils.MultiplyByValue(value, matrix);

            return (value > 0) ? matrix : MathUtils.Identity5x5;
        }

        public static float[][] GetGrayscaleMatrix(int value)
        {
            float[][] matrix = {
                new float[]{0.3f,  0.3f,  0.3f,  0f, 0f},
                new float[]{0.59f, 0.59f, 0.59f, 0f, 0f},
                new float[]{0.11f, 0.11f, 0.11f, 0f, 0f},
                new float[]{0f,    0f,    0f,    1f, 0f},
                new float[]{0f,    0f,    0f,    0f, 1f}
            };

            matrix = MathUtils.MultiplyByValue(value, matrix);

            return (value > 0) ? matrix : MathUtils.Identity5x5; 
        }

        public static float[][] GetNegativeMatrix(int value)
        {
            float[][] matrix =  {
                new float[]{-1f,  0f,  0f, 0f, 0f},
                new float[]{ 0f, -1f,  0f, 0f, 0f},
                new float[]{ 0f,  0f, -1f, 0f, 0f},
                new float[]{ 0f,  0f,  0f, 1f, 0f},
                new float[]{ 1f,  1f,  1f, 0f, 1f}
            };

            return (value > 0) ? matrix : MathUtils.Identity5x5;
        }

        public static float[][] GetTransparencyMatrix(int value)
        {
            float[][] matrix = new float[][]
            {
                new float[]{1f, 0f, 0f, 0f,   0f},
                new float[]{0f, 1f, 0f, 0f,   0f},
                new float[]{0f, 0f, 1f, 0f,   0f},
                new float[]{0f, 0f, 0f, 0.3f, 0f},
                new float[]{0f, 0f, 0f, 0f,   1f}
            };

            return (value > 0) ? matrix : MathUtils.Identity5x5;
        }

        public static float[][] GetDarkMatrix(int value)
        {
            float[][] matrix = new float[][]
            {
                new float[]{0.276f, 0f,     0f,     0f, 0f},
                new float[]{0f,     0.256f, 0f,     0f, 0f},
                new float[]{0f,     0f,     0.256f, 0f, 0f},
                new float[]{0f,     0f,     0f,     1f, 0f},
                new float[]{0f,     0f,     0f,     0f, 1f}
            };

            return (value > 0) ? matrix : MathUtils.Identity5x5;
        }

        public static float[][] GetBlueMatrix(int value)
        {
            float[][] matrix = new float[][]
            {
                new float[]{1f,   0f, 0f,   0f,   0f},
                new float[]{0.2f, 1f, 0.3f, 1.9f, 3f},
                new float[]{0.1f, 0f, 1.7f, 0f,   0f},
                new float[]{0f,   0f, 0f,   1f,   0.7f},
                new float[]{0f,   0f, 0f,   0f,   1f}
            };

            return (value > 0) ? matrix : MathUtils.Identity5x5;
        }

        public static float[][] GetPurpleMatrix(int value)
        {
            float[][] matrix = new float[][]
            {
                new float[]{1f,   0f, 0f,   0f,   0f},
                new float[]{0.2f, 1f, 0.3f, 1.9f, 3f},
                new float[]{0.1f, 0f, 1f,   0f,   0f},
                new float[]{0f,   0f, 0f,   1f,   0.7f},
                new float[]{0f,   0f, 0f,   0f,   1f}
            };

            return (value > 0) ? matrix : MathUtils.Identity5x5;
        }

        public static float[][] GetHueMatrix(int value)
        {
            float h = (float)Math.PI * value / 180f;

            float cosVal = (float)Math.Cos(h);
            float sinVal = (float)Math.Sin(h);

            float lumR = 0.213f;
            float lumG = 0.715f;
            float lumB = 0.072f;

            float[][] matrix = new float[][]
            {
                new float[]{lumR + cosVal * (1 - lumR) + sinVal * (-lumR),     lumG + cosVal * (-lumG) + sinVal * (-lumG),      lumB + cosVal * (-lumB) + sinVal * (1 - lumB),  0f, 0f},
                new float[]{lumR + cosVal * (-lumR) + sinVal * 0.143f,         lumG + cosVal * (1 - lumG) + sinVal * 0.140f,    lumB + cosVal * (-lumB) + sinVal * (-0.283f),   0f, 0f},
                new float[]{lumR + cosVal * (-lumR) + sinVal * (-(1 - lumR)),  lumG + cosVal * (-lumG) + sinVal * (lumG),       lumB + cosVal * (1 - lumB) + sinVal * (lumB),   0f, 0f},
                new float[]{0f,                                                0f,                                              0f,                                             1f, 0f},
                new float[]{0f,                                                0f,                                              0f,                                             0f, 1f}
            };

            return matrix;
        }

        public static double[,] GaussianBlur(int length, double weight)
        {
            double[,] kernel = new double[length, length];
            double kernelSum = 0;
            int foff = (length - 1) / 2;
            double distance = 0;
            double constant = 1d / (2 * Math.PI * weight * weight);
            for (int y = -foff; y <= foff; y++)
            {
                for (int x = -foff; x <= foff; x++)
                {
                    distance = ((y * y) + (x * x)) / (2 * weight * weight);
                    kernel[y + foff, x + foff] = constant * Math.Exp(-distance);
                    kernelSum += kernel[y + foff, x + foff];
                }
            }
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    kernel[y, x] = kernel[y, x] * 1d / kernelSum;
                }
            }
            return kernel;
        }

        public static Bitmap Convolve(in Bitmap srcImage, double[,] kernel)
        {
            int width = srcImage.Width;
            int height = srcImage.Height;

            BitmapData srcData = srcImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = srcData.Stride * srcData.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];

            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
            srcImage.UnlockBits(srcData);
            int colorChannels = 3;
            double[] rgb = new double[colorChannels];
            int foff = (kernel.GetLength(0) - 1) / 2;

            int kcenter = 0;
            int kpixel = 0;

            for (int y = foff; y < height - foff; y++)
            {
                for (int x = foff; x < width - foff; x++)
                {
                    for (int c = 0; c < colorChannels; c++)
                    {
                        rgb[c] = 0.0;
                    }
                    kcenter = y * srcData.Stride + x * 4;
                    for (int fy = -foff; fy <= foff; fy++)
                    {
                        for (int fx = -foff; fx <= foff; fx++)
                        {
                            kpixel = kcenter + fy * srcData.Stride + fx * 4;
                            for (int c = 0; c < colorChannels; c++)
                            {
                                rgb[c] += (double)(buffer[kpixel + c]) * kernel[fy + foff, fx + foff];
                            }
                        }
                    }
                    for (int c = 0; c < colorChannels; c++)
                    {
                        if (rgb[c] > 255)
                        {
                            rgb[c] = 255;
                        }
                        else if (rgb[c] < 0)
                        {
                            rgb[c] = 0;
                        }
                    }
                    for (int c = 0; c < colorChannels; c++)
                    {
                        result[kcenter + c] = (byte)rgb[c];
                    }
                    result[kcenter + 3] = 255;
                }
            }

            Bitmap resultImage = new Bitmap(width, height);
            BitmapData resultData = resultImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, resultData.Scan0, bytes);
            resultImage.UnlockBits(resultData);

            // Tell the GC to collect the redundant data
            resultData = null;
            result = null;
            buffer= null;
                 
            return resultImage;
        }

        public static Bitmap MedianFilter(in Bitmap sourceBitmap,
                                  int matrixSize)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;


            int byteOffset = 0;


            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    neighbourPixels.Clear();


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {


                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);


                            neighbourPixels.Add(BitConverter.ToInt32(
                                             pixelBuffer, calcOffset));
                        }
                    }


                    neighbourPixels.Sort();

                    middlePixel = BitConverter.GetBytes(
                                       neighbourPixels[filterOffset]);


                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);


            resultBitmap.UnlockBits(resultData);

            // Tell the GC to collect the redundant data
            resultData = null;
            resultBuffer = null;
            pixelBuffer = null;

            return resultBitmap;
        }

        private static bool CheckThreshold(byte[] pixelBuffer,
                                   int offset1, int offset2,
                                   ref int gradientValue,
                                   byte threshold,
                                   int divideBy = 1)
        {
            gradientValue +=
            Math.Abs(pixelBuffer[offset1] -
            pixelBuffer[offset2]) / divideBy;


            gradientValue +=
            Math.Abs(pixelBuffer[offset1 + 1] -
            pixelBuffer[offset2 + 1]) / divideBy;


            gradientValue +=
            Math.Abs(pixelBuffer[offset1 + 2] -
            pixelBuffer[offset2 + 2]) / divideBy;


            return (gradientValue >= threshold);
        }

        public static Bitmap GradientBasedEdgeDetectionFilter(
                        in Bitmap sourceBitmap,
                        byte threshold = 0)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);


            int sourceOffset = 0, gradientValue = 0;
            bool exceedsThreshold = false;


            for (int offsetY = 1; offsetY < sourceBitmap.Height - 1; offsetY++)
            {
                for (int offsetX = 1; offsetX < sourceBitmap.Width - 1; offsetX++)
                {
                    sourceOffset = offsetY * sourceData.Stride + offsetX * 4;
                    gradientValue = 0;
                    exceedsThreshold = true;


                    // Horizontal Gradient 
                    CheckThreshold(pixelBuffer,
                                   sourceOffset - 4,
                                   sourceOffset + 4,
                                   ref gradientValue, threshold, 2);
                    // Vertical Gradient 
                    exceedsThreshold =
                    CheckThreshold(pixelBuffer,
                                   sourceOffset - sourceData.Stride,
                                   sourceOffset + sourceData.Stride,
                                   ref gradientValue, threshold, 2);


                    if (exceedsThreshold == false)
                    {
                        gradientValue = 0;


                        // Horizontal Gradient 
                        exceedsThreshold =
                        CheckThreshold(pixelBuffer,
                                       sourceOffset - 4,
                                       sourceOffset + 4,
                                       ref gradientValue, threshold);


                        if (exceedsThreshold == false)
                        {
                            gradientValue = 0;

                            // Vertical Gradient 
                            exceedsThreshold =
                            CheckThreshold(pixelBuffer,
                                           sourceOffset - sourceData.Stride,
                                           sourceOffset + sourceData.Stride,
                                           ref gradientValue, threshold);


                            if (exceedsThreshold == false)
                            {
                                gradientValue = 0;

                                // Diagonal Gradient : NW-SE 
                                CheckThreshold(pixelBuffer,
                                               sourceOffset - 4 - sourceData.Stride,
                                               sourceOffset + 4 + sourceData.Stride,
                                               ref gradientValue, threshold, 2);
                                // Diagonal Gradient : NE-SW 
                                exceedsThreshold =
                                CheckThreshold(pixelBuffer,
                                               sourceOffset - sourceData.Stride + 4,
                                               sourceOffset - 4 + sourceData.Stride,
                                               ref gradientValue, threshold, 2);


                                if (exceedsThreshold == false)
                                {
                                    gradientValue = 0;

                                    // Diagonal Gradient : NW-SE 
                                    exceedsThreshold =
                                    CheckThreshold(pixelBuffer,
                                                   sourceOffset - 4 - sourceData.Stride,
                                                   sourceOffset + 4 + sourceData.Stride,
                                                   ref gradientValue, threshold);


                                    if (exceedsThreshold == false)
                                    {
                                        gradientValue = 0;

                                        // Diagonal Gradient : NE-SW 
                                        exceedsThreshold =
                                        CheckThreshold(pixelBuffer,
                                                       sourceOffset - sourceData.Stride + 4,
                                                       sourceOffset + sourceData.Stride - 4,
                                                       ref gradientValue, threshold);
                                    }
                                }
                            }
                        }
                    }


                    resultBuffer[sourceOffset] = (byte)(exceedsThreshold ? 255 : 0);
                    resultBuffer[sourceOffset + 1] = resultBuffer[sourceOffset];
                    resultBuffer[sourceOffset + 2] = resultBuffer[sourceOffset];
                    resultBuffer[sourceOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);


            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            // Tell the GC to collect the redundant data
            resultData = null;
            resultBuffer = null;
            pixelBuffer = null;

            return resultBitmap;
        }
    }
}
