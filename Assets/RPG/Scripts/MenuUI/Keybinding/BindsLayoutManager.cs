using System.Collections.Generic;
using UnityEngine;
using System;

namespace Keybinds
{
    public class BindsLayoutManager : MonoBehaviour
    {
        [SerializeField] private Transform bindButtonHolder;
        [SerializeField] private GameObject bindButton;

        private List<GameObject> buttonsToClear = new List<GameObject>();
        private List<string> buttonNames = new List<string>();

        // Start is called before the first frame update
        void Start()
        {
            foreach(Binding binding in BindingManager.instance.defaultBindings)
            {
                buttonNames.Add(binding.Name);
            }

            // make sure that the buttons are not still spawned
            if(buttonsToClear != null)
            {
                foreach(GameObject obj in buttonsToClear)
                {
                    buttonsToClear.Remove(obj);
                    Destroy(obj);
                }
            }
            SpawnButton(bindButton);
        }

        /// <summary>
        /// Spawns a set of buttons for keybinding
        /// </summary>
        /// <param name="_button">the button prefab we want to spawn</param>
        private void SpawnButton(GameObject _button)
        {
            // spawn a set of buttons with  the corrosponding keybind settings
            // make sure they are spawned either left or right
            for(int i = 0; i < buttonNames.Count; i++)
            {
                // spawns objects in a set position depending on if the object is odd or even in the list
                Instantiate(_button, bindButtonHolder);
                // gets the buttons component and sets the name of the button
                BindingButton _bindName = _button.GetComponent<BindingButton>();
                _bindName.bindingToMap = buttonNames[i];
                buttonsToClear.Add(_button);
            }
        }
    }
}
