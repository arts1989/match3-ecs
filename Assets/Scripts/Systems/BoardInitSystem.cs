using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class BoardInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SaveManager _saveManager;
        private GameConfig _gameConfig;

        public void Init()
        {
            // система генерации ентитей доски - спавним префабы из конфига
            // и ставим геймобжекту ссылку на ентитю через компонент-монобех

            var level = _saveManager.GetData().Level;
            var levelConfig = _gameConfig.levels[level];

            for (int x = 0; x < levelConfig.BoardWitdh; x++) 
            {
                for(int y = 0; y < levelConfig.BoardHeight; y++)
                {
                    var entity = _world.NewEntity();

                    int randomNum = Random.Range(0, _gameConfig.gems.Count); 
                    var gem = Object.Instantiate(_gameConfig.gems[randomNum].sprite);

                    gem.AddComponent<LinkToEntity>().entity = entity; //link from gameobject to entity
                    gem.AddComponent<BoxCollider>();
                    gem.transform.position = new Vector3(
                        x + _gameConfig.offset.x * x,
                        y + _gameConfig.offset.y * y
                    );

                    entity.Get<Position>().value = new Vector2(x, y);
                    entity.Get<Cell>().type = _gameConfig.gems[randomNum].type;
                    entity.Get<LinkToObject>().value = gem;
                }
            }
        }
    }
}