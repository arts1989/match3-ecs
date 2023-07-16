using UnityEngine;

namespace Match3
{
	[CreateAssetMenu]
	public class Level : ScriptableObject
	{
		public int PointsToWin = 100;
		
		public int Columns = 5;
		public int Rows = 5;

		public int MoveCount = 5;

    }
}