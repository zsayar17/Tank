using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameAreas GameAreas;
    public GamePhysics GamePhysics;
    public GameSelect GameSelect;
    public GamePoints GamePoints;
    public GameAction GameAction;
    public GameCamera GameCamera;

    public GameTour GameTour;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private void Awake()
    {
        GamePhysics.Awake();
        GameSelect.Awake();
        GamePoints.Awake();
        GameAreas.Awake();
        GameAction.Awake();
        GameCamera.Awake();
        GameTour.Awake();
    }

    private void Update()
    {
        GamePhysics.Update();
        GameSelect.Update();
        GameAreas.Update();
        GameAction.Update();
        GameCamera.Update();
        GamePoints.Update();
        GameTour.Update();
    }
}



