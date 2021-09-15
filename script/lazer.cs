using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    public GameObject luch;
    public bool end = false;
    public Vector2 pos = new Vector2(0.5f, 0);
    Vector2 lol;
    GameObject lastpos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(punch());
    }
    IEnumerator punch()
    {
        end = false;
        pos = new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, -0.3f));
        transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan(pos.y / pos.x)));
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.05f);
            if (!end)
            {
                if (i == 0) lol = new Vector2(transform.position.x + pos.x, transform.position.y + pos.y);
                else lol = new Vector2(lastpos.transform.position.x + pos.x, lastpos.transform.position.y + pos.y);
                GameObject kek = Instantiate(luch, lol, transform.rotation);
                kek.tag = "luchnoBoss";
                kek.GetComponent<luch>().hoz = gameObject.name;
                if (i == 0)
                {
                    kek.transform.SetParent(transform);
                }
                else kek.transform.SetParent(lastpos.transform);
                kek.transform.position = lol;
                lastpos = kek;
            }
        }
        yield return new WaitForSeconds(1.5f);
        var ObjectBonus1 = GameObject.FindGameObjectsWithTag("luchnoBoss");
        for (int i = 0; i < ObjectBonus1.Length; i++)
        {
            Destroy(ObjectBonus1[i]);
        }
        yield return new WaitForSeconds(2f);
        transform.Rotate(new Vector3(0, 0, -Mathf.Rad2Deg * Mathf.Atan(pos.y / pos.x)));
        StartCoroutine(punch());
    }
}
