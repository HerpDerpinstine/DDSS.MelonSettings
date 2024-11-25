using DDSS_MelonSettings.Components;
using System.Collections;
using UnityEngine;

namespace DDSS_MelonSettings.Utils
{
    internal static class Extensions
    {
        internal static Coroutine StartCoroutine<T>(this T behaviour, IEnumerator enumerator)
            where T : MonoBehaviour
            => behaviour.StartCoroutine(
                new Il2CppSystem.Collections.IEnumerator(
                new ManagedEnumerator(enumerator).Pointer));
    }
}
