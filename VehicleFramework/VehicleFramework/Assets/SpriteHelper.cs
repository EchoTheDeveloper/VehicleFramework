﻿using UnityEngine;
using System.IO;
using System.Reflection;

namespace VehicleFramework.Assets
{
    public static class SpriteHelper
    {
        internal static Atlas.Sprite GetSpriteInternal(string name)
        {
            string modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fullPath = Path.Combine(modPath, "Sprites", name);
            return GetSpriteGeneric(fullPath);
        }
        public static Atlas.Sprite GetSprite(string relativePath)
        {
            string modPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            string fullPath = Path.Combine(modPath, relativePath);
            return GetSpriteGeneric(fullPath);
        }
        public static Sprite GetSpriteRaw(string relativePath)
        {
            string modPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            string fullPath = Path.Combine(modPath, relativePath);
            return GetSpriteGenericRaw(fullPath);
        }
        private static Atlas.Sprite GetSpriteGeneric(string fullPath)
        {
            return new Atlas.Sprite(GetSpriteGenericRaw(fullPath));
        }
        private static Sprite GetSpriteGenericRaw(string fullPath)
        {
            byte[] spriteBytes = System.IO.File.ReadAllBytes(fullPath);
            Texture2D SpriteTexture = new Texture2D(128, 128);
            SpriteTexture.LoadImage(spriteBytes);
            return Sprite.Create(SpriteTexture, new Rect(0.0f, 0.0f, SpriteTexture.width, SpriteTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}
