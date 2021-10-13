using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;

namespace MathForGames1013
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private static Icon[,] _buffer;

        //Called to begin the application
        public void Run() 
        {
            //Call start for the entire application
            Start();

            //Loop until the application is told to close
            while (!_applicationShouldClose) 
            {
                Update();
                Draw();
                Thread.Sleep(50);
            }
            //Call end for entire application
            End();
        }

        //Called when the application starts
        private void Start() 
        {
            Scene scene = new Scene();
            //Actor actor = new Actor('P', new MathLibrary.Vector2 { X = 0, Y = 0}, "Actor1", ConsoleColor.Magenta, ConsoleColor.Black);
            //Actor actor2 = new Actor('E', new MathLibrary.Vector2 { X = 1, Y = 1}, "Actor2", ConsoleColor.Green, ConsoleColor.Black);
            //Actor actor3 = new Actor('I', new MathLibrary.Vector2 { X = 2, Y = 2}, "Actor3", ConsoleColor.Blue, ConsoleColor.Black);
            Actor child = new Actor('■', new MathLibrary.Vector2 { X = 4, Y = 4}, "child", ConsoleColor.DarkGray, ConsoleColor.Black);
            Player player = new Player('☻', 5, 5, 1, "Player", ConsoleColor.White);

            //adds the actor to the scene and takes in that actor
            //scene.AddActor(actor);
            //scene.AddActor(actor2);
            //scene.AddActor(actor3);
            scene.AddActor(child);
            scene.AddActor(player);

            //player.Child = child;

            _currentSceneIndex = AddScene(scene);

            _scenes[_currentSceneIndex].Start();
        }

        //Called everytime the the game loops
        private void Update() 
        {
            _scenes[_currentSceneIndex].Update();

            while (Console.KeyAvailable) 
            {
                Console.ReadKey(true);
            }
        }

        //Called every time the game loops to update visuals
        private void Draw() 
        {
            //clear the the current screen in the last frame
            _buffer = new Icon[Console.WindowWidth, Console.WindowHeight - 1];
            //resests the cursors positon back to 0,0 to draw over.
            Console.SetCursorPosition(0, 0);

            //add all of the icons back to the buffer
            _scenes[_currentSceneIndex].Draw();

            //incraments through the buffer
            for (int y = 0; y < _buffer.GetLength(1); y++)
            {
                for (int x = 0; x < _buffer.GetLength(0); x++)
                {
                    if (_buffer[x, y].Symbol == '\0')
                    {
                        _buffer[x, y].Symbol = ' ';
                        _buffer[x, y].bgColor = ConsoleColor.Black;
                    }

                    //sets the color of the buffers items
                    Console.BackgroundColor = _buffer[x, y].bgColor;
                    Console.ForegroundColor = _buffer[x, y].color;
                    //sets the symbol of the buffers items
                    Console.Write(_buffer[x, y].Symbol);

                    //makes the cursorVisible false now there is no cursor
                    Console.CursorVisible = false;
                }

                //skip a line once the end of the row has been reached.
                Console.WriteLine();
            }
        }

        //Called when the application exits
        private void End() 
        {
            _scenes[_currentSceneIndex].End();
        }


        //Adds a scene to the engine's scene array
        public int AddScene(Scene scene) 
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into new array
            for (int i = 0; i < _scenes.Length; i++) 
            {
                tempArray[i] = _scenes[i];
            }
            //Set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length - 1;
        }
        public static ConsoleKey GetNewtKey()
        {
            //if there are no keys being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;
            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }
        /// adds the icon to the buffer to print to the screeen in the next draw call;
        /// prints the icon t the given position in the buffer
        public static bool Render(Icon icon, Vector2 position)
        {
            //if the position in the y and x are in the out of bounds...
            if (position.X < 0 || position.X >= _buffer.GetLength(0) || position.Y < 0 || position.Y >= _buffer.GetLength(0))
                //return false.
                return false;
            //Else set the buffer at the index of the given position to be the icon
            _buffer[(int)position.X, (int)position.Y] = icon;
            return true;
        }

        public static void CloseApplication() 
        {
            _applicationShouldClose = true;
        }
    }
}
