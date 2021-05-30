using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 0.1f;
    
    //���� ���� ����
    [SerializeField]
    private float jumpForce = 300f;
    //���� �������� Ȯ��
    private bool jump = false;

    //�Ѿ� �߻� ���� ����
    [SerializeField]
    private GameObject bulletObj = null;

    [SerializeField]
    private GameObject InstantiateObj = null;



    void Update()
    {
        //�̵����� �Լ�
        Move();

        // ����Ű�� ������ ����
        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

        //���̾�Ű�� ������ �Ѿ� �߻�
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        
        Vector3 vector3 = new Vector3();
        //x ���� ���� ��ǲ �� + �÷��̾�ǵ� * �ð������� �ٲ۴�.
        float moveSpeed = h * playerSpeed * Time.deltaTime;
        vector3.x = moveSpeed;

        // �ش��ϴ� ������Ʈ�� �̵���Ų��.
        transform.Translate(vector3);

        //Horizontal ���� �������
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
        //�������°� �Ǿ� ���� �������� �����ϵ��� ��
        if (jump == false)
        {
            // �ִϸ��̼� ó��
            GetComponent<Animator>().SetBool("isWalk", false);
            GetComponent<Animator>().SetBool("isJump", true);

            // ��������ŭ Add force
            Vector2 vetor2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vetor2);
            jump = true;

        }
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        //�浹ü�� �ݶ��̴��� �÷��� �±׶��
        if(collision.collider.tag == "Platform")
        {
            //Jump ���¸� �����Ѵ�
            GetComponent<Animator>().SetBool("isJump", false);
            jump = false;
        }
    }

    private void Fire()
    {
        GetComponent<AudioSource>().Play();
        //���Ⱚ ����
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        //�Ѿ� �������� �����ϰ�, �������� Bullet ������Ʈ�� InstantiateBullet �Լ���  ȣ���Ѵ�.
        Instantiate(bulletObj, InstantiateObj.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);

    }
}
