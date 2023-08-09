using System;
using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;
using UnityEngine.Pool;

namespace Draw
{
    public class SnakeDraw : MonoBehaviour, ISnakeDraw
    {
        [SerializeField] private SnakeDirection head;
        [SerializeField] private SnakeDirection tail;
        [SerializeField] private SnakeBodyDirection bodyPrefab;

        private ObjectPool<SnakeBodyDirection> _bodyPool;

        private readonly List<SnakeBodyDirection> _bodyList = new();

        private void Awake()
        {
            head.gameObject.SetActive(false);
            tail.gameObject.SetActive(false);
            bodyPrefab.gameObject.SetActive(false);

            _bodyPool = new ObjectPool<SnakeBodyDirection>(() =>
            {
                var body = Instantiate(bodyPrefab);
                return body;
            });
        }

        public void Draw(IEnumerable<BodyPart> bodyEnumerable)
        {
            foreach (var b in _bodyList)
            {
                _bodyPool.Release(b);
            }

            _bodyList.Clear();

            var body = bodyEnumerable.ToArray();
            for (var i = 0; i < body.Length; i++)
            {
                Transform bodyTransform = null;

                BodyPart lastBody = i > 0 && i < body.Length ? body[i - 1] : null;
                BodyPart bodyPart = body[i];
                BodyPart nextBody = i < body.Length - 1 ? body[i + 1] : null;

                if (bodyPart.BodyType == BodyType.Head)
                {
                    bodyTransform = CreateHead(bodyPart, nextBody);
                }

                if (bodyPart.BodyType == BodyType.Body)
                {
                    bodyTransform = CreateBody(lastBody, nextBody, bodyPart);
                }

                if (bodyPart.BodyType == BodyType.Tail)
                {
                    bodyTransform = CreateTail(bodyPart, lastBody);
                }

                bodyTransform.gameObject.SetActive(true);
                bodyTransform.transform.position = bodyPart.PointToMap.GetVector3();
            }
        }

        private Transform CreateHead(BodyPart bodyPart, BodyPart nextBody)
        {
            if (bodyPart.PointToMap.X > nextBody.PointToMap.X)
            {
                head.SetDirection(Direction.Right);
            }
            else if (bodyPart.PointToMap.X < nextBody.PointToMap.X)
            {
                head.SetDirection(Direction.Left);
            }
            else
            {
                if (bodyPart.PointToMap.Y < nextBody.PointToMap.Y)
                {
                    head.SetDirection(Direction.Down);
                }
                else
                {
                    head.SetDirection(Direction.Up);
                }
            }
            
            return head.transform;
        }

        private Transform CreateTail(BodyPart bodyPart, BodyPart lastBody)
        {
            if (bodyPart.PointToMap.X > lastBody.PointToMap.X)
            {
                tail.SetDirection(Direction.Right);
            }
            else if (bodyPart.PointToMap.X < lastBody.PointToMap.X)
            {
                tail.SetDirection(Direction.Left);
            }
            else
            {
                if (bodyPart.PointToMap.Y < lastBody.PointToMap.Y)
                {
                    tail.SetDirection(Direction.Down);
                }
                else
                {
                    tail.SetDirection(Direction.Up);
                }
            }
            
            return tail.transform;
        }

        private Transform CreateBody(BodyPart lastBody, BodyPart nextBody, BodyPart bodyPart)
        {
            var newBody = _bodyPool.Get();
            _bodyList.Add(newBody);

            var lastPoint = lastBody.PointToMap;
            var nextPoint = nextBody.PointToMap;

            var x = bodyPart.PointToMap.X;
            var y = bodyPart.PointToMap.Y;

            if (lastPoint.X < x && nextPoint.X > x || nextPoint.X < x && lastPoint.X > x)
            {
                newBody.SetSprite(BodyDirection.Horizontal);
            }
            else if (lastPoint.X < x && nextPoint.Y < y || nextPoint.X < x && lastPoint.Y < y)
            {
                newBody.SetSprite(BodyDirection.DownLeft);
            }
            else if (lastPoint.Y > y && nextPoint.Y < y || nextPoint.Y > y && lastPoint.Y < y)
            {
                newBody.SetSprite(BodyDirection.Vertical);
            }
            else if (lastPoint.Y > y && nextPoint.X < x || nextPoint.Y > y && lastPoint.X < x)
            {
                newBody.SetSprite(BodyDirection.UpLeft);
            }
            else if (lastPoint.X > x && nextPoint.Y > y || nextPoint.X > x && lastPoint.Y > y)
            {
                newBody.SetSprite(BodyDirection.UpRight);
            }
            else if (lastPoint.Y < y && nextPoint.X > x || nextPoint.Y < y && lastPoint.X > x)
            {
                newBody.SetSprite(BodyDirection.DownRight);
            }

            return newBody.transform;
        }
    }
}