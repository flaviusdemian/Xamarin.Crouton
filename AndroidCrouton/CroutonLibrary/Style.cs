using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Views;
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

        static Style()
        {
            ALERT = new StyleBuilder()
              .setBackgroundColorValue((int)holoRedLight)
              .build();
            CONFIRM = new StyleBuilder()
              .setBackgroundColorValue((int)holoGreenLight)
              .build();
            INFO = new StyleBuilder()
              .setBackgroundColorValue((int)holoBlueLight)
              .build();
        }

        /**
         * The {@link Configuration} for this {@link Style}.
         * It can be overridden via {@link Crouton#setConfiguration(Configuration)}.
         */
        public Configuration configuration;

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
        public int backgroundDrawableResourceId;

        /**
         * The backgroundColorResourceValue's e.g. 0xffff4444;
         * <p/>
         * NOT_SET for no value.
         */
        public int backgroundColorValue;

        /** Whether we should isTileEnabled the backgroundResourceId or not. */
        public  bool isTileEnabled;

        /**
         * The text colorResourceId's resource id.
         * <p/>
         * 0 sets the text colorResourceId to the system theme default.
         */
        public int textColorResourceId;

        /**
         * The textColorResourceValue's e.g. 0xffff4444;
         * <p/>
         * NOT_SET for no value.
         */
        public int textColorValue;

        /** The height of the {@link Crouton} in pixels. */
        public int heightInPixels;

        /** Resource ID for the height of the {@link Crouton}. */
        public int heightDimensionResId;

        /** The width of the {@link Crouton} in pixels. */
        public int widthInPixels;

        /** Resource ID for the width of the {@link Crouton}. */
        public int widthDimensionResId;

        /** The text's gravity as provided by {@link Gravity}. */
        public int gravity;

        /** An additional image to display in the {@link Crouton}. */
        public Drawable imageDrawable;

        /** An additional image to display in the {@link Crouton}. */
        public int imageResId;

        /** The {@link ImageView.ScaleType} for the image to display in the {@link Crouton}. */
        public ImageView.ScaleType imageScaleType;

        /**
         * The text size in sp
         * <p/>
         * 0 sets the text size to the system theme default
         */
        public int textSize;

        /** The text shadow color's resource id */
        public int textShadowColorResId;

        /** The text shadow radius */
        public float textShadowRadius;

        /** The text shadow vertical offset */
        public float textShadowDy;

        /** The text shadow horizontal offset */
        public float textShadowDx;

        /** The text appearance resource id for the text. */
        public int textAppearanceResId;

        /** The padding for the crouton view content in pixels */
        public int paddingInPixels;

        /** The resource id for the padding for the view content */
        public int paddingDimensionResId;

        /** The file path and font name for the view content */
        public String fontName;

        /** The file path and font name resource id for the view content */
        public int fontNameResId;

        public Style(StyleBuilder builder)
        {
            this.configuration = builder.configuration;
            this.backgroundColorResourceId = builder.backgroundColorResourceId;
            this.backgroundDrawableResourceId = builder.backgroundDrawableResourceId;
            this.isTileEnabled = builder.isTileEnabled;
            this.textColorResourceId = builder.textColorResourceId;
            this.textColorValue = builder.textColorValue;
            this.heightInPixels = builder.heightInPixels;
            this.heightDimensionResId = builder.heightDimensionResId;
            this.widthInPixels = builder.widthInPixels;
            this.widthDimensionResId = builder.widthDimensionResId;
            this.gravity = builder.gravity;
            this.imageDrawable = builder.imageDrawable;
            this.textSize = builder.textSize;
            this.textShadowColorResId = builder.textShadowColorResId;
            this.textShadowRadius = builder.textShadowRadius;
            this.textShadowDx = builder.textShadowDx;
            this.textShadowDy = builder.textShadowDy;
            this.textAppearanceResId = builder.textAppearanceResId;
            this.imageResId = builder.imageResId;
            this.imageScaleType = builder.imageScaleType;
            this.paddingInPixels = builder.paddingInPixels;
            this.paddingDimensionResId = builder.paddingDimensionResId;
            this.backgroundColorValue = builder.backgroundColorValue;
            this.fontName = builder.fontName;
            this.fontNameResId = builder.fontNameResId;
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