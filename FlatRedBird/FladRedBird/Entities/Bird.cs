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
		    Y = 0;
		    BirdSpriterObject.StartAnimation("Flap");
		    BirdSpriterObject.Animating = false;
		    Alive = true;
		}

	    private readonly float _downRotation = MathHelper.ToRadians(-30f);
	    private readonly float _fastDownRotation = MathHelper.ToRadians(-80f);

		private void CustomActivity()
		{
		    if (!Alive)
		    {
		        BirdSpriterObject.Animating = false;
		        return;
		    }
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
                BirdSpriterObject.StartAnimation("Flap");
                BirdSpriterObject.Animating = false;
            }
            
            if (YVelocity <= -400)
            {
                RotationZ = _fastDownRotation;
            }
            else if (YVelocity <= -300)
            {
                RotationZ = _downRotation;
            }
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	    public bool Alive { get; set; }
	}
}
