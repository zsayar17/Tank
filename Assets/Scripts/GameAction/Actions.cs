using System.Collections.Generic;
using UnityEngine;
public struct GameAction
{
    public void Awake()
    {

    }
    public void Update()
    {
        Action.Update();
    }
}

public enum ActionModes : short
{
    SHOOT, MOVEMENT, NONE
}

public struct Action
{
    public static float weight;

    public static bool isAction;
    public static bool isActionMode;

    public static bool FinishAction;

    public static float w;

    public static bool IsNextAction => w > 0;
    public static void Update()
    {
        ControlWeight();
        ControlAction();
    }

    static void ControlWeight()
    {
        if (!FinishAction) return;

        if (weight >= 1) weight = 0;
        else 
        {
            UIAction.Instance.Open();
            w = weight;
        }

        isAction = false;
        FinishAction = false;
    }

    static void ControlAction()
    {
        if (!isAction) return;

        if(UIAction.Instance.isOpen) UIAction.Instance.Close();
    }

    public static void SetWeight(float amount)
    {
        weight += amount;
        w = 0;
        isAction = true;
    }

   
}
