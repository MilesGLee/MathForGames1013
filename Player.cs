using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames1013
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Actor _child;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(char icon, float x, float y, float speed, string name = "Actor", ConsoleColor color = ConsoleColor.Cyan, ConsoleColor bgc = ConsoleColor.Black)
            : base(icon, x, y, name, color, bgc)
        {
            _speed = speed;

        }

        public Actor Child
        {
            get { return _child; }
            set { _child = value; }
        }

        public override void Update()
        {
            Vector2 moveDirection = new Vector2();

            ConsoleKey keyPessed = Engine.GetNewtKey();

            if (keyPessed == ConsoleKey.A)
            {
                moveDirection = new Vector2 { X = -1 };
                if (_child != null)
                    _child.Postion = Postion;
            }
            if (keyPessed == ConsoleKey.D)
            {
                moveDirection = new Vector2 { X = 1 };
                if (_child != null)
                    _child.Postion = Postion;
            }
            if (keyPessed == ConsoleKey.W)
            {
                moveDirection = new Vector2 { Y = -1 };
                if (_child != null)
                    _child.Postion = Postion;
            }
            if (keyPessed == ConsoleKey.S)
            {
                moveDirection = new Vector2 { Y = 1 };
                if (_child != null)
                    _child.Postion = Postion;
            }
            if (keyPessed == ConsoleKey.Spacebar) 
            {
                if (_child != null)
                    _child = null;
            }

            Velocity = moveDirection * Speed;

            Postion += Velocity;
        }

        public override void Draw()
        {
            Engine.Render(Icon, Postion);
        }

        public override void OnCollision(Actor actor)
        {
            //Engine.CloseApplication();
            _child = actor;
        }
    }
}
