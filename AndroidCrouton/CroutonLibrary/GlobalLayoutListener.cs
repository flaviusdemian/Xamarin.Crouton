using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace CroutonLibrary
{
    internal class GlobalLayoutListener : Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        public void OnGlobalLayout(Action global_layout)
        {
            global_layout();
        }

        public void OnGlobalLayout() {}
    }
}