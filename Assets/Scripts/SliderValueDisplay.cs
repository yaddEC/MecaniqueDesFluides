using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;
    public TMP_Text valueText;

    private void Start()
    {
        // Assurez-vous que le composant Slider est attach�
        if (slider == null)
        {
            Debug.LogError("Le composant Slider n'est pas attach� au script SliderValueDisplay !");
            return;
        }

        // Assurez-vous que le composant TextMeshPro ou Text est attach�
        if (valueText == null)
        {
            Debug.LogError("Le composant TextMeshPro ou Text n'est pas attach� au script SliderValueDisplay !");
            return;
        }

        // Mettez � jour la valeur initiale
        UpdateSliderValue(slider.value);

        // Attachez la fonction � l'�v�nement OnValueChanged du Slider
        slider.onValueChanged.AddListener(UpdateSliderValue);
    }

    private void OnDestroy()
    {
        // Assurez-vous de retirer l'�couteur d'�v�nement lorsque le script est d�truit
        slider.onValueChanged.RemoveListener(UpdateSliderValue);
    }

    private void UpdateSliderValue(float value)
    {
        // Mettez � jour le texte avec la valeur actuelle du Slider
        valueText.text = value.ToString("F2");
    }
}
