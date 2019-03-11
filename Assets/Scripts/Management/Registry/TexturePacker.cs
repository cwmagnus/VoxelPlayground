using System.Collections.Generic;
using UnityEngine;

namespace Management.Registry
{
    public class TexturePacker : MonoBehaviour
    {
        private Rect[] textureCoordnates;
        private List<string> textureNameList = new List<string>();

        public Texture2D LoadedTextureAtlas { get; private set; }

        public Rect GetTextureCoordnate(ushort id)
        {
            return textureCoordnates[id-1];
        }

        public void GenerateTextureAtlas()
        {
            LoadedTextureAtlas = new Texture2D(1100, 1100);
            textureCoordnates = new Rect[textureNameList.Count];
            textureCoordnates = LoadedTextureAtlas.PackTextures(LoadTextures(), 0, 1100);
            LoadedTextureAtlas.name = "Atlas";
            LoadedTextureAtlas.Apply();
        }

        public void AddTextureName(string name)
        {
            textureNameList.Add(name);
        }

        private Texture2D[] LoadTextures()
        {
            Texture2D[] loadedTextures = new Texture2D[textureNameList.Count];
            for (int i = 0; i < loadedTextures.Length; i++)
            {
                loadedTextures[i] = Resources.Load<Texture2D>($"Textures/Blocks/{textureNameList[i]}");
            }
            return loadedTextures;
        }
    }
}
