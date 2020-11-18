using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeIndicator : MonoBehaviour
{
    Player player = null;
    RectTransform rectTransform = null;

    private void Start()
    {
        player = InGameSceneManager.instance.Player;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // 대쉬 거리만큼 인디케이터 높이 조정
        rectTransform.sizeDelta = new Vector2(player.AttackRange * 2, player.AttackRange * 2);
        // 인디케이터 위치 및 회전 정보 초기화
        Vector3 pos = player.transform.position;
        pos.y += 0.2f;
        rectTransform.transform.localPosition = pos;
        //rectTransform.transform.eulerAngles = new Vector3(90.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

    }
}
