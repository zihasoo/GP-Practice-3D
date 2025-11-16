using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float speed;
    public int maxHP;
    public int damage;
    public Slider HPBar;
    public GameObject hitParticle;

    private int HP;
    private bool isDead = false;
    private Rigidbody rb;
    private Animator anim;

    private void Start()
    {
        HP = maxHP;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;
        rb.velocity = transform.forward * speed;

        if (transform.position.z < -0.5f)
        {
            UIManager.instance.player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP--;
            SoundManager.instance.AudioStart(0);
            Instantiate(hitParticle, other.gameObject.transform.position, Quaternion.identity);

            if (!HPBar.gameObject.activeSelf) 
                HPBar.gameObject.SetActive(true);
            HPBar.value = (float)HP / maxHP;
            other.transform.parent.gameObject.SetActive(false);
            if (!isDead && HP <= 0)
            {
                isDead = true;
                anim.SetTrigger("Die");
                HPBar.gameObject.SetActive(false);
                Destroy(rb);
                GetComponent<CapsuleCollider>().enabled = false;
                Destroy(gameObject, 4.0f);
                UIManager.instance.UpdateScore(damage);
            }
        }
    }
}
