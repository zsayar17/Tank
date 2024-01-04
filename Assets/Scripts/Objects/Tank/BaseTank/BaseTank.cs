using UnityEngine;

// BaseObject -> BaseTank -> DerivedTank -> (changeable) -> SpecializedTank
public abstract partial class BaseTank : BaseObject
{
    void Awake()
    {
        SetBaseVariables(); // set base variables of tank from BaseTank
        DerivedAwake(); // set derived variables from derived class
        SetTankFeautures(); // set tank feautures from specialized class
        SpecializedAwake(); // set specialized variables from specialized class
    }

    void Update()
    {
        if (!isSelected) return;

        Movement(); // movement of tank from BaseTank but can be changed from specialized class
        DerivedUpdate(); // update derived variables from derived class
        SpecializedUpdate(); // update specialized variables from specialized class
    }

    protected virtual void DerivedAwake() { /*do nothing */ }
    protected virtual void DerivedUpdate() { /*do nothing */ }
    protected virtual void SpecializedAwake() { /*do nothing */ }
    protected virtual void SpecializedUpdate() { /*do nothing */ }
}
