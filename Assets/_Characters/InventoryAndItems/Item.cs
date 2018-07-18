using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(fileName = "New Menu", menuName = ("RPG/Item"))]
    public class Item : ScriptableObject
    {

        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;


    }
}
