using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    public class GameState
    {
        public Sprite background;
        public AudioClip backgroundAudioClip;
        public List<SerializeItem<Vector2Int, BlockTypes>> blockPositions;

        public bool blockPositionsActivated;

        public List<SerializeItem<Vector2Int, BlockTypes>> blocksProperties;
        public Dictionary<Vector2Int, EcsEntity> Board = new();
        public int Columns;

        public int currentLevel;
        public AudioClip denySound;
        public AudioClip destroySound;
        public List<Vector2Int> emptyPositions;

        public bool emptyPositionsActivated;

        public bool freezeBoard = false;
        public LevelTypes LevelType;
        public int MovesAvaliable;
        public int ObstacleCount;
        public int PointsScored;
        public int PointsToWin;
        public int Rows;
        public AudioClip spawnSound;

        public AudioClip swipeSound;

        //public int numberOfCombinations; // для винконкондишенов 
        public int TargetWinLevel; // для винконкондишенов 
        public int UnderlayCount;
        public List<SerializeItem<Vector3Int, UnderlayTypes>> underlayPositions;

        public bool underlayPositionsActivated;
        public bool waterfallSpawnEnable;

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
        public int Points { get; set; }
    }
}