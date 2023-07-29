using UnityEngine;
using Leopotam.Ecs;
using Match3;

public class CheckingNeighboringGems
{
    private readonly EcsEntity _currentEntity;
    private readonly GameState _gameState;
    private Vector2Int _currentPosition;

    public CheckingNeighboringGems(EcsEntity currentEntity, GameState gameState, Vector2Int direction)
    {
        _currentEntity = currentEntity;
        _gameState = gameState;
        _currentPosition = currentEntity.Get<Position>().value + direction;
    }
    
    public int CheckRight()
    {
        BlockTypes currentTypeGem = _currentEntity.Get<BlockType>().value;
        int match = 0;
            
        for (int x = _currentPosition.x + 1; x < _currentPosition.x + 3 && x < _gameState.Columns; x++)
        {
            var positionGemRight = new Vector2Int(x, _currentPosition.y);
            EcsEntity indexGem = _gameState.Board[positionGemRight];
            
            if(currentTypeGem ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }

        return match;
    }
        
    public int CheckLeft()
    {
        BlockTypes currentTypeGem = _currentEntity.Get<BlockType>().value;
        int match = 0;
            
        for (int x = _currentPosition.x - 1; x > _currentPosition.x - 3 && x > -1; x--)
        {
            var positionGemRight = new Vector2Int(x, _currentPosition.y);
            EcsEntity indexGem = _gameState.Board[positionGemRight];
            
            if(currentTypeGem ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }
            
        return match;
    }
        
    public int CheckUp()
    {
        BlockTypes currentTypeGem = _currentEntity.Get<BlockType>().value;
        int match = 0;
            
        for (int y = _currentPosition.y + 1; y < _currentPosition.y + 3 && y < _gameState.Rows; y++)
        {
            var positionGemRight = new Vector2Int(_currentPosition.x, y);
            EcsEntity indexGem = _gameState.Board[positionGemRight];
            
            if(currentTypeGem ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }
            
        return match;
    }
        
    public int CheckDown()
    {
        BlockTypes currentTypeGem = _currentEntity.Get<BlockType>().value;
        int match = 0;
            
        for (int y = _currentPosition.y - 1; y > _currentPosition.y - 3 && y > -1; y--)
        {
            var positionGemRight = new Vector2Int(_currentPosition.x, y);
            EcsEntity indexGem = _gameState.Board[positionGemRight];
            
            if(currentTypeGem ==  indexGem.Get<BlockType>().value) match += 1;
            else return match;
        }
            
        return match;
    }
}