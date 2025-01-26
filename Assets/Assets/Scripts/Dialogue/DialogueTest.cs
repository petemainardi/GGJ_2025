using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using MainArtery.Utilities;

/// ===========================================================================================
/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
/// ===========================================================================================
/**
 *  This class does things...
 */
/// ===========================================================================================
/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
/// ===========================================================================================
public class DialogueTest : MonoBehaviour, IDialogueSource
{
	/// =======================================================================================
	/// Fields
	/// =======================================================================================
	[field: SerializeField] public Color Color { get; private set; } = Color.white;

    [field: SerializeField] public float TimeBetweenDialogue { get; private set; } = 5f;
	private float dialogueTimer = 0f;

    [SerializeField] private DialogueOptions[] TextSets;
	private readonly System.Random rand = new System.Random();

	/// =======================================================================================
	/// Mono
	/// =======================================================================================
	void Awake()
	{
		transform.Find("Mesh").GetComponent<MeshRenderer>().material.color = Color;
    }
    /// ---------------------------------------------------------------------------------------
	void Update()
    {
		if (dialogueTimer > 0)
			dialogueTimer -= Time.deltaTime;
    }
    /// =======================================================================================
    /// Collision
    /// =======================================================================================
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && dialogueTimer <= 0)
        {
            dialogueTimer = TimeBetweenDialogue;

            DialogueOptions textSet = TextSets.RandomElement(rand);
            DialogueManager.Instance.Add(textSet.Options.RandomElement(rand), this, textSet.DemandsResponse);
        }
    }
    /// ---------------------------------------------------------------------------------------
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueTimer = 0;
            DialogueManager.Instance.Clear(this);
        }
    }
    /// =======================================================================================
}
/// ===========================================================================================
/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
/// ===========================================================================================