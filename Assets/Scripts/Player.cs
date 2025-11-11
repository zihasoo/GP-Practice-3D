using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float shootInterval = 0.5f;
    public float speed;
    public int bulletCount = 1;
    public float bulletSpacing = 0.5f; // 총알 간격
    public ParticleSystem levelUpParticle;

    Vector3 startPos;
    Vector3 endPos;
    Rigidbody rb;
    Animator animator;
    BulletSystem bulletSystem;

    List<string> states = new() { "IsIDLE", "IsRun"};

    void Start()
    {
        bulletSystem = GetComponent<BulletSystem>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        StartCoroutine(ShootRoutine());
        if (rb == null) print("What");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) 
        {
            endPos = Input.mousePosition;
            var distance = endPos - startPos;
            if (distance.magnitude <= 0.5f) return;

            int value = (int)Mathf.Sign(distance.x); // (1: 오른쪽, -1: 왼쪽)

            if (value == 1) // 오른쪽으로 스와이프
            {
                startPos.x = endPos.x - 0.5f;
                rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z); // 오른쪽으로 이동
                ChangeAnimator("IsRun");
            }
            else if (value == -1) // 왼쪽으로 스와이프
            {
                startPos.x = endPos.x + 0.5f;
                rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z); // 왼쪽으로 이동
                ChangeAnimator("IsRun");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startPos = Vector3.zero;
            endPos = Vector3.zero;
            rb.velocity = Vector3.zero;
            ChangeAnimator("IsIDLE");
        }

        // 키보드 입력 처리
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
            ChangeAnimator("IsRun");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
            ChangeAnimator("IsRun");
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            ChangeAnimator("IsIDLE");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ATK_Speed"))
        {
            LevelUP(other.gameObject);
            shootInterval *= 0.8f;
            //if (shootInterval < 0.2f)
            //    shootInterval = 0.2f;
        }
        else if(other.CompareTag("ATK_Count"))
        {
            LevelUP(other.gameObject);
            bulletCount++;
            //if (bulletCount > 4) 
            //    bulletCount = 4;
        }
    }

    private void Shoot()
    {
        // 총알이 홀수개일 때: 가운데를 기준으로 배치
        // 총알이 짝수개일 때: 가운데 양쪽으로 대칭 배치
        float startOffset = -(bulletCount - 1) * bulletSpacing / 2f;
        
        for (int i = 0; i < bulletCount; i++)
        {
            float xOffset = startOffset + (i * bulletSpacing);
            Vector3 spawnPos = new Vector3(
                transform.position.x + xOffset, 
                transform.position.y + 0.5f, 
                transform.position.z + 1.0f
            );
            
            bulletSystem.MakeBullet(spawnPos);
        }
 
        animator.SetTrigger("Shoot");
        SoundManager.instance.AudioStart(1);
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void ChangeAnimator(string target)
    {
        foreach (var state in states)
        {
            if (state == target) animator.SetBool(state, true);
            else animator.SetBool(state, false);
        }
    }

    private void LevelUP(GameObject obj)
    {
        SoundManager.instance.AudioStart(2);
        Destroy(obj);
        levelUpParticle.Play();
    }
}
