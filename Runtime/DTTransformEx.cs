using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExt;

namespace DTExt
{
    public static class DTTransformEx
    {
        #region Fade
        public static List<Tween> ExScale(this List<Transform> transforms, Vector3 localScale, float duration)
        {
            return _ExScale(transforms, localScale, duration, null, null, null);
        }
        public static List<Tween> ExScale(this List<Transform> transforms, Vector3 localScale, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExScale(transforms, localScale, duration, mono, (list) => { OnComplete?.Invoke(); }, null);
        }
        public static List<Tween> ExScale(this List<Transform> transforms, Vector3 localScale, float duration,
            params Transform[] exceptions)
        {
            return _ExScale(transforms, localScale, duration, null, null, exceptions);
        }
        public static List<Tween> ExScale(this List<Transform> transforms, Vector3 localScale, float duration,
            MonoBehaviour mono, System.Action OnComplete, params Transform[] exceptions)
        {
            return _ExScale(transforms, localScale, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions);
        }
        public static List<Tween> ExScaleSequential(this List<Transform> transforms, Vector3 localScale, float duration,
            MonoBehaviour mono, System.Action OnComplete)
        {
            return _ExScale(transforms, localScale, duration, mono, (list) => { OnComplete?.Invoke(); }, null, true);
        }
        public static List<Tween> ExScaleSequential(this List<Transform> transforms, Vector3 localScale, float duration,
            MonoBehaviour mono, System.Action OnComplete, params Transform[] exceptions)
        {
            return _ExScale(transforms, localScale, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions, true);
        }
        static List<Tween> _ExScale(List<Transform> transforms, Vector3 localScale, float duration,
            MonoBehaviour mono, System.Action<List<Tween>> OnComplete, Transform[] exceptions, bool sequential = false)
        {
            if (transforms.ExIsValid() == false) { return null; }

            var result = new List<Tween>();
            result = result.ExGetListWithCount(transforms.ExNotNullCount());
            int validExceptionCount = 0;
            if (exceptions.ExIsValid())
            {
                validExceptionCount = exceptions.ExNotNullCount();
            }

            var completedCount = 0;
            if (sequential == false)
            {
                transforms.ExForEach((transform, i) =>
                {
                    if (transform != null)
                    {
                        var doIt = true;
                        if (exceptions.ExIsValid())
                        {
                            if (exceptions.ExContains(transform)) { doIt = false; }
                        }
                        if (doIt)
                        {
                            result[i] = transform.DOScale(localScale, duration).OnComplete(() =>
                            {
                                completedCount++;
                            });
                        }
                    }
                });
            }

            if (mono != null)
            {
                mono.StartCoroutine(COR());
            }
            return result;
            IEnumerator COR()
            {
                if (sequential)
                {
                    if (transforms.ExIsValid())
                    {
                        for (int i = 0; i < transforms.Count; i++)
                        {
                            var transform = transforms[i];
                            if (transform == null) { continue; }
                            var doIt = true;
                            if (exceptions.ExIsValid())
                            {
                                if (exceptions.ExContains(transform)) { doIt = false; }
                            }
                            if (doIt)
                            {
                                var done = false;
                                result[i] = transform.DOScale(localScale, duration).OnComplete(() =>
                                {
                                    completedCount++;
                                    done = true;
                                });
                                while (!done) { yield return null; }
                            }
                        }
                    }
                }
                else
                {
                    var exceptionValid = exceptions.ExIsValid();
                    while (true)
                    {
                        if (exceptionValid)
                        {
                            if (completedCount >= (transforms.Count - validExceptionCount)) { break; }
                        }
                        else
                        {
                            if (completedCount >= transforms.Count) { break; }
                        }
                        yield return null;
                    }
                }
                OnComplete?.Invoke(result);
            }
        }
        #endregion
    }
}