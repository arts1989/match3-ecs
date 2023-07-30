using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Match3
{
    internal partial class DetectSwipeSystem : IEcsRunSystem
    {
        private SceneData _sceneData;

        private Vector3 _swipeStartPos;
        private Vector3 _swipeEndPos;
        
        private float _swipeStartTime;
        private float _swipeEndTime;

        private Vector2Int _swipeVector;
        private EcsEntity _entityClicked;

        private float swipeMinimumDistance = .2f;
        private float swipeMaximumTime = 1f;
        private float swipeDirectionThreshold = .9f;

        public void Run()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // 
            {
                var camera = _sceneData.Camera;
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out var hitInfo))
                {
                    var entity = hitInfo.collider.GetComponent<LinkToEntity>();
                    if(entity)
                    {
                        _entityClicked = entity.entity;
                        _swipeStartPos = camera.ScreenToWorldPoint(Input.mousePosition);
                        _swipeStartTime = Time.time;
                    }
                }
            }
            else if(Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(!_entityClicked.IsNull()) {
                    _swipeEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _swipeEndTime = Time.time * Time.deltaTime;

                    if (Vector2.Distance(_swipeStartPos, _swipeEndPos) >= swipeMinimumDistance && (_swipeEndTime - _swipeStartTime) <= swipeMaximumTime)
                    {
                        Vector3 swipeDirection = _swipeEndPos - _swipeStartPos;
                        Vector2 swipeDirection2D = new Vector2(swipeDirection.x, swipeDirection.y).normalized;

                        _swipeVector = (Vector2.Dot(Vector2.up, swipeDirection2D) > swipeDirectionThreshold) ? Vector2Int.up
                            : (Vector2.Dot(Vector2.down, swipeDirection2D) > swipeDirectionThreshold) ? Vector2Int.down
                            : (Vector2.Dot(Vector2.left, swipeDirection2D) > swipeDirectionThreshold) ? Vector2Int.left
                            : (Vector2.Dot(Vector2.right, swipeDirection2D) > swipeDirectionThreshold) ? Vector2Int.right 
                            : Vector2Int.zero;

                        if(_swipeVector != Vector2Int.zero)
                            _entityClicked.Get<CheckMoveEvent>().direction = _swipeVector;
                    }
                }
            }
        }
    }
}