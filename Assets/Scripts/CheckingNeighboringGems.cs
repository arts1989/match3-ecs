using UnityEngine;
using Leopotam.Ecs;
using Match3;

public class CheckingNeighboringGems
{
    private readonly Vector2Int _currentPosition;
    private readonly GameState _gameState;
    private readonly BlockTypes _currentBlockType;
    
    public CheckingNeighboringGems(Vector2Int currentPosition, GameState gameState, Vector2Int direction, BlockTypes currentBlockType)
    {
        _currentPosition = currentPosition + direction;
        _gameState = gameState;
        _currentBlockType = currentBlockType;
    }
    
    public int CheckRight()
    {
        int match = 0;
            
        for (int x = _currentPosition.x + 1; x < _currentPosition.x + 3 && x < _gameState.Columns; x++)
        {
            EcsEntity indexGem = _gameState.Board[new Vector2Int(x, _currentPosition.y)];
            
            if(_currentBlockType ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }

        return match;
    }
        
    public int CheckLeft()
    {
        int match = 0;
            
        for (int x = _currentPosition.x - 1; x > _currentPosition.x - 3 && x > -1; x--)
        {
            EcsEntity indexGem = _gameState.Board[new Vector2Int(x, _currentPosition.y)];
            
            if(_currentBlockType ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }
            
        return match;
    }
        
    public int CheckUp()
    {
        int match = 0;
            
        for (int y = _currentPosition.y + 1; y < _currentPosition.y + 3 && y < _gameState.Rows; y++)
        {
            EcsEntity indexGem = _gameState.Board[new Vector2Int(_currentPosition.x, y)];
            
            if(_currentBlockType ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }
            
        return match;
    }
        
    public int CheckDown()
    {
        int match = 0;
            
        for (int y = _currentPosition.y - 1; y > _currentPosition.y - 3 && y > -1; y--)
        {
            EcsEntity indexGem = _gameState.Board[new Vector2Int(_currentPosition.x, y)];
            
            if(_currentBlockType ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }
            
        return match;
    }
}