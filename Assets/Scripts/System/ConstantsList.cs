using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsList
{
    public static Dictionary<string, string> Scenes = new Dictionary<string, string>
    {
        { "OpeningScene", "HubScene" },
        { "1", "Level1" },
        { "2", "Level2" },
        { "3", "Level3" },
        { "4", "Level4" },
        { "5", "Level5" },
        { "6", "Level6" },
        { "7", "Level7" },
        { "8", "Level8" },
        { "9", "Level9" },
        { "10", "Level10" },
        { "11", "Level11" },
        { "12", "Level12" },
        { "13", "Level13" },
        { "14", "Level14" },
        { "15", "Level15" },
        { "16", "Level16" },
    };

    public static Dictionary<string, string> Instructions = new Dictionary<string, string>
    {
        { "FirstInstruction", "Drag and release the ball to fire" },
        { "NormalBar", "White blocks don't destroy the ball when hit" },
        { "BouncyBar", "Turquoise blocks are very bouncy" },
        { "ButtonDoor", "Hit the button to open the door" },
        { "DashBar", "Dash blocks will accelerate the ball according to direction" }
    };
}
