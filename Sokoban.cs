using Sokoban.Architecture;
using Avalonia.Input;

namespace Sokoban
{
    class Wall : ICreature
    {
        public string GetImageFileName() => "Wall.png";
        public int GetDrawingPriority() => 3;
        public CreatureCommand Act(int x, int y) => new CreatureCommand();
        public bool DeadInConflict(ICreature enemy) => false;
    }

    class Target : ICreature
    {
        public string GetImageFileName() => "Target.png";
        public int GetDrawingPriority() => 2;
        public CreatureCommand Act(int x, int y) => new CreatureCommand();
        public bool DeadInConflict(ICreature enemy)
        {
            if (enemy is Box)
            {
                Game.Scores += 10;
                return true;
            }
            return false;
        }
    }

    class Box : ICreature
    {
        private bool isOnTarget;

        public string GetImageFileName()
        {
            if (isOnTarget)
            {
                return "WinBox.png";
            }
            return "Box.png";
        }
        public int GetDrawingPriority() => -1;

        public CreatureCommand Act(int x, int y)
        {
            var playerDirection = GetPlayerPushDirection(x, y);

            if (playerDirection != null)
            {
                int deltaX = playerDirection.DeltaX;
                int deltaY = playerDirection.DeltaY;

                int targetX = x + deltaX;
                int targetY = y + deltaY;

                if (CanMove(x, y, deltaX, deltaY))
                {
                    isOnTarget = Game.Map[targetX, targetY] is Target;

                    return new CreatureCommand { DeltaX = deltaX, DeltaY = deltaY };
                }
            }

            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature enemy) => false;

        public bool CanMove(int x, int y, int deltaX, int deltaY)
        {
            int newX = x + deltaX, newY = y + deltaY;

            if (newX < 0 || newX >= Game.MapWidth || newY < 0 || newY >= Game.MapHeight)
                return false;

            var nextEntity = Game.Map[newX, newY];
            return nextEntity is null || nextEntity is Target;
        }

        private CreatureCommand? GetPlayerPushDirection(int x, int y)
        {
            if (x > 0 && Game.Map[x-1 , y] is Player)
                return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
            if (x < Game.MapWidth && Game.Map[x+1, y] is Player)
                return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
            if (y > 0 && Game.Map[x, y-1] is Player)
                return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
            if (y < Game.MapHeight && Game.Map[x, y+1] is Player)
                return new CreatureCommand { DeltaX = 0, DeltaY = -1 };

            return null;
        }
    }

    class Player : ICreature
    {
        public string GetImageFileName() => "Player.png";
        public int GetDrawingPriority() => 0;

        public CreatureCommand Act(int x, int y)
        {
            var step = HandleKeys();
            int newX = x + step.DeltaX, newY = y + step.DeltaY;

            if (newX < 0 || newX >= Game.MapWidth || newY < 0 || newY >= Game.MapHeight)
                return new CreatureCommand();


            var nextEntity = Game.Map[newX, newY];

            if (nextEntity is null || nextEntity is Target)
            {
                return step;
            }

            if (nextEntity is Box box)
            {
                int boxNewX = newX + step.DeltaX;
                int boxNewY = newY + step.DeltaY;

                if (box.CanMove(newX, newY, step.DeltaX, step.DeltaY))
                {
                    Game.Map[boxNewX, boxNewY] = box;
                    Game.Map[newX, newY] = this;
                    return step;
                }
            }

            return new CreatureCommand();
        }

        private CreatureCommand HandleKeys()
        {
            var step = new CreatureCommand();
            if (Game.KeyPressed == Key.Left)
                step.DeltaX = -1;
            else if (Game.KeyPressed == Key.Right)
                step.DeltaX = 1;
            else if (Game.KeyPressed == Key.Up)
                step.DeltaY = -1;
            else if (Game.KeyPressed == Key.Down)
                step.DeltaY = 1;

            return step;
        }

        public bool DeadInConflict(ICreature enemy) => false;
    }

}