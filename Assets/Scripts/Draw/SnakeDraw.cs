using System;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using UnityEngine.Pool;

namespace Draw
{
    public class SnakeDraw : MonoBehaviour , ISnakeDraw
    {
        [SerializeField] private Transform head;
        [SerializeField] private Transform tail;
        [SerializeField] private Transform bodyPrefab;
        
        private ObjectPool<Transform> _bodyPool;
        
        private List<Transform> _bodySnake = new();
        
        private void Awake()
        {
            head.gameObject.SetActive(false);
            tail.gameObject.SetActive(false);
            bodyPrefab.gameObject.SetActive(false);
            
            _bodyPool = new ObjectPool<Transform>(() =>
            {
                var body = Instantiate(bodyPrefab);
                return body;
            } , defaultCapacity: 20);
        }
        
        public void Draw(IEnumerable<BodyPart> body)
        {
            foreach (var bodyTr in _bodySnake)
            {
                bodyTr.gameObject.SetActive(false);
                _bodyPool.Release(bodyTr);
            }
            
            _bodySnake.Clear();
            
            foreach (var bodyPart in body)
            {
                Transform bodyTr;
                switch (bodyPart.BodyType)
                {
                    case BodyType.Head:
                        bodyTr = head;
                        break;
                    case BodyType.Body:
                        bodyTr = _bodyPool.Get();
                        _bodySnake.Add(bodyTr);
                        break;
                    case BodyType.Tail:
                        bodyTr = tail;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                bodyTr.gameObject.SetActive(true);
                bodyTr.transform.position = bodyPart.PointToMap.GetVector3();
            }
        }
    }
}