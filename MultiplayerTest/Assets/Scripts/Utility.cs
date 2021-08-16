using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public static class Utility
{
    /// <summary>
    /// Puts the string into the Clipboard.
    /// </summary>
    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}
