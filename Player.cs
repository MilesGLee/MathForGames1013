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

        public Player(char icon, float x, float y, float speed, string name = "Actor", ConsoleColor color = ConsoleColor.Cyan)
            : base(icon, x, y, name, color)
        {
            _speed = speed;

        }

        public override void Update()
        {
            Vector2 moveDirection = new Vector2();

            ConsoleKey keyPessed = Engine.GetNewtKey();

            if (keyPessed == ConsoleKey.A)
                moveDirection = new Vector2 { X = -1 };
            if (keyPessed == ConsoleKey.D)
                moveDirection = new Vector2 { X = 1 };
            if (keyPessed == ConsoleKey.W)
                moveDirection = new Vector2 { Y = -1 };
            if (keyPessed == ConsoleKey.S)
                moveDirection = new Vector2 { Y = 1 };

            Velocity = moveDirection * Speed;

            Postion += Velocity;

        }

        public override void OnCollision(Actor actor)
        {
            Engine.CloseApplication();
        }
    }
}
