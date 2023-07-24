using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Match3
{
    public class GameState
    {
        public Dictionary<Vector2Int, EcsEntity> Board = new Dictionary<Vector2Int, EcsEntity> ();
        
        public int currentLevel;
        public int Rows;
        public int Columns;
        public int MovesAvaliable;

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
