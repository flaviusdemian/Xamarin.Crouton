using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
//using Android.Views;
using Android.Views;
using Android.Widget;

namespace CroutonLibrary
{

    /** Builder for the {@link Style} object. */
    public class StyleBuilder
    {
        public Configuration configuration;
        public int backgroundColorValue;
        public int backgroundColorResourceId;
        public int backgroundDrawableResourceId;
        public bool isTileEnabled;
        public int textColorResourceId;
        public int textColorValue;
        public int heightInPixels;
        public int heightDimensionResId;
        public int widthInPixels;
        public int widthDimensionResId;
        public int gravity;
        public Drawable imageDrawable;
        public int textSize;
        public int textShadowColorResId;
        public float textShadowRadius;
        public float textShadowDx;
        public float textShadowDy;
        public int textAppearanceResId;
        public int imageResId;
        public ImageView.ScaleType imageScaleType;
        public int paddingInPixels;
        public int paddingDimensionResId;
        public String fontName;
        public int fontNameResId; 
        /** Creates a {@link Builder} to build a {@link Style} upon. */
        public StyleBuilder()
        {
            configuration = Configuration.DEFAULT;
            paddingInPixels = 10;
            backgroundColorResourceId = Resource.Color.holo_blue_light;
            backgroundDrawableResourceId = 0;
            backgroundColorValue = Style.NOT_SET;
            isTileEnabled = false;
            textColorResourceId = Resource.Color.white;
            textColorValue = Style.NOT_SET;
            heightInPixels = ViewGroup.LayoutParams.WrapContent;
            widthInPixels = ViewGroup.LayoutParams.MatchParent;
            gravity = (int)GravityFlags.Center;
            imageDrawable = null;
            imageResId = 0;
            imageScaleType = ImageView.ScaleType.FitXy;
            fontName = null;
            fontNameResId = 0;
        }

        /**
         * Creates a {@link Builder} to build a {@link Style} upon.
         *
         * @param baseStyle
         *   The base {@link Style} to use for this {@link Style}.
         */
        public StyleBuilder(Style baseStyle)
        {
            configuration = baseStyle.configuration;
            backgroundColorValue = baseStyle.backgroundColorValue;
            backgroundColorResourceId = baseStyle.backgroundColorResourceId;
            backgroundDrawableResourceId = baseStyle.backgroundDrawableResourceId;
            isTileEnabled = baseStyle.isTileEnabled;
            textColorResourceId = baseStyle.textColorResourceId;
            textColorValue = baseStyle.textColorValue;
            heightInPixels = baseStyle.heightInPixels;
            heightDimensionResId = baseStyle.heightDimensionResId;
            widthInPixels = baseStyle.widthInPixels;
            widthDimensionResId = baseStyle.widthDimensionResId;
            gravity = baseStyle.gravity;
            imageDrawable = baseStyle.imageDrawable;
            textSize = baseStyle.textSize;
            textShadowColorResId = baseStyle.textShadowColorResId;
            textShadowRadius = baseStyle.textShadowRadius;
            textShadowDx = baseStyle.textShadowDx;
            textShadowDy = baseStyle.textShadowDy;
            textAppearanceResId = baseStyle.textAppearanceResId;
            imageResId = baseStyle.imageResId;
            imageScaleType = baseStyle.imageScaleType;
            paddingInPixels = baseStyle.paddingInPixels;
            paddingDimensionResId = baseStyle.paddingDimensionResId;
            fontName = baseStyle.fontName;
            fontNameResId = baseStyle.fontNameResId;
        }
        /**
         * Set the {@link Configuration} option of the {@link Crouton}.
         *
         * @param configuration
         *   The {@link Configuration}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setConfiguration(Configuration configuration)
        {
            this.configuration = configuration;
            return this;
        }

        /**
         * Set the backgroundColorResourceId option of the {@link Crouton}.
         *
         * @param backgroundColorResourceId
         *   The backgroundColorResourceId's resource id.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setBackgroundColor(int backgroundColorResourceId)
        {
            this.backgroundColorResourceId = backgroundColorResourceId;
            return this;
        }

        /**
         * Set the backgroundColorResourceValue option of the {@link Crouton}.
         *
         * @param backgroundColorValue
         *   The backgroundColorResourceValue's e.g. 0xffff4444;
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setBackgroundColorValue(int backgroundColorValue)
        {
            this.backgroundColorValue = backgroundColorValue;
            return this;
        }

        /**
         * Set the backgroundDrawableResourceId option for the {@link Crouton}.
         *
         * @param backgroundDrawableResourceId
         *   Resource ID of a backgroundDrawableResourceId image drawable.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setBackgroundDrawable(int backgroundDrawableResourceId)
        {
            this.backgroundDrawableResourceId = backgroundDrawableResourceId;
            return this;
        }

        /**
         * Set the heightInPixels option for the {@link Crouton}.
         *
         * @param height
         *   The height of the {@link Crouton} in pixel. Can also be
         *   {@link LayoutParams#MATCH_PARENT} or
         *   {@link LayoutParams#WRAP_CONTENT}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setHeight(int height)
        {
            this.heightInPixels = height;
            return this;
        }

        /**
         * Set the resource id for the height option for the {@link Crouton}.
         *
         * @param heightDimensionResId
         *   Resource ID of a dimension for the height of the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setHeightDimensionResId(int heightDimensionResId)
        {
            this.heightDimensionResId = heightDimensionResId;
            return this;
        }

        /**
         * Set the widthInPixels option for the {@link Crouton}.
         *
         * @param width
         *   The width of the {@link Crouton} in pixel. Can also be
         *   {@link LayoutParams#MATCH_PARENT} or
         *   {@link LayoutParams#WRAP_CONTENT}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setWidth(int width)
        {
            this.widthInPixels = width;
            return this;
        }

        /**
         * Set the resource id for the width option for the {@link Crouton}.
         *
         * @param widthDimensionResId
         *   Resource ID of a dimension for the width of the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setWidthDimensionResId(int widthDimensionResId)
        {
            this.widthDimensionResId = widthDimensionResId;
            return this;
        }

        /**
         * Set the isTileEnabled option for the {@link Crouton}.
         *
         * @param isTileEnabled
         *   <code>true</code> if you want the backgroundResourceId to be
         *   tiled, else <code>false</code>.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setTileEnabled(bool isTileEnabled)
        {
            this.isTileEnabled = isTileEnabled;
            return this;
        }

        /**
         * Set the textColorResourceId option for the {@link Crouton}.
         *
         * @param textColor
         *   The resource id of the text colorResourceId.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setTextColor(int textColor)
        {
            this.textColorResourceId = textColor;
            return this;
        }

        /**
         * Set the textColorResourceValue option of the {@link Crouton}.
         *
         * @param textColorValue
         *   The textColorResourceValue's e.g. 0xffff4444;
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setTextColorValue(int textColorValue)
        {
            this.textColorValue = textColorValue;
            return this;
        }

        /**
         * Set the gravity option for the {@link Crouton}.
         *
         * @param gravity
         *   The text's gravity as provided by {@link Gravity}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setGravity(int gravity)
        {
            this.gravity = gravity;
            return this;
        }

        /**
         * Set the image option for the {@link Crouton}.
         *
         * @param imageDrawable
         *   An additional image to display in the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setImageDrawable(Drawable imageDrawable)
        {
            this.imageDrawable = imageDrawable;
            return this;
        }

        /**
         * Set the image resource option for the {@link Crouton}.
         *
         * @param imageResId
         *   An additional image to display in the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */
        public StyleBuilder setImageResource(int imageResId)
        {
            this.imageResId = imageResId;
            return this;
        }

        /** The text size in sp. */
        public StyleBuilder setTextSize(int textSize)
        {
            this.textSize = textSize;
            return this;
        }

        /** The text shadow color resource id. */
        public StyleBuilder setTextShadowColor(int textShadowColorResId)
        {
            this.textShadowColorResId = textShadowColorResId;
            return this;
        }

        /** The text shadow radius. */
        public StyleBuilder setTextShadowRadius(float textShadowRadius)
        {
            this.textShadowRadius = textShadowRadius;
            return this;
        }

        /** The text shadow horizontal offset. */
        public StyleBuilder setTextShadowDx(float textShadowDx)
        {
            this.textShadowDx = textShadowDx;
            return this;
        }

        /** The text shadow vertical offset. */
        public StyleBuilder setTextShadowDy(float textShadowDy)
        {
            this.textShadowDy = textShadowDy;
            return this;
        }

        /** The text appearance resource id for the text. */
        public StyleBuilder setTextAppearance(int textAppearanceResId)
        {
            this.textAppearanceResId = textAppearanceResId;
            return this;
        }

        /** The {@link android.widget.ImageView.ScaleType} for the image. */
        public StyleBuilder setImageScaleType(ImageView.ScaleType imageScaleType)
        {
            this.imageScaleType = imageScaleType;
            return this;
        }

        /** The padding for the crouton view's content in pixels. */
        public StyleBuilder setPaddingInPixels(int padding)
        {
            this.paddingInPixels = padding;
            return this;
        }

        /** The resource id for the padding for the crouton view's content. */
        public StyleBuilder setPaddingDimensionResId(int paddingResId)
        {
            this.paddingDimensionResId = paddingResId;
            return this;
        }

        /** The file path and name of the font for the crouton view's content. */
        public StyleBuilder setFontName(String fontName)
        {
            this.fontName = fontName;
            return this;
        }

        /** The resource id for the file path and name of the font for the crouton view's content. */
        public StyleBuilder setFontNameResId(int fontNameResId)
        {
            this.fontNameResId = fontNameResId;
            return this;
        }

        /** @return a configured {@link Style} object. */
        public Style build()
        {
            return new Style(this);
        }
    }
}