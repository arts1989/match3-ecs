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

        public bool blockPositionsActivated;
        public List<SerializeItem<Vector2Int, BlockTypes>> blockPositions;

        public bool underlayPositionsActivated;
        public List<SerializeItem<Vector3Int, UnderlayTypes>> underlayPositions;

        public bool emptyPositionsActivated;
        public List<Vector2Int> emptyPositions;

        public bool freezeBoard = false;

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
    }
}
