using UnityEngine;
using UnityEngine.UI;

namespace MFGameLib.Utils
{
    public class UIUtils
    {

        private static string defaultTextPanelLocation = "UI/Canvas";

        public static void shwoDialog(string text)
        {
            Object prefab = Resources.Load(defaultTextPanelLocation);
            if (prefab != null)
            {
                GameObject textCanvasGameObject = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                textCanvasGameObject.transform.position = new Vector3(0, 0);
                Image image = textCanvasGameObject.GetComponentInChildren<Image>();
                UnityEngine.UI.Text textField = image.GetComponentInChildren<UnityEngine.UI.Text>();
                textField.text = text;
            }
        }
    }
}