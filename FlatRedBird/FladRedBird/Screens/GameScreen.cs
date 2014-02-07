using System;
using FlatRedBird.Entities;
using FlatRedBall.Content;
using FlatRedBall.Debugging;
using FlatRedBall.Math;
using Microsoft.Xna.Framework;

namespace FlatRedBird.Screens
{
	public partial class GameScreen
	{
	    private double _lastSpawn = double.MinValue;
	    readonly Random _random = new Random();
	    private bool _winning = true;
	    private int _score;

	    void CustomInitialize()
		{
            
		}

// ReSharper disable once UnusedParameter.Local
		void CustomActivity(bool firstTimeCalled)
		{
            Debugger.Write(string.Format("Score: {0}", _score));
		    if (_winning && PauseAdjustedSecondsSince(_lastSpawn) >= SpawnFrequency)
		    {
		        var obstacle = Factories.ObstacleFactory.CreateNew();
		        obstacle.XVelocity = ObstacleVelocityX;
		        obstacle.X = 600f;
		        obstacle.Y = _random.Next(MinObstacleY, MaxObstacleY);
		        _lastSpawn = PauseAdjustedCurrentTime;
		    }

		    if (BirdInstance.BirdCollision.CollideAgainst(Boundary))
		    {
		        GameOver();
		    }
		    else
		    {
		        foreach (var obstacle in ObstacleList)
		        {
		            if (BirdInstance.BirdCollision.CollideAgainst(obstacle.CollisionShapeCollection))
		            {
		                GameOver();
		            }
		            else if (BirdInstance.BirdCollision.CollideAgainst(obstacle.PassThroughShapeCollection))
		            {
		                ++_score;
		                obstacle.PassThroughShapeCollection.Clear();
		            }
		        }
		    }
		}

	    private void GameOver()
	    {
	        if (_winning)
	        {
	            BirdInstance.Velocity = Vector3.Zero;
	            BirdInstance.Acceleration = Vector3.Zero;
	            _winning = false;
	            foreach (var obstacle1 in ObstacleList)
	            {
	                obstacle1.Velocity = Vector3.Zero;
	            }
	        }
	    }

	    void CustomDestroy()
		{


		}

// ReSharper disable once UnusedParameter.Local
        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
