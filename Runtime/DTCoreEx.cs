using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityExt;

namespace DTExt
{
    public static class DTCoreEx
    {
        public static void ExResetDT(this Tween dt)
        {
            if (dt != null && dt.IsActive()) { dt.Kill(); }
        }
        public static bool ExIsValidDT(this Tween dt)
        {
            return dt != null && dt.IsActive();
        }
        public static void ExResetDT(this List<Tween> dtList)
        {
            dtList.ExForEachSafeCustomClass((dt) =>
            {
                if (dt != null) { dt.ExResetDT(); }
            });
        }
    }
}