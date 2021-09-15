using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    public Vector2 speed;
    public int maxkol;
    public float time = 1.5f;
    int kolvo=0;
    int storona = 1;
    bool onjump = false;
    bool nanes = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = 800/speed.y;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        kolvo++;
        rb.AddForce(speed);
        onjump = true;
        GetComponent<Animator>().SetBool("Jump", true);
        yield return new WaitForSeconds(time);
        if (kolvo == maxkol)
        {
            kolvo = 0;
            storona = -storona;
            speed = new Vector2(speed.x * storona, speed.y);
            transform.localScale = new Vector2(storona * transform.localScale.x, transform.localScale.y);
        }
        StartCoroutine(Jump());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == player.name && onjump&&!nanes)
        {
            player.GetComponent<movement>().hp -= 50;
            onjump = false;
            StartCoroutine(w());
        }
    }
    IEnumerator w()
    {
        nanes = true;
        yield return new WaitForSeconds(0.5f);
        nanes = false;
    }
    void EndJump()
    {
        GetComponent<Animator>().SetBool("Jump", false);
    }
}
