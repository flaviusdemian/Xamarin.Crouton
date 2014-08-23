using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace CroutonLibrary
{
    internal class GlobalLayoutListener : Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        public void OnGlobalLayout(Action action)
        {
            action();
        }

        public void OnGlobalLayout() {}
    }
}