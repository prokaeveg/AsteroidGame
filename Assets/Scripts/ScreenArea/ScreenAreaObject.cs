using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAreaObject : MonoBehaviour
{



    public void Update()
    {


        var size = ScreenArea.areaSize;
        var halfSize = ScreenArea.halfAreaSize;

        if (transform.position.x > halfSize.x)
        {
            var pos = transform.position;
            pos.x -= size.x;
            transform.position = pos;
        }
        else if (transform.position.x < -halfSize.x)
        {
            var pos = transform.position;
            pos.x += size.x;
            transform.position = pos;
        }

        if (transform.position.y > halfSize.y)
        {
            var pos = transform.position;
            pos.y -= size.y;
            transform.position = pos;
        }
        else if (transform.position.y < -halfSize.y)
        {
            var pos = transform.position;
            pos.y += size.y;
            transform.position = pos;
        }
    }
}
