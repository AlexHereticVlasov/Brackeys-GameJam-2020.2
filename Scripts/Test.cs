using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //IRewindable[] rewindables = FindObjectsOfType<IRewindable>();

            LinkedList<IRewindable> list = new LinkedList<IRewindable>();
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Rewindable");
            foreach (var item in objects)
            {
                if (item.TryGetComponent(out IRewindable rewindable))
                    list.AddLast(rewindable);
            }

            foreach (var item in list)
            {
                item.IsRewinding = true;
            }
        }
    }
}
