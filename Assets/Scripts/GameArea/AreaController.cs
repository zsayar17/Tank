using UnityEngine;
using Utils;

public struct GameAreas
{
    public static Area FullTankArea;
    public static Area HalfTankArea;

    public static MovementPointArea MovementPointArea;
    public void Awake()
    {
        FullTankArea = new Area();
        HalfTankArea = new Area();
        MovementPointArea = new MovementPointArea();
    }

    public void Update()
    {

    }
}
public struct MovementAreaColors
{
    public static Color DEFAULT = new Color(0, 3.924528f, 2.82184f);
    public static Color HALF = new Color(8, 1.412322f, 0);

}

public enum MovementPointAreaIcon { LOCK = 0, NONE = 1 };

public class Area
{
    private GameObject areaSphere;

    public float RADIUS;
    public Vector3 Position
    {
        get => areaSphere.transform.position;
        set => areaSphere.transform.position = value;
    }

    public Vector3 Scale
    {
        get => areaSphere.transform.localScale;
        set => areaSphere.transform.localScale = value;
    }

    public bool IsClickOnField(float radius)
    {
        return RayCatch.CatchObject("Ground") && !GamePoints.MovementPoint.CheckMovementArea(radius);
    }

    public Area()
    {
        areaSphere = GameObject.Instantiate(Resources.Load<GameObject>("ShottingArea"), Vector3.zero, Quaternion.identity);
        Deactivate();
    }

    public void Activate(Vector3 position, float radius)
    {
        float rad2scale = radius * 2;
        RADIUS = radius;

        Position = position;
        Scale = Vector3.one * rad2scale;

        areaSphere.SetActive(true);
    }
    public void Activate(Vector3 position, float radius,Color color)
    {
        float rad2scale = radius * 2;
        RADIUS = radius;

        Position = position;
        Scale = Vector3.one * rad2scale;

        areaSphere.GetComponent<Renderer>().material.SetColor("_Color", color);

        areaSphere.SetActive(true);
    }

    public void Deactivate()
    {
        areaSphere.SetActive(false);
    }

}







public class MovementPointArea // yeni
{
    private ParallelToGround parallelToGround;
    private Material material;
    public bool isArea;
    public bool isFullArea;

    public MovementPointArea()
    {
        parallelToGround = new ParallelToGround("MovementPointArea", .2f, .5f);
        material = parallelToGround.GameObject.GetComponent<Renderer>().material;
    }

    public void Update()
    {
        parallelToGround.UpdateSlide();
        SetArea();
    }

    private void SetArea()
    {
        float radius;

        radius = GameAreas.FullTankArea.RADIUS; // ikinci hamlede buranin ikiye bolunmesi gerek sonra movementdistance tekrardan eski distance degerini alacak.

        if (!isArea && GamePoints.MovementPoint.CheckMovementArea(radius))
        {
            SetIcon(MovementPointAreaIcon.LOCK);
            isArea = true;
        }
        else if (isArea && !GamePoints.MovementPoint.CheckMovementArea(radius))
        {
            SetIcon(MovementPointAreaIcon.NONE);
            isArea = false;
        }

        if (Action.weight > 0) return;//sikintili

        if (!isFullArea && !GamePoints.MovementPoint.CheckMovementArea(radius / 2))
        {
            SetColor(MovementAreaColors.HALF);
            isFullArea = true;
        }
        else if (isFullArea && GamePoints.MovementPoint.CheckMovementArea(radius / 2))
        {
            SetColor(MovementAreaColors.DEFAULT);
            isFullArea = false;
        }
    }

    public void SetIcon(MovementPointAreaIcon icon)
    {
        material.SetFloat("_Boolean", (byte)icon);
    }
    public void SetColor(Color color)
    {
        material.color = color;
    }

    public void SetActive(bool active)
    {
        parallelToGround.GameObject.SetActive(active);
    }
}
