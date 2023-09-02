using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgress 
{
    public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
    public class ProgressChangedEventArgs : EventArgs
    {
        public float progress;
    }
}
