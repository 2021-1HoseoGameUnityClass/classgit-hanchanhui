using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 0.1f;
    
    //점프 관련 변수
    [SerializeField]
    private float jumpForce = 300f;
    //점프 상태인지 확인
    private bool jump = false;

    //총알 발사 관연 변수
    [SerializeField]
    private GameObject bulletObj = null;

    [SerializeField]
    private GameObject InstantiateObj = null;



    void Update()
    {
        //이동관련 함수
        Move();

        // 점프키를 누르면 점프
        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

        //파이어키를 누르면 총알 발사
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        
        Vector3 vector3 = new Vector3();
        //x 축의 값을 인풋 값 + 플레이어스피드 * 시간값으로 바꾼다.
        float moveSpeed = h * playerSpeed * Time.deltaTime;
        vector3.x = moveSpeed;

        // 해당하는 오브젝트를 이동시킨다.
        transform.Translate(vector3);

        //Horizontal 값이 음수라면
        if (h < 0)
        {
            GetComponent<Animator>().SetBool("isWalk", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            GetComponent<Animator>().SetBool("isWalk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void PlayerJump()
    {
        //점프상태가 되어 있지 않을때만 점프하도록 함
        if (jump == false)
        {
            // 애니메이션 처리
            GetComponent<Animator>().SetBool("isWalk", false);
            GetComponent<Animator>().SetBool("isJump", true);

            // 점프량만큼 Add force
            Vector2 vetor2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vetor2);
            jump = true;

        }
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌체위 콜라이더가 플렛폼 태그라면
        if(collision.collider.tag == "Platform")
        {
            //Jump 상태를 해제한다
            GetComponent<Animator>().SetBool("isJump", false);
            jump = false;
        }
    }

    private void Fire()
    {
        GetComponent<AudioSource>().Play();
        //방향값 선언
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        //총알 프리랩을 생성하고, 프리렙의 Bullet 컴포넌트의 InstantiateBullet 함수를  호출한다.
        Instantiate(bulletObj, InstantiateObj.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);

    }
}
