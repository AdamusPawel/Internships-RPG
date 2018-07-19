using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(fileName = "New Menu", menuName = ("RPG/Item"))]
    public class Item : ScriptableObject
    {

        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;

        public virtual void Use()
        {
            //use the item
            //something have to happen

            Debug.Log("Item" + name + " got used!");
        }
    }
}
