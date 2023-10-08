using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
	[CreateAssetMenu]
	public class Level : ScriptableObject
	{
		public int PointsToWin = 100;
		public LevelType LevelType; //уровень
		public int TargetLevel; //число для победы

		public int Columns = 5;
		public int Rows = 5;

        public bool waterfallSpawnEnable = false;

        public int MovesAvailable = 5;

		public int ObstacleCount = 0;
		public int UnderlayCount = 0;
		public Sprite background;

		[Header("Board properties")]
		public List<SerializeItem<Vector2Int,BlockTypes>> blocksProperties;
		public List<SerializeItem<Vector3Int,UnderlayTypes>> underlaysProperties;

		[Header("BackgroundSound")]
		public AudioClip backgroundSound;		
	}
}