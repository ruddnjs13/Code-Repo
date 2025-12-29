using UnityEngine;
using UnityEngine.EventSystems;

namespace _00Work.LKW.Code.UI
{
    public enum BtnType
    {
        Start,
        Setting,
        Exit,
        Quit
    }
    
    public class ButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private BtnType btnType;
        private Vector2 originSize;
        private  readonly float sizeMultiplier = 1.4f;

        public void Click()
        {
            BtnManager.Instance.CheckButton(btnType);
        }
        
        private void Start()
        {
            originSize = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = originSize * sizeMultiplier;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = originSize;
        }
    }
}