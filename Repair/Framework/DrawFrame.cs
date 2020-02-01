using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
namespace Repair.Framework
{
    public abstract class DrawFrame : Frame
    {
        List<Vector2> nodes = new List<Vector2>();
        Vector2 MinBox, MaxBox;
        readonly float nodeSize = 15.0f;
        int MaxNumber;
        float percentageToComplete;
        bool drawActive = false;

        protected void SetUpDrawArea(float requiredAmount, Vector2 minBox, Vector2 maxBox)
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
            if (drawActive && Input.LeftMouseButtonDown)
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
                    OnNodeAdded(Input.MousePosition);
                }

                if (nodes.Count > MaxNumber * percentageToComplete)
                {
                    AreaFilled();
                }
            }
        }

        protected virtual void AreaFilled()
        {
            drawActive = false;
        }

        public abstract void OnNodeAdded(Vector2 NodePos);
    }
}

