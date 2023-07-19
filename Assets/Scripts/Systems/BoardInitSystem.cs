using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class BoardInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Configuration _configuration;
        private GameState _gameState;
        private SaveManager _saveManager;

        public void Init() 
        {
            // система генерации ентитей доски - спавним префабы из конфига
            // и ставим геймобжекту ссылку на ентитю через компонент-монобех
            // сохраняем состояние доски по координатам, в них лежит айди ентити
            var level = _saveManager.GetData().Level;
            var levelConfig = _configuration.levels[level];

            for (int x = 0; x < levelConfig.Rows; x++)
             {
                for (int y = 0; y < levelConfig.Columns; y++)
                {
                    var entity = _world.NewEntity();

                    int randomNum = Random.Range(0, _configuration.gems.Count);
                    var obj = Object.Instantiate(_configuration.gems[randomNum].sprite);

                    obj.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
                    obj.AddComponent<BoxCollider>();
                    obj.transform.position = new Vector3(
                        x + _configuration.offset.x * x,
                        y + _configuration.offset.y * y
                    );
                    
                    var position = new Vector2Int(x, y);
                    entity.Get<Position>().value = position;
                    entity.Get<BlockType>().value = _configuration.gems[randomNum].type;
                    entity.Get<LinkToObject>().value = obj;

                    _gameState.Board[position] = entity; 
                }
            }
        }
    }
}