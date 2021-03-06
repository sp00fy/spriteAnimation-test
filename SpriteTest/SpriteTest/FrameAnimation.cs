﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpriteTest
{
    class FrameAnimation : ICloneable
    {
        #region Variables

        private Rectangle rectInitialFrame;

        // Number of frames in the Animation
        private int iFrameCount = 1;

        // The frame currently being displayed
        private int iCurrentFrame = 0;

        // Amount of time (in seconds) to display each frame
        private float fFrameLength = 0.2f;

        // Amount of time that has passed since we last animated
        private float fFrameTimer = 0.0f;

        // The number of times this animation has been played
        private int iPlayCount = 0;

        // Animation that should be played after this animation
        private string sNextAnimation = null;

        #endregion

        #region Properties

        // Number of frames the animation contains
        public int FrameCount
        {
            get { return iFrameCount; }
            set { iFrameCount = value; }
        }

        // Time (in seconds) to display each frame
        public float FrameLength
        {
            get { return fFrameLength; }
            set { fFrameLength = value; }
        }

        // Frame number currently being displayed
        public int CurrentFrame
        {
            get { return iCurrentFrame; }
            set { iCurrentFrame = (int)MathHelper.Clamp(value, 0, iFrameCount - 1); }
        }

        public int FrameWidth
        {
            get { return rectInitialFrame.Width; }
        }

        public int FrameHeight
        {
            get { return rectInitialFrame.Height; }
        }

        // Rectangle associated with the current animation frame.
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(
                    rectInitialFrame.X + (rectInitialFrame.Width * iCurrentFrame),
                    rectInitialFrame.Y, rectInitialFrame.Width, rectInitialFrame.Height);
            }
        }

        public int PlayCount
        {
            get { return iPlayCount; }
            set { iPlayCount = value; }
        }

        public string NextAnimation
        {
            get { return sNextAnimation; }
            set { sNextAnimation = value; }
        }

        #endregion

        #region Constructor

        public FrameAnimation(Rectangle FirstFrame, int Frames)
        {
            rectInitialFrame = FirstFrame;
            iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
            fFrameLength = FrameLength;
        }

        public FrameAnimation(int X, int Y,
            int Width, int Height, int Frames, float FrameLength, string strNextAnimation)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
            fFrameLength = FrameLength;
            sNextAnimation = strNextAnimation;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            fFrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (fFrameTimer > fFrameLength)
            {
                fFrameTimer = 0.0f;
                iCurrentFrame = (iCurrentFrame + 1) % iFrameCount;
                if (iCurrentFrame == 0)
                    iPlayCount = (int)MathHelper.Min(iPlayCount + 1, int.MaxValue);
            }
        }

        object ICloneable.Clone()
        {
            return new FrameAnimation(this.rectInitialFrame.X, this.rectInitialFrame.Y,
                this.rectInitialFrame.Width, this.rectInitialFrame.Height,
                this.iFrameCount, this.fFrameLength, sNextAnimation);
        }

        #endregion

    }
}
