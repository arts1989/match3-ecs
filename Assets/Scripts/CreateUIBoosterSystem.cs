using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal sealed class CreateUIBoosterSystem : IEcsInitSystem
    {
        private Configuration _configuration;

        public void Init()
        {
            var uiBoosterView = Object.Instantiate(_configuration.uIBoosterView);
        }
    }
}