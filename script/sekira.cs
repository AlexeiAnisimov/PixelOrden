using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sekira : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player&&!GetComponentInParent<knightBOSS>().damageinpl)
        {
            player.GetComponent<movement>().hp -= 70;
            GetComponentInParent<knightBOSS>().damageinpl = true;
        }
    }
}
