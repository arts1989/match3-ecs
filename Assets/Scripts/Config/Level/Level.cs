using System.Collections.Generic;
using Config.Level;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Match3
{
	[CreateAssetMenu]
	public class Level : ScriptableObject
	{
		public int PointsToWin = 100;
		
		public int Columns = 5;
		public int Rows = 5;

		public int MovesAvailable = 5;
		
		[Header("Board properties")]
		public List<ItemsDictionaryService<BlockTypes, Vector2Int>> gemsProperties;
		public List<ItemsDictionaryService<BlockTypes, Vector2Int>> substratesProperties;
	}
}