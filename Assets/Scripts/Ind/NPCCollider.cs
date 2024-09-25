using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollider : MonoBehaviour
{
    public GameObject sadFace;
    public GameObject happyFace;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Vanessa") && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this.gameObject);
            Instantiate(happyFace, sadFace.transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Vanessa") && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this.gameObject);
            Instantiate(happyFace, sadFace.transform.position, Quaternion.identity);
        }
    }
}