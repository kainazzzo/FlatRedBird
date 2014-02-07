using System;

namespace FlappyBird.Screens
{
	public partial class GameScreen
	{
	    private double _lastSpawn = double.MinValue;
	    readonly Random _random = new Random();
		void CustomInitialize()
		{
            
		}

// ReSharper disable once UnusedParameter.Local
		void CustomActivity(bool firstTimeCalled)
		{
		    if (PauseAdjustedCurrentTime - _lastSpawn >= SpawnFrequency)
		    {
		        var obstacle = Factories.ObstacleFactory.CreateNew();
		        obstacle.XVelocity = ObstacleVelocityX;
		        obstacle.X = 600f;
		        obstacle.Y = _random.Next(MinObstacleY, MaxObstacleY);
		        _lastSpawn = PauseAdjustedCurrentTime;
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
