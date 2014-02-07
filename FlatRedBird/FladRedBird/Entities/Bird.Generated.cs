
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using FlatRedBird.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using FlatRedBird.Entities;
using FlatRedBird.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall_Spriter;
using Microsoft.Xna.Framework.Graphics;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace FlatRedBird.Entities
{
	public partial class Bird : PositionedObject, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static FlatRedBall.Math.Geometry.ShapeCollection BirdShapeCollection;
		protected static FlatRedBall_Spriter.SpriterObjectCollection SpriterObjectCollectionFile;
		protected static Microsoft.Xna.Framework.Graphics.Texture2D redball;
		
		private FlatRedBall.Math.Geometry.ShapeCollection mBirdCollision;
		public FlatRedBall.Math.Geometry.ShapeCollection BirdCollision
		{
			get
			{
				return mBirdCollision;
			}
		}
		private FlatRedBall_Spriter.SpriterObject BirdSpriterObject;
		public float FallYAcceleration = -600f;
		public float BounceYVelocity = 250f;
		protected Layer LayerProvidedByContainer = null;

        public Bird()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Bird(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Bird(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			mBirdCollision = BirdShapeCollection.Clone();
			BirdSpriterObject = SpriterObjectCollectionFile.FindByName("Bird").Clone();
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			if (BirdCollision != null)
			{
				BirdCollision.RemoveFromManagers(ContentManagerName != "Global");
			}
			if (BirdSpriterObject != null)
			{
				BirdSpriterObject.Destroy();
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			mBirdCollision.CopyAbsoluteToRelative(false);
			mBirdCollision.AttachAllDetachedTo(this, false);
			BirdCollision.Visible = true;
			if (BirdSpriterObject.Parent == null)
			{
				BirdSpriterObject.CopyAbsoluteToRelative();
				BirdSpriterObject.AttachTo(this, false);
			}
			BirdSpriterObject.RelativeScaleX = 2f;
			BirdSpriterObject.RelativeScaleY = 2f;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			// We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
			float oldRotationX = RotationX;
			float oldRotationY = RotationY;
			float oldRotationZ = RotationZ;
			
			float oldX = X;
			float oldY = Y;
			float oldZ = Z;
			
			X = 0;
			Y = 0;
			Z = 0;
			RotationX = 0;
			RotationY = 0;
			RotationZ = 0;
			mBirdCollision.AddToManagers(layerToAddTo);
			mBirdCollision.Visible = true;
			BirdSpriterObject.AddToManagers(layerToAddTo);
			BirdSpriterObject.RelativeScaleX = 2f;
			BirdSpriterObject.RelativeScaleY = 2f;
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
			FallYAcceleration = -600f;
			BounceYVelocity = 250f;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BirdStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/bird/birdshapecollection.shcx", ContentManagerName))
				{
					registerUnload = true;
				}
				BirdShapeCollection = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/bird/birdshapecollection.shcx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall_Spriter.SpriterObjectCollection>(@"content/entities/bird/spriterobjectcollectionfile.scml", ContentManagerName))
				{
					registerUnload = true;
				}
				SpriterObjectCollectionFile = SpriterObjectSave.FromFile("content/entities/bird/spriterobjectcollectionfile.scml").ToRuntime();
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/bird/redball.png", ContentManagerName))
				{
					registerUnload = true;
				}
				redball = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/bird/redball.png", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BirdStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
				if (BirdShapeCollection != null)
				{
					BirdShapeCollection.RemoveFromManagers(ContentManagerName != "Global");
					BirdShapeCollection= null;
				}
				if (SpriterObjectCollectionFile != null)
				{
					SpriterObjectCollectionFile.Destroy();
					SpriterObjectCollectionFile= null;
				}
				if (redball != null)
				{
					redball= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "BirdShapeCollection":
					return BirdShapeCollection;
				case  "SpriterObjectCollectionFile":
					return SpriterObjectCollectionFile;
				case  "redball":
					return redball;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "BirdShapeCollection":
					return BirdShapeCollection;
				case  "SpriterObjectCollectionFile":
					return SpriterObjectCollectionFile;
				case  "redball":
					return redball;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "BirdShapeCollection":
					return BirdShapeCollection;
				case  "SpriterObjectCollectionFile":
					return SpriterObjectCollectionFile;
				case  "redball":
					return redball;
			}
			return null;
		}
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(BirdCollision);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
