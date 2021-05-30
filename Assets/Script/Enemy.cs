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
        //���� �ʾҴٸ�
        if(isDead == false)
        {
            // ���̾� ����ũ
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMask.GetMask("Platform");

            //����ĳ��Ʈ
            RaycastHit2D ray = Physics2D.Raycast(rayObj.transform.position, new Vector2(0, -1), 1.1f, layerMask.value);
            Debug.DrawRay(rayObj.transform.position, new Vector3(0, -1, 0), Color.red);

            //������ ��Ʈ ���� ������
            if(ray == false)
            {
                //������ �ݴ��
                if (moveRight)
                {
                    moveRight = false;
                }
                else
                {
                    moveRight = true;
                }
            }
            //�̵�
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

        //���⿡ �°� ��������Ʈ�� ����
        Vector3 vector3 = new Vector3(direction, 1, 1);
        transform.localScale = vector3;
        //���ǵ� ���� �̵�
        float speed = moveSpeed * Time.deltaTime * direction;
        vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") == true)
        {
            //������ -1
            lifePoint = lifePoint - 1;
            

            //�������� 0�� �Ǿ��ٸ�
            if(lifePoint < 1)
            {
                // ������ ��� ���� ����
                isDead = true; 
                //���� �ִϸ��̼��� true��
                GetComponent<Animator>().SetBool("Death", true);

                //1���� ���ӿ�����Ʈ�� �����Ѵ�.
                Destroy(gameObject, 0.5f);
            }
        }
    }

}
