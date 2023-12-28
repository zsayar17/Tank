using UnityEngine;

public struct GameSelect // Tank Secme dongusunun yapildi sinif
{
    public static SelectTank SelectedTanks; // Tank secme islemlerinin yapildigi obje
    public void Awake()
    {
        SelectedTanks = new SelectTank();
    }

    public void Update()
    {
        if (MouseInput.isClickLeft == false) return;

        SelectedTanks.SelectControlableTank();
    }
}

public class SelectTank
{
    public BaseTank currentTank; // guncel secilen tanki tutan obje

    private bool OnUI
    {
        get => RayCatch.CatchUI;
    }

    public bool IsSelectableTank // secilebilir tank mi kontrolu
    {
        get
        {
            if (OnUI || Action.isActionMode || Action.IsNextAction) return true; // UI uzerindeyse ya da bir aksiyonda bulunuyorsa true.

            return RayCatch.CatchGameObject.CompareTag("Player") & !Action.isAction; // Player ise true degilse false doner ve aksiyon da degilse
        }
    }

    public void SelectControlableTank() // secilebilir tanki secme islemi
    {
        if (!Action.isActionMode && IsSelectableTank && !OnUI && !Action.IsNextAction)
        {
            if (currentTank != null) currentTank.isSelected = false;

            currentTank = RayCatch.CatchComponent<BaseTank>();
            currentTank.isSelected = true;

            GameCamera.CameraController.Focus(currentTank.transform);
        }
    }
}
