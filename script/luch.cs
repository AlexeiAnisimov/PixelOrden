using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luch : MonoBehaviour
{
    public string hoz;
    public bool boss;
    bool w = false;
    public bool nanes=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="peshera")GameObject.Find(hoz).GetComponent<lazer>().end=true;
        if (collision.gameObject.name == "Player" && !boss && !nanes)
        {
            StartCoroutine(damWait(collision));
            var ObjectBonus1 = GameObject.FindGameObjectsWithTag("luchnoBoss");
            for (int i = 0; i < ObjectBonus1.Length; i++)
            {
                ObjectBonus1[i].GetComponent<luch>().nanes = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            if (Application.loadedLevelName == "level2") { if (boss && GameObject.Find("ПРiВiДЕНiЕ").GetComponent<GhostBOSS>().damageInPlayer) StartCoroutine(GameObject.Find("ПРiВiДЕНiЕ").GetComponent<GhostBOSS>().attackDamage(0.15f)); }
            else if (Application.loadedLevelName == "level6") if (boss && GameObject.Find("EXODUS").GetComponent<EXODUSboss>().damageInPlayer) StartCoroutine(GameObject.Find("EXODUS").GetComponent<EXODUSboss>().attackDamage(0.1f));
        }
    }
   IEnumerator damWait(Collider2D collision)
    {
        collision.gameObject.GetComponent<movement>().hp -= 50;
        collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
        yield return new WaitForSeconds(0.3f);
    }
}
