using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject rayObj = null;
    private bool moveRight = true;
    [SerializeField] private int lifePoint = 3;
    private bool isDead = false;

    void Update()
    {
        CheckRay();
    }

    void CheckRay()
    {
        //죽지 않았다면
        if(isDead == false)
        {
            // 레이어 마스크
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMask.GetMask("Platform");

            //레이캐스트
            RaycastHit2D ray = Physics2D.Raycast(rayObj.transform.position, new Vector2(0, -1), 1.1f, layerMask.value);
            Debug.DrawRay(rayObj.transform.position, new Vector3(0, -1, 0), Color.red);

            //광선에 히트 되지 않으면
            if(ray == false)
            {
                //방향을 반대로
                if (moveRight)
                {
                    moveRight = false;
                }
                else
                {
                    moveRight = true;
                }
            }
            //이동
            Move();
        }
        
    }

    private void Move()
    {
        float direction = 0f;
        if(moveRight == true)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        //방향에 맞게 스프라이트를 수정
        Vector3 vector3 = new Vector3(direction, 1, 1);
        transform.localScale = vector3;
        //스피드 계산과 이동
        float speed = moveSpeed * Time.deltaTime * direction;
        vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") == true)
        {
            //라이프 -1
            lifePoint = lifePoint - 1;
            

            //라이프가 0이 되었다면
            if(lifePoint < 1)
            {
                // 죽으면 모든 동작 정지
                isDead = true; 
                //즉음 애니메이션을 true로
                GetComponent<Animator>().SetBool("Death", true);

                //1초후 게임오브젝트를 삭제한다.
                Destroy(gameObject, 0.5f);
            }
        }
    }

}
