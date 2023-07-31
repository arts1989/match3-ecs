using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
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

        public static bool IsCheckMoveAvaliable(Vector2Int direction, Vector2Int currentPosition, GameState gameState, 
            CheckingNeighboringGems checkingNeighboringGems, BlockTypes blockType)
        { 
            // Swipe right check
            if (direction == Vector2Int.right)
            {
                if (currentPosition.x == gameState.Columns - 1) return false;
                else
                {
                    checkingNeighboringGems = new CheckingNeighboringGems(currentPosition, gameState, Vector2Int.right, blockType);

                    if (checkingNeighboringGems.CheckRight() == 2 || checkingNeighboringGems.CheckUp() == 2
                                                                  || checkingNeighboringGems.CheckDown() == 2
                                                                  || checkingNeighboringGems.CheckDown()
                                                                  + checkingNeighboringGems.CheckUp() >= 2)
                        Debug.Log("Match found");
                    return true;
                }
            }
                
        //     // Swipe left check
        //     if (direction == Vector2Int.left)
        //     {
        //         if (currentPosition.x == 0) return false;
        //         else
        //         {
        //             checkingNeighboringGems = new CheckingNeighboringGems(currentEntity, _gameState, Vector2Int.left);
        //                 
        //             if(_checkingNeighboringGems.CheckLeft() == 2 || _checkingNeighboringGems.CheckUp() == 2 
        //                                                          || _checkingNeighboringGems.CheckDown() == 2 
        //                                                          || _checkingNeighboringGems.CheckDown() 
        //                                                          + _checkingNeighboringGems.CheckUp() >= 2) 
        //                 Debug.Log("Match found");
        //         }
        //     }
        //         
        //     // Swipe up check
        //     if (direction == Vector2Int.up)
        //     {
        //         if (currentPosition.y == gameState.Rows - 1) return false;
        //         else
        //         {
        //             checkingNeighboringGems = new CheckingNeighboringGems(currentEntity, _gameState, Vector2Int.up);
        //                 
        //             if(_checkingNeighboringGems.CheckUp() == 2 || _checkingNeighboringGems.CheckLeft() == 2 
        //                                                        || _checkingNeighboringGems.CheckRight() == 2 
        //                                                        || _checkingNeighboringGems.CheckRight() 
        //                                                        + _checkingNeighboringGems.CheckLeft() >= 2) 
        //                 Debug.Log("Match found");
        //         }
        //     }
        //         
        //     // Swipe down check
        //     if (direction == Vector2Int.down)
        //     {
        //         if (currentPosition.y == 0) return false;
        //         else
        //         {
        //             checkingNeighboringGems = new CheckingNeighboringGems(currentEntity, _gameState, Vector2Int.down);
        //                 
        //             if(_checkingNeighboringGems.CheckDown() == 2 || _checkingNeighboringGems.CheckLeft() == 2 
        //                                                          || _checkingNeighboringGems.CheckRight() == 2 
        //                                                          || _checkingNeighboringGems.CheckRight() 
        //                                                          + _checkingNeighboringGems.CheckLeft() >= 2) 
        //                 Debug.Log("Match found");
        //         }
        //     } 
        //     
            return false;
        }
    }
}
