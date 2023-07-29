using Leopotam.Ecs;
using Match3;
using UnityEngine;

namespace Systems
{
    internal class CheckMoveSystem : IEcsRunSystem
    {
        private EcsFilter<CheckMoveEvent, LinkToObject, Position, BlockType> _filter;
        private GameState _gameState;

        private CheckingNeighboringGems _checkingNeighboringGems;
        
        public void Run()
        {
            //system to check if the heme can be moved in the direction
            //vector - to the next row in a combination or leave it in place
            
            if (!_filter.IsEmpty())
            {
                ref var direction = ref _filter.Get1(0).direction;
                ref var currentPosition = ref _filter.Get3(0).value;
                
                var board = _gameState.Board;
                var neighboringPosition = currentPosition + direction;
                
                var currentEntity = board[currentPosition];
                var neighboringEntity = board[neighboringPosition];

                // Swipe right check
                if (direction == Vector2Int.right)
                {
                    if (currentEntity.Get<Position>().value.x == _gameState.Columns - 1) Debug.Log("Do not move gem");
                    else
                    {
                        currentEntity.Get<MoveEvent>();
                        neighboringEntity.Get<MoveEvent>();

                        _checkingNeighboringGems = new CheckingNeighboringGems(currentEntity, _gameState, Vector2Int.right);

                        if(_checkingNeighboringGems.CheckRight() == 2 || _checkingNeighboringGems.CheckUp() == 2 
                                                                      || _checkingNeighboringGems.CheckDown() == 2 
                                                                      || _checkingNeighboringGems.CheckDown() 
                                                                      + _checkingNeighboringGems.CheckUp() >= 2) 
                            Debug.Log("Match found");
                    }
                }
                
                // Swipe left check
                if (direction == Vector2Int.left)
                {
                    if (currentPosition.x == 0) Debug.Log("Do not move gem");
                    else
                    {
                        currentEntity.Get<MoveEvent>();
                        neighboringEntity.Get<MoveEvent>();

                        _checkingNeighboringGems = new CheckingNeighboringGems(currentEntity, _gameState, Vector2Int.left);
                        
                        if(_checkingNeighboringGems.CheckLeft() == 2 || _checkingNeighboringGems.CheckUp() == 2 
                                                                      || _checkingNeighboringGems.CheckDown() == 2 
                                                                      || _checkingNeighboringGems.CheckDown() 
                                                                      + _checkingNeighboringGems.CheckUp() >= 2) 
                            Debug.Log("Match found");
                    }
                }
                
                // Swipe up check
                if (direction == Vector2Int.up)
                {
                    if (currentPosition.y == _gameState.Rows - 1) Debug.Log("Do not move gem");
                    else
                    {
                        currentEntity.Get<MoveEvent>();
                        neighboringEntity.Get<MoveEvent>();

                        _checkingNeighboringGems = new CheckingNeighboringGems(currentEntity, _gameState, Vector2Int.up);
                        
                        if(_checkingNeighboringGems.CheckUp() == 2 || _checkingNeighboringGems.CheckLeft() == 2 
                                                                     || _checkingNeighboringGems.CheckRight() == 2 
                                                                     || _checkingNeighboringGems.CheckRight() 
                                                                     + _checkingNeighboringGems.CheckLeft() >= 2) 
                            Debug.Log("Match found");
                    }
                }
                
                // Swipe down check
                if (direction == Vector2Int.down)
                {
                    if (currentPosition.y == 0) Debug.Log("Do not move gem");
                    else
                    {
                        currentEntity.Get<MoveEvent>();
                        neighboringEntity.Get<MoveEvent>();
                        
                        _checkingNeighboringGems = new CheckingNeighboringGems(currentEntity, _gameState, Vector2Int.down);
                        
                        if(_checkingNeighboringGems.CheckDown() == 2 || _checkingNeighboringGems.CheckLeft() == 2 
                                                                   || _checkingNeighboringGems.CheckRight() == 2 
                                                                   || _checkingNeighboringGems.CheckRight() 
                                                                   + _checkingNeighboringGems.CheckLeft() >= 2) 
                            Debug.Log("Match found");
                    }
                }
            }
        }
    }  
}
