using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpriteTest
{
    class Background
    {
        #region Vars

        //Texture to hold background
        Texture2D t2dBackground;

        int iViewportWidth = 1280;
        int iViewportHeight = 720;

        int iBackgroundWidth = 256;
        int iBackgroundHeight = 207;
        
        #endregion

        #region properties

        int iBackgroundOffset;
                
        public int BackgroundOffset
        {
            get { return iBackgroundOffset; }
            set
            {
                iBackgroundOffset = value;
                if (iBackgroundOffset < 0)
                {
                    iBackgroundOffset += iBackgroundWidth;
                }
                if (iBackgroundOffset > iBackgroundWidth)
                {
                    iBackgroundOffset -= iBackgroundWidth;
                }
            }
        }

        #endregion

        #region Constructor

        public Background(ContentManager content, string sBackground)
        {
            t2dBackground = content.Load<Texture2D>(sBackground);
            iBackgroundWidth = t2dBackground.Width;
            iBackgroundHeight = t2dBackground.Height;
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2dBackground, new Rectangle(-1 * iBackgroundOffset, 0, iBackgroundWidth, iViewportHeight), Color.White);

            if (iBackgroundOffset > iBackgroundWidth - iViewportWidth)
            {
                spriteBatch.Draw(t2dBackground, new Rectangle((-1 * iBackgroundOffset) + iBackgroundWidth, 0, iBackgroundWidth, iViewportHeight), Color.White);
            }
        }

        #endregion

    }
}
