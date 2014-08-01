using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;

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


    /** The style for a {@link Crouton}. */

    public class Style
    {
        public static int NOT_SET = -1;

        /** Default style for alerting the user. */
        public static Style ALERT;
        /** Default style for confirming an action. */
        public static Style CONFIRM;
        /** Default style for general information. */
        public static Style INFO;

        /**
         * The resource id of the backgroundResourceId.
         * <p/>
         * 0 for no backgroundResourceId.
         */
        public int BackgroundColorResourceId;

        /**
         * The resource id of the BackgroundDrawableResourceId.
         * <p/>
         * 0 for no BackgroundDrawableResourceId.
         */

        /**
         * The backgroundColorResourceValue's e.g. 0xffff4444;
         * <p/>
         * NOT_SET for no value.
         */
        public int BackgroundColorValue;
        public int BackgroundDrawableResourceId;
        public Configuration Configuration;
        public String FontName;

        /** The file path and font name resource id for the view content */
        public int FontNameResId;

        /** Whether we should IsTileEnabled the backgroundResourceId or not. */

        /** The text's Gravity as provided by {@link Gravity}. */
        public int Gravity;
        public int HeightDimensionResId;
        public int HeightInPixels;

        /** An additional image to display in the {@link Crouton}. */
        public Drawable ImageDrawable;

        /** An additional image to display in the {@link Crouton}. */
        public int ImageResId;

        /** The {@link ImageView.ScaleType} for the image to display in the {@link Crouton}. */
        public ImageView.ScaleType ImageScaleType;
        public bool IsTileEnabled;
        public int PaddingDimensionResId;
        public int PaddingInPixels;
        public int TextAppearanceResId;
        public int TextColorResourceId;

        /**
         * The textColorResourceValue's e.g. 0xffff4444;
         * <p/>
         * NOT_SET for no value.
         */
        public int TextColorValue;

        /**
         * The text size in sp
         * <p/>
         * 0 sets the text size to the system theme default
         */

        /** The text shadow color's resource id */
        public int TextShadowColorResId;

        /** The text shadow radius */

        /** The text shadow horizontal offset */
        public float TextShadowDx;
        public float TextShadowDy;
        public float TextShadowRadius;
        public int TextSize;
        public int WidthDimensionResId;
        public int WidthInPixels;

        static Style()
        {
            ALERT = new StyleBuilder().SetBackgroundColor(Resource.Color.holo_red_light).Build();
            CONFIRM = new StyleBuilder().SetBackgroundColorValue(Resource.Color.holo_blue_light).Build();
            INFO = new StyleBuilder().SetBackgroundColorValue(Resource.Color.holo_blue_light).Build();
        }

        /** The text appearance resource id for the text. */

        public Style(StyleBuilder builder)
        {
            Configuration = builder.Configuration;
            BackgroundColorResourceId = builder.BackgroundColorResourceId;
            BackgroundDrawableResourceId = builder.BackgroundDrawableResourceId;
            IsTileEnabled = builder.IsTileEnabled;
            TextColorResourceId = builder.TextColorResourceId;
            TextColorValue = builder.TextColorValue;
            HeightInPixels = builder.HeightInPixels;
            HeightDimensionResId = builder.HeightDimensionResId;
            WidthInPixels = builder.WidthInPixels;
            WidthDimensionResId = builder.WidthDimensionResId;
            Gravity = builder.Gravity;
            ImageDrawable = builder.ImageDrawable;
            TextSize = builder.TextSize;
            TextShadowColorResId = builder.TextShadowColorResId;
            TextShadowRadius = builder.TextShadowRadius;
            TextShadowDx = builder.TextShadowDx;
            TextShadowDy = builder.TextShadowDy;
            TextAppearanceResId = builder.TextAppearanceResId;
            ImageResId = builder.ImageResId;
            ImageScaleType = builder.ImageScaleType;
            PaddingInPixels = builder.PaddingInPixels;
            PaddingDimensionResId = builder.PaddingDimensionResId;
            BackgroundColorValue = builder.BackgroundColorValue;
            FontName = builder.FontName;
            FontNameResId = builder.FontNameResId;
        }

        public override String ToString()
        {
            return "Style{" +
                   "Configuration=" + Configuration +
                   ", BackgroundColorResourceId=" + BackgroundColorResourceId +
                   ", BackgroundDrawableResourceId=" + BackgroundDrawableResourceId +
                   ", BackgroundColorValue=" + BackgroundColorValue +
                   ", IsTileEnabled=" + IsTileEnabled +
                   ", TextColorResourceId=" + TextColorResourceId +
                   ", TextColorValue=" + TextColorValue +
                   ", HeightInPixels=" + HeightInPixels +
                   ", HeightDimensionResId=" + HeightDimensionResId +
                   ", WidthInPixels=" + WidthInPixels +
                   ", WidthDimensionResId=" + WidthDimensionResId +
                   ", Gravity=" + Gravity +
                   ", ImageDrawable=" + ImageDrawable +
                   ", ImageResId=" + ImageResId +
                   ", ImageScaleType=" + ImageScaleType +
                   ", TextSize=" + TextSize +
                   ", TextShadowColorResId=" + TextShadowColorResId +
                   ", TextShadowRadius=" + TextShadowRadius +
                   ", TextShadowDy=" + TextShadowDy +
                   ", TextShadowDx=" + TextShadowDx +
                   ", TextAppearanceResId=" + TextAppearanceResId +
                   ", PaddingInPixels=" + PaddingInPixels +
                   ", PaddingDimensionResId=" + PaddingDimensionResId +
                   ", FontName=" + FontName +
                   ", FontNameResId=" + FontNameResId +
                   '}';
        }
    }
}