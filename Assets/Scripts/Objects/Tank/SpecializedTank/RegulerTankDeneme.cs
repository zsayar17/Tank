using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegulerTankDeneme : RegularTank
{
    protected override void SetTankFeautures()
    {
        health = 10;
        hitrate = 10;
        shootpower = 10;
        movementdistance = 30;

    }


}
