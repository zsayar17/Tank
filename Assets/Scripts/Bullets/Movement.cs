using UnityEngine;

public class Movement : MonoBehaviour
{
    float time, passed_time;
    float v0;
    private float height;
    private float angle;

    private Vector3 beginPoint;
    private Vector3 groundPoint;

    [SerializeField] float _Speed;
    [SerializeField] float _PrefferedHeight;
    [SerializeField] LineRenderer line;


    void Start()
    {
        beginPoint = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit)) return;
            passed_time = 0;
            transform.position = beginPoint;
            CalculateNewVars(hit.point);
        }
        setPath();
        Move();

    }

    void Move()
    {
        float x, y;

        x = v0 * Mathf.Cos(angle) * passed_time;
        y = v0 * Mathf.Sin(angle) * passed_time - (0.5f * -Physics.gravity.y * Mathf.Pow(passed_time, 2));

        transform.position = beginPoint + groundPoint * x + UnityEngine.Vector3.up * y;

        passed_time += Time.deltaTime * _Speed;
    }


    void setPath()
    {
        float step = 0.1f;
        int count = 0;
        float x, y;

        line.positionCount = (int)(time / step) + 2;

        for (float i = 0; i < time; i += step)
        {
            x = v0 * Mathf.Cos(angle) * i;
            y = v0 * Mathf.Sin(angle) * i - (0.5f * -Physics.gravity.y * Mathf.Pow(i, 2));

            line.SetPosition(count, beginPoint + groundPoint * x + Vector3.up * y);
            count++;
        }

        x = v0 * Mathf.Cos(angle) * time;
        y = v0 * Mathf.Sin(angle) * time - (0.5f * -Physics.gravity.y * Mathf.Pow(time, 2));
        line.SetPosition(count, beginPoint + groundPoint * x + Vector3.up * y);
    }

    void CalculateNewVars(Vector3 pos)
    {
        Vector3 MousePos;
        float a, b, c;
        float t1, t2;
        float g, h;
        float horizontalDistance;

        MousePos = pos - transform.position;
        groundPoint = new Vector3(MousePos.x, 0, MousePos.z);
        horizontalDistance = groundPoint.magnitude;
        groundPoint = groundPoint.normalized;

        g = -Physics.gravity.y;
        h = Mathf.Max(MousePos.y + MousePos.magnitude / 2, _PrefferedHeight);

        a = -0.5f * g;
        b = Mathf.Sqrt(2 * g * h);
        c = -MousePos.y;

        t1 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        t2 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        time = Mathf.Max(t1, t2);
        angle = Mathf.Atan(b * time / horizontalDistance);
        v0 = b / Mathf.Sin(angle);
    }
}
