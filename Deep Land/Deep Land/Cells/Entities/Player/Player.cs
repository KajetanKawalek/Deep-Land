using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    class Player : Entity
    {
        int movementDirection;
        bool jump;
        bool onGround;

        int count;
        int jumpTime;
        int count2;

        public Player(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, bool _hasGravity, int _health = 10, int _maxHealth = 10, int _armour = 1) : base(_name, _display, _color, _positionInArray, _hasGravity, _health, _maxHealth, _armour)
        {

        }

        public override void PreUpdate()
        {
            movementDirection = 0;
            if (Input.PressedKeys[Input.Keys.A])
                movementDirection = -1;
            if (Input.PressedKeys[Input.Keys.D])
                movementDirection = 1;
            if (Input.PressedKeys[Input.Keys.D] && Input.PressedKeys[Input.Keys.A])
                movementDirection = 0;

            if (Input.PressedKeys[Input.Keys.SpaceBar] || Input.PressedKeys[Input.Keys.W])
                jump = true;
        }

        public override void Update()
        {
            onGround = CheckForNotEmpty(new Vector2(positionInArray.X, positionInArray.Y + 1));

            if(count > 5)
            {
                if (movementDirection == -1)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X - 1, positionInArray.Y)))
                    {
                        MoveTo(new Vector2(positionInArray.X - 1, positionInArray.Y));
                    }
                    else
                    {
                        if (CheckCell(new Vector2(positionInArray.X - 1, positionInArray.Y)) is Fluid)
                        {
                            SwitchPlace(new Vector2(positionInArray.X - 1, positionInArray.Y));
                        }
                    }
                }
                else if (movementDirection == 1)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X + 1, positionInArray.Y)))
                    {
                        MoveTo(new Vector2(positionInArray.X + 1, positionInArray.Y));
                    }
                    else
                    {
                        if (CheckCell(new Vector2(positionInArray.X + 1, positionInArray.Y)) is Fluid)
                        {
                            SwitchPlace(new Vector2(positionInArray.X + 1, positionInArray.Y));
                        }
                    }
                }
                count = 0;
            }
            count++;

            if (onGround && jump)
            {
                InitJump();
            }

            if(jump)
            {
                UpdateJump();
            }

            base.Update();
        }

        public override void PostUpdate()
        {

        }

        void InitJump()
        {
            hasGravity = false;
        }

        void UpdateJump()
        {
            if (count2 > 5)
            {
                if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y - 1)))
                {
                    MoveTo(new Vector2(positionInArray.X, positionInArray.Y - 1));
                }
                else
                {
                    if (CheckCell(new Vector2(positionInArray.X, positionInArray.Y - 1)) is Fluid)
                    {
                        SwitchPlace(new Vector2(positionInArray.X, positionInArray.Y - 1));
                    }
                }

                count2 = 0;
            }
            count2++;

            if (jumpTime > 30)
            {
                hasGravity = true;
                jump = false;
                jumpTime = 0;
                count2 = 0;
            }
            jumpTime++;
        }
    }
}
