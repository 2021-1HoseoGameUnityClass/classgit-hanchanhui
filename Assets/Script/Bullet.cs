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
        //스피드
        float speed = moveSpeed * Time.deltaTime * direction;
        Vector3 vector3 = new Vector3(speed, 0, 0);
        //이동
        transform.Translate(vector3);
    }

    public void InstantiateBullet(float _direction)
    {
        //방향값 저장
        direction = _direction;
        //방향값으로 스케일을 바꿈
        Vector3 vector3 = new Vector3(_direction, 1, 1);
        transform.localScale = vector3;
        //2초뒤 삭제
        Destroy(gameObject, 2f);
    }
}
