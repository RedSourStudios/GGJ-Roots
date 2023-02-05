using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private bool _isTouchingWall;
    public bool isTouchingWall
    {
        get {return _isTouchingWall; }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wall")
        {
            _isTouchingWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Wall")
        {
            _isTouchingWall = false;
        }
    }
}
