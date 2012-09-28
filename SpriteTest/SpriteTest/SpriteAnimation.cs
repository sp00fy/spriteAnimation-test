using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpriteTest
{
    class SpriteAnimation
    {
        #region Variables

        Texture2D t2dTexture;

        // True if the animation is playing
        bool bAnimating = true;

        // Colorize sprite
        Color colorTint = Color.White;

        // VertexPositionColor of sprite
        Vector2 v2Position = new Vector2(0, 0);
        Vector2 v2LastPosition = new Vector2(0, 0);

        // Dictionary to hold the frame animations
        Dictionary<string, FrameAnimation> faAnimations = new Dictionary<string, FrameAnimation>();

        string sCurrentAnimation = null;

        bool bRotateByPosition = false;

        // How much the sprite should be rotated by when drawn
        // In radians
        float fRotation = 0f;

        // Calculated center of the sprite
        Vector2 v2Center;

        // Calculated width and height of the sprite
        int iWidth;
        int iHeight;

        #endregion

        #region Properties

        // Position of sprite's upper left corner pixel
        public Vector2 Position
        {
            get { return v2Position; }
            set
            {
                v2LastPosition = v2Position;
                v2Position = value;
                UpdateRotation();
            }
        }

        // X position of sprite upper left corner
        public int X
        {
            get { return (int)v2Position.X; }
            set
            {
                v2LastPosition.X = v2Position.X;
                v2Position.X = value;
                UpdateRotation();
            }
        }

        // Y position of sprite upper left corner
        public int Y
        {
            get { return (int)v2Position.Y; }
            set
            {
                v2LastPosition.Y = v2Position.Y;
                v2Position.Y = value;
                UpdateRotation();
            }
        }

        // width of sprite animation frames
        public int Width
        {
            get { return iWidth; }
        }

        // Height of sprite animation frames
        public int Height
        {
            get { return iHeight; }
        }

        // Auto rotation of sprite in direction of motion
        // whenever sprite's position changes
        public bool AutoRotate
        {
            get { return bRotateByPosition; }
            set { bRotateByPosition = value; }
        }

        // Degree of rotation (in radians) to be applied to the sprite
        // when drawn
        public float Rotation
        {
            get { return fRotation; }
            set { fRotation = value; }
        }

        // Screen coordinates of the bounding box surrounding this sprite
        public Rectangle BoundingBox
        {
            get { return new Rectangle(X, Y, iWidth, iHeight); }
        }

        // Texture associated with the sprite.  All FrameAnimations
        // will be relative to this texture.

        public Texture2D Texture
        {
            get { return t2dTexture; }
        }

        public Color Tint
        {
            get { return colorTint; }
            set { colorTint = value; }
        }

        public bool IsAnimating
        {
            get { return bAnimating; }
            set { bAnimating = value; }
        }

        public FrameAnimation CurrentFrameAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(sCurrentAnimation))
                    return faAnimations[sCurrentAnimation];
                else
                    return null;
            }
        }

        public string CurrentAnimation
        {
            get { return sCurrentAnimation; }
            set
            {
                if (faAnimations.ContainsKey(value))
                {
                    sCurrentAnimation = value;
                    faAnimations[sCurrentAnimation].CurrentFrame = 0;
                    faAnimations[sCurrentAnimation].PlayCount = 0;
                }
            }
        }

        #endregion

        #region Constructor

        public SpriteAnimation(Texture2D Texture)
        {
            t2dTexture = Texture;
        }

        #endregion

        #region Methods

        void UpdateRotation()
        {
            if (bRotateByPosition)
            {
                fRotation = (float)Math.Atan2(v2Position.Y - v2LastPosition.Y, v2Position.X - v2LastPosition.X);
            }
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames,
            float FrameLength, string NextAnimation)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength, NextAnimation));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }
        public FrameAnimation GetAnimationByName(string Name)
        {
            if (faAnimations.ContainsKey(Name))
            {
                return faAnimations[Name];
            }
            else
            {
                return null;
            }
        }

        public void MoveBy(int x, int y)
        {
            v2LastPosition = v2Position;
            v2Position.X += x;
            v2Position.Y += y;
            UpdateRotation();
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (bAnimating)
            {
                if (CurrentAnimation == null)
                {
                    if (faAnimations.Count > 0)
                    {
                        string[] sKeys = new string[faAnimations.Count];
                        faAnimations.Keys.CopyTo(sKeys, 0);
                        CurrentAnimation = sKeys[0];
                    }
                    else
                    {
                        return;
                    }
                }
            }
            CurrentFrameAnimation.Update(gameTime);

            if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
            {
                if (CurrentFrameAnimation.PlayCount > 0)
                {
                    CurrentAnimation = CurrentFrameAnimation.NextAnimation;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset)
        {
            if (bAnimating)
                spriteBatch.Draw(t2dTexture, (v2Position + new Vector2(XOffset, YOffset) + v2Center),
                    CurrentFrameAnimation.FrameRectangle, colorTint,
                    fRotation, v2Center, 1f, SpriteEffects.None, 0);
        }

        #endregion


        #region Draw



        #endregion
    }
}


