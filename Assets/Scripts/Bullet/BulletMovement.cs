using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement
{
    public void MoveForward(Transform bullet, float movementSpeed)
    {
        bullet.Translate(movementSpeed * Time.deltaTime * Vector2.up);
    }
}
