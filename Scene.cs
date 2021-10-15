using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames1013
{
    class Scene
    {
        // Array made for the actors in the scene
        private Actor[] _actors;
        private Actor[] _UIElements;

        /// Makes actor in a Scene
        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];
        }

        // calls start for all of the actors in the actors array
        public virtual void Start()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Start();



        }

        
        // calls the update for the actors in the actors array
        public virtual void Update()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update();

                for (int j = 0; j < _actors.Length; j++) 
                {
                    if (_actors[i].Position == _actors[j].Position && j != i)
                    {
                        _actors[i].OnCollision(_actors[j]);
                    }
                }
            }

        }

        
        // calls Draw for the actors in the actors array
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        public virtual void DrawUI() 
        {
            for (int i = 0; i < _UIElements.Length; i++) 
            {
                _UIElements[i].Draw();
            }
        }

        
        // calls end for actors in the actors array
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].End();
        }

        
        // makes a array and adds it to the actors array 
        public void AddActor(Actor actor)
        {
            //makes a new array called temArray and mades it the lengh of actors + a nother spot
            Actor[] temArray = new Actor[_actors.Length + 1];

            //incremens through the actors array
            for (int i = 0; i < _actors.Length; i++)
            {
                temArray[i] = _actors[i];
            }

            //sets temArray to the actors array and set it to actor
            temArray[_actors.Length] = actor;

            //then sets actors to temarray
            _actors = temArray;

        }

        public void AddUIElement(Actor UI)
        {
            //makes a new array called temArray and mades it the lengh of actors + a nother spot
            Actor[] temArray = new Actor[_UIElements.Length + 1];

            //incremens through the actors array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                temArray[i] = _UIElements[i];
            }

            //sets temArray to the actors array and set it to actor
            temArray[_UIElements.Length] = UI;

            //then sets actors to temarray
            _UIElements = temArray;

        }

        // makes a new array then subtracts a existing actor form that array
        public virtual bool RemoveActor(Actor actor)
        {
            //create a varialbe to store if the removal was successful
            bool actorRemoved = false;

            //created a new array that is small than the original array.
            Actor[] temArray = new Actor[_actors.Length - 1];

            //is there to the second array and not have space from removed actor.
            int j = 0;

            //incremens through the temArray
            for (int i = 0; i < temArray.Length; i++)
            {
                //sais that if actor is not equal to the actor that is choosen then dont go into but..
                if (_actors[i] != actor)
                {
                    //make temArray have j and make it equal to actors with i so there is no left over space in the array.
                    temArray[j] = _actors[i];
                    //increment j
                    j++;
                }
                //if none of that is needed return true.
                else
                    actorRemoved = true;

            }

            //will only happen if the actor is being removed and will the set actors with temArray.
            if (actorRemoved)
                _actors = temArray;

            //...then returns
            return actorRemoved;
        }
    }
}
