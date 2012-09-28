using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteTest
{
    class AnimatedSprite
    {
        Texture2D t2dTexture;

        float fFrameRate = 0.04f;
        float fElapsed = 0.0f;

        int iFrameOffsetX = 0;
        int iFrameOffsetY = 0;
        int spriteFrameWidth = 49;
        int spriteFrameHeight = 46;

        int spriteFrameCount = 1;
        int iCurrentFrame = 0;
        int iScreenX = 0;
        int iScreenY = 0;

        bool bAnimating = true;

        #region Properties
        
        public int X
        {
            get { return iScreenX; }
            set { iScreenX = value; }
        }

        public int Y
        {
            get { return iScreenY; }
            set { iScreenY = value; }
        }

        public int Frame
        {
            get { return iCurrentFrame; }
            set { iCurrentFrame = (int)MathHelper.Clamp(value, 0, spriteFrameCount); }
        }

        public float FrameLength
        {
            get { return fFrameRate; }
            set { fFrameRate = (float)Math.Max(value, 0f); }
        }

        public bool IsAnimating
        {
            get { return bAnimating; }
            set { bAnimating = value; }
        }

        #endregion

        #region Constructor

        public AnimatedSprite( Texture2D texture, int FrameOffsetX, int FrameOffsetY, int FrameWidth, int FrameHeight, int FrameCount)
        {
            t2dTexture = texture;
            iFrameOffsetX = FrameOffsetX;
            iFrameOffsetY = FrameOffsetY;
            spriteFrameWidth = FrameWidth;
            spriteFrameHeight = FrameHeight;
            spriteFrameCount = FrameCount;
        }

        #endregion

        #region Method

        public Rectangle GetSourceRect()
        {
            return new Rectangle(
                iFrameOffsetX + (spriteFrameWidth * iCurrentFrame), iFrameOffsetY, spriteFrameWidth, spriteFrameHeight);
        }

        #endregion

        #region Update

        public void Update(GameTime gametime)
        {
            // Accumulate elapsed time...
            fElapsed += (float)gametime.ElapsedGameTime.TotalSeconds;

            // Until it passes our frame length
            if (fElapsed > fFrameRate)
            {
                // Increment the current frame, wrapping back to 0 at iFrameCount
                iCurrentFrame = (iCurrentFrame + 1) % spriteFrameCount;

                // Reset the elapsed frame time
                fElapsed = 0.0f;
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset, bool NeedBeginEnd)
        {
            if (NeedBeginEnd)
                spriteBatch.Begin();

            spriteBatch.Draw(t2dTexture, new Rectangle(iScreenX + XOffset, iScreenY + YOffset, spriteFrameWidth, spriteFrameHeight), GetSourceRect(), Color.White);
            if (NeedBeginEnd)
                spriteBatch.End();
        }

        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset)
        {
            Draw(spriteBatch, XOffset, YOffset, true);
        }

        #endregion
    }
}
