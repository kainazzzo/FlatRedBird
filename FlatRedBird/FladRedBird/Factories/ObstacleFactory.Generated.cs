using FlatRedBird.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using FlatRedBird.Performance;

namespace FlatRedBird.Factories
{
	public class ObstacleFactory : IEntityFactory
	{
		public static Obstacle CreateNew ()
		{
			return CreateNew(null);
		}
		public static Obstacle CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			Obstacle instance = null;
			instance = new Obstacle(mContentManagerName, false);
			instance.AddToManagers(layer);
			if (mScreenListReference != null)
			{
				mScreenListReference.Add(instance);
			}
			if (EntitySpawned != null)
			{
				EntitySpawned(instance);
			}
			return instance;
		}
		
		public static void Initialize (PositionedObjectList<Obstacle> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mScreenListReference = listFromScreen;
		}
		
		public static void Destroy ()
		{
			mContentManagerName = null;
			mScreenListReference = null;
			mPool.Clear();
			EntitySpawned = null;
		}
		
		private static void FactoryInitialize ()
		{
			const int numberToPreAllocate = 20;
			for (int i = 0; i < numberToPreAllocate; i++)
			{
				Obstacle instance = new Obstacle(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (Obstacle objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (Obstacle objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<Obstacle> mScreenListReference;
			static PoolList<Obstacle> mPool = new PoolList<Obstacle>();
			public static Action<Obstacle> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return ObstacleFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return ObstacleFactory.CreateNew(layer);
			}
			public static PositionedObjectList<Obstacle> ScreenListReference
			{
				get
				{
					return mScreenListReference;
				}
				set
				{
					mScreenListReference = value;
				}
			}
			static ObstacleFactory mSelf;
			public static ObstacleFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new ObstacleFactory();
					}
					return mSelf;
				}
			}
	}
}
