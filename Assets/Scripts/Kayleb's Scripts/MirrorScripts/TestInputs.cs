using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputs : MonoBehaviour {
	
	void Update ()
    {
        SwapInput();
        MirrorToggleInput();
        TorchToggleInput();
	}

    void SwapInput()
    {
        if (Input.GetKeyDown("f"))
        {
            UniverseController.Instance.Swap();
        }
    }

    void MirrorToggleInput()
    {
        if (Input.GetKeyDown("q"))
        {
            InventoryController.Instance.ToggleMirror();
        }
    }

    void TorchToggleInput()
    {
        if (Input.GetKeyDown("e"))
        {
            InventoryController.Instance.ToggleTorch();
        }
    }
}