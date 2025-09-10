using UnityEngine;
using UnityEngine.UI;
using Game.ViewModels;

namespace Game.Views
{
    public class CardView : MonoBehaviour
    {
        public Image frontImage;
        public Image backImage;

        private ICardViewModel vm;

        public void Bind(ICardViewModel viewModel, Sprite sprite)
        {
            vm = viewModel;
            frontImage.sprite = sprite;

            vm.OnRevealed += ShowFront;
            vm.OnHidden += ShowBack;
            vm.OnMatched += Disable;

            ShowBack(vm);
        }

        public void OnClick()
        {
            vm?.Reveal();
        }

        private void ShowFront(ICardViewModel _)
        {
            frontImage.gameObject.SetActive(true);
            backImage.gameObject.SetActive(false);
        }

        private void ShowBack(ICardViewModel _)
        {
            frontImage.gameObject.SetActive(false);
            backImage.gameObject.SetActive(true);
        }

        private void Disable(ICardViewModel _)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
