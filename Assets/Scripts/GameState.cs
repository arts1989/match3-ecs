using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class GameState
    {
        public Dictionary<Vector2Int, EcsEntity> Board = new Dictionary<Vector2Int, EcsEntity> ();
        
        public int currentLevel;
        public int Rows;
        public int Columns;
        public int MovesAvaliable;
        public int PointsScored;
        public int PointsToWin;
        public int ObstacleCount;
        public int UnderlayCount;
        public bool waterfallSpawnEnable;
        public Sprite background;
        public AudioClip backgroundAudioClip;
        public AudioClip swipeSound;
        public AudioClip destroySound;
        public AudioClip spawnSound;
        public AudioClip denySound;
        public LevelType LevelType;                
        
        public List<SerializeItem<Vector2Int, BlockTypes>> blocksProperties;

        public bool freezeBoard = false;

        public GameState(Configuration configuration)
        {
            var _saveManager = new SaveManager();
            var saveData = _saveManager.GetData();
            var level = configuration.levels[saveData.Level];
            Init(level);
        }

        private void Init(Level level)
        {
            LevelType = level.LevelType;
        }

        /*
        private int[] _cells;
        private int _columns;
        private int _rows;
        private int _cellsAmount;
        public int CellsAmount => _cellsAmount;
        public int Rows => _rows;
        public int Columns => _columns;
        public int this[int column, int row]
        {
            get => _cells[row * _columns + column];
            set => _cells[row * _columns + column] = value;
        }
        public int this[int index] => _cells[index];
        public GameState(int colums, int row)
        {
            _columns = colums;
            _rows = row;
            _cellsAmount = _rows * _columns;
            _cells = new int[_cellsAmount];
        }
        */

        /*
         public static int GetCountBytape(BlockTypes types)
         {
             return collectionOfElements[types];
         }

         public static bool SetElement(BlockTypes types, Level level)
         {
             if (level.LevelType == 1)
             {
                 collectionOfElements[types]++;

                 if (collectionOfElements[types] >= level.TargetLevel)
                 {
                     return true;
                 }
             }
             return false;
         } */
    }
}
