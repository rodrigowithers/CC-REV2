using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line {

    const float VerticalLineGradient = 1e5f;
    float gradient;
    float intercept;
    Vector2 pointonline_2;
    Vector2 pointonline_3;
    bool aproachside;
    float gradientperpendicular;



    public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
    {
        float dx = pointOnLine.x - pointPerpendicularToLine.x;
        float dy = pointOnLine.y - pointPerpendicularToLine.y;
        if (dx == 0)
            gradientperpendicular = VerticalLineGradient;
        else
            gradientperpendicular = dy / dx;

        if (gradientperpendicular == 0)
            gradient = VerticalLineGradient;
        else
            gradient = -1 / gradientperpendicular;



        intercept = pointOnLine.y - gradient * pointOnLine.x;
        pointonline_2 = pointOnLine;
        pointonline_3 = pointOnLine + new Vector2(1,gradient);
        aproachside = false;
        aproachside = GetSide(pointPerpendicularToLine);
    }

    bool GetSide(Vector2 p)
    {
        return (p.x - pointonline_2.x) * (pointonline_3.y - pointonline_2.y) > (p.y - pointonline_2.y) * (pointonline_3.x - pointonline_2.x);
    }

    public bool HasCrossedLine(Vector2 p)
    {
        return GetSide(p) != aproachside;
    }


    public void DrawWithGizmos(float lenght)
    {
        Vector2 dir = new Vector2(1,gradient).normalized;
        Vector2 center = new Vector2(pointonline_2.x,pointonline_2.y);
        Gizmos.DrawLine(center -(dir*(lenght/2)),center + (dir * (lenght / 2)));
    }
}
