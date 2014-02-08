using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

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
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using FlatRedBird.Entities;
using FlatRedBird.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;

namespace FlatRedBird.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		protected static FlatRedBall.Math.Geometry.ShapeCollection BoundaryShapeCollection;
		protected static FlatRedBall.Scene SkySceneFile;
		
		private FlatRedBird.Entities.Bird BirdInstance;
		private PositionedObjectList<FlatRedBird.Entities.Obstacle> ObstacleList;
		private FlatRedBall.Math.Geometry.ShapeCollection Boundary;
		private FlatRedBird.Entities.Ground GroundInstance;
		private FlatRedBall.Scene SkySceneInstance;
		public System.Double SpawnFrequency = 3.5;
		public int MaxObstacleY = 150;
		public int MinObstacleY = -150;
		public static float ObstacleVelocityX = -150f;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			Boundary = BoundaryShapeCollection;
			SkySceneInstance = SkySceneFile;
			for (int i = 0; i < SkySceneInstance.Texts.Count; i++)
			{
				SkySceneInstance.Texts[i].AdjustPositionForPixelPerfectDrawing = true;
			}
			BirdInstance = new FlatRedBird.Entities.Bird(ContentManagerName, false);
			BirdInstance.Name = "BirdInstance";
			ObstacleList = new PositionedObjectList<FlatRedBird.Entities.Obstacle>();
			GroundInstance = new FlatRedBird.Entities.Ground(ContentManagerName, false);
			GroundInstance.Name = "GroundInstance";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			ObstacleFactory.Initialize(ObstacleList, ContentManagerName);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				BirdInstance.Activity();
				for (int i = ObstacleList.Count - 1; i > -1; i--)
				{
					if (i < ObstacleList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						ObstacleList[i].Activity();
					}
				}
				GroundInstance.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}
			SkySceneInstance.ManageAll();


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			ObstacleFactory.Destroy();
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				BoundaryShapeCollection.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				BoundaryShapeCollection.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				BoundaryShapeCollection = null;
			}
			else
			{
				BoundaryShapeCollection.MakeOneWay();
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SkySceneFile.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SkySceneFile.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SkySceneFile = null;
			}
			else
			{
				SkySceneFile.MakeOneWay();
			}
			
			if (BirdInstance != null)
			{
				BirdInstance.Destroy();
				BirdInstance.Detach();
			}
			for (int i = ObstacleList.Count - 1; i > -1; i--)
			{
				ObstacleList[i].Destroy();
			}
			if (Boundary != null)
			{
				Boundary.RemoveFromManagers(ContentManagerName != "Global");
			}
			if (GroundInstance != null)
			{
				GroundInstance.Destroy();
				GroundInstance.Detach();
			}
			if (SkySceneInstance != null)
			{
				SkySceneInstance.RemoveFromManagers(ContentManagerName != "Global");
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			Boundary.Visible = false;
			if (GroundInstance.Parent == null)
			{
				GroundInstance.X = -400f;
			}
			else
			{
				GroundInstance.RelativeX = -400f;
			}
			if (GroundInstance.Parent == null)
			{
				GroundInstance.Y = -250f;
			}
			else
			{
				GroundInstance.RelativeY = -250f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			BoundaryShapeCollection.AddToManagers(mLayer);
			SkySceneFile.AddToManagers(mLayer);
			BirdInstance.AddToManagers(mLayer);
			Boundary.Visible = false;
			GroundInstance.AddToManagers(mLayer);
			if (GroundInstance.Parent == null)
			{
				GroundInstance.X = -400f;
			}
			else
			{
				GroundInstance.RelativeX = -400f;
			}
			if (GroundInstance.Parent == null)
			{
				GroundInstance.Y = -250f;
			}
			else
			{
				GroundInstance.RelativeY = -250f;
			}
			SpawnFrequency = 3.5;
			MaxObstacleY = 150;
			MinObstacleY = -150;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			BirdInstance.ConvertToManuallyUpdated();
			for (int i = 0; i < ObstacleList.Count; i++)
			{
				ObstacleList[i].ConvertToManuallyUpdated();
			}
			GroundInstance.ConvertToManuallyUpdated();
			SkySceneInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
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
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/boundaryshapecollection.shcx", contentManagerName))
			{
			}
			BoundaryShapeCollection = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/boundaryshapecollection.shcx", contentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/gamescreen/skyscenefile.scnx", contentManagerName))
			{
			}
			SkySceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/gamescreen/skyscenefile.scnx", contentManagerName);
			FlatRedBird.Entities.Bird.LoadStaticContent(contentManagerName);
			FlatRedBird.Entities.Ground.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "BoundaryShapeCollection":
					return BoundaryShapeCollection;
				case  "SkySceneFile":
					return SkySceneFile;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "BoundaryShapeCollection":
					return BoundaryShapeCollection;
				case  "SkySceneFile":
					return SkySceneFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "BoundaryShapeCollection":
					return BoundaryShapeCollection;
				case  "SkySceneFile":
					return SkySceneFile;
			}
			return null;
		}


	}
}
