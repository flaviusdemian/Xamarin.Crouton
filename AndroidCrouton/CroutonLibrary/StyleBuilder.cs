using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace CroutonLibrary
{
    /** Builder for the {@link Style} object. */

    public class StyleBuilder
    {
        public Color BackgroundColor;
        public int BackgroundDrawableResourceId;
        public Configuration Configuration;
        public String FontName;
        public int FontNameResId;
        public int Gravity;
        public int HeightDimensionResId;
        public int HeightInPixels;
        public Drawable ImageDrawable;
        public int ImageResId;
        public ImageView.ScaleType ImageScaleType;
        public bool IsTileEnabled;
        public int PaddingDimensionResId;
        public int PaddingInPixels;
        public int TextAppearanceResId;
        public int TextColorResourceId;
        public Color TextColor;
        public int TextShadowColorResId;
        public float TextShadowDx;
        public float TextShadowDy;
        public float TextShadowRadius;
        public int TextSize;
        public int WidthDimensionResId;
        public int WidthInPixels;
        /** Creates a {@link Builder} to build a {@link Style} upon. */

        public StyleBuilder()
        {
            Configuration = Configuration.DEFAULT;
            PaddingInPixels = 10;
            BackgroundColor = Color.Blue;
            BackgroundDrawableResourceId = 0;
            IsTileEnabled = false;
            TextColorResourceId = Resource.Color.white;
            TextColor= Color.Black;
            HeightInPixels = ViewGroup.LayoutParams.WrapContent;
            WidthInPixels = ViewGroup.LayoutParams.MatchParent;
            Gravity = (int) GravityFlags.Center;
            ImageDrawable = null;
            ImageResId = 0;
            ImageScaleType = ImageView.ScaleType.FitXy;
            FontName = null;
            FontNameResId = 0;
        }

        /**
         * Creates a {@link Builder} to build a {@link Style} upon.
         *
         * @param baseStyle
         *   The base {@link Style} to use for this {@link Style}.
         */

        public StyleBuilder(Style baseStyle)
        {
            Configuration = baseStyle.Configuration;
            BackgroundColor = baseStyle.BackgroundColor;
            BackgroundDrawableResourceId = baseStyle.BackgroundDrawableResourceId;
            IsTileEnabled = baseStyle.IsTileEnabled;
            TextColorResourceId = baseStyle.TextColorResourceId;
            TextColor = baseStyle.TextColor;
            HeightInPixels = baseStyle.HeightInPixels;
            HeightDimensionResId = baseStyle.HeightDimensionResId;
            WidthInPixels = baseStyle.WidthInPixels;
            WidthDimensionResId = baseStyle.WidthDimensionResId;
            Gravity = baseStyle.Gravity;
            ImageDrawable = baseStyle.ImageDrawable;
            TextSize = baseStyle.TextSize;
            TextShadowColorResId = baseStyle.TextShadowColorResId;
            TextShadowRadius = baseStyle.TextShadowRadius;
            TextShadowDx = baseStyle.TextShadowDx;
            TextShadowDy = baseStyle.TextShadowDy;
            TextAppearanceResId = baseStyle.TextAppearanceResId;
            ImageResId = baseStyle.ImageResId;
            ImageScaleType = baseStyle.ImageScaleType;
            PaddingInPixels = baseStyle.PaddingInPixels;
            PaddingDimensionResId = baseStyle.PaddingDimensionResId;
            FontName = baseStyle.FontName;
            FontNameResId = baseStyle.FontNameResId;
        }

        /**
         * Set the {@link Configuration} option of the {@link Crouton}.
         *
         * @param Configuration
         *   The {@link Configuration}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetConfiguration(Configuration configuration)
        {
            Configuration = configuration;
            return this;
        }

        /**
         * Set the BackgroundColorResourceId option of the {@link Crouton}.
         *
         * @param BackgroundColorResourceId
         *   The BackgroundColorResourceId's resource id.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetBackgroundColor(Color backgroundColorResourceId)
        {
            BackgroundColor = backgroundColorResourceId;
            return this;
        }

        /**
         * Set the BackgroundDrawableResourceId option for the {@link Crouton}.
         *
         * @param BackgroundDrawableResourceId
         *   Resource ID of a BackgroundDrawableResourceId image drawable.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetBackgroundDrawable(int backgroundDrawableResourceId)
        {
            BackgroundDrawableResourceId = backgroundDrawableResourceId;
            return this;
        }

        /**
         * Set the HeightInPixels option for the {@link Crouton}.
         *
         * @param height
         *   The height of the {@link Crouton} in pixel. Can also be
         *   {@link LayoutParams#MATCH_PARENT} or
         *   {@link LayoutParams#WRAP_CONTENT}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetHeight(int height)
        {
            HeightInPixels = height;
            return this;
        }

        /**
         * Set the resource id for the height option for the {@link Crouton}.
         *
         * @param HeightDimensionResId
         *   Resource ID of a dimension for the height of the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetHeightDimensionResId(int heightDimensionResId)
        {
            HeightDimensionResId = heightDimensionResId;
            return this;
        }

        /**
         * Set the WidthInPixels option for the {@link Crouton}.
         *
         * @param width
         *   The width of the {@link Crouton} in pixel. Can also be
         *   {@link LayoutParams#MATCH_PARENT} or
         *   {@link LayoutParams#WRAP_CONTENT}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetWidth(int width)
        {
            WidthInPixels = width;
            return this;
        }

        /**
         * Set the resource id for the width option for the {@link Crouton}.
         *
         * @param WidthDimensionResId
         *   Resource ID of a dimension for the width of the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetWidthDimensionResId(int widthDimensionResId)
        {
            WidthDimensionResId = widthDimensionResId;
            return this;
        }

        /**
         * Set the IsTileEnabled option for the {@link Crouton}.
         *
         * @param IsTileEnabled
         *   <code>true</code> if you want the backgroundResourceId to be
         *   tiled, else <code>false</code>.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetTileEnabled(bool isTileEnabled)
        {
            IsTileEnabled = isTileEnabled;
            return this;
        }

        /**
         * Set the TextColorResourceId option for the {@link Crouton}.
         *
         * @param textColor
         *   The resource id of the text colorResourceId.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetTextColor(int textColor)
        {
            TextColorResourceId = textColor;
            return this;
        }

        /**
         * Set the textColorResourceValue option of the {@link Crouton}.
         *
         * @param TextColorValue
         *   The textColorResourceValue's e.g. 0xffff4444;
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetTextColor(Color textColorValue)
        {
            TextColor = textColorValue;
            return this;
        }

        /**
         * Set the Gravity option for the {@link Crouton}.
         *
         * @param Gravity
         *   The text's Gravity as provided by {@link Gravity}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetGravity(int gravity)
        {
            Gravity = gravity;
            return this;
        }

        /**
         * Set the image option for the {@link Crouton}.
         *
         * @param ImageDrawable
         *   An additional image to display in the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetImageDrawable(Drawable imageDrawable)
        {
            ImageDrawable = imageDrawable;
            return this;
        }

        /**
         * Set the image resource option for the {@link Crouton}.
         *
         * @param ImageResId
         *   An additional image to display in the {@link Crouton}.
         *
         * @return the {@link Builder}.
         */

        public StyleBuilder SetImageResource(int imageResId)
        {
            ImageResId = imageResId;
            return this;
        }

        /** The text size in sp. */

        public StyleBuilder SetTextSize(int textSize)
        {
            TextSize = textSize;
            return this;
        }

        /** The text shadow color resource id. */

        public StyleBuilder SetTextShadowColor(int textShadowColorResId)
        {
            TextShadowColorResId = textShadowColorResId;
            return this;
        }

        /** The text shadow radius. */

        public StyleBuilder SetTextShadowRadius(float textShadowRadius)
        {
            TextShadowRadius = textShadowRadius;
            return this;
        }

        /** The text shadow horizontal offset. */

        public StyleBuilder SetTextShadowDx(float textShadowDx)
        {
            TextShadowDx = textShadowDx;
            return this;
        }

        /** The text shadow vertical offset. */

        public StyleBuilder SetTextShadowDy(float textShadowDy)
        {
            TextShadowDy = textShadowDy;
            return this;
        }

        /** The text appearance resource id for the text. */

        public StyleBuilder SetTextAppearance(int textAppearanceResId)
        {
            TextAppearanceResId = textAppearanceResId;
            return this;
        }

        /** The {@link android.widget.ImageView.ScaleType} for the image. */

        public StyleBuilder SetImageScaleType(ImageView.ScaleType imageScaleType)
        {
            ImageScaleType = imageScaleType;
            return this;
        }

        /** The padding for the crouton view's content in pixels. */

        public StyleBuilder SetPaddingInPixels(int padding)
        {
            PaddingInPixels = padding;
            return this;
        }

        /** The resource id for the padding for the crouton view's content. */

        public StyleBuilder SetPaddingDimensionResId(int paddingResId)
        {
            PaddingDimensionResId = paddingResId;
            return this;
        }

        /** The file path and name of the font for the crouton view's content. */

        public StyleBuilder SetFontName(String fontName)
        {
            FontName = fontName;
            return this;
        }

        /** The resource id for the file path and name of the font for the crouton view's content. */

        public StyleBuilder SetFontNameResId(int fontNameResId)
        {
            FontNameResId = fontNameResId;
            return this;
        }

        /** @return a configured {@link Style} object. */

        public Style Build()
        {
            return new Style(this);
        }
    }
}