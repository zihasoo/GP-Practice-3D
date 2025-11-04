using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float speed;
    public int HP = 3;
    public Slider HPBar;

    private bool isDead = false;
    private Rigidbody rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isDead) return;
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP--;
            if (!HPBar.gameObject.activeSelf) 
                HPBar.gameObject.SetActive(true);
            HPBar.value = HP / 3.0f;
            Destroy(other.transform.parent.gameObject);
            if (!isDead && HP <= 0)
            {
                isDead = true;
                anim.SetTrigger("Die");
                HPBar.gameObject.SetActive(false);
                Destroy(rb);
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

}
