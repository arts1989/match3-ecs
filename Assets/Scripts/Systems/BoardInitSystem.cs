using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class BoardInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Configuration _configuration;
        private GameState _gameState;

        public void Init()
        {
            var board = _gameState.Board;
            _world.spawnBlocksParent();

            if (_gameState.blockPositionsActivated)
            {
                Sprite blockSprite = null;
                int blockPoints = 0;

                foreach (var block in _gameState.blockPositions)
                {
                    var entity = _world.NewEntity();
                    var position = block.Key;
                    var blockType = block.Value;

                    foreach (var configBlock in _configuration.blocks)
                    {
                        if (blockType == configBlock.type)
                        {
                            blockSprite = configBlock.sprites[0];
                            blockPoints = configBlock.points;
                        }
                    }

                    foreach (var configBlock in _configuration.boosters)
                    {
                        if (blockType == configBlock.type)
                        {
                            blockSprite = configBlock.sprites[0];
                            blockPoints = configBlock.points;

                            entity.Get<BlockType>().isBooster = true;
                        }
                    }

                    foreach (var configBlock in _configuration.obstacles)
                    {
                        if (blockType == configBlock.type)
                        {
                            blockSprite = configBlock.sprites[0];
                            blockPoints = configBlock.points;

                            entity.Get<BlockType>().isObstacle = true;
                        }
                    }

                    var obj = _world.spawnGameObject(position, entity, blockSprite);
                     
                    entity.Get<Position>().value = position;
                    entity.Get<BlockType>().value = blockType;
                    entity.Get<Points>().value = blockPoints;
                    entity.Get<LinkToObject>().value = obj; //link to entity from gameobject

                    board[position] = entity;
                }
            }
            else
            {
                for (int x = 0; x < _gameState.Columns; x++)
                {
                    for (int y = 0; y < _gameState.Rows; y++)
                    {
                        var position = new Vector2Int(x, y);

                        if (_gameState.emptyPositionsActivated)
                            if (_gameState.emptyPositions.Contains(position))
                                continue;

                        var entity = _world.NewEntity();
                        int randomNum = Random.Range(0, _configuration.blocks.Count);
                        var blockType = _configuration.blocks[randomNum].type;

                        while (board.hasNearbySameType(ref position, ref blockType))
                        {
                            randomNum = Random.Range(0, _configuration.blocks.Count);
                            blockType = _configuration.blocks[randomNum].type;
                        }

                        var obj = _world.spawnGameObject(position, entity, _configuration.blocks[randomNum].sprites[0]);

                        entity.Get<Position>().value = position;
                        entity.Get<BlockType>().value = _configuration.blocks[randomNum].type;
                        entity.Get<Points>().value = _configuration.blocks[randomNum].points;
                        entity.Get<LinkToObject>().value = obj; //link to entity from gameobject

                        board[position] = entity;
                    }
                }

                var obstacleCount = _gameState.ObstacleCount;
                while (obstacleCount > 0)
                {
                    var x = Random.Range(0, _gameState.Columns);
                    var y = Random.Range(0, _gameState.Rows);
                    var position = new Vector2Int(x, y);

                    while (board.isObstacle(ref position))
                    {
                        x = Random.Range(0, _gameState.Columns);
                        y = Random.Range(0, _gameState.Rows);
                        position = new Vector2Int(x, y);
                    }

                    var entity = board[position];

                    Object.Destroy(entity.Get<LinkToObject>().value);
                    entity.Destroy();

                    entity = _world.NewEntity();

                    var obj = _world.spawnGameObject(position, entity, _configuration.obstacles[0].sprites[0]);

                    entity.Get<Position>().value = position;
                    entity.Get<BlockType>().value = _configuration.obstacles[0].type;
                    entity.Get<BlockType>().isObstacle = true;
                    entity.Get<Points>().value = _configuration.obstacles[0].points;
                    entity.Get<LinkToObject>().value = obj; //link to entity from gameobject
                    entity.Get<Health>().value = _configuration.obstacles[0].health;

                    board[position] = entity;

                    obstacleCount--;
                }
            }
        }
    }
}   