using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerController playerControllerscript;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerscript = GameObject.Find("Player").GetComponent<PlayerController>(); //  씬 계층 구조에서 Player 오브젝트 찾아서
                                                                                             //  PlayerController 스크립트에 대한 레퍼런스 연결
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerscript.gameOver == false) // 게임이 끝나기 전까지 계속 왼쪽으로 이동
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) // 지나간 장애물 삭제
        {
            Destroy(gameObject);
        }
    }
}
