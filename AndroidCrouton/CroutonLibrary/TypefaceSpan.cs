using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using String = System.String;

namespace CroutonLibrary
{

    /*
     * Copyright 2012 - 2014 Benjamin Weiss
     *
     * Licensed under the Apache License, Version 2.0 (the "License");
     * you may not use this file except in compliance with the License.
     * You may obtain a copy of the License at
     *
     *     http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    //package de.keyboardsurfer.android.widget.crouton;

    //import android.content.Context;
    //import android.graphics.Typeface;
    //import android.support.v4.util.LruCache;
    //import android.text.TextPaint;
    //import android.text.style.MetricAffectingSpan;

    /**
     * Style a spannable with a custom {@link Typeface}.
     */
    public class TypefaceSpan : MetricAffectingSpan
    {
        /** An <code>LruCache</code> for previously loaded typefaces. */
        private static LruCache sTypefaceCache = new LruCache(5);

        private Typeface mTypeface;

        /**
         * Load the {@link Typeface} and apply to a spannable.
         */
        public TypefaceSpan(Context context, String typefaceName)
        {
            mTypeface = (Typeface)sTypefaceCache.Get(typefaceName);

            if (mTypeface == null)
            {
                mTypeface = Typeface.CreateFromAsset(context.ApplicationContext.Assets, String.Format("%s", typefaceName));

                // Cache the loaded Typeface
                sTypefaceCache.Put(typefaceName, mTypeface);
            }
        }

        public override void UpdateMeasureState(TextPaint p)
        {
            p.SetTypeface(mTypeface);
        }

        public override void UpdateDrawState(TextPaint tp)
        {
            tp.SetTypeface(mTypeface);
        }
    }
}