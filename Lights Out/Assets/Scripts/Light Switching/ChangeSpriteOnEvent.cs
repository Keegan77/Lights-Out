using UnityEngine;

namespace Light_Switching
{
    public class ChangeSpriteOnEvent : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite darkSprite;
        [SerializeField] private Sprite lightSprite;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            ChangeSprite(); // call here to update sprite on scene start - fixes any platform inversion issues
        }
        private void OnEnable() => TimePhaseManager.OnPhaseChanged += ChangeSprite;
        private void OnDisable() => TimePhaseManager.OnPhaseChanged -= ChangeSprite;

        private void ChangeSprite()
        {
            spriteRenderer.sprite = TimePhaseManager.Instance.IsLight() ? darkSprite : lightSprite;
        }

    }
}
