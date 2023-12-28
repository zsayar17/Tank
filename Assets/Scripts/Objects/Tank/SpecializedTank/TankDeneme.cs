using UnityEngine;

public class Deneme : ObuseTank
{
    protected override void SetTankFeautures()
    {
        health = 10;
        hitrate = 10;
        shootpower = 10;
        movementdistance = 30;
    }

    protected override void SpecializedAwake()
    {
    }

    protected override void SpecializedUpdate()
    {
    }
}
