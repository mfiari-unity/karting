using System.Collections;
using UnityEngine;

namespace MFGameLib.Utils
{
    public class ScreenFader : MonoBehaviour
    {

        Animator anim;
        bool isFading = false;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public IEnumerator fadeToClear()
        {
            isFading = true;
            anim.SetTrigger("FadeIn");

            while (isFading)
                yield return null;
        }

        public IEnumerator fadeToBlack()
        {
            isFading = true;
            anim.SetTrigger("FadeOut");

            while (isFading)
                yield return null;
        }

        void animationCompleted()
        {
            isFading = false;
        }
    }
}