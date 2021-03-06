﻿using System;
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
        public override string HintSpriteName => "controls_clickdrag";

        List<Vector2> nodes = new List<Vector2>();
        Vector2 MinBox, MaxBox;
        readonly float nodeSize = 15.0f;
        int MaxNumber;
        float percentageToComplete;
        bool drawActive = false;
        protected bool pressing = false;

        protected bool IsInZone => Input.MousePosition.X > MinBox.X && Input.MousePosition.X < MaxBox.X &&
                                   Input.MousePosition.Y > MinBox.Y && Input.MousePosition.Y < MaxBox.Y;

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
            base.Update();
            if (drawActive)
            {
                CheckForInput();
            }
        }

        private void CheckForInput()
        {
            if (Input.LeftMouseButtonDown && IsInZone)
            {
                pressing = true;
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
            else
            {
                pressing = false;
            }
        }

        protected virtual void AreaFilled()
        {
            drawActive = false;
        }

        protected bool IsDrawActive()
        {
            return drawActive;
        }

        public abstract void OnNodeAdded(Vector2 NodePos);
    }
}

