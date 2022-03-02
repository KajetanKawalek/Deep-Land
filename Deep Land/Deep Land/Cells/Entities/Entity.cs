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
        protected Vector2 size;

        int count;

        public Entity(string _name, char _display, ConsoleColor _color, Vector2 _positionInArray, bool _hasGravity, Vector2 _size, int _health = 10, int _maxHealth = 10, int _armour = 1)
        {
            name = _name;
            display = _display;
            color = _color;
            positionInArray = _positionInArray;
            hasGravity = _hasGravity;
            heath = _health;
            maxHealth = _maxHealth;
            armour = _armour;
            size = _size;
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

        /*public void MoveIntoFluid(Vector2 translation)
        {
            if(translation.X == 1)
            {
                List<Cell> cells = new List<Cell>();
                for(int i = 0; i < size.Y; i++)
                {
                    if (CheckForCell(new Vector2((int)positionInArray.X + (int)size.X, (int)positionInArray.Y - i)))
                        cells.Add(World.loadedCellsArray[(int)positionInArray.X + (int)size.X, (int)positionInArray.Y - i]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < size.Y; i++)
                {
                    if (i < cells.Count)
                        cells[i].MoveTo(new Vector2(cells[i].positionInArray.X - size.X - 1, cells[i].positionInArray.Y), true);
                }

                positionInArray = positionInArray + translation;
            }
            if (translation.X == -1)
            {
                List<Cell> cells = new List<Cell>();
                for (int i = 0; i < size.Y; i++)
                {
                    if(CheckForCell(new Vector2((int)positionInArray.X - 1, (int)positionInArray.Y - i)))
                        cells.Add(World.loadedCellsArray[(int)positionInArray.X - 1, (int)positionInArray.Y - i]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < size.Y; i++)
                {
                    if(i < cells.Count)
                        cells[i].MoveTo(new Vector2(cells[i].positionInArray.X + size.X + 1, cells[i].positionInArray.Y), true);
                }

                positionInArray = positionInArray + translation;
            }
            if (translation.Y == 1)
            {
                List<Cell> cells = new List<Cell>();
                for (int i = 0; i < size.X; i++)
                {
                    if (CheckForCell(new Vector2((int)positionInArray.X + i, (int)positionInArray.Y + 1)))
                        cells.Add(World.loadedCellsArray[(int)positionInArray.X + i, (int)positionInArray.Y + 1]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < size.X; i++)
                {
                    if (i < cells.Count)
                        cells[i].MoveTo(new Vector2(cells[i].positionInArray.X, cells[i].positionInArray.Y - size.Y - 1), true);
                }

                positionInArray = positionInArray + translation;
            }
            if (translation.Y == -1)
            {
                List<Cell> cells = new List<Cell>();
                for (int i = 0; i < size.X; i++)
                {
                    if (CheckForCell(new Vector2((int)positionInArray.X + i, (int)positionInArray.Y + (int)size.Y)))
                        cells.Add(World.loadedCellsArray[(int)positionInArray.X + i, (int)positionInArray.Y + (int)size.Y]);
                }

                World.loadedCellsArray[(int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y] = this;

                for (int i = 0; i < size.X; i++)
                {
                    if (i < cells.Count)
                        cells[i].MoveTo(new Vector2(cells[i].positionInArray.X, cells[i].positionInArray.Y + size.Y + 1), true);
                }

                positionInArray = positionInArray + translation;
            }
        }*/

        public void MoveAndDisplace(Vector2 translation)
        {
            List<Cell> cells = new List<Cell>();

            if (translation.X == 1)
            {
                cells.AddRange(RightCollide());

                MoveTo(new Vector2((int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y), true);

                foreach (Cell cell in cells)
                {
                    if (cell != null)
                        cell.MoveTo(new Vector2(cell.positionInArray.X - size.X, cell.positionInArray.Y), true);
                }
            }
            if (translation.X == -1)
            {
                cells.AddRange(LeftCollide());

                MoveTo(new Vector2((int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y), true);

                foreach (Cell cell in cells)
                {
                    if (cell != null)
                        cell.MoveTo(new Vector2(cell.positionInArray.X + size.X, cell.positionInArray.Y), true);
                }
            }
            if (translation.Y == 1)
            {
                cells.AddRange(BottomCollide());

                MoveTo(new Vector2((int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y), true);

                foreach (Cell cell in cells)
                {
                    if(cell != null)
                        cell.MoveTo(new Vector2(cell.positionInArray.X, cell.positionInArray.Y - size.Y), true);
                }
            }
            if (translation.Y == -1)
            {
                cells.AddRange(TopCollide());

                MoveTo(new Vector2((int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y), true);

                foreach (Cell cell in cells)
                {
                    if (cell != null)
                        cell.MoveTo(new Vector2(cell.positionInArray.X, cell.positionInArray.Y + size.Y), true);
                }
            }
        }

        /*public void SwitchPlaceWithSize(Vector2 newPositionInArray)
        {
            List<Cell> cells = new List<Cell>(); 

            for(int i = 1; i < size.X; i++)
            {
                for (int i2 = 1; i2 < size.Y; i2++)
                {
                    if (CheckForCell(new Vector2(newPositionInArray.X + i, newPositionInArray.Y + i2)))
                        cells.Add(new Vector2(newPositionInArray.X + i, newPositionInArray.Y + i2));
                        SwitchPlace(new Vector2(newPositionInArray.X + i, newPositionInArray.Y + i2));
                }
            }
        }*/

        public Cell[] RightCollide()
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < size.Y; i++)
            {
                cells.Add(CheckCell(new Vector2(positionInArray.X + size.X, positionInArray.Y - i)));
            }

            return cells.ToArray();
        }

        public Cell[] LeftCollide()
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < size.Y; i++)
            {
                cells.Add(CheckCell(new Vector2(positionInArray.X - 1, positionInArray.Y - i)));
            }

            return cells.ToArray();
        }

        public Cell[] BottomCollide()
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < size.X; i++)
            {
                cells.Add(CheckCell(new Vector2(positionInArray.X + i, positionInArray.Y + 1)));
            }
            return cells.ToArray();
        }

        public Cell[] TopCollide()
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < size.X; i++)
            {
                cells.Add(CheckCell(new Vector2(positionInArray.X + i, positionInArray.Y - size.Y)));
            }

            return cells.ToArray();
        }
    }
}
