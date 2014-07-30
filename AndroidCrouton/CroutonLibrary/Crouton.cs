using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Lang;
using String = System.String;

namespace CroutonLibrary
{
    public class Crouton : Object
    {
        private static String NULL_PARAMETERS_ARE_NOT_ACCEPTED = "Null parameters are not accepted";
        private static int IMAGE_ID = 0x100;
        private static int TEXT_ID = 0x101;
        private String text;
        private Style style;
        private Configuration configuration = null;
        private View customView;

        private View.IOnClickListener onClickListener;

        private Activity activity;
        private ViewGroup viewGroup;
        private FrameLayout croutonView;
        private Animation inAnimation;
        private Animation outAnimation;
        private LifecycleCallback lifecycleCallback = null;

        /**
         * Creates the {@link Crouton}.
         *
         * @param activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         */
        private Crouton(Activity activity, String text, Style style)
        {
            if ((activity == null) || (text == null) || (style == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            this.activity = activity;
            this.viewGroup = null;
            this.text = text;
            this.style = style;
            this.customView = null;
        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        private Crouton(Activity activity, String text, Style style, ViewGroup viewGroup)
        {
            if ((activity == null) || (text == null) || (style == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            this.activity = activity;
            this.text = text;
            this.style = style;
            this.viewGroup = viewGroup;
            this.customView = null;
        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param customView
         *     The custom {@link View} to display
         */
        private Crouton(Activity activity, View customView)
        {
            if ((activity == null) || (customView == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            this.activity = activity;
            this.viewGroup = null;
            this.customView = customView;
            this.style = new StyleBuilder().build();
            this.text = null;
        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param customView
         *     The custom {@link View} to display
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        private Crouton(Activity activity, View customView, ViewGroup viewGroup)
            : this(activity, customView, viewGroup, Configuration.DEFAULT)
        {

        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param customView
         *     The custom {@link View} to display
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         * @param configuration
         *     The {@link Configuration} for this {@link Crouton}.
         */
        private Crouton(Activity activity, View customView, ViewGroup viewGroup, Configuration configuration)
        {
            if ((activity == null) || (customView == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            this.activity = activity;
            this.customView = customView;
            this.viewGroup = viewGroup;
            this.style = new StyleBuilder().build();
            this.text = null;
            this.configuration = configuration;
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton makeText(Activity activity, String text, Style style)
        {
            return new Crouton(activity, text, style);
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton makeText(Activity activity, String text, Style style, ViewGroup viewGroup)
        {
            return new Crouton(activity, text, style, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton makeText(Activity activity, String text, Style style, int viewGroupResId)
        {
            return new Crouton(activity, text, style, activity.FindViewById<ViewGroup>(viewGroupResId));
        }


        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param textResourceId
         *     The resource id of the text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton makeText(Activity activity, int textResourceId, Style style)
        {
            return makeText(activity, activity.GetString(textResourceId), style);
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton makeText(Activity activity, int textResourceId, Style style, ViewGroup viewGroup)
        {
            return makeText(activity, activity.GetString(textResourceId), style, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton makeText(Activity activity, int textResourceId, Style style, int viewGroupResId)
        {
            return makeText(activity, activity.GetString(textResourceId), style, activity.FindViewById<ViewGroup>(viewGroupResId));
        }


        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param customView
         *     The custom {@link View} to display
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton make(Activity activity, View customView)
        {
            return new Crouton(activity, customView);
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param customView
         *     The custom {@link View} to display
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton make(Activity activity, View customView, ViewGroup viewGroup)
        {
            return new Crouton(activity, customView, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param customView
         *     The custom {@link View} to display
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton make(Activity activity, View customView, int viewGroupResId)
        {
            return new Crouton(activity, customView, activity.FindViewById<ViewGroup>(viewGroupResId));
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param customView
         *     The custom {@link View} to display
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         * @param configuration
         *     The configuration for this crouton.
         *
         * @return The created {@link Crouton}.
         */
        public static Crouton make(Activity activity, View customView, int viewGroupResId, Configuration configuration)
        {
            return new Crouton(activity, customView, activity.FindViewById<ViewGroup>(viewGroupResId), configuration);
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given activity
         * and displays it directly.
         *
         * @param activity
         *     The {@link android.app.Activity} that the {@link Crouton} should
         *     be attached to.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         */
        public static void showText(Activity activity, String text, Style style)
        {
            makeText(activity, text, style).show();
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given activity
         * and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        public static void showText(Activity activity, String text, Style style, ViewGroup viewGroup)
        {
            makeText(activity, text, style, viewGroup).show();
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given activity
         * and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        public static void showText(Activity activity, String text, Style style, int viewGroupResId)
        {
            makeText(activity, text, style, activity.FindViewById<ViewGroup>(viewGroupResId)).show();
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given activity
         * and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param text
         *     The text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         * @param configuration
         *     The configuration for this Crouton.
         */
        public static void showText(Activity activity, String text, Style style, int viewGroupResId, Configuration configuration)
        {
            makeText(activity, text, style, activity.FindViewById<ViewGroup>(viewGroupResId)).setConfiguration(configuration).show();
        }


        /**
         * Creates a {@link Crouton} with provided text and style for a given activity
         * and displays it directly.
         *
         * @param activity
         *     The {@link android.app.Activity} that the {@link Crouton} should
         *     be attached to.
         * @param customView
         *     The custom {@link View} to display
         */
        public static void show(Activity activity, View customView)
        {
            make(activity, customView).show();
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given activity
         * and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param customView
         *     The custom {@link View} to display
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        public static void show(Activity activity, View customView, ViewGroup viewGroup)
        {
            make(activity, customView, viewGroup).show();
        }

        /**
         * Creates a {@link Crouton} with provided text and style for a given activity
         * and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param customView
         *     The custom {@link View} to display
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        public static void show(Activity activity, View customView, int viewGroupResId)
        {
            make(activity, customView, viewGroupResId).show();
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param textResourceId
         *     The resource id of the text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         */
        public static void showText(Activity activity, int textResourceId, Style style)
        {
            showText(activity, activity.GetString(textResourceId), style);
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        public static void showText(Activity activity, int textResourceId, Style style, ViewGroup viewGroup)
        {
            showText(activity, activity.GetString(textResourceId), style, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided text-resource and style for a given
         * activity and displays it directly.
         *
         * @param activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the text you want to display.
         * @param style
         *     The style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         */
        public static void showText(Activity activity, int textResourceId, Style style, int viewGroupResId)
        {
            showText(activity, activity.GetString(textResourceId), style, viewGroupResId);
        }

        /**
         * Allows hiding of a previously displayed {@link Crouton}.
         *
         * @param crouton
         *     The {@link Crouton} you want to hide.
         */
        public static void hide(Crouton crouton)
        {
            crouton.hide();
        }

        /**
         * Cancels all queued {@link Crouton}s. If there is a {@link Crouton}
         * displayed currently, it will be the last one displayed.
         */
        public static void cancelAllCroutons()
        {
            Manager.getInstance().clearCroutonQueue();
        }

        /**
         * Clears (and removes from {@link Activity}'s content view, if necessary) all
         * croutons for the provided activity
         *
         * @param activity
         *     - The {@link Activity} to clear the croutons for.
         */
        public static void clearCroutonsForActivity(Activity activity)
        {
            Manager.getInstance().clearCroutonsForActivity(activity);
        }

        /**
         * Cancels a {@link Crouton} immediately.
         */
        public void cancel()
        {
            Manager manager = Manager.getInstance();
            manager.removeCroutonImmediately(this);
        }

        /**
         * Displays the {@link Crouton}. If there's another {@link Crouton} visible at
         * the time, this {@link Crouton} will be displayed afterwards.
         */
        public void show()
        {
            Manager.getInstance().add(this);
        }

        public Animation getInAnimation()
        {
            if ((null == this.inAnimation) && (null != this.activity))
            {
                if (getConfiguration().inAnimationResId > 0)
                {
                    this.inAnimation = AnimationUtils.LoadAnimation(getActivity(), getConfiguration().inAnimationResId);
                }
                else
                {
                    measureCroutonView();
                    this.inAnimation = DefaultAnimationsBuilder.buildDefaultSlideInDownAnimation(getView());
                }
            }

            return inAnimation;
        }

        public Animation getOutAnimation()
        {
            if ((null == this.outAnimation) && (null != this.activity))
            {
                if (getConfiguration().outAnimationResId > 0)
                {
                    this.outAnimation = AnimationUtils.LoadAnimation(getActivity(), getConfiguration().outAnimationResId);
                }
                else
                {
                    this.outAnimation = DefaultAnimationsBuilder.buildDefaultSlideOutUpAnimation(getView());
                }
            }

            return outAnimation;
        }

        /**
         * @param lifecycleCallback
         *     Callback object for notable events in the life of a Crouton.
         */
        public void setLifecycleCallback(LifecycleCallback lifecycleCallback)
        {
            this.lifecycleCallback = lifecycleCallback;
        }

        /**
         * Removes this {@link Crouton}.
         *
         * @since 1.9
         */
        public void hide()
        {
            Manager.getInstance().removeCrouton(this);
        }

        /**
         * Allows setting of an {@link OnClickListener} directly to a {@link Crouton} without having to use a custom view.
         *
         * @param onClickListener
         *     The {@link OnClickListener} to set.
         *
         * @return this {@link Crouton}.
         */
        public Crouton setOnClickListener(View.IOnClickListener onClickListener)
        {
            this.onClickListener = onClickListener;
            return this;
        }

        /**
         * Set the {@link Configuration} on this {@link Crouton}, prior to showing it.
         *
         * @param configuration
         *     a {@link Configuration} built using the {@link Configuration.Builder}.
         *
         * @return this {@link Crouton}.
         */
        public Crouton setConfiguration(Configuration configuration)
        {
            this.configuration = configuration;
            return this;
        }
        public override string ToString()
        {

            return "Crouton{" +
                "text=" + text +
                ", style=" + style +
                ", configuration=" + configuration +
                ", customView=" + customView +
                ", onClickListener=" + onClickListener +
                ", activity=" + activity +
                ", viewGroup=" + viewGroup +
                ", croutonView=" + croutonView +
                ", inAnimation=" + inAnimation +
                ", outAnimation=" + outAnimation +
                ", lifecycleCallback=" + lifecycleCallback +
                '}';
        }

        /**
         * Convenience method to get the license text for embedding within your application.
         *
         * @return The license text.
         */
        public static String getLicenseText()
        {
            return "This application uses the Crouton library.\n\n" +
                "Copyright 2012 - 2013 Benjamin Weiss \n" +
                "\n" +
                "Licensed under the Apache License, Version 2.0 (the \"License\");\n" +
                "you may not use this file except in compliance with the License.\n" +
                "You may obtain a copy of the License at\n" +
                "\n" +
                "   http://www.apache.org/licenses/LICENSE-2.0\n" +
                "\n" +
                "Unless required by applicable law or agreed to in writing, software\n" +
                "distributed under the License is distributed on an \"AS IS\" BASIS,\n" +
                "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.\n" +
                "See the License for the specific language governing permissions and\n" +
                "limitations under the License.";
        }

        //////////////////////////////////////////////////////////////////////////////////////
        // You have reached the internal API of Crouton.
        // If you do not plan to develop for Crouton there is nothing of interest below here.
        //////////////////////////////////////////////////////////////////////////////////////

        /**
         * @return <code>true</code> if the {@link Crouton} is being displayed, else
         * <code>false</code>.
         */
        public bool isShowing()
        {
            return (null != activity) && (isCroutonViewNotNull() || isCustomViewNotNull());
        }

        private bool isCroutonViewNotNull()
        {
            return (null != croutonView) && (null != croutonView.Parent);
        }

        private bool isCustomViewNotNull()
        {
            return (null != customView) && (null != customView.Parent);
        }

        /**
         * Removes the activity reference this {@link Crouton} is holding
         */
        public void detachActivity()
        {
            activity = null;
        }

        /**
         * Removes the viewGroup reference this {@link Crouton} is holding
         */
        public void detachViewGroup()
        {
            viewGroup = null;
        }

        /**
         * Removes the lifecycleCallback reference this {@link Crouton} is holding
         */
        public void detachLifecycleCallback()
        {
            lifecycleCallback = null;
        }

        /**
         * @return the lifecycleCallback
         */
        public LifecycleCallback getLifecycleCallback()
        {
            return lifecycleCallback;
        }

        /**
         * @return the style
         */
        Style getStyle()
        {
            return style;
        }

        /**
         * @return this croutons configuration
         */
        public Configuration getConfiguration()
        {
            if (null == configuration)
            {
                configuration = getStyle().configuration;
            }
            return configuration;
        }

        /**
         * @return the activity
         */
        public Activity getActivity()
        {
            return activity;
        }

        /**
         * @return the viewGroup
         */
        public ViewGroup getViewGroup()
        {
            return viewGroup;
        }

        /**
         * @return the text
         */
        public String getText()
        {
            return text;
        }

        /**
         * @return the view
         */
        public View getView()
        {
            // return the custom view if one exists
            if (null != this.customView)
            {
                return this.customView;
            }

            // if already setup return the view
            if (null == this.croutonView)
            {
                initializeCroutonView();
            }

            return croutonView;
        }

        private void measureCroutonView()
        {
            View view = getView();
            int widthSpec;
            if (null != viewGroup)
            {
                widthSpec = View.MeasureSpec.MakeMeasureSpec(viewGroup.MeasuredWidth, MeasureSpecMode.AtMost);
            }
            else
            {
                widthSpec = View.MeasureSpec.MakeMeasureSpec(activity.Window.DecorView.MeasuredWidth, MeasureSpecMode.AtMost);
            }

            view.Measure(widthSpec, View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
        }

        private void initializeCroutonView()
        {
            Resources resources = this.activity.Resources;

            this.croutonView = initializeCroutonViewGroup(resources);

            // create content view
            RelativeLayout contentView = initializeContentView(resources);
            this.croutonView.AddView(contentView);
        }

        private FrameLayout initializeCroutonViewGroup(Resources resources)
        {
            FrameLayout croutonView = new FrameLayout(this.activity);

            if (null != onClickListener)
            {
                croutonView.SetOnClickListener(onClickListener);
            }

            int height;
            if (this.style.heightDimensionResId > 0)
            {
                height = resources.GetDimensionPixelSize(this.style.heightDimensionResId);
            }
            else
            {
                height = this.style.heightInPixels;
            }

            int width;
            if (this.style.widthDimensionResId > 0)
            {
                width = resources.GetDimensionPixelSize(this.style.widthDimensionResId);
            }
            else
            {
                width = this.style.widthInPixels;
            }

            croutonView.LayoutParameters = new FrameLayout.LayoutParams(width != 0 ? width : FrameLayout.LayoutParams.MatchParent, height);

            // set background
            if (this.style.backgroundColorValue != Style.NOT_SET)
            {
                croutonView.SetBackgroundColor(resources.GetColor(this.style.backgroundColorValue));
            }
            else
            {
                croutonView.SetBackgroundColor(resources.GetColor(this.style.backgroundColorResourceId));
            }

            // set the background drawable if set. This will override the background
            // color.
            if (this.style.backgroundDrawableResourceId != 0)
            {
                Bitmap background = BitmapFactory.DecodeResource(resources, this.style.backgroundDrawableResourceId);
                BitmapDrawable drawable = new BitmapDrawable(resources, background);
                if (this.style.isTileEnabled)
                {
                    drawable.SetTileModeXY(Shader.TileMode.Repeat, Shader.TileMode.Repeat);
                }
                croutonView.SetBackgroundDrawable(drawable);
            }
            return croutonView;
        }

        private RelativeLayout initializeContentView(Resources resources)
        {
            RelativeLayout contentView = new RelativeLayout(this.activity);
            contentView.LayoutParameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);

            // set padding
            int padding = this.style.paddingInPixels;

            // if a padding dimension has been set, this will overwrite any padding
            // in pixels
            if (this.style.paddingDimensionResId > 0)
            {
                padding = resources.GetDimensionPixelSize(this.style.paddingDimensionResId);
            }
            contentView.SetPadding(padding, padding, padding, padding);

            // only setup image if one is requested
            ImageView image = null;
            if ((null != this.style.imageDrawable) || (0 != this.style.imageResId))
            {
                image = initializeImageView();
                contentView.AddView(image, image.LayoutParameters);
            }

            TextView text = initializeTextView(resources);

            RelativeLayout.LayoutParams textParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent,
                RelativeLayout.LayoutParams.WrapContent);
            if (null != image)
            {
                textParams.AddRule(LayoutRules.RightOf, image.Id);
            }

            if ((this.style.gravity & (int)GravityFlags.Center) != 0)
            {
                textParams.AddRule(LayoutRules.CenterInParent);
            }
            else if ((this.style.gravity & (int)GravityFlags.CenterVertical) != 0)
            {
                textParams.AddRule(LayoutRules.CenterVertical);
            }
            else if ((this.style.gravity & (int)GravityFlags.CenterHorizontal) != 0)
            {
                textParams.AddRule(LayoutRules.CenterHorizontal);
            }

            contentView.AddView(text, textParams);
            return contentView;
        }

        private TextView initializeTextView(Resources resources)
        {
            TextView text = new TextView(this.activity);
            text.Id = TEXT_ID;
            if (this.style.fontName != null)
            {
                setTextWithCustomFont(text, this.style.fontName);
            }
            else if (this.style.fontNameResId != 0)
            {
                setTextWithCustomFont(text, resources.GetString(this.style.fontNameResId));
            }
            else
            {
                text.Text = this.text;
            }
            text.Gravity = (GravityFlags)this.style.gravity;

            // set the text color if set
            if (this.style.textColorValue != Style.NOT_SET)
            {
                text.SetTextColor(resources.GetColor(this.style.textColorValue));
            }
            else if (this.style.textColorResourceId != 0)
            {
                text.SetTextColor(resources.GetColor(this.style.textColorResourceId));
            }

            // Set the text size. If the user has set a text size and text
            // appearance, the text size in the text appearance
            // will override this.
            if (this.style.textSize != 0)
            {
                text.SetTextSize(ComplexUnitType.Sp, this.style.textSize);
            }

            // Setup the shadow if requested
            if (this.style.textShadowColorResId != 0)
            {
                initializeTextViewShadow(resources, text);
            }

            // Set the text appearance
            if (this.style.textAppearanceResId != 0)
            {
                text.SetTextAppearance(this.activity, this.style.textAppearanceResId);
            }
            return text;
        }

        private void setTextWithCustomFont(TextView text, String fontName)
        {
            if (this.text != null)
            {
                SpannableString s = new SpannableString(this.text);
                s.SetSpan(new TypefaceSpan(text.Context, fontName), 0, s.Length(), SpanTypes.ExclusiveExclusive);
                text.Text = s.ToString();
            }
        }

        private void initializeTextViewShadow(Resources resources, TextView text)
        {
            float textShadowRadius = this.style.textShadowRadius;
            float textShadowDx = this.style.textShadowDx;
            float textShadowDy = this.style.textShadowDy;
            text.SetShadowLayer(textShadowRadius, textShadowDx, textShadowDy, resources.GetColor(this.style.textShadowColorResId));
        }

        private ImageView initializeImageView()
        {
            ImageView image;
            image = new ImageView(this.activity);
            image.Id = IMAGE_ID;
            image.SetAdjustViewBounds(true);
            image.SetScaleType(this.style.imageScaleType);

            // set the image drawable if not null
            if (null != this.style.imageDrawable)
            {
                image.SetImageDrawable(this.style.imageDrawable);
            }

            // set the image resource if not 0. This will overwrite the drawable
            // if both are set
            if (this.style.imageResId != 0)
            {
                image.SetImageResource(this.style.imageResId);
            }

            RelativeLayout.LayoutParams imageParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            imageParams.AddRule(LayoutRules.AlignParentLeft, (int)LayoutRules.True);
            imageParams.AddRule(LayoutRules.CenterVertical, (int)LayoutRules.True);

            image.LayoutParameters = imageParams;

            return image;
        }
    }
}