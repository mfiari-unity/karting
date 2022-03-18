using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public string Horizontal = "Horizontal";
        public string Vertical = "Vertical";
        public MobileInput mobileInput;

        public override Vector2 GenerateInput() {
            if (mobileInput != null)
            {
                return new Vector2
                {
                    x = Input.GetAxis(Horizontal) + mobileInput.GetHorizontalMove(),
                    y = Input.GetAxis(Vertical) + mobileInput.GetVerticalMove()
                };
            } else
            {
                return new Vector2
                {
                    x = Input.GetAxis(Horizontal),
                    y = Input.GetAxis(Vertical)
                };
            }
            
        }
    }
}
