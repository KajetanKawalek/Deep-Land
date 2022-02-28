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
        public Vector2 positionInWorld;

        Vector2 lastPositionInWorld;

        int movementDirection;
        bool tryJump;
        bool isJumping;
        bool onGround;

        int count;
        public int jumpTime;
        int count2;

        public Player(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, Vector2 _positionInWorld, bool _hasGravity, int _health = 10, int _maxHealth = 10, int _armour = 1) : base(_name, _display, _color, _positionInArray, _hasGravity, _health, _maxHealth, _armour)
        {
            positionInWorld = _positionInWorld;
            lastPositionInWorld = _positionInWorld;
        }

        public override void PreUpdate()
        {
            tryJump = false;
            movementDirection = 0;
            if (Input.PressedKeys[Input.Keys.A])
                movementDirection = -1;
            if (Input.PressedKeys[Input.Keys.D])
                movementDirection = 1;
            if (Input.PressedKeys[Input.Keys.D] && Input.PressedKeys[Input.Keys.A])
                movementDirection = 0;

            if (Input.PressedKeys[Input.Keys.SpaceBar] || Input.PressedKeys[Input.Keys.W])
                tryJump = true;
        }

        public override void Update()
        {
            //Check for floor
            Cell floor = CheckCell(new Vector2(positionInArray.X, positionInArray.Y + 1));
            if(!(floor is Fluid))
            {
                onGround = CheckForNotEmpty(new Vector2(positionInArray.X, positionInArray.Y + 1));
            }

            //Jump
            if (onGround && tryJump)
            {
                InitJump();
            }

            if (isJumping)
            {
                UpdateJump();
            }

            //Move
            Move();

            //Gravity
            if (hasGravity)
            {
                if (count > 10)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                    {
                        MoveTo(new Vector2(positionInArray.X, positionInArray.Y + 1));
                        positionInWorld.Y += 1;
                    }
                    else
                    {
                        if (CheckCell(new Vector2(positionInArray.X, positionInArray.Y + 1)) is Fluid)
                        {
                            SwitchPlace(new Vector2(positionInArray.X, positionInArray.Y + 1));
                            positionInWorld.Y += 1;
                        }
                    }

                    count = 0;
                }
                count++;
            }
        }

        public override void PostUpdate()
        {
            //Move World and Save
            Vector2 lastchunk = new Vector2((float)Math.Ceiling((lastPositionInWorld.X + 1) / 15), (float)Math.Ceiling((lastPositionInWorld.Y + 1) / 15));
            Vector2 thischunk = new Vector2((float)Math.Ceiling((positionInWorld.X + 1) / 15), (float)Math.Ceiling((positionInWorld.Y + 1) / 15));

            if (thischunk.X != lastchunk.X || thischunk.Y != lastchunk.Y)
            {
                World.SaveLoadedCells();
                World.LoadCells(positionInWorld);
            }

            lastPositionInWorld = positionInWorld;
        }

        void InitJump()
        {
            hasGravity = false;
            isJumping = true;
        }

        public void ReloadJump(string t ,int _jumpTime)
        {
            if(t == "t")
            {
                hasGravity = false;
                isJumping = true;
                jumpTime = _jumpTime;
                count2 = 10;
            }
        }

        void UpdateJump()
        {
            if (jumpTime >= 5 || (!tryJump && jumpTime > 1))
            {
                hasGravity = true;
                isJumping = false;
                jumpTime = 0;
                count2 = 0;
            }
            else if (count2 > 10)
            {
                if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y - 1)))
                {
                    MoveTo(new Vector2(positionInArray.X, positionInArray.Y - 1));
                    positionInWorld.Y -= 1;
                }
                else
                {
                    if (CheckCell(new Vector2(positionInArray.X, positionInArray.Y - 1)) is Fluid)
                    {
                        SwitchPlace(new Vector2(positionInArray.X, positionInArray.Y - 1));
                        positionInWorld.Y -= 1;
                    }
                }

                count2 = 0;
                jumpTime++;
            }
            count2++;
        }

        void Move()
        {
            if (count > 10)
            {
                if (movementDirection == -1)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X - 1, positionInArray.Y)))
                    {
                        MoveTo(new Vector2(positionInArray.X - 1, positionInArray.Y));
                        positionInWorld.X -= 1;
                    }
                    else
                    {
                        if (CheckCell(new Vector2(positionInArray.X - 1, positionInArray.Y)) is Fluid)
                        {
                            SwitchPlace(new Vector2(positionInArray.X - 1, positionInArray.Y));
                            positionInWorld.X -= 1;
                        }
                    }
                }
                else if (movementDirection == 1)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X + 1, positionInArray.Y)))
                    {
                        MoveTo(new Vector2(positionInArray.X + 1, positionInArray.Y));
                        positionInWorld.X += 1;
                    }
                    else
                    {
                        if (CheckCell(new Vector2(positionInArray.X + 1, positionInArray.Y)) is Fluid)
                        {
                            SwitchPlace(new Vector2(positionInArray.X + 1, positionInArray.Y));
                            positionInWorld.X += 1;
                        }
                    }
                }
                count = 0;
            }
            count++;
        }
    }
}
