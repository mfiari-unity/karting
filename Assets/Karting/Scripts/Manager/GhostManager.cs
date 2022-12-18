using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public struct GhostTransform
{
    public Vector3 position;
    public Quaternion rotation;

    public GhostTransform (Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
    }
}

public class GhostManager : MonoBehaviour
{

    public Transform kart;
    public Transform ghost;
    public Transform cameraPlaceholder;
    public CinemachineVirtualCamera cinemachineCam;

    public bool recording;
    public bool playing;

    private List<GhostTransform> recordedGhostTransforms = new List<GhostTransform>();
    private List<GhostTransform> playingGhostTransforms = new List<GhostTransform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (recording)
        {
            var newGhostTransform = new GhostTransform(kart);
            recordedGhostTransforms.Add(newGhostTransform);
        }
        if (playing)
        {
            Play();
        }
    }

    void Play()
    {
        ghost.gameObject.SetActive(true);
        StartCoroutine(StartGhost());

        /*cinemachineCam.LookAt = cameraPlaceholder;
        cinemachineCam.Follow = cameraPlaceholder;*/
        /*cinemachineCam.LookAt = ghost;
        cinemachineCam.Follow = ghost;*/

        playing = false;
    }

    IEnumerator StartGhost ()
    {
        for (int i = 0; i < playingGhostTransforms.Count; i++)
        {
            ghost.position = playingGhostTransforms[i].position;
            ghost.rotation = playingGhostTransforms[i].rotation;
            yield return new WaitForFixedUpdate();
        }
    }

    public void saveGhost (float time)
    {
        if (LevelManager.instance != null)
        {
            LevelManager.instance.setGhost(recordedGhostTransforms, time);
        }
    }

    public void LoadGhost ()
    {
        if (LevelManager.instance != null)
        {
            playingGhostTransforms = LevelManager.instance.GetGhost();
        }
    }
}
