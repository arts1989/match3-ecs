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

		public int MovesAvailable = 5;

		public int ObstacleCount = 0;
		public int UnderlayCount = 0;

		[Header("Blocks positions on board")]
		public bool blockPositionsActivated = false;
		public List<SerializeItem<Vector2Int,BlockTypes>> blockPositions;

        [Header("Underlay positions on board")]
        public bool underlayPositionsActivated = false;
        public List<SerializeItem<Vector3Int,UnderlayTypes>> underlayPositions;

        [Header("Empty positions on board")]
		public bool emptyPositionsActivated = false;
        public List<Vector2Int> emptyPositions;
	}	
}