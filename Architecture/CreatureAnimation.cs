using Avalonia;

namespace Sokoban.Architecture;

public class CreatureAnimation
{
	public CreatureCommand Command;
	public ICreature Creature;
	public Point Location;
	public Point TargetLogicalLocation;
}