using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Identifying characteristics for something that serves dialogue to the manager.
/// </summary>
public interface IDialogueSource
{
    public Color Color { get; }
}
