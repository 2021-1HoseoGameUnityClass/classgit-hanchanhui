using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private float direction = 1;


    void Update()
    {
        //���ǵ�
        float speed = moveSpeed * Time.deltaTime * direction;
        Vector3 vector3 = new Vector3(speed, 0, 0);
        //�̵�
        transform.Translate(vector3);
    }

    public void InstantiateBullet(float _direction)
    {
        //���Ⱚ ����
        direction = _direction;
        //���Ⱚ���� �������� �ٲ�
        Vector3 vector3 = new Vector3(_direction, 1, 1);
        transform.localScale = vector3;
        //2�ʵ� ����
        Destroy(gameObject, 2f);
    }
}
