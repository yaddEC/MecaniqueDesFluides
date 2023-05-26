using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;
    public TMP_Text valueText;

    private void Start()
    {
        // Assurez-vous que le composant Slider est attaché
        if (slider == null)
        {
            Debug.LogError("Le composant Slider n'est pas attaché au script SliderValueDisplay !");
            return;
        }

        // Assurez-vous que le composant TextMeshPro ou Text est attaché
        if (valueText == null)
        {
            Debug.LogError("Le composant TextMeshPro ou Text n'est pas attaché au script SliderValueDisplay !");
            return;
        }

        // Mettez à jour la valeur initiale
        UpdateSliderValue(slider.value);

        // Attachez la fonction à l'événement OnValueChanged du Slider
        slider.onValueChanged.AddListener(UpdateSliderValue);
    }

    private void OnDestroy()
    {
        // Assurez-vous de retirer l'écouteur d'événement lorsque le script est détruit
        slider.onValueChanged.RemoveListener(UpdateSliderValue);
    }

    private void UpdateSliderValue(float value)
    {
        // Mettez à jour le texte avec la valeur actuelle du Slider
        valueText.text = value.ToString("F2");
    }
}
