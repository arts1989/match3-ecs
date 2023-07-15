﻿using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal partial class DetectSwipeSystem : IEcsRunSystem
    {
        //private EcsFilter<SwapGemEvent, CellViewRef> _filter;
        private EcsWorld _world;

        private SceneData _sceneData;

        private Vector3 _swipeStartPos;
        private Vector3 _swipeEndPos;
        
        private float _swipeStartTime;
        private float _swipeEndTime;

        private Vector2 _swipeVector;
        private EcsEntity _entityClicked;

        //game config ?
        private float swipeMinimumDistance = .2f;
        private float swipeMaximumTime = 1f;
        private float swipeDirectionThreshold = .9f;

        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var camera = _sceneData.Camera;
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out var hitInfo))
                {
                    var entity = hitInfo.collider.GetComponent<LinkToEntity>();
                    if(entity)
                    {
                        _entityClicked = entity.entity;
                    }
                }
                _swipeStartPos = camera.ScreenToWorldPoint(Input.mousePosition);
                _swipeStartTime = Time.time;
                //Debug.Log("----");
                //Debug.Log(swipeStartTime);
                //Debug.Log(swipeStartPos.ToString());
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _swipeEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _swipeEndTime = Time.time * Time.deltaTime;

                //Debug.Log("----");
                //Debug.Log(swipeEndTime); 
                //Debug.Log(swipeEndPos.ToString());

                if (Vector2.Distance(_swipeStartPos, _swipeEndPos) >= swipeMinimumDistance && 
                    (_swipeEndTime - _swipeStartTime) <= swipeMaximumTime)
                {
                    Vector3 swipeDirection = _swipeEndPos - _swipeStartPos;
                    Vector2 swipeDirection2D = new Vector2(swipeDirection.x, swipeDirection.y).normalized;

                    if(Vector2.Dot(Vector2.up, swipeDirection2D) > swipeDirectionThreshold)
                    {
                        _swipeVector = Vector2.up;
                    }
                    if (Vector2.Dot(Vector2.down, swipeDirection2D) > swipeDirectionThreshold)
                    {
                        _swipeVector = Vector2.down;
                    }
                    if (Vector2.Dot(Vector2.left, swipeDirection2D) > swipeDirectionThreshold)
                    {
                        _swipeVector = Vector2.left;
                    }
                    if (Vector2.Dot(Vector2.right, swipeDirection2D) > swipeDirectionThreshold)
                    {
                        _swipeVector = Vector2.right;
                    }

                    _entityClicked.Get<CheckMoveEvent>().moveVector = _swipeVector;
                    //Debug.Log("add move event");
                    //Debug.DrawLine(swipeStartPos, swipeEndPos, Color.red);
                }
            }
        }
    }
}