using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace CroutonLibrary
{
    internal class GlobalLayoutListener : Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private readonly Action on_global_layout;

        public void OnGlobalLayout(Action global_layout)
        {
            global_layout();
        }

        public void OnGlobalLayout()
        {
            on_global_layout();
        }
    }
}