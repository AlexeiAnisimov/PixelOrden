using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rozbiinik : MonoBehaviour
{
    GameObject pla;
    public GameObject patron;
    int storona;
    float speed;
    Vector2 polet;
    Vector2 respawnPulia;
    public int hp = 100;
    public float rast = 3;
    public float animSpeed=1;
    Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = animSpeed;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        hpText =GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        hpText.text = hp.ToString();
        gameObject.name = "rozbinik";
        storona = (int)Mathf.Sign(transform.localScale.x);
        pla = GameObject.Find("Player");
        //GetComponentInChildren<Canvas>().worldCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        respawnPulia = new Vector2(storona * 0.1f+transform.position.x, transform.position.y);
        Vector2 cursor = new Vector2(pla.transform.position.x, pla.transform.position.y+0.3f);
        polet = cursor - (Vector2)transform.position;
        if (Vector2.Distance(cursor, transform.position) < rast)
        {
            speed = 2f * Vector2.Distance(cursor, transform.position)+1;
            GetComponent<Animator>().SetBool("poliba", true);
        }
        else GetComponent<Animator>().SetBool("poliba", false);
        if (storona == Mathf.Sign(transform.position.x-cursor.x))
        {
            //Debug.Log(Mathf.Tan(polet.y / polet.x));
            storona = -storona;
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            hpText.rectTransform.localScale = new Vector2(-hpText.rectTransform.localScale.x, hpText.rectTransform.localScale.y);
        }
        if (!pla.GetComponent<movement>().nanes && pla.GetComponent<movement>().udar)
            polucheniaDamage();
        if (hp <= 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool("Death", true);
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    void OnVistrel()
    {
        
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
        GameObject patrVistrel = Instantiate(patron, respawnPulia, rotation);
        patrVistrel.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
        patrVistrel.GetComponent<patron>().hoziain = gameObject.name;
    }
    void polucheniaDamage()
    {
        if (pla.GetComponent<Animator>().GetBool("dwoechka")){
            if (Vector2.Distance(pla.GetComponent<movement>().kulak.position, transform.position) <= pla.GetComponent<movement>().raznica)
            {
                pla.GetComponent<movement>().nanes = true;
                hp -= pla.GetComponent<movement>().damage / 2;
            }
        }
        else if (pla.GetComponent<Animator>().GetBool("lokot"))
        {
            if (Vector2.Distance(pla.GetComponent<movement>().kulak.position, transform.position) <= (pla.GetComponent<movement>().raznica) / 2)
            {
                pla.GetComponent<movement>().nanes = true;
                hp -= pla.GetComponent<movement>().damage * 2;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(pla.GetComponent<movement>().storona * 45, Vector3.up.y * 150));
            }
        }
        hpText.text = hp.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain != gameObject.name)
        {
            hp -= GameObject.Find(collision.gameObject.GetComponent<patron>().hoziain).GetComponent<movement>().damage;
        }
        hpText.text = hp.ToString();
    }
}
