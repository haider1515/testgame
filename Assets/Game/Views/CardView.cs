using UnityEngine;
using UnityEngine.UI;
using Game.ViewModels;

namespace Game.Views
{
    [RequireComponent(typeof(Button))]
    public class CardView : MonoBehaviour
    {
        public Image frontImage;
        public Button backImage;

        private ICardViewModel vm;
        private Button button;
        private bool isAnimating;

        [Header("Flip Settings")]
        public float flipDuration = 0.4f;

        public void Bind(ICardViewModel viewModel, Sprite sprite)
        {
            vm = viewModel;
            frontImage.sprite = sprite;

            vm.OnRevealed += _ => Flip(true);
            vm.OnHidden += _ => Flip(false);
            vm.OnMatched += Disable;

            button = GetComponent<Button>();
            backImage.onClick.RemoveAllListeners();
            backImage.onClick.AddListener(OnClick);

            ShowBackImmediate();
        }

        private void OnClick()
        {
            if (!isAnimating)
                vm?.Reveal();
        }

        private void Flip(bool showFront)
        {
            // Cancel any ongoing tween on this card
            LeanTween.cancel(gameObject);
            isAnimating = true;

            float halfTime = flipDuration / 2f;

            float startRot = transform.localRotation.eulerAngles.y;
            float midRot = 90;

            LeanTween.rotateY(gameObject, midRot, halfTime)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    frontImage.gameObject.SetActive(showFront);
                    backImage.gameObject.SetActive(!showFront);

                    float endRot = showFront ? 180 : 0;
                    LeanTween.rotateY(gameObject, endRot, halfTime)
                        .setEase(LeanTweenType.easeInOutQuad)
                        .setOnComplete(() => isAnimating = false);
                });
        }


        private void ShowBackImmediate()
        {
            (transform as RectTransform).localRotation = Quaternion.Euler(0, 0, 0);
            frontImage.gameObject.SetActive(false);
            backImage.gameObject.SetActive(true);
        }

        private void Disable(ICardViewModel _)
        {
            if (button != null)
                button.interactable = false;

            LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.15f)
                .setEasePunch();
        }
    }
}
