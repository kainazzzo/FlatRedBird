#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using Microsoft.Xna.Framework;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

#endif
#endregion

namespace FlatRedBird.Entities
{
	public partial class Bird
	{
		private void CustomInitialize()
		{
		    YAcceleration = FallYAcceleration;
		    BirdSpriterObject.StartAnimation("Flap");
		    BirdSpriterObject.Animating = false;
		}

		private void CustomActivity()
		{
		    if (InputManager.Keyboard.KeyPushed(Keys.Up))
		    {
		        YVelocity = BounceYVelocity;
                RotationZ = 0f;
		    }

            if (YVelocity > 0 && (BirdSpriterObject.CurrentAnimation == null || !BirdSpriterObject.Animating ||BirdSpriterObject.CurrentAnimation.Name != "Flap"))
		    {
		        BirdSpriterObject.StartAnimation("Flap");
		        RotationZ = 0f;
		    }
            else if (YVelocity <= 0)
            {
                BirdSpriterObject.Animating = false;
            }
            
            if (YVelocity <= -350)
            {
                RotationZ = MathHelper.ToRadians(-70f);
            }
            else if (YVelocity <= -200)
            {
                RotationZ = MathHelper.ToRadians(-40f);
            }
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
