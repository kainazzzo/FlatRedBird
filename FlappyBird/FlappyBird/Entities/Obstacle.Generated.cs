
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using FlappyBird.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;

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

namespace FlappyBird.Entities
{
	public partial class Obstacle : PositionedObject, IDestroyable
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
		protected static FlatRedBall.Math.Geometry.ShapeCollection ShapeCollectionFile;
		protected static FlatRedBall.Math.Geometry.ShapeCollection PassThroughShapeCollectionFile;
		
		private FlatRedBall.Math.Geometry.ShapeCollection CollisionShapeCollection;
		private FlatRedBall.Math.Geometry.ShapeCollection PassThroughShapeCollection;
		protected Layer LayerProvidedByContainer = null;

        public Obstacle()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Obstacle(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Obstacle(string contentManagerName, bool addToManagers) :
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
			CollisionShapeCollection = ShapeCollectionFile.Clone();
			PassThroughShapeCollection = PassThroughShapeCollectionFile.Clone();
			
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
			
			if (CollisionShapeCollection != null)
			{
				CollisionShapeCollection.RemoveFromManagers(ContentManagerName != "Global");
			}
			if (PassThroughShapeCollection != null)
			{
				PassThroughShapeCollection.RemoveFromManagers(ContentManagerName != "Global");
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			CollisionShapeCollection.CopyAbsoluteToRelative(false);
			CollisionShapeCollection.AttachAllDetachedTo(this, false);
			CollisionShapeCollection.Visible = true;
			PassThroughShapeCollection.CopyAbsoluteToRelative(false);
			PassThroughShapeCollection.AttachAllDetachedTo(this, false);
			PassThroughShapeCollection.Visible = true;
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
			CollisionShapeCollection.AddToManagers(layerToAddTo);
			CollisionShapeCollection.Visible = true;
			PassThroughShapeCollection.AddToManagers(layerToAddTo);
			PassThroughShapeCollection.Visible = true;
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("ObstacleStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/obstacle/shapecollectionfile.shcx", ContentManagerName))
				{
					registerUnload = true;
				}
				ShapeCollectionFile = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/obstacle/shapecollectionfile.shcx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/obstacle/passthroughshapecollectionfile.shcx", ContentManagerName))
				{
					registerUnload = true;
				}
				PassThroughShapeCollectionFile = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/obstacle/passthroughshapecollectionfile.shcx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("ObstacleStaticUnload", UnloadStaticContent);
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
				if (ShapeCollectionFile != null)
				{
					ShapeCollectionFile.RemoveFromManagers(ContentManagerName != "Global");
					ShapeCollectionFile= null;
				}
				if (PassThroughShapeCollectionFile != null)
				{
					PassThroughShapeCollectionFile.RemoveFromManagers(ContentManagerName != "Global");
					PassThroughShapeCollectionFile= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "ShapeCollectionFile":
					return ShapeCollectionFile;
				case  "PassThroughShapeCollectionFile":
					return PassThroughShapeCollectionFile;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "ShapeCollectionFile":
					return ShapeCollectionFile;
				case  "PassThroughShapeCollectionFile":
					return PassThroughShapeCollectionFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "ShapeCollectionFile":
					return ShapeCollectionFile;
				case  "PassThroughShapeCollectionFile":
					return PassThroughShapeCollectionFile;
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
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(CollisionShapeCollection);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(PassThroughShapeCollection);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
