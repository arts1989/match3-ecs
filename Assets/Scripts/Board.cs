using Unity.Mathematics;

namespace Match3
{
    public class Board
    {
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

        public Board(int colums, int row)
        {
            _columns = colums;
            _rows = row;
            _cellsAmount = _rows * _columns;
            _cells = new int[_cellsAmount];
        }

        //public void Swap(int2 pos1, int2 pos2)
        //{
        //    var temp = this[pos1.x, pos1.y];
        //    this[pos1.x, pos1.y] = this[pos2.x, pos2.y];
        //    this[pos2.x, pos2.y] = temp;
        //}
    }
}
