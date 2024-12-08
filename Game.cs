using Avalonia.Input;
using Sokoban.Architecture;

namespace Sokoban;

public static class Game
{
	private const string map_1 = @"
     WWWWWWW 
     W  W  W 
     W  CC W 
WWWWWW CW  W 
WTT WW  W  WW
W   W       W
WT    C C   W
W   W    WC W
WTTTWWW W  WW
WWWWWW     W 
     W PW  W 
     wwWWWWW ";

    private const string map_2 = @"
WWWWWWWWWWWWWWWWWWWW
W        W         W
W        WT        W
W        W         W
W        W         W
W        WWWWW  WWWW
W                  W
W                  W
W        W         W
W       WWW        W
W        W         W
W                  W
W                  W
W                  W
W   B              W
W                  W
W        P         W
WWWWWWWWWWWWWWWWWWWW";

    //хорошая,но я криво реализовал толкание ящиков, поэтому не подходит
    private const string map_3 = @"
    WWWWW             
    W   W             
    WB  W             
  WWW  BWWW           
  W  B  B W           
WWW W WWW W     WWWWWW
W   W WWW WWWWWWW  TTW
W B  B             TTW
WWWWW WWWW WPWWWW  TTW
    W      WWW  WWWWWW
    WWWWWWWW          ";

    public static ICreature[,] Map;
	public static int Scores;
	public static bool IsOver;

	public static Key KeyPressed;
	public static int MapWidth => Map.GetLength(0);
	public static int MapHeight => Map.GetLength(1);

	public static void CreateMap()
	{
		Map = CreatureMapCreator.CreateMap(map_1);
	}
}