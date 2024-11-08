using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;

    void Start()
    {
        group.alpha = 0f;
        group.DOFade(1f, 0.5f);
    }
}
