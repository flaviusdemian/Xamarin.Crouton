using System;
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
        public static uint holoRedLight = 0xffff4444;
        public static uint holoGreenLight = 0xff99cc00;
        public static uint holoBlueLight = 0xff33b5e5;

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
        public int backgroundColorResourceId;

        /**
         * The resource id of the backgroundDrawableResourceId.
         * <p/>
         * 0 for no backgroundDrawableResourceId.
         */

        /**
         * The backgroundColorResourceValue's e.g. 0xffff4444;
         * <p/>
         * NOT_SET for no value.
         */
        public int backgroundColorValue;
        public int backgroundDrawableResourceId;
        public Configuration configuration;
        public String fontName;

        /** The file path and font name resource id for the view content */
        public int fontNameResId;

        /** Whether we should isTileEnabled the backgroundResourceId or not. */

        /** The text's gravity as provided by {@link Gravity}. */
        public int gravity;
        public int heightDimensionResId;
        public int heightInPixels;

        /** An additional image to display in the {@link Crouton}. */
        public Drawable imageDrawable;

        /** An additional image to display in the {@link Crouton}. */
        public int imageResId;

        /** The {@link ImageView.ScaleType} for the image to display in the {@link Crouton}. */
        public ImageView.ScaleType imageScaleType;
        public bool isTileEnabled;
        public int paddingDimensionResId;
        public int paddingInPixels;
        public int textAppearanceResId;
        public int textColorResourceId;

        /**
         * The textColorResourceValue's e.g. 0xffff4444;
         * <p/>
         * NOT_SET for no value.
         */
        public int textColorValue;

        /**
         * The text size in sp
         * <p/>
         * 0 sets the text size to the system theme default
         */

        /** The text shadow color's resource id */
        public int textShadowColorResId;

        /** The text shadow radius */

        /** The text shadow horizontal offset */
        public float textShadowDx;
        public float textShadowDy;
        public float textShadowRadius;
        public int textSize;
        public int widthDimensionResId;
        public int widthInPixels;

        static Style()
        {
            ALERT = new StyleBuilder()
                .setBackgroundColorValue((int) holoRedLight)
                .build();
            CONFIRM = new StyleBuilder()
                .setBackgroundColorValue((int) holoGreenLight)
                .build();
            INFO = new StyleBuilder()
                .setBackgroundColorValue((int) holoBlueLight)
                .build();
        }

        /** The text appearance resource id for the text. */

        public Style(StyleBuilder builder)
        {
            configuration = builder.configuration;
            backgroundColorResourceId = builder.backgroundColorResourceId;
            backgroundDrawableResourceId = builder.backgroundDrawableResourceId;
            isTileEnabled = builder.isTileEnabled;
            textColorResourceId = builder.textColorResourceId;
            textColorValue = builder.textColorValue;
            heightInPixels = builder.heightInPixels;
            heightDimensionResId = builder.heightDimensionResId;
            widthInPixels = builder.widthInPixels;
            widthDimensionResId = builder.widthDimensionResId;
            gravity = builder.gravity;
            imageDrawable = builder.imageDrawable;
            textSize = builder.textSize;
            textShadowColorResId = builder.textShadowColorResId;
            textShadowRadius = builder.textShadowRadius;
            textShadowDx = builder.textShadowDx;
            textShadowDy = builder.textShadowDy;
            textAppearanceResId = builder.textAppearanceResId;
            imageResId = builder.imageResId;
            imageScaleType = builder.imageScaleType;
            paddingInPixels = builder.paddingInPixels;
            paddingDimensionResId = builder.paddingDimensionResId;
            backgroundColorValue = builder.backgroundColorValue;
            fontName = builder.fontName;
            fontNameResId = builder.fontNameResId;
        }

        public override String ToString()
        {
            return "Style{" +
                   "configuration=" + configuration +
                   ", backgroundColorResourceId=" + backgroundColorResourceId +
                   ", backgroundDrawableResourceId=" + backgroundDrawableResourceId +
                   ", backgroundColorValue=" + backgroundColorValue +
                   ", isTileEnabled=" + isTileEnabled +
                   ", textColorResourceId=" + textColorResourceId +
                   ", textColorValue=" + textColorValue +
                   ", heightInPixels=" + heightInPixels +
                   ", heightDimensionResId=" + heightDimensionResId +
                   ", widthInPixels=" + widthInPixels +
                   ", widthDimensionResId=" + widthDimensionResId +
                   ", gravity=" + gravity +
                   ", imageDrawable=" + imageDrawable +
                   ", imageResId=" + imageResId +
                   ", imageScaleType=" + imageScaleType +
                   ", textSize=" + textSize +
                   ", textShadowColorResId=" + textShadowColorResId +
                   ", textShadowRadius=" + textShadowRadius +
                   ", textShadowDy=" + textShadowDy +
                   ", textShadowDx=" + textShadowDx +
                   ", textAppearanceResId=" + textAppearanceResId +
                   ", paddingInPixels=" + paddingInPixels +
                   ", paddingDimensionResId=" + paddingDimensionResId +
                   ", fontName=" + fontName +
                   ", fontNameResId=" + fontNameResId +
                   '}';
        }
    }
}