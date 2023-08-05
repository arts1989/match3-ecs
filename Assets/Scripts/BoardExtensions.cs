using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public static class BoardExtensions
    {
        
        static private List<Vector2Int> _directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };
        static private int _chainLenght = 3;

        public static List<Vector2Int> getMatchCoords(this Dictionary<Vector2Int, EcsEntity> board, Vector2Int position, Vector2Int oldPosition)
        {
            var matchCoords = new List<Vector2Int>();
            var coords = new List<Vector2Int>();

            // �������� ���� 3 � ���
            foreach (var direction in _directions)
            { 
                if (direction + position == oldPosition)
                    continue; // ������ ������, �� ��������, ��� ��� ������� ����

                var chainLenght = 1;
                var startPos = position;
                var prevBlockType = BlockTypes.None;
                coords.Clear();

                if (direction != (oldPosition - position) && board.ContainsKey(position - direction)) // ������� -1 ��������� ��� �������� ������ "������ ����� ����� ����������� ����"
                {
                    startPos -= direction;
                }

                while (board.TryGetValue(startPos, out var entity))
                {
                    var blockType = entity.Get<BlockType>().value;
                    
                    // 0 1 1 1 1 0 [�������] 1 1 0 1 1 1 0 0 1 1 0
                    if (blockType == prevBlockType) {
                        coords.Add(startPos);
                        chainLenght++;
                    }
                    else if (chainLenght >= _chainLenght - 1)
                    {
                        break;
                    }

                    prevBlockType = blockType;
                    startPos += direction;
                }

                if (coords.Count >= _chainLenght - 1)
                {
                    var firstBlockInChainPos = coords[0] - direction;
                    coords.Add(firstBlockInChainPos);

                    foreach(var coord in coords)
                    {
                        if (!matchCoords.Contains(coord))
                        {
                            matchCoords.Add(coord);
                        }
                    }
                }
            }

            // �������� ���������� ������� �� 4
            var swipeDirection = position - oldPosition;
            
            foreach (var direction in _directions)
            {
                if (direction + position == oldPosition || position - direction == oldPosition)
                    continue; // ������� ����������� ������ � �������

                coords.Clear();
                
                coords.Add(position);
                coords.Add(position + direction);
                coords.Add(swipeDirection + position);
                coords.Add(swipeDirection + position + direction);

                var prevBlockType = BlockTypes.None;
                var chainLenght = 1;

                foreach (var coord in coords)
                {
                    if (board.TryGetValue(coord, out var entity))
                    {
                        var blockType = entity.Get<BlockType>().value;

                        if (prevBlockType == blockType)
                            chainLenght++;

                        prevBlockType = blockType;
                    }
                }

                if (chainLenght == 4)
                {
                    foreach (var coord in coords)
                    {
                        if (!matchCoords.Contains(coord))
                        {
                            matchCoords.Add(coord);
                        }
                    }
                }
            }

            return matchCoords;
        }

        public static bool checkMoveAvaliable(this Dictionary<Vector2Int, EcsEntity> board, Vector2Int position, Vector2Int swipeDirection)
        {
            if(!board.ContainsKey(position + swipeDirection)) // ����� �� ������� �����
                return false;

            // �������� ���� 3 � ���
            foreach (var direction in _directions) 
            {
                if (direction + swipeDirection == Vector2.zero) 
                    continue; // ������� �������� ����������� ������, ��� �� ����� ���� ����������

                var chainLenght = 1;
                var startPos = position + swipeDirection;
                var prevBlockType = BlockTypes.None;
                var coordToCheck = 3;

                if (direction != swipeDirection && board.ContainsKey(startPos - direction)) // ������� -1 ��������� ��� �������� ������ "������ ����� ����� ����������� ����"
                { 
                    startPos -= direction;
                    coordToCheck++;
                } 

                while (board.TryGetValue(startPos, out var entity))
                {
                    var blockType = (startPos == position + swipeDirection) 
                        ? board[position].Get<BlockType>().value // ����������� �������
                        : entity.Get<BlockType>().value;

                    if (blockType == prevBlockType)
                        chainLenght++;
                   
                    prevBlockType = blockType;
                    startPos += direction;
                    coordToCheck--;

                    if (chainLenght == _chainLenght)
                        return true;
                    
                    if (coordToCheck == 0)
                        break;
                }
            }

            // �������� ���������� ������� �� 4
            var coords = new List<Vector2Int>();

            foreach (var direction in _directions)
            {
                if (direction + swipeDirection == Vector2.zero || direction == swipeDirection)
                    continue; // ������� ����������� ������ � �������

                coords.Clear();

                var startPos = position + swipeDirection;

                coords.Add(startPos);
                coords.Add(startPos + direction);
                coords.Add(swipeDirection + startPos);
                coords.Add(swipeDirection + startPos + direction);

                var prevBlockType = BlockTypes.None;
                var chainLenght = 1;

                foreach (var coord in coords)
                {
                    if (board.TryGetValue(coord, out var entity))
                    {
                        var blockType = (coord == position + swipeDirection)
                            ? board[position].Get<BlockType>().value // ����������� �������
                            : entity.Get<BlockType>().value;

                        if (prevBlockType == blockType)
                            chainLenght++;

                        prevBlockType = blockType;
                    }
                }

                if (chainLenght == 4)
                    return true;
            }

            return false;
        }
    }
}
