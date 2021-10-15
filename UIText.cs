using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames1013
{
    class UIText : Actor
    {
        private string _text;
        private int _width;
        private int _height;

        public string Text 
        {
            get { return _text; }
            set { _text = value; }
        }

        public int Width 
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height 
        {
            get { return _height; }
            set { _height = value; }
        }

        //Sets the starting values for the text box.
        public UIText(float x, float y, string name, ConsoleColor color, int width, int height, string text = "") 
            : base('\0', x, y, false, name, color) 
        {
            _text = text;
            _width = width;
            _height = height;
        }

        public override void Draw()
        {
            int cursorPosX = (int)Position.X;
            int CursorPosY = (int)Position.Y;

            Icon currentLetter = new Icon { color = Icon.color };

            //Convert the string for text into a character array
            char[] textChars = Text.ToCharArray();

            //Iterate through all characters in the string
            for (int i = 0; i < textChars.Length; i++)
            {
                //Set the icon symbol to the current character in the array.
                currentLetter.Symbol = textChars[i];

                if (currentLetter.Symbol == '\n') 
                {
                    cursorPosX = (int)Position.X;
                    CursorPosY++;
                    continue;
                }

                //Add the current character to the buffer
                Engine.Render(currentLetter, new Vector2 { X = cursorPosX, Y = CursorPosY });

                //Increment the cursor position
                cursorPosX++;

                if (cursorPosX - (int)Position.X > Width) 
                {
                    cursorPosX = (int)Position.X;
                    CursorPosY++;
                }

                if (CursorPosY - (int)Position.Y > Height)
                    break;
            }
        }
    }
}
