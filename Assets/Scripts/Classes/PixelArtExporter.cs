using System;
using System.IO;
using System.Drawing;
using UnityEngine;

public class PixelArtExporter
{
    public string Export(UnityEngine.Color[,] pixelArt, string filename, FileFormat format)
    {
        Texture2D texture = GenerateTexture(pixelArt);

        if (format == FileFormat.PNG)
            return SavePNG(texture, filename);

        return SaveBMP(texture, filename);
    }

    private Texture2D GenerateTexture(UnityEngine.Color[,] colors)
    {
        int width = colors.GetLength(0);
        int height = colors.GetLength(1);
        Texture2D texture = new(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                texture.SetPixel(x, y, colors[x, y]);
            }
        }

        texture.Apply();
        return texture;
    }

    private string SavePNG(Texture2D texture, string filename)
    {
        filename += ".png";
        byte[] bytes = texture.EncodeToPNG();

        string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string filepath = Path.Combine(directoryPath, filename);
        File.WriteAllBytes(filepath, bytes);

        Debug.Log($"Pixel art saved to {filepath}");
        return filepath;
    }

    private string SaveBMP(Texture2D texture, string filename)
    {
        filename += ".bmp";

        int width = texture.width;
        int height = texture.height;
        byte[] bmpData = new byte[54 + 4 * width * height];

        // BMP Header
        bmpData[0] = (byte)'B';
        bmpData[1] = (byte)'M';
        int fileSize = 54 + 4 * width * height;
        bmpData[2] = (byte)(fileSize);
        bmpData[3] = (byte)(fileSize >> 8);
        bmpData[4] = (byte)(fileSize >> 16);
        bmpData[5] = (byte)(fileSize >> 24);

        // Reserved fields (not used)
        bmpData[10] = 54; // Pixel data offset

        // DIB Header
        bmpData[14] = 40; // DIB header size
        bmpData[18] = (byte)(width);
        bmpData[19] = (byte)(width >> 8);
        bmpData[20] = (byte)(width >> 16);
        bmpData[21] = (byte)(width >> 24);
        bmpData[22] = (byte)(-height);
        bmpData[23] = (byte)(-height >> 8);
        bmpData[24] = (byte)(-height >> 16);
        bmpData[25] = (byte)(-height >> 24);
        bmpData[26] = 1;  // Number of color planes
        bmpData[28] = 32; // Bits per pixel

        // Pixel data
        Color32[] pixels = texture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            int pixelIndex = 54 + i * 4;
            bmpData[pixelIndex + 0] = pixels[i].b;
            bmpData[pixelIndex + 1] = pixels[i].g;
            bmpData[pixelIndex + 2] = pixels[i].r;
            bmpData[pixelIndex + 3] = pixels[i].a;
        }

        string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string filepath = Path.Combine(directoryPath, filename);
        File.WriteAllBytes(filepath, bmpData);

        Debug.Log($"Pixel art saved to {filepath}");
        return filepath;
    }
}
