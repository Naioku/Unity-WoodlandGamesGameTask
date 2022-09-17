using System.Collections;
using _PROJECT.Scripts.Core;
using TMPro;
using UnityEngine;

namespace _PROJECT.Scripts.UI
{
    public class StartingStageCanvas : MonoBehaviour
    {
        [Tooltip("Fade time set separately to both fade in and out.")]
        [SerializeField] private float fadeInOutTime = 1f;
        [SerializeField] private float waitTimeBetweenFades = 1f;
        [SerializeField] private TextMeshProUGUI stageInfoLabel;
        
        private CanvasGroup _canvasGroup;
        private StageData _stageData;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _stageData = FindObjectOfType<StageData>();
        }

        private void Start()
        {
            FadeOutImmediate();
            stageInfoLabel.text = $"Stage {_stageData.StageLevel.ToString()}";
            StartCoroutine(FadeInOut());
        }

        private IEnumerator FadeInOut()
        {
            yield return FadeIn(fadeInOutTime);
            yield return new WaitForSecondsRealtime(waitTimeBetweenFades);
            yield return FadeOut(fadeInOutTime);
        }

        private void FadeOutImmediate()
        {
            _canvasGroup.alpha = 0f;
        }

        private IEnumerator FadeIn(float time)
        {
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        
        private IEnumerator FadeOut(float time)
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
