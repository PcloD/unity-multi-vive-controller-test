using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeReticle : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    public GameObject reticle;
    private Vector3 hitPoint;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {        
        RaycastHit hit;

        if (Physics.Raycast(trackedObj.transform.position, trackedObj.transform.forward, out hit, 100)) {
            hitPoint = hit.point;
            SetReticleToHitPoint(hit);
        }
        
    }

    private void SetReticleToHitPoint(RaycastHit hit) {
        reticle.transform.position = hitPoint;
    }
}
