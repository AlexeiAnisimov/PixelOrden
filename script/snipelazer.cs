using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snipelazer : MonoBehaviour
{
    public GameObject snipeluch;
    GameObject player;
    Vector2 polet;
    float atan = 0;
    bool snipeat = false;
    public bool ready = false;
    public GameObject puli;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (snipeluch.active && !snipeat)
        {
            polet = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            atan = Mathf.Atan(polet.y / polet.x);
            snipeluch.transform.localRotation = Quaternion.Euler(0, 0, (atan * Mathf.Rad2Deg));
            if (atan * Mathf.Rad2Deg > 0) snipeluch.transform.localPosition = new Vector2(-Mathf.Cos(atan) * 6.3f, -Mathf.Sin(atan) * 6.3f);
            else snipeluch.transform.localPosition = new Vector2(Mathf.Cos(atan) * 6.3f, Mathf.Sin(atan) * 6.3f);
        }
        if (ready)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            ready = false;
            StartCoroutine(v());
        }
    }
    IEnumerator v()
    {
        yield return new WaitForSeconds(1.5f);
        snipeat = true;
        yield return new WaitForSeconds(0.07f);
        respGO(puli, transform.position, Quaternion.Euler(0, 0, atan * Mathf.Rad2Deg), "pulia", 200, 20);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        snipeat = false;
        enabled = false;
    }
    void respGO(GameObject weap, Vector2 respawnPulia, Quaternion rotation, string name, int damage, float speed)
    {
        GameObject weapon = Instantiate(weap, respawnPulia, rotation);
        weapon.name = name;
        weapon.GetComponent<weapon>().hoziain = gameObject.name;
        weapon.GetComponent<weapon>().damage = damage;
        weapon.transform.localScale = new Vector3(Mathf.Sign(polet.x) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        weapon.GetComponent<Rigidbody2D>().velocity = snipeluch.transform.localPosition.normalized * speed;
    }
}
