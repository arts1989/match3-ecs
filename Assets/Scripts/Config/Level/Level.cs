using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
    public class Level : ScriptableObject
    {
        public int PointsToWin = 100;

        public LevelTypes LevelType; //уровень

        // public int TargetLevel; 
        public int TargetWinLevel; //число для победы j

        public int Columns = 5;
        public int Rows = 5;
        public bool waterfallSpawnEnable;
        public int MovesAvailable = 5;
        public int ObstacleCount;
        public int UnderlayCount;
        public Sprite background;

        [Space] public bool blockPositionsActivated;

        public List<SerializeItem<Vector2Int, BlockTypes>> blockPositions;

        public bool underlayPositionsActivated;
        public List<SerializeItem<Vector3Int, UnderlayTypes>> underlayPositions;

        public bool emptyPositionsActivated;
        public List<Vector2Int> emptyPositions;

        [Header("BackgroundSound")] public AudioClip backgroundSound;
    }
}