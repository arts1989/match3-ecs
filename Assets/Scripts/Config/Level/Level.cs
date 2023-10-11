using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
	[CreateAssetMenu]
	public class Level : ScriptableObject
	{
		public int PointsToWin = 100;

		public int Columns = 5;
		public int Rows = 5;
		public bool waterfallSpawnEnable = false;
		public int MovesAvailable = 5;
		public int ObstacleCount = 0;
		public int UnderlayCount = 0;
		public Sprite background;

		[Space]

		public bool blockPositionsActivated = false;
		public List<SerializeItem<Vector2Int,BlockTypes>> blockPositions;

		public bool underlayPositionsActivated = false;
		public List<SerializeItem<Vector3Int,UnderlayTypes>> underlayPositions;

		public bool emptyPositionsActivated = false;
		public List<Vector2Int> emptyPositions;

  		[Header("BackgroundSound")]
		public AudioClip backgroundSound;

	}
}