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
    NONE, SHOOT, MOVEMENT
}

public class ActionAbilities
{
    public static int FullAbility = 2;


    // Tank
    public static int HalfMovement = 1;
    public static int FullMovement = 2;
    public static int Shoot = 2;
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

