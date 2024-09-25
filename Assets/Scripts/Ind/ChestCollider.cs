using UnityEngine;

public class ChestCollider : MonoBehaviour
{
    public GameObject potion;
    public GameObject chest;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(this.gameObject);
            Instantiate(potion, chest.transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {   
            Destroy(this.gameObject);
            Instantiate(potion, chest.transform.position, Quaternion.identity);
        }
    }
}


