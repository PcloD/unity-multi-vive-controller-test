using System.Collections.Generic;
using UnityEngine;

public class VRControllerMonitor : MonoBehaviour {
    private static string OPENVR_CONTROLLER_LEFT = "OpenVR Controller - Left";
    private static string OPENVR_CONTROLLER_RIGHT = "OpenVR Controller - Right";
    private static int INVALID_VR_CONTROLLER_INDEX = -1;

    private List<int> vrControllerIndices = new List<int>(10);
    public float waitTime = 2.0f;
    private float timer = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > waitTime) {
            DetectControllerChanges();
            // Reset the timer
            timer = 0.0f;
        }
	}

    void DetectControllerChanges() {
        //Debug.Log("Checking controller changes");
        string[] inputDevices = Input.GetJoystickNames();

        for (int deviceIndex = 0; deviceIndex < inputDevices.Length; deviceIndex++) {
            string deviceName = inputDevices[deviceIndex];
            //Debug.Log(deviceName);

            if (deviceName.Equals(OPENVR_CONTROLLER_LEFT) || deviceName.Equals(OPENVR_CONTROLLER_RIGHT)) {
                // If the index doesn't already exist then this is a new controller attached
                if(!vrControllerIndices.Contains(deviceIndex)) {
                    int emptySlot = vrControllerIndices.IndexOf(INVALID_VR_CONTROLLER_INDEX);

                    if (emptySlot >= 0) {
                        vrControllerIndices[emptySlot] = deviceIndex;
                    }
                    else {
                        vrControllerIndices.Add(deviceIndex);
                        emptySlot = vrControllerIndices.Count - 1;
                    }
                    OnControllerAdded(emptySlot);
                }
            }
            else {
                // Does the index exist in the list but not match the names of the VR controllers?
                // Must have been removed or swapped with something else so assign an invalid index to maintain the array size
                if(vrControllerIndices.Contains(deviceIndex)) {
                    int index = vrControllerIndices.IndexOf(deviceIndex);
                    vrControllerIndices[index] = INVALID_VR_CONTROLLER_INDEX;
                    OnControllerRemoved(index);
                }
            }
        }
    }

    void OnControllerAdded(int index) {
        Debug.Log("Controller added at index " +index);

    }

    void OnControllerRemoved(int index) {
        Debug.Log("Controller removed from index "+index);

    }
}
