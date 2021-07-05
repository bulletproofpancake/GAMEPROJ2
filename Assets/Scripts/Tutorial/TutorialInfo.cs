using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "TutorialInfo", menuName = "Data/Tutorial Info", order = 0)]
public class TutorialInfo : ScriptableObject
{
    [SerializeField] private string title;
    [TextArea] 
    [SerializeField] private string body;
    [SerializeField] private Sprite sprite;

    public string Title => title;
    public string Body => body;
    public Sprite Display => sprite;
}