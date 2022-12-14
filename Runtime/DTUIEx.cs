using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExt;

namespace DTExt
{
    public static class DTUIEx
    {
        //todo blink possibly do not work, test
        #region Fade
        public static List<Tween> ExFade(this List<MaskableGraphic> graphics, float alpha, float duration,
            params MaskableGraphic[] exceptions)
        {
            return _ExFade(graphics, alpha, duration, null, null, exceptions);
        }
        public static List<Tween> ExFade(this List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour callerContext, System.Action OnComplete = null, params MaskableGraphic[] exceptions)
        {
            return _ExFade(graphics, alpha, duration, callerContext, (list) => { OnComplete?.Invoke(); }, exceptions);
        }
        public static List<Tween> ExFadeSequentially(this List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour callerContext, System.Action OnComplete = null, params MaskableGraphic[] exceptions)
        {
            return _ExFade(graphics, alpha, duration, callerContext, (list) => { OnComplete?.Invoke(); }, exceptions, true);
        }
        static List<Tween> _ExFade(List<MaskableGraphic> graphics, float alpha, float duration,
            MonoBehaviour callerContext, System.Action<List<Tween>> OnComplete, MaskableGraphic[] exceptions, bool sequential = false)
        {
            if (graphics.ExIsValid() == false) { return null; }

            var result = new List<Tween>();
            result = result.ExGetListWithCount(graphics.ExNotNullCount());
            int validExceptionCount = 0;
            if (exceptions.ExIsValid())
            {
                validExceptionCount = exceptions.ExNotNullCount();
            }

            var completedCount = 0;
            if (!sequential)
            {
                graphics.ExForEachSafe((graphic, i) =>
                {
                    if (graphic != null)
                    {
                        var doIt = true;
                        if (exceptions.ExIsValid())
                        {
                            if (exceptions.ExContains(graphic)) { doIt = false; }
                        }
                        if (doIt)
                        {
                            result[i] = graphic.DOFade(alpha, duration).OnComplete(() =>
                            {
                                completedCount++;
                            });
                        }
                    }
                });
            }

            if (callerContext != null)
            {
                callerContext.StartCoroutine(COR());
            }
            return result;
            IEnumerator COR()
            {
                if (sequential)
                {
                    if (graphics.ExIsValid())
                    {
                        for (int i = 0; i < graphics.Count; i++)
                        {
                            var graphic = graphics[i];
                            if (graphic == null) { continue; }
                            var doIt = true;
                            if (exceptions.ExIsValid())
                            {
                                if (exceptions.ExContains(graphic)) { doIt = false; }
                            }
                            if (doIt)
                            {
                                var done = false;
                                result[i] = graphic.DOFade(alpha, duration).OnComplete(() =>
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
                            if (completedCount >= (graphics.Count - validExceptionCount)) { break; }
                        }
                        else
                        {
                            if (completedCount >= graphics.Count) { break; }
                        }
                        yield return null;
                    }
                }
                OnComplete?.Invoke(result);
            }
        }
        #endregion

        #region Color
        public static List<Tween> ExColor(this List<MaskableGraphic> graphics, Color endColor, float duration,
            params MaskableGraphic[] exceptions)
        {
            return _ExColor(graphics, endColor, duration, null, null, exceptions);
        }
        public static List<Tween> ExColor(this List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour callerContext, System.Action OnComplete = null, params MaskableGraphic[] exceptions)
        {
            return _ExColor(graphics, endColor, duration, callerContext, (list) => { OnComplete?.Invoke(); }, exceptions);
        }
        public static List<Tween> ExColorSequentially(this List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour callerContext, System.Action OnComplete = null, params MaskableGraphic[] exceptions)
        {
            return _ExColor(graphics, endColor, duration, callerContext, (list) => { OnComplete?.Invoke(); }, exceptions, true);
        }
        static List<Tween> _ExColor(List<MaskableGraphic> graphics, Color endColor, float duration,
            MonoBehaviour callerContext, System.Action<List<Tween>> OnComplete, MaskableGraphic[] exceptions, bool sequential = false)
        {
            if (graphics.ExIsValid() == false) { return null; }

            var result = new List<Tween>();
            result = result.ExGetListWithCount(graphics.ExNotNullCount());
            int validExceptionCount = 0;
            if (exceptions.ExIsValid())
            {
                validExceptionCount = exceptions.ExNotNullCount();
            }

            var completedCount = 0;
            if (!sequential)
            {
                graphics.ExForEachSafe((graphic, i) =>
                {
                    var doIt = true;
                    if (exceptions.ExIsValid())
                    {
                        if (exceptions.ExContains(graphic)) { doIt = false; }
                    }
                    if (doIt)
                    {
                        result[i] = graphic.DOColor(endColor, duration).OnComplete(() =>
                        {
                            completedCount++;
                        });
                    }
                });
            }

            if (callerContext != null)
            {
                callerContext.StartCoroutine(COR());
            }
            return result;
            IEnumerator COR()
            {
                if (sequential)
                {
                    if (graphics.ExIsValid())
                    {
                        for (int i = 0; i < graphics.Count; i++)
                        {
                            var graphic = graphics[i];
                            if (graphic == null) { continue; }
                            var doIt = true;
                            if (exceptions.ExIsValid())
                            {
                                if (exceptions.ExContains(graphic)) { doIt = false; }
                            }
                            if (doIt)
                            {
                                var done = false;
                                result[i] = graphic.DOColor(endColor, duration).OnComplete(() =>
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
                            if (completedCount >= (graphics.Count - validExceptionCount)) { break; }
                        }
                        else
                        {
                            if (completedCount >= graphics.Count) { break; }
                        }
                        yield return null;
                    }
                }
                OnComplete?.Invoke(result);
            }
        }
        #endregion

        #region FillAmount
        public static Tween ExFillAmount(this Image image, float fillTarget, float duration, System.Action<float> OnUpdate, System.Action OnComplete, Ease ease = Ease.Linear)
        {
            float tvar = image.fillAmount;
            var tw = DOTween.To(() =>
            {
                return tvar;
            },
           x =>
           {
               tvar = x;
               image.fillAmount = tvar;
               OnUpdate?.Invoke(tvar);
           }
           , fillTarget, duration).OnComplete((() =>
           {
               OnComplete?.Invoke();
           })).SetEase(ease);
            return tw;
        }
        public static List<Tween> ExFillAmount(this List<Image> images, float fillTarget, float duration,
            params Image[] exceptions)
        {
            return _ExFillAmount(images, fillTarget, duration, null, null, exceptions);
        }
        public static List<Tween> ExFillAmount(this List<Image> images, float fillTarget, float duration,
            MonoBehaviour mono, System.Action OnComplete = null, params Image[] exceptions)
        {
            return _ExFillAmount(images, fillTarget, duration, mono, (list) => { OnComplete?.Invoke(); }, exceptions);
        }
        public static List<Tween> ExFillAmountSequentially(this List<Image> images, float fillTarget, float duration,
            MonoBehaviour callerContext, System.Action OnComplete = null, params Image[] exceptions)
        {
            return _ExFillAmount(images, fillTarget, duration, callerContext, (list) => { OnComplete?.Invoke(); }, exceptions, true);
        }
        static List<Tween> _ExFillAmount(List<Image> images, float fillTarget, float duration,
            MonoBehaviour callerContext, System.Action<List<Tween>> OnComplete, Image[] exceptions, bool sequential = false)
        {
            if (images.ExIsValid() == false) { return null; }

            var result = new List<Tween>();
            result = result.ExGetListWithCount(images.ExNotNullCount());
            int validExceptionCount = 0;
            if (exceptions.ExIsValid())
            {
                validExceptionCount = exceptions.ExNotNullCount();
            }

            var completedCount = 0;
            if (sequential == false)
            {
                images.ExForEachSafe((graphic, i) =>
                {
                    var doIt = true;
                    if (exceptions.ExIsValid())
                    {
                        if (exceptions.ExContains(graphic)) { doIt = false; }
                    }
                    if (doIt)
                    {
                        result[i] = graphic.DOFillAmount(fillTarget, duration).OnComplete(() =>
                        {
                            completedCount++;
                        });
                    }
                });
            }

            if (callerContext != null)
            {
                callerContext.StartCoroutine(COR());
            }
            return result;
            IEnumerator COR()
            {
                if (sequential)
                {
                    if (images.ExIsValid())
                    {
                        for (int i = 0; i < images.Count; i++)
                        {
                            var graphic = images[i];
                            if (graphic == null) { continue; }
                            var doIt = true;
                            if (exceptions.ExIsValid())
                            {
                                if (exceptions.ExContains(graphic)) { doIt = false; }
                            }
                            if (doIt)
                            {
                                var done = false;
                                result[i] = graphic.DOFillAmount(fillTarget, duration).OnComplete(() =>
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
                            if (completedCount >= (images.Count - validExceptionCount)) { break; }
                        }
                        else
                        {
                            if (completedCount >= images.Count) { break; }
                        }
                        yield return null;
                    }
                }
                OnComplete?.Invoke(result);
            }
        }
        #endregion

        #region Blink
        public static Coroutine ExBlinkContinue(this MaskableGraphic graphic, MonoBehaviour callerContext, float cycleTime)
        {
            return _ExBlink(callerContext, graphic, null, cycleTime, -1f, null);
        }
        public static Coroutine ExBlinkUntil(this MaskableGraphic graphic, MonoBehaviour callerContext,
            float cycleTime, WhenToDoFunc stopperCondition, System.Action OnComplete = null)
        {
            return _ExBlink(callerContext, graphic, stopperCondition, cycleTime, -1f, OnComplete);
        }
        public static Coroutine ExBlinkUntil(this MaskableGraphic graphic, MonoBehaviour callerContext,
            float cycleTime, float maxTime, System.Action OnComplete = null)
        {
            return _ExBlink(callerContext, graphic, null, cycleTime, maxTime, OnComplete);
        }
        public static Coroutine ExBlinkUntilConditionOrTime(this MaskableGraphic graphic, MonoBehaviour callerContext,
            float cycleTime, float maxTime, WhenToDoFunc stopperCondition, System.Action OnComplete = null)
        {
            return _ExBlink(callerContext, graphic, stopperCondition, cycleTime, maxTime, OnComplete);
        }
        static Coroutine _ExBlink(MonoBehaviour callerContext, MaskableGraphic graphic,
            WhenToDoFunc stopperCondition, float cycleTime, float maxTime, System.Action OnComplete)
        {
            return callerContext.StartCoroutine(TapToStartBlink(graphic, stopperCondition, cycleTime, maxTime, OnComplete));
            IEnumerator TapToStartBlink(MaskableGraphic graphic, WhenToDoFunc stopperCondition,
                float cycleTime, float maxTime, System.Action OnComplete)
            {
                graphic.gameObject.SetActive(true);
                var timer = 0.0f;
                while (true)
                {
                    if ((maxTime > 0.0f && timer > maxTime) || (stopperCondition != null && stopperCondition.Invoke()))
                    {
                        graphic.ExSetAlpha(0.0f);
                        OnComplete?.Invoke();
                        yield break;
                    }

                    {
                        var fadeIn = false;
                        graphic.ExSetAlpha(0.0f);
                        graphic.DOFade(1.0f, cycleTime).OnComplete(() => { fadeIn = true; });
                        while (!fadeIn)
                        {
                            if (stopperCondition != null && stopperCondition.Invoke()) { OnComplete?.Invoke(); yield break; }

                            timer += Time.deltaTime;
                            yield return null;
                        }
                    }

                    {
                        var fadeOut = false;
                        graphic.ExSetAlpha(1.0f);
                        graphic.DOFade(0.0f, cycleTime).OnComplete(() => { fadeOut = true; });
                        while (!fadeOut)
                        {
                            if (stopperCondition != null && stopperCondition.Invoke()) { OnComplete?.Invoke(); yield break; }

                            timer += Time.deltaTime;
                            yield return null;
                        }
                    }
                    yield return null;
                }
            }
        }
        #endregion
    }
}