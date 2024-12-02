using System;
using System.IO;
using UnityEngine;

public static class PixelArtExporter
{
    public static string Export(Color[,] pixelArt, string filename)
    {
        Texture2D texture = GenerateTexture(pixelArt);
        return SaveToFile(texture, filename);
    }

    private static Texture2D GenerateTexture(Color[,] colors)
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


    private static string SaveToFile(Texture2D texture, string filename)
    {
        byte[] bytes = texture.EncodeToPNG();

        string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string filepath = Path.Combine(directoryPath, filename);
        File.WriteAllBytes(filepath, bytes);

        Debug.Log($"Pixel art saved to {filepath}");
        return filepath;
    }
}
