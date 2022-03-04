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
        Vector2 lastPositionInWorld;

        int movementDirection;
        bool tryJump;
        bool isJumping;
        bool onGround;

        int count;

        public int jumpTime;

        public Player(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, Vector2 _positionInWorld, bool _hasGravity, Vector2 _size, int _health = 10, int _maxHealth = 10, int _armour = 1) : base(_name, _display, _color, _positionInArray, _hasGravity, _size, _health, _maxHealth, _armour)
        {
            positionInWorld = _positionInWorld;
            lastPositionInWorld = _positionInWorld;
        }

        public override void PreUpdate()
        {
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
            if (count > 100)
                count = 0;
            count++;

            //Attached Cells
            ClearAttachedCells(positionInArray);
            CreateAttachedCell(new Vector2(positionInArray.X + 1, positionInArray.Y), '⌊', ConsoleColor.DarkYellow);
            CreateAttachedCell(new Vector2(positionInArray.X, positionInArray.Y - 1), '⎛', ConsoleColor.DarkGreen);
            CreateAttachedCell(new Vector2(positionInArray.X + 1, positionInArray.Y - 1), '⎞', ConsoleColor.DarkGreen);
            CreateAttachedCell(new Vector2(positionInArray.X, positionInArray.Y - 2), '·', ConsoleColor.White);
            CreateAttachedCell(new Vector2(positionInArray.X + 1, positionInArray.Y - 2), '·', ConsoleColor.White);
            CreateAttachedCell(new Vector2(positionInArray.X, positionInArray.Y - 3), '╱', ConsoleColor.Yellow);
            CreateAttachedCell(new Vector2(positionInArray.X + 1, positionInArray.Y - 3), '╲', ConsoleColor.Yellow);

            //Check for floor
            onGround = !BottomCollide().OfType<Cell>().All(n => n == null || n is Fluid);

            if (count % 5 == 0)
            {
                //Jump
                if (onGround && tryJump)
                {
                    InitJump();
                }

                if (isJumping)
                {
                    UpdateJump();
                }

                //Gravity
                Gravity();

                tryJump = false;
            }

            if (count % 10 == 0)
            {
                //Move
                Move();

                movementDirection = 0;
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
                PlayerData.AssignPlayer();
            }

            lastPositionInWorld = positionInWorld;
        }

        void InitJump()
        {
            hasGravity = false;
            isJumping = true;
        }

        public void ReloadJump(string t, int _jumpTime)
        {
            if (t == "t")
            {
                hasGravity = false;
                isJumping = true;
                jumpTime = _jumpTime;
            }
        }

        void UpdateJump()
        {
            if (jumpTime >= 5 || (!tryJump && jumpTime > 1))
            {
                hasGravity = true;
                isJumping = false;
                jumpTime = 0;
            }
            else
            {
                if (TopCollide().All(n => n == null))
                {
                    MoveTo(new Vector2(positionInArray.X, positionInArray.Y - 1));
                    positionInWorld.Y -= 1;
                }
                else if (TopCollide().All(n => n is Fluid || n == null))
                {
                    MoveAndDisplace(new Vector2(0, -1));
                    positionInWorld.Y -= 1;
                }
                jumpTime++;
            }
        }

        void Move()
        {
            if (movementDirection == -1)
            {
                if (LeftCollide().All(n => n == null))
                {
                    MoveTo(new Vector2(positionInArray.X - 1, positionInArray.Y));
                    positionInWorld.X -= 1;
                }
                else if (LeftCollide().All(n => n is Fluid || n == null))
                {
                    MoveAndDisplace(new Vector2(-1, 0));
                    positionInWorld.X -= 1;
                }
            }
            else if (movementDirection == 1)
            {
                if (RightCollide().All(n => n == null))
                {
                    MoveTo(new Vector2(positionInArray.X + 1, positionInArray.Y));
                    positionInWorld.X += 1;
                }
                else if (RightCollide().All(n => n is Fluid || n == null))
                {
                    MoveAndDisplace(new Vector2(1, 0));
                    positionInWorld.X += 1;
                }
            }
        }
    }
}
