using System;
using Microsoft.Xna.Framework;
using Nez;

namespace Framework
{
    public abstract class Frame
    {
        private bool isFinished = false;

        protected Frame()
        {

        }

        public virtual void Update()
        {
        
        }


        public virtual void Draw()
        {

        }

        public virtual void Finish()
        {
            isFinished = true;
        }

        public bool IsFinished()
        {
            return isFinished;
        }
    }
}
