using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTextBehaviour : MonoBehaviour {
    Animator anim;
    public string text;

    public void Start()
    {
        GetComponent<Text>().text = text;
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(destroyAfter());
    }

    IEnumerator destroyAfter()
    {
        float time = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(time - 0.05f);
        Destroy(this.gameObject);
    }


}
