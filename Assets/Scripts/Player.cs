using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;

    Vector3 startPos;
    Vector3 endPos;
    Rigidbody rb;
    Animator animator;

    List<string> states = new() { "IsIDLE", "IsRun"};

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MakeBullet();
            animator.SetTrigger("Shoot");
        }
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼이 눌렸을 때
        {
            endPos = Input.mousePosition;
            var distance = endPos - startPos;
            if (distance.magnitude <= 0.5f) return;

            int value = (int)Mathf.Sign(distance.x); // (1: 오른쪽, -1: 왼쪽)

            if (value == 1) // 오른쪽으로 스와이프
            {
                startPos.x = endPos.x - 1.0f;
                rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z); // 오른쪽으로 이동
                ChangeAnimator("IsRun");
            }
            else if (value == -1) // 왼쪽으로 스와이프
            {
                startPos.x = endPos.x + 1.0f;
                rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z); // 오른쪽으로 이동
                ChangeAnimator("IsRun");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            startPos = Vector3.zero;
            endPos = Vector3.zero;
            rb.velocity = Vector3.zero;
            ChangeAnimator("IsIDLE");
        }
    }

    private void MakeBullet()
    {
        var obj = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 1.0f)
            , Quaternion.identity);
        Destroy(obj, 3.0f);
    }

    private void ChangeAnimator(string target)
    {
        foreach (var state in states)
        {
            if (state == target) animator.SetBool(state, true);
            else animator.SetBool(state, false);
        }
    }
}
