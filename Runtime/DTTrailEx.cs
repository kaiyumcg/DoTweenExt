using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExt;

namespace DTExt
{
    public static class DTTrailEx
    {
        public static void ExFade(this TrailRenderer trail, float within, System.Action OnComplete = null)
        {
            var tw1 = DOTween.To(
                () =>
                {
                    return trail.startColor;
                },
                (color) =>
                {
                    trail.startColor = color;
                }, Color.clear, within);
            var tw2 = DOTween.To(
                () =>
                {
                    return trail.endColor;
                },
                (color) =>
                {
                    trail.endColor = color;
                }, Color.clear, within).OnComplete(() =>
                {
                    tw1.ExResetDT();
                    OnComplete?.Invoke();
                });
        }
    }
}