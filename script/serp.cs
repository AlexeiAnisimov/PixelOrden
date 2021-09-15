using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serp : MonoBehaviour
{
    public float eangl = 0;
    public int damage = 25;
    int st = 1;
    float radius = 0.6f;
    bool go = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(life());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.name == "serp")
        {
            if (radius > 0.6f) st = -1;
            else if (radius < 0.4f) st = 1;
            radius += 0.005f * st;
            eangl += 3.5f;
        }
        else if (gameObject.name == "molot")
        {
            if (radius > 4f) st = -1;
            else if (radius < 0.4f) st = 1;
            radius += 0.025f * st;
            eangl += 2f;
        }
        else if (gameObject.name == "fireball")
        {
            radius = 0.3f;
            eangl += 2f;
        }
        transform.localPosition = new Vector2(Mathf.Cos(eangl * Mathf.Deg2Rad) * radius, Mathf.Sin(eangl * Mathf.Deg2Rad) * radius);
        transform.rotation = Quaternion.Euler(0, 0, eangl);
    }
    IEnumerator life()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            collision.gameObject.GetComponent<movement>().hp -= damage;
            collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
            if (gameObject.name == "molot") collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.localPosition.normalized*2);
        }
        if (collision.name == "patron") Destroy(collision.gameObject);
    }
}
