using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCollider : MonoBehaviour
{


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Jake"))
        {
            if (Input.GetKeyDown(KeyCode.F))
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Jake"))
        {
            if (Input.GetKeyDown(KeyCode.F))
                Destroy(this.gameObject);
        }
    }
}
