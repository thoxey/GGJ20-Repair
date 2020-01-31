using System;
using Microsoft.Xna.Framework;
using Nez;

namespace Framework
{
    public class Frame
    {
        public Color Color;

        public Frame()
        {
            var x = new System.Random();
            Color = new Color((float)x.NextDouble(), (float)x.NextDouble(), (float)x.NextDouble(), 1.0f);
        }

        public void Init()
        {
            Core.Scene.ClearColor = Color;
        }

        public void Update()
        {
        
        }


        public void Draw()
        {

        }

    }
}
