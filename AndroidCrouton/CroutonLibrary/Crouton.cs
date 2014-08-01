using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Lang;
using Exception = System.Exception;
using String = System.String;

namespace CroutonLibrary
{
    public class Crouton : Object
    {
        private static String NULL_PARAMETERS_ARE_NOT_ACCEPTED = "Null parameters are not accepted";
        private static int IMAGE_ID = 0x100;
        private static int TEXT_ID = 0x101;
        private readonly View CustomView;
        private readonly Style Style;
        private readonly String Text;

        private Activity Activity;
        private Configuration Configuration;
        private FrameLayout CroutonView;
        private Animation InAnimation;
        private LifecycleCallback LifecycleCallback;
        private View.IOnClickListener OnClickListener;
        private Animation OutAnimation;
        private ViewGroup ViewGroup;

        /**
         * Creates the {@link Crouton}.
         *
         * @param Activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         */

        private Crouton(Activity activity, String text, Style style)
        {
            if ((activity == null) || (text == null) || (style == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            Activity = activity;
            ViewGroup = null;
            Text = text;
            Style = style;
            CustomView = null;
        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        private Crouton(Activity activity, String text, Style style, ViewGroup viewGroup)
        {
            if ((activity == null) || (text == null) || (style == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            Activity = activity;
            Text = text;
            Style = style;
            ViewGroup = viewGroup;
            CustomView = null;
        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param Activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param CustomView
         *     The custom {@link View} to display
         */

        private Crouton(Activity activity, View customView)
        {
            if ((activity == null) || (customView == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            Activity = activity;
            ViewGroup = null;
            CustomView = customView;
            Style = new StyleBuilder().Build();
            Text = null;
        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param CustomView
         *     The custom {@link View} to display
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        private Crouton(Activity activity, View customView, ViewGroup viewGroup)
            : this(activity, customView, viewGroup, Configuration.DEFAULT)
        {
        }

        /**
         * Creates the {@link Crouton}.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param CustomView
         *     The custom {@link View} to display
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         * @param Configuration
         *     The {@link Configuration} for this {@link Crouton}.
         */

        private Crouton(Activity activity, View customView, ViewGroup viewGroup, Configuration configuration)
        {
            if ((activity == null) || (customView == null))
            {
                throw new IllegalArgumentException(NULL_PARAMETERS_ARE_NOT_ACCEPTED);
            }

            Activity = activity;
            CustomView = customView;
            ViewGroup = viewGroup;
            Style = new StyleBuilder().Build();
            Text = null;
            Configuration = configuration;
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton MakeText(Activity activity, String text, Style style)
        {
            return new Crouton(activity, text, style);
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton MakeText(Activity activity, String text, Style style, ViewGroup viewGroup)
        {
            return new Crouton(activity, text, style, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton MakeText(Activity activity, String text, Style style, int viewGroupResId)
        {
            return new Crouton(activity, text, style, activity.FindViewById<ViewGroup>(viewGroupResId));
        }


        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param textResourceId
         *     The resource id of the Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton MakeText(Activity activity, int textResourceId, Style style)
        {
            return MakeText(activity, activity.GetString(textResourceId), style);
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton MakeText(Activity activity, int textResourceId, Style style, ViewGroup viewGroup)
        {
            return MakeText(activity, activity.Resources.GetString(textResourceId), style, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton MakeText(Activity activity, int textResourceId, Style style, int viewGroupResId)
        {
            return MakeText(activity, activity.GetString(textResourceId), style,
                activity.FindViewById<ViewGroup>(viewGroupResId));
        }


        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param CustomView
         *     The custom {@link View} to display
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton Make(Activity activity, View customView)
        {
            return new Crouton(activity, customView);
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param CustomView
         *     The custom {@link View} to display
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton Make(Activity activity, View customView, ViewGroup viewGroup)
        {
            return new Crouton(activity, customView, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param CustomView
         *     The custom {@link View} to display
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton Make(Activity activity, View customView, int viewGroupResId)
        {
            return new Crouton(activity, customView, activity.FindViewById<ViewGroup>(viewGroupResId));
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param CustomView
         *     The custom {@link View} to display
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         * @param Configuration
         *     The Configuration for this crouton.
         *
         * @return The created {@link Crouton}.
         */

        public static Crouton Make(Activity activity, View customView, int viewGroupResId, Configuration configuration)
        {
            return new Crouton(activity, customView, activity.FindViewById<ViewGroup>(viewGroupResId), configuration);
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given Activity
         * and displays it directly.
         *
         * @param Activity
         *     The {@link android.app.Activity} that the {@link Crouton} should
         *     be attached to.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         */

        public static void ShowText(Activity activity, String text, Style style)
        {
            MakeText(activity, text, style).Show();
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given Activity
         * and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        public static void ShowText(Activity activity, String text, Style style, ViewGroup viewGroup)
        {
            MakeText(activity, text, style, viewGroup).Show();
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given Activity
         * and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        public static void ShowText(Activity activity, String text, Style style, int viewGroupResId)
        {
            MakeText(activity, text, style, activity.FindViewById<ViewGroup>(viewGroupResId)).Show();
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given Activity
         * and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param Text
         *     The Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         * @param Configuration
         *     The Configuration for this Crouton.
         */

        public static void ShowText(Activity activity, String text, Style style, int viewGroupResId,
            Configuration configuration)
        {
            MakeText(activity, text, style, activity.FindViewById<ViewGroup>(viewGroupResId))
                .SetConfiguration(configuration)
                .Show();
        }


        /**
         * Creates a {@link Crouton} with provided Text and Style for a given Activity
         * and displays it directly.
         *
         * @param Activity
         *     The {@link android.app.Activity} that the {@link Crouton} should
         *     be attached to.
         * @param CustomView
         *     The custom {@link View} to display
         */

        public static void Show(Activity activity, View customView)
        {
            Make(activity, customView).Show();
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given Activity
         * and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param CustomView
         *     The custom {@link View} to display
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        public static void Show(Activity activity, View customView, ViewGroup viewGroup)
        {
            Make(activity, customView, viewGroup).Show();
        }

        /**
         * Creates a {@link Crouton} with provided Text and Style for a given Activity
         * and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param CustomView
         *     The custom {@link View} to display
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        public static void Show(Activity activity, View customView, int viewGroupResId)
        {
            Make(activity, customView, viewGroupResId).Show();
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that the {@link Crouton} should be attached
         *     to.
         * @param textResourceId
         *     The resource id of the Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         */

        public static void ShowText(Activity activity, int textResourceId, Style style)
        {
            ShowText(activity, activity.GetString(textResourceId), style);
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param ViewGroup
         *     The {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        public static void ShowText(Activity activity, int textResourceId, Style style, ViewGroup viewGroup)
        {
            ShowText(activity, activity.GetString(textResourceId), style, viewGroup);
        }

        /**
         * Creates a {@link Crouton} with provided Text-resource and Style for a given
         * Activity and displays it directly.
         *
         * @param Activity
         *     The {@link Activity} that represents the context in which the Crouton should exist.
         * @param textResourceId
         *     The resource id of the Text you want to display.
         * @param Style
         *     The Style that this {@link Crouton} should be created with.
         * @param viewGroupResId
         *     The resource id of the {@link ViewGroup} that this {@link Crouton} should be added to.
         */

        public static void ShowText(Activity activity, int textResourceId, Style style, int viewGroupResId)
        {
            ShowText(activity, activity.GetString(textResourceId), style, viewGroupResId);
        }

        /**
         * Allows hiding of a previously displayed {@link Crouton}.
         *
         * @param crouton
         *     The {@link Crouton} you want to Hide.
         */

        public static void Hide(Crouton crouton)
        {
            crouton.Hide();
        }

        /**
         * Cancels all queued {@link Crouton}s. If there is a {@link Crouton}
         * displayed currently, it will be the last one displayed.
         */

        public static void CancelAllCroutons()
        {
            Manager.getInstance().ClearCroutonQueue();
        }

        /**
         * Clears (and removes from {@link Activity}'s content view, if necessary) all
         * croutons for the provided Activity
         *
         * @param Activity
         *     - The {@link Activity} to clear the croutons for.
         */

        public static void ClearCroutonsForActivity(Activity activity)
        {
            Manager.getInstance().ClearCroutonsForActivity(activity);
        }

        /**
         * Cancels a {@link Crouton} immediately.
         */

        public void Cancel()
        {
            Manager manager = Manager.getInstance();
            manager.RemoveCroutonImmediately(this);
        }

        /**
         * Displays the {@link Crouton}. If there's another {@link Crouton} visible at
         * the time, this {@link Crouton} will be displayed afterwards.
         */

        public void Show()
        {
            Manager.getInstance().add(this);
        }

        public Animation GetInAnimation()
        {
            if ((null == InAnimation) && (null != Activity))
            {
                if (GetConfiguration().InAnimationResId > 0)
                {
                    InAnimation = AnimationUtils.LoadAnimation(GetActivity(), GetConfiguration().InAnimationResId);
                }
                else
                {
                    MeasureCroutonView();
                    InAnimation = DefaultAnimationsBuilder.BuildDefaultSlideInDownAnimation(GetView());
                }
            }

            return InAnimation;
        }

        public Animation GetOutAnimation()
        {
            if ((null == OutAnimation) && (null != Activity))
            {
                if (GetConfiguration().OutAnimationResId > 0)
                {
                    OutAnimation = AnimationUtils.LoadAnimation(GetActivity(), GetConfiguration().OutAnimationResId);
                }
                else
                {
                    OutAnimation = DefaultAnimationsBuilder.BuildDefaultSlideOutUpAnimation(GetView());
                }
            }

            return OutAnimation;
        }

        /**
         * @param LifecycleCallback
         *     Callback object for notable events in the life of a Crouton.
         */

        public void SetLifecycleCallback(LifecycleCallback lifecycleCallback)
        {
            LifecycleCallback = lifecycleCallback;
        }

        /**
         * Removes this {@link Crouton}.
         *
         * @since 1.9
         */

        public void Hide()
        {
            Manager.getInstance().RemoveCrouton(this);
        }

        /**
         * Allows setting of an {@link OnClickListener} directly to a {@link Crouton} without having to use a custom view.
         *
         * @param OnClickListener
         *     The {@link OnClickListener} to set.
         *
         * @return this {@link Crouton}.
         */

        public Crouton SetOnClickListener(View.IOnClickListener onClickListener)
        {
            OnClickListener = onClickListener;
            return this;
        }

        /**
         * Set the {@link Configuration} on this {@link Crouton}, prior to showing it.
         *
         * @param Configuration
         *     a {@link Configuration} built using the {@link Configuration.Builder}.
         *
         * @return this {@link Crouton}.
         */

        public Crouton SetConfiguration(Configuration configuration)
        {
            Configuration = configuration;
            return this;
        }

        public override string ToString()
        {
            return "Crouton{" +
                   "Text=" + Text +
                   ", Style=" + Style +
                   ", Configuration=" + Configuration +
                   ", CustomView=" + CustomView +
                   ", OnClickListener=" + OnClickListener +
                   ", Activity=" + Activity +
                   ", ViewGroup=" + ViewGroup +
                   ", CroutonView=" + CroutonView +
                   ", InAnimation=" + InAnimation +
                   ", OutAnimation=" + OutAnimation +
                   ", LifecycleCallback=" + LifecycleCallback +
                   '}';
        }

        /**
         * Convenience method to get the license Text for embedding within your application.
         *
         * @return The license Text.
         */

        public static String GetLicenseText()
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

        public bool IsShowing()
        {
            return (null != Activity) && (IsCroutonViewNotNull() || IsCustomViewNotNull());
        }

        private bool IsCroutonViewNotNull()
        {
            return (null != CroutonView) && (null != CroutonView.Parent);
        }

        private bool IsCustomViewNotNull()
        {
            return (null != CustomView) && (null != CustomView.Parent);
        }

        /**
         * Removes the Activity reference this {@link Crouton} is holding
         */

        public void DetachActivity()
        {
            Activity = null;
        }

        /**
         * Removes the ViewGroup reference this {@link Crouton} is holding
         */

        public void DetachViewGroup()
        {
            ViewGroup = null;
        }

        /**
         * Removes the LifecycleCallback reference this {@link Crouton} is holding
         */

        public void DetachLifecycleCallback()
        {
            LifecycleCallback = null;
        }

        /**
         * @return the LifecycleCallback
         */

        public LifecycleCallback GetLifecycleCallback()
        {
            return LifecycleCallback;
        }

        /**
         * @return the Style
         */

        private Style GetStyle()
        {
            return Style;
        }

        /**
         * @return this croutons Configuration
         */

        public Configuration GetConfiguration()
        {
            if (null == Configuration)
            {
                Configuration = GetStyle().Configuration;
            }
            return Configuration;
        }

        /**
         * @return the Activity
         */

        public Activity GetActivity()
        {
            return Activity;
        }

        /**
         * @return the ViewGroup
         */

        public ViewGroup GetViewGroup()
        {
            return ViewGroup;
        }

        /**
         * @return the Text
         */

        public String GetText()
        {
            return Text;
        }

        /**
         * @return the view
         */

        public View GetView()
        {
            // return the custom view if one exists
            if (null != CustomView)
            {
                return CustomView;
            }

            // if already setup return the view
            if (null == CroutonView)
            {
                InitializeCroutonView();
            }

            return CroutonView;
        }

        private void MeasureCroutonView()
        {
            View view = GetView();
            int widthSpec;
            if (null != ViewGroup)
            {
                widthSpec = View.MeasureSpec.MakeMeasureSpec(ViewGroup.MeasuredWidth, MeasureSpecMode.AtMost);
            }
            else
            {
                widthSpec = View.MeasureSpec.MakeMeasureSpec(Activity.Window.DecorView.MeasuredWidth,
                    MeasureSpecMode.AtMost);
            }

            view.Measure(widthSpec, View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
        }

        private void InitializeCroutonView()
        {
            Resources resources = Activity.Resources;

            CroutonView = InitializeCroutonViewGroup(resources);

            // create content view
            RelativeLayout contentView = InitializeContentView(resources);
            CroutonView.AddView(contentView);
        }

        private FrameLayout InitializeCroutonViewGroup(Resources resources)
        {
            var croutonView = new FrameLayout(Activity);

            if (null != OnClickListener)
            {
                croutonView.SetOnClickListener(OnClickListener);
            }

            int height;
            if (Style.HeightDimensionResId > 0)
            {
                height = resources.GetDimensionPixelSize(Style.HeightDimensionResId);
            }
            else
            {
                height = Style.HeightInPixels;
            }

            int width;
            if (Style.WidthDimensionResId > 0)
            {
                width = resources.GetDimensionPixelSize(Style.WidthDimensionResId);
            }
            else
            {
                width = Style.WidthInPixels;
            }

            croutonView.LayoutParameters =
                new FrameLayout.LayoutParams(width != 0 ? width : ViewGroup.LayoutParams.MatchParent, height);

            croutonView.SetBackgroundColor(Color.LightGreen);
            // set background
            //if (this.Style.BackgroundColorValue != Style.NOT_SET)
            //{
            //    //TODO: FIX
            //    //CroutonView.SetBackgroundColor(resources.GetColor(this.Style.BackgroundColorValue));

            //}
            //else
            //{
            //    CroutonView.SetBackgroundColor(resources.GetColor(this.Style.BackgroundColorResourceId));
            //}

            // set the background drawable if set. This will override the background
            // color.
            if (Style.BackgroundDrawableResourceId != 0)
            {
                Bitmap background = BitmapFactory.DecodeResource(resources, Style.BackgroundDrawableResourceId);
                var drawable = new BitmapDrawable(resources, background);
                if (Style.IsTileEnabled)
                {
                    drawable.SetTileModeXY(Shader.TileMode.Repeat, Shader.TileMode.Repeat);
                }
                croutonView.SetBackgroundDrawable(drawable);
            }
            return croutonView;
        }

        private RelativeLayout InitializeContentView(Resources resources)
        {
            var contentView = new RelativeLayout(Activity);
            contentView.LayoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);

            // set padding
            int padding = Style.PaddingInPixels;

            // if a padding dimension has been set, this will overwrite any padding
            // in pixels
            if (Style.PaddingDimensionResId > 0)
            {
                padding = resources.GetDimensionPixelSize(Style.PaddingDimensionResId);
            }
            contentView.SetPadding(padding, padding, padding, padding);

            // only setup image if one is requested
            ImageView image = null;
            if ((null != Style.ImageDrawable) || (0 != Style.ImageResId))
            {
                image = InitializeImageView();
                contentView.AddView(image, image.LayoutParameters);
            }

            TextView text = InitializeTextView(resources);

            var textParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent);
            if (null != image)
            {
                textParams.AddRule(LayoutRules.RightOf, image.Id);
            }

            if ((Style.Gravity & (int) GravityFlags.Center) != 0)
            {
                textParams.AddRule(LayoutRules.CenterInParent);
            }
            else if ((Style.Gravity & (int) GravityFlags.CenterVertical) != 0)
            {
                textParams.AddRule(LayoutRules.CenterVertical);
            }
            else if ((Style.Gravity & (int) GravityFlags.CenterHorizontal) != 0)
            {
                textParams.AddRule(LayoutRules.CenterHorizontal);
            }

            contentView.AddView(text, textParams);
            return contentView;
        }

        private TextView InitializeTextView(Resources resources)
        {
            var text = new TextView(Activity);
            text.Id = TEXT_ID;
            if (Style.FontName != null)
            {
                SetTextWithCustomFont(text, Style.FontName);
            }
            else if (Style.FontNameResId != 0)
            {
                SetTextWithCustomFont(text, resources.GetString(Style.FontNameResId));
            }
            else
            {
                text.Text = Text;
            }
            text.Gravity = (GravityFlags) Style.Gravity;

            // set the Text color if set
            //TODO: FIX
            try
            {
                CroutonView.SetBackgroundColor(Color.LightGreen);
                //if (this.Style.TextColorValue != Style.NOT_SET)
                //{
                //    Text.SetTextColor(this.Style.TextColorValue);
                //}
                //else
                //{
                //Text.SetTextColor(resources.GetColor(this.Style.TextColorResourceId));
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            // Set the Text size. If the user has set a Text size and Text
            // appearance, the Text size in the Text appearance
            // will override this.
            if (Style.TextSize != 0)
            {
                text.SetTextSize(ComplexUnitType.Sp, Style.TextSize);
            }

            // Setup the shadow if requested
            if (Style.TextShadowColorResId != 0)
            {
                InitializeTextViewShadow(resources, text);
            }

            // Set the Text appearance
            if (Style.TextAppearanceResId != 0)
            {
                text.SetTextAppearance(Activity, Style.TextAppearanceResId);
            }
            return text;
        }

        private void SetTextWithCustomFont(TextView text, String fontName)
        {
            if (Text != null)
            {
                var s = new SpannableString(Text);
                s.SetSpan(new TypefaceSpan(text.Context, fontName), 0, s.Length(), SpanTypes.ExclusiveExclusive);
                text.Text = s.ToString();
            }
        }

        private void InitializeTextViewShadow(Resources resources, TextView text)
        {
            float textShadowRadius = Style.TextShadowRadius;
            float textShadowDx = Style.TextShadowDx;
            float textShadowDy = Style.TextShadowDy;
            text.SetShadowLayer(textShadowRadius, textShadowDx, textShadowDy,
                resources.GetColor(Style.TextShadowColorResId));
        }

        private ImageView InitializeImageView()
        {
            ImageView image;
            image = new ImageView(Activity);
            image.Id = IMAGE_ID;
            image.SetAdjustViewBounds(true);
            image.SetScaleType(Style.ImageScaleType);

            // set the image drawable if not null
            if (null != Style.ImageDrawable)
            {
                image.SetImageDrawable(Style.ImageDrawable);
            }

            // set the image resource if not 0. This will overwrite the drawable
            // if both are set
            if (Style.ImageResId != 0)
            {
                image.SetImageResource(Style.ImageResId);
            }

            var imageParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            imageParams.AddRule(LayoutRules.AlignParentLeft, (int) LayoutRules.True);
            imageParams.AddRule(LayoutRules.CenterVertical, (int) LayoutRules.True);

            image.LayoutParameters = imageParams;

            return image;
        }
    }
}