using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField]
    LineRenderer line1 = null, line2 = null;
    Vector2[] points = new Vector2[3];
    RaycastHit2D hit;
    [SerializeField]
    LayerMask wallLayer = 0;
    [SerializeField]
    float maxDistance = 50;
    [SerializeField]
    float lineLenght = 2;
    public void TICK(Vector2 direction)
    {
        DrawLines();
        hit = Physics2D.Raycast(points[0], direction,maxDistance,wallLayer);
        Collider2D collider = hit.collider;
        if (collider == null) return;

        if(collider.tag==Helper.WALL)
        {
            points[1] = hit.point;
            Vector2 reflect = Vector2.Reflect(points[1], hit.normal);
            points[2] = reflect*lineLenght;
        }
        else
        {
            points[2] = points[1];
        }
    }

    public void SetGuidePosition(Vector2 pos)
    {
        transform.position = pos;
        points[0] = pos;
        points[1] = pos;
        points[2] = pos;
    }

    void DrawLines()
    {
        line1.SetPosition(0, points[0]);
        line1.SetPosition(1, points[1]);

        line2.SetPosition(0, points[1]);
        line2.SetPosition(1, points[2]);
    }
}
