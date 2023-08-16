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

		[Header("Blocks and underlays properties")]
		public List<SerializeItem<Vector2Int,BlockTypes>> blocksProperties;
		public List<SerializeItem<Vector3Int,UnderlayTypes>> underlaysProperties;
	}	
}