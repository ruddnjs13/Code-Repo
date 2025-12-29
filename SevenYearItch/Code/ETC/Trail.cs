using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _00Work.LKW.Code.ETC
{
    public class Trail : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(DelayedDisable());
        }

        private IEnumerator DelayedDisable()
        {
            yield return new WaitForSeconds(0.1f); 
            Disable();
        }

        public void Disable()
        {
            transform.DOScale(0f, 0.08f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}