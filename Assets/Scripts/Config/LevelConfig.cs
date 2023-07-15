using UnityEngine;

namespace Match3
{
	[CreateAssetMenu]
	public class LevelConfig : ScriptableObject
	{
		public int PointsToWin = 100;
		
		public int BoardWitdh = 5;
		public int BoardHeight = 5;

		public int MoveCount = 5;

    }
}