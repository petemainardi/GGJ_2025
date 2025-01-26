using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName="Dialogue Options", menuName="Dialogue Options")]
public class DialogueOptions : ScriptableObject
{
	[field: SerializeField] public bool DemandsResponse { get; private set; }

	[TextArea()]
	public List<string> Options = new List<string>();
}