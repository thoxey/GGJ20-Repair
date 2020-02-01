using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
namespace Repair.Framework
{
    public class DrawFrame : Frame
    {
        List<Vector2> nodes;
        Vector2 MinBox, MaxBox;
        static float nodeSize = 5.0f;
        int MaxNumber;
        float percentageToComplete;
        bool drawReady = false;

        public DrawFrame()
        {
        }

        public void SetUpDrawArea(float requiredAmount, Vector2 minBox, Vector2 maxBox)
        {
            MinBox = minBox;
            MaxBox = maxBox;
            percentageToComplete = requiredAmount;

            float area = (MaxBox - MinBox).X * (MaxBox - MinBox).Y;
            MaxNumber = (int)area / (int)(nodeSize * nodeSize * 4);

            drawActive = true;
        }

        public override void Update()
        {
            if (drawReady && Input.LeftMouseButtonDown)
            {
                bool overlapped = false;
                foreach (Vector2 node in nodes)
                {
                    if ((node - Input.MousePosition).Length() < nodeSize)
                    {
                        overlapped = true;
                        break;
                    }
                }
                if (!overlapped)
                {
                    nodes.Add(Input.MousePosition);
                }

                if (nodes.Count > MaxNumber * percentageToComplete)
                {
                    AreaFilled();
                }
            }
        }

        public virtual void AreaFilled()
        {
            drawReady = false;
        }
    }
}

