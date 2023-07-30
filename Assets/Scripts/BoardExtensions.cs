using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Match3
{
    public static class BoardExtensions
    {
        public static List<Vector2Int> getMatchCoords(this Dictionary<Vector2Int, EcsEntity> board, Vector2Int position, int minChainLenght)
        {
            var horizontalCoords = board.getMatchInLine(position, minChainLenght, Vector2Int.right);
            var verticalCoords = board.getMatchInLine(position, minChainLenght, Vector2Int.up);
            Debug.Log("horizontalChain === " + horizontalCoords.Count);
            Debug.Log("verticalChain === " + verticalCoords.Count);
            foreach (var coord in verticalCoords)
            {
                if (!horizontalCoords.Contains(coord))
                { 
                    horizontalCoords.Add(coord);
                }
            }  
            return horizontalCoords;
        }

        public static List<Vector2Int> getMatchInLine(this Dictionary<Vector2Int, EcsEntity> board, Vector2Int position, int minChainLenght, Vector2Int direction)
        {
            var startPos = direction == Vector2Int.right ? new Vector2Int(0, position.y) : new Vector2Int(position.x, 0); //Vector2Int.up
            var reverseDirection = direction == Vector2Int.right ? Vector2Int.left : Vector2Int.down;

            var coords = new List<Vector2Int>();
            var prevBlockType = BlockTypes.None;

            coords.Clear();

            while (board.TryGetValue(startPos, out var entity))
            {
                var blockType = entity.Get<BlockType>().value;
                if (blockType == prevBlockType)
                {
                    coords.Add(startPos);
                }
                else if (coords.Count == 1)
                {
                    coords.Clear();
                }
                prevBlockType = blockType;
                startPos += direction;
            }

            if (coords.Count >= minChainLenght - 1)
            {
                var firstBlockInChainPos = coords.First() + reverseDirection;
                coords.Add(firstBlockInChainPos);
            }
            else
            {
                coords.Clear();
            }

            return coords;
        }

        public static bool checkMoveAvaliable(this Dictionary<Vector2Int, EcsEntity> board, Vector2Int position, Vector2Int swipeDirection)
        {
            if(!board.ContainsKey(position + swipeDirection)) // свайп за пределы доски
                return false;
            
            var directions  = new List<Vector2Int>() { 
                Vector2Int.up, Vector2Int.down, 
                Vector2Int.right, Vector2Int.left 
            };

            foreach(var currentDirection in directions) 
            {
                if (currentDirection + swipeDirection == Vector2.zero) continue; // убираем обратное направление свайпа, там не может быть комбинации

                var chainLenght = 1;
                var startPos = position + swipeDirection;
                var prevBlockType = BlockTypes.None;

                if (currentDirection != swipeDirection && board.ContainsKey(startPos - currentDirection)) // зацепим -1 кординату для проверки случая "двигаю между двумя одинакового типа"
                    startPos -= currentDirection;

                while (board.TryGetValue(startPos, out var entity))
                {
                    var blockType = (startPos == position + swipeDirection) 
                        ? board[position].Get<BlockType>().value // виртуальная подмена
                        : entity.Get<BlockType>().value;

                    if (blockType == prevBlockType)
                        chainLenght++;
                   
                    prevBlockType = blockType;
                    startPos += currentDirection;

                    if (chainLenght >= 3)
                        return true;
                }
            }

            return false;
        }
    }
}
