using UnityEngine;
using System.Collections.Generic;

public enum Stages
{
    stage1,
    stage2,
    stage3,
    stage4
}

public class TextStages : MonoBehaviour
{
    public List<string> Stage1 = new List<string>
    {
        "Convoy three delayed",
        "Signal weak",
        "Log ammo counts",
        "Status Good",
        "Power flickers",
        "Cycle tables",
        "Patrol seven late",
        "Rations good",
        "Review cipher C",
        "Unknown transmission",
        "Silence on open freq",
        "Parts eta LATE",
        "Send decoded logs",
        "Storm incoming Hold signals",
        "schedule unchanged"
    };
    public List<string> Stage2 = new List<string>
    {
        "Supply route stalled.",
        "Fuel reserves thin.",
        "Outpost echo silent.",
        "Reinforce line bravo.",
        "Evac teams delayed.",
        "Shelter capacity strained.",
        "Contact lost with ridge post.",
        "Hold defensive pattern.",
        "Broadcast clarity fading.",
        "Medical stock low.",
        "Patrol unit scattered.",
        "Civilians refusing orders.",
        "Report casualties rising.",
        "Command channel unstable.",
        "Reinforcements unlikely.",
    };
    public List<string> Stage3 = new List<string>
    {
        "They’re in the smoke.",
        "I hear them.",
        "No time left.",
        "They found me.",
        "Shadows are closer.",
        "This is it.",
        "I won’t run.",
        "They’re coming now.",
        "I see figures.",
        "End’s at my door.",
        "Footsteps. Getting closer.",
        "I’m already gone.",
        "Too late now.",
        "Can’t hold on.",
        "They see me.",
    };
    public List<string> Stage4 = new List<string>
    {

    };
}
