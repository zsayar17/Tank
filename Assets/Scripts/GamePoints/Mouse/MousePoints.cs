using UnityEngine;

public struct GamePoints // Oyun icerisinde mouse ile alinan noktalarin bilgilerini donduren sinif
{
    public static ShootPoint ShootPoint;
    public static MovementPoint MovementPoint;

    public static MouseCursors MouseCursors;
    public void Awake()
    {
        ShootPoint = new ShootPoint();
        MovementPoint = new MovementPoint();
        MouseCursors = new MouseCursors();
    }

    public void Update()
    {
    }
}

public class ShootPoint // shoot modunda mouse ile alinan noktalarin bilgilerini donduren sinif
{
    public Vector3 Point
    {
        get
        {
            return new Vector3(GamePhysics.RayFromScreen.Hit.point.x, GameSelect.SelectedTanks.currentTank.transform.position.y, GamePhysics.RayFromScreen.Hit.point.z);
        }
    }
    
}




public class MovementPoint
{
    private Vector3 point;
    public Vector3 Point
    {
       get
       {
           if (!Action.isAction)
           {
               point = GamePhysics.RayFromScreen.Hit.point;
               return GamePhysics.RayFromScreen.Hit.point;
           }
           return point;
       }
    }

    public float DistanceWithTank => Vector3.Distance(GameSelect.SelectedTanks.currentTank.transform.position, Point);

    public bool IsDistancePointBetweenTank
        {
            get
            {
                BaseTank tank = GameSelect.SelectedTanks.currentTank;

                if (DistanceWithTank <= tank.transform.position.y - Point.y) 
                {
                    GameAreas.MovementPointArea.SetActive(false);
                    return false;
                }
                return true;
            }
        }

    public bool CheckMovementArea(float radius)
    {
        return Vector3.Distance(Point, GameAreas.FullTankArea.Position) > radius;
    }
}



