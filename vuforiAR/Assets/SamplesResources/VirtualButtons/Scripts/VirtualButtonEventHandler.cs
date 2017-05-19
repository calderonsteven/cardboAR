/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections.Generic;
using Vuforia;

/// <summary>
/// This class implements the IVirtualButtonEventHandler interface and
/// contains the logic to swap materials for the teapot model depending on what 
/// virtual button has been pressed.
/// </summary>
public class VirtualButtonEventHandler : MonoBehaviour,
                                         IVirtualButtonEventHandler
{
    #region PUBLIC_MEMBERS
    /// <summary>
    /// The materials that will be set for the teapot model
    /// </summary>
    public Material[] m_TeapotMaterials;
    public Material m_VirtualButtonMaterial;
    public Transform carModel;
    #endregion // PUBLIC_MEMBERS


    #region PRIVATE_MEMBERS
    private GameObject mTeapot;
    #endregion // PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        // Register with the virtual buttons TrackableBehaviour and set the material
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            vbs[i].RegisterEventHandler(this);

            if (m_VirtualButtonMaterial != null)
            {
                vbs[i].GetComponent<MeshRenderer>().sharedMaterial = m_VirtualButtonMaterial;
            }
        }

        // Get handle to the teapot object
        mTeapot = transform.FindChild("honda-type-r").gameObject;
        // mTeapot = carModel;
    }

    #endregion // MONOBEHAVIOUR_METHODS
    
	
    #region PUBLIC_METHODS
    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("OnButtonPressed: " + vb.VirtualButtonName);

        if (!IsValid())
        {
            return;
        }

        int indexMaterial = -1;

        // Add the material corresponding to this virtual button
        // to the active material list:
        switch (vb.VirtualButtonName)
        {
        case "red":
            indexMaterial = 0;
            break;

        case "blue":
            indexMaterial = 1;
            break;

        case "yellow":
            indexMaterial = 2;
            break;

        case "green":
            indexMaterial = 3;
            break;
        }

        if(indexMaterial != -1){
            // Apply the new material:
            foreach (Transform child in carModel){
                if (child.CompareTag("car-color-mat")) {
                    // Debug.Log("CHILD: " + child.name);
                    child.GetComponent<Renderer>().material = m_TeapotMaterials[indexMaterial];
                }
            }
        }

        if(vb.VirtualButtonName == "leftButton" || vb.VirtualButtonName == "rightButton"){
            var audiR8 = transform.FindChild("audi-r8-red").gameObject;
            mTeapot.SetActive(!mTeapot.activeInHierarchy);
            audiR8.SetActive(!audiR8.activeInHierarchy);
        }
        
    }

    /// <summary>
    /// Called when the virtual button has just been released:
    /// </summary>
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {

    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    private bool IsValid()
    {
        // Check the materials and teapot have been set:
        return m_TeapotMaterials != null &&
            m_TeapotMaterials.Length == 5 &&
            mTeapot != null;
    }
    #endregion //PRIVATE_METHODS
}
