using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab; // 프리팹이므로 GameObject 사용
    private Vector3 spawnPos = new Vector3(45, 0, 0); // 위치 정하고 가져오기
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate); // 시간 경과에 따라 SpawnObstacle() 메서드를 지속적으로 호출
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation); // 프리랩 생성 (프리팹, 위치, 방향)
        }
    }
}
