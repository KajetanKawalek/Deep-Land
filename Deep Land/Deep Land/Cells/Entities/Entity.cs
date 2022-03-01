using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Deep_Land
{
    class Entity : Cell
    {
        protected int heath;
        protected int maxHealth;
        protected int armour = 1;
        protected bool hasGravity;

        int count;

        public Entity(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, bool _hasGravity, int _health = 10, int _maxHealth = 10, int _armour = 1)
        {
            name = _name;
            display = _display;
            color = _color;
            positionInArray = _positionInArray;
            hasGravity = _hasGravity;
            heath = _health;
            maxHealth = _maxHealth;
            armour = _armour;
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {
            if(hasGravity)
            {
                if (count > 10)
                {
                    if (!CheckForCell(new Vector2(positionInArray.X, positionInArray.Y + 1)))
                    {
                        MoveTo(new Vector2(positionInArray.X, positionInArray.Y + 1));
                    }
                    else
                    {
                        if (CheckCell(new Vector2(positionInArray.X, positionInArray.Y + 1)) is Fluid)
                        {
                            SwitchPlace(new Vector2(positionInArray.X, positionInArray.Y + 1));
                        }
                    }

                    count = 0;
                }
                count++;
            }
        }

        public override void PostUpdate()
        {

        }

        public void TakeDamage(int damage)
        {
            int damageToTake = damage - armour;
            if (damageToTake < 0)
                damageToTake = 0;

            heath -= damageToTake;
        }

        public void MoveIntoFluid(Vector2 translation, Vector2 entitySize)
        {
            if(translation.X == 1)
            {
                List<Cell> cells = new List<Cell>();
                for(int i = 0; i < entitySize.Y; i++)
                {
                    cells.Add(World.loadedCellsArray[(int)positionInArray.X + (int)entitySize.X, (int)positionInArray.Y - i]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < entitySize.Y; i++)
                {
                    cells[i].MoveTo(new Vector2(cells[i].positionInArray.X - entitySize.X - 1, cells[i].positionInArray.Y), true);
                }

                positionInArray = positionInArray + translation;
            }
            if (translation.X == -1)
            {
                List<Cell> cells = new List<Cell>();
                for (int i = 0; i < entitySize.Y; i++)
                {
                    cells.Add(World.loadedCellsArray[(int)positionInArray.X - 1, (int)positionInArray.Y - i]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < entitySize.Y; i++)
                {
                    cells[i].MoveTo(new Vector2(cells[i].positionInArray.X + entitySize.X + 1, cells[i].positionInArray.Y), true);
                }

                positionInArray = positionInArray + translation;
            }
            if (translation.Y == 1)
            {
                List<Cell> cells = new List<Cell>();
                for (int i = 0; i < entitySize.X; i++)
                {
                    cells.Add(World.loadedCellsArray[(int)positionInArray.X + i, (int)positionInArray.Y + 1]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < entitySize.X; i++)
                {
                    cells[i].MoveTo(new Vector2(cells[i].positionInArray.X, cells[i].positionInArray.Y - entitySize.Y - 1), true);
                }

                positionInArray = positionInArray + translation;
            }
            if (translation.Y == -1)
            {
                List<Cell> cells = new List<Cell>();
                for (int i = 0; i < entitySize.X; i++)
                {
                    cells.Add(World.loadedCellsArray[(int)positionInArray.X + i, (int)positionInArray.Y + (int)entitySize.Y]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < entitySize.X; i++)
                {
                    cells[i].MoveTo(new Vector2(cells[i].positionInArray.X, cells[i].positionInArray.Y + entitySize.Y + 1), true);
                }

                positionInArray = positionInArray + translation;
            }
        }
    }
}
