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
             for (int x = 0; x < _gameState.Columns; x++)
             {
                for (int y = 0; y < _gameState.Rows; y++)
                {
                    var entity = _world.NewEntity();

                    int randomNum = Random.Range(0, _configuration.blocks.Count);
                    var obj = Object.Instantiate(_configuration.blocks[randomNum].sprite);

                    obj.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
                    obj.AddComponent<BoxCollider>();
                    obj.transform.position = new Vector3(
                        x + _configuration.offset.x * x,
                        y + _configuration.offset.y * y
                    );
                    
                    var position = new Vector2Int(x, y);
                    entity.Get<Position>().value = position;
                    entity.Get<BlockType>().value = _configuration.blocks[randomNum].type;
                    entity.Get<Points>().value = _configuration.blocks[randomNum].points;
                    entity.Get<LinkToObject>().value = obj; //link to entity from gameobject

                    _gameState.Board[position] = entity; 
                }
            }
        } 
    }
}