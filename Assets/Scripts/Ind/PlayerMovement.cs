using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 

    public Rigidbody2D myBody;
    public Collider2D groundCollider;
    public Collider2D doorCollider;
    public Collider2D chestCollider;
    public Collider2D woodBoxCollider;
    public GameObject player;
    public GameObject box;
    public Transform boxSpawn;
    public float jumpForce;
    public float moveSpeed;
    private float horizontal;
    private float doubleJump = 0;
    private float dash = 0;
    private bool usandoDash;
    private float boxNumber = 0;
    private bool podeGerarCaixas = true;

    // Update is called once per frame
    void Update()
    {
        if (usandoDash)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");

        myBody.velocity = new Vector2(horizontal * moveSpeed, myBody.velocity.y);

        if (horizontal < 0)
            player.transform.localScale = new(1, 1, 1);

        if(horizontal > 0)
            player.transform.localScale = new(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && (myBody.IsTouching(groundCollider) || myBody.IsTouching(doorCollider) || myBody.IsTouching(chestCollider) || myBody.IsTouching(woodBoxCollider)))
        {
            doubleJump = 0;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && myBody.velocity.y > 0f)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, myBody.velocity.y * 0.5f);
        }

        if (player.name.Equals("Alice"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !(myBody.IsTouching(groundCollider) || myBody.IsTouching(doorCollider) || myBody.IsTouching(chestCollider) || myBody.IsTouching(woodBoxCollider)) && doubleJump == 0)
            {
                doubleJump = 1;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpForce / 2);
            }
        }

        if (player.name.Equals("Jimmy"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && dash == 0)
                StartCoroutine(Dash());
        }

        if (player.name.Equals("Gabriel"))
            StartCoroutine(BoxSpawn());



    }

    private IEnumerator Dash()
    {

        dash = 1;
        usandoDash = true;
        float gravidadeOriginal = myBody.gravityScale;
        myBody.gravityScale = 0;
        myBody.velocity = new Vector2(myBody.velocity.x * 5, 0);
        yield return new WaitForSeconds(0.2f);
        myBody.gravityScale = gravidadeOriginal;
        usandoDash = false;
        yield return new WaitForSeconds(1);
        dash = 0;

    }
    private IEnumerator BoxSpawn() {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (podeGerarCaixas)
            {
                if (boxNumber == 0)
                {
                    podeGerarCaixas = false;
                    box.gameObject.SetActive(true);
                    box.transform.SetParent(null);
                    boxNumber++;
                    yield return new WaitForSeconds(1);
                    podeGerarCaixas = true;
                }

                if (boxNumber != 0)
                {
                    podeGerarCaixas = false;
                    box.transform.position = boxSpawn.transform.position;
                    yield return new WaitForSeconds(1);
                    podeGerarCaixas = true;
                }
            }
        }

    }


}
