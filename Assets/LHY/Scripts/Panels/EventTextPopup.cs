using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class EventTextPopup : MonoBehaviour
{
    public float DownSpeed = 1f;
    public float UpSpeed = 1f;
    public float StopTime = 3f;

    void Start()
    {
        transform.DOMove(new Vector3(960, 540 + 400, 0), DownSpeed).SetEase(Ease.OutQuad);

        transform.DOMove(new Vector3(960, 540 + 400 + 300, 0), UpSpeed).SetEase(Ease.OutQuad).SetDelay(StopTime);

        Destroy(transform.root.gameObject, 5f);
    }

}
