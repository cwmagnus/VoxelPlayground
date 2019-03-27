using System.Collections.Generic;
using UnityEngine;

namespace Management.Registry
{
    public class TexturePack
    {
        private Rect[] textureCoordnates;
        private List<string> texturePaths = new List<string>();

        public Texture2D Atlas { get; private set; }

        public Rect GetTextureRect(ushort id)
        {
            return textureCoordnates[id - 1];
        }

        public void GenerateTextureAtlas(int textureSize, int padding)
        {
            Atlas = new Texture2D(textureSize, textureSize);
            textureCoordnates = new Rect[texturePaths.Count];
            textureCoordnates = Atlas.PackTextures(LoadTextures(), padding, textureSize);
            Atlas.name = "Atlas";
            Atlas.Apply();
        }

        public void AddTexture(string path)
        {
            texturePaths.Add(path);
        }

        private Texture2D[] LoadTextures()
        {
            Texture2D[] loadedTextures = new Texture2D[texturePaths.Count];
            for (int i = 0; i < loadedTextures.Length; i++)
            {
                loadedTextures[i] = Resources.Load<Texture2D>(texturePaths[i]);
            }
            return loadedTextures;
        }
    }
}
