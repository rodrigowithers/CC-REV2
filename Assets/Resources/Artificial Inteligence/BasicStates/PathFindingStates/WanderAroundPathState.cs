using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAroundPathState : WanderPathState
{

    Player p;
    Vector2 cam;

    public override void Enter(Piece piece)
{
        p = Player.Instance;
        cam = Camera.main.transform.position;
        base.Enter(piece);
}

public override void Execute(Piece piece)
{
        base.Execute(piece);
}
public override void Exit(Piece piece)
{
    //  base.Exit(piece);
}

    protected override Vector2 WhereTo()
    {
        float playerRange = 8;
        float safetyoffset = 1;

        float enemyRange = 6;

        float x = 0;
        float y = 0;
        Vector2 pos = main_script.transform.position;
        Vector2 toreturn = Vector2.zero;
        bool allowed = false;

        int tries = 0;

        while (!allowed)
        {
            Vector2 dir = main_script.RandomDirection();
            Vector2 _pos = pos + (dir * enemyRange);
            tries++;
            if(tries > 3)
            {
                return base.WhereTo();
            }
            if (((Vector2)player.transform.position - _pos).magnitude > playerRange + safetyoffset)
            {
                if ((_pos.x < 15 && _pos.x > -15) && (_pos.y < cam.y + 10 && _pos.y > cam.y - 10))
                {
                    allowed = true;
                    toreturn = _pos;
                }
            }
        }

        return toreturn;
    }

}
