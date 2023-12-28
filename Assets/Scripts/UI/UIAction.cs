using UnityEngine;
public class UIAction : MonoBehaviour
{
    private Animator anim;
    [HideInInspector] public bool isOpen;
    private static UIAction instance;

    public static UIAction Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIAction>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!MouseInput.isClickLeft) return;

        if (GameSelect.SelectedTanks.IsSelectableTank) Open();
        else Close();
    }

    public void ClickShoot()
    {
        GameSelect.SelectedTanks.currentTank.OpenShootingMode();
    }
    public void ClickMovement()
    {
        GameSelect.SelectedTanks.currentTank.OpenMovementMode();
    }


    #region UI ACILIP KAPANMA KONTROL
    public void Open()
    {
        if (isOpen) return;

        anim.SetTrigger("Open");
        isOpen = true;
    }

    public void Close()
    {
        if (!isOpen) return;

        anim.SetTrigger("Close");

        GameSelect.SelectedTanks.currentTank.CloseMode();
        isOpen = false;
    }
    #endregion



}
