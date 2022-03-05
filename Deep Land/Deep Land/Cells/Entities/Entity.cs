using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Diagnostics;

namespace Deep_Land
{
    class Entity : Cell
    {
        protected int heath;
        protected int maxHealth;
        protected int armour = 1;
        protected bool hasGravity;
        protected Vector2 size;
        protected Vector2 positionInWorld;

        List<Cell> attachedCells = new List<Cell>();

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
            positionInWorld = positionInArray + World.edgePoint;
        }

        public override void PreUpdate()
        {

        }

        public override void Update()
        {

        }

        public override void PostUpdate()
        {

        }

        public virtual void OnInteract()
        {

        }

        protected void Gravity()
        {
            if (hasGravity)
            {
                if (BottomCollide().All(n => n == null))
                {
                    MoveTo(new Vector2(positionInArray.X, positionInArray.Y + 1));
                    positionInWorld.Y += 1;
                }
                else if (BottomCollide().All(n => n is Fluid || n == null))
                {
                    MoveAndDisplace(new Vector2(0, 1));
                    positionInWorld.Y += 1;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            int damageToTake = damage - armour;
            if (damageToTake < 0)
                damageToTake = 0;

            heath -= damageToTake;
        }

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
                    {
                        if (cell.positionInArray == positionInArray)
                            cell.MoveTo(new Vector2(cell.positionInArray.X - size.X, cell.positionInArray.Y), false);
                        else
                            cell.MoveTo(new Vector2(cell.positionInArray.X - size.X, cell.positionInArray.Y), true);
                    }
                }
            }
            if (translation.X == -1)
            {
                cells.AddRange(LeftCollide());

                MoveTo(new Vector2((int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y), true);

                foreach (Cell cell in cells)
                {
                    if (cell != null)
                    {
                        if (cell.positionInArray == positionInArray)
                            cell.MoveTo(new Vector2(cell.positionInArray.X + size.X, cell.positionInArray.Y), false);
                        else
                            cell.MoveTo(new Vector2(cell.positionInArray.X + size.X, cell.positionInArray.Y), true);
                    }
                }
            }
            if (translation.Y == 1)
            {
                cells.AddRange(BottomCollide());

                MoveTo(new Vector2((int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y), true);

                foreach (Cell cell in cells)
                {
                    if (cell != null)
                    {
                        if (cell.positionInArray == positionInArray)
                            cell.MoveTo(new Vector2(cell.positionInArray.X, cell.positionInArray.Y - size.Y), false);
                        else
                            cell.MoveTo(new Vector2(cell.positionInArray.X, cell.positionInArray.Y - size.Y), true);
                    }
                }
            }
            if (translation.Y == -1)
            {
                cells.AddRange(TopCollide());

                MoveTo(new Vector2((int)positionInArray.X + (int)translation.X, (int)positionInArray.Y + (int)translation.Y), true);

                foreach (Cell cell in cells)
                {
                    if (cell != null)
                    {
                        if (cell.positionInArray == positionInArray)
                            cell.MoveTo(new Vector2(cell.positionInArray.X, cell.positionInArray.Y + size.Y), false);
                        else
                            cell.MoveTo(new Vector2(cell.positionInArray.X, cell.positionInArray.Y + size.Y), true);
                    }
                }
            }
        }

        public void CreateAttachedCell(Vector2 positionInArray, char display, ConsoleColor color)
        {
            if (!CheckForCell(positionInArray))
            {
                Block cell = new Block("?", display, color, positionInArray);
                World.loadedCellsArray[(int)positionInArray.X, (int)positionInArray.Y] = cell;
                attachedCells.Add(cell);
            }
        }

        public void ClearAttachedCells(Vector2 CoreCellPosition)
        {
            foreach (Cell cell in attachedCells)
            {
                if (cell.positionInArray != CoreCellPosition && World.loadedCellsArray[(int)cell.positionInArray.X, (int)cell.positionInArray.Y] == cell)
                    World.loadedCellsArray[(int)cell.positionInArray.X, (int)cell.positionInArray.Y] = null;
            }
            attachedCells.Clear();
        }

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
