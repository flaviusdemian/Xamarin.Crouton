using System;
using System.Linq;
using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;
using Java.Interop;
using Java.Util;
using Java.Util.Concurrent;

namespace CroutonLibrary
{
    /**
     * Manages the lifecycle of {@link Crouton}s.
     */

    public class Manager : Handler
    {
        private static Manager INSTANCE;
        private readonly IQueue croutonQueue;

        private Manager()
        {
            croutonQueue = new LinkedBlockingQueue();
        }

        /**
   * @return The currently used instance of the {@link Manager}.
   */
        //static synchronized Manager getInstance() {
        public static Manager getInstance()
        {
            if (null == INSTANCE)
            {
                INSTANCE = new Manager();
            }

            return INSTANCE;
        }

        /**
   * Inserts a {@link Crouton} to be displayed.
   *
   * @param crouton
   *     The {@link Crouton} to be displayed.
   */

        public void add(Crouton crouton)
        {
            croutonQueue.Add(crouton);
            displayCrouton();
        }

        /**
   * Displays the next {@link Crouton} within the queue.
   */

        private void displayCrouton()
        {
            if (croutonQueue.IsEmpty)
            {
                return;
            }

            // First peek whether the Crouton has an activity.
            var currentCrouton = croutonQueue.Peek().JavaCast<Crouton>();

            // If the activity is null we poll the Crouton off the queue.
            if (null == currentCrouton.GetActivity())
            {
                croutonQueue.Poll();
            }

            if (!currentCrouton.IsShowing())
            {
                // Display the Crouton
                sendMessage(currentCrouton, Convert.ToInt32(Messages.ADD_CROUTON_TO_VIEW));
                if (null != currentCrouton.GetLifecycleCallback())
                {
                    currentCrouton.GetLifecycleCallback().OnDisplayed();
                }
            }
            else
            {
                SendMessageDelayed(currentCrouton, Messages.DISPLAY_CROUTON,
                    calculateCroutonDuration(currentCrouton));
            }
        }

        private long calculateCroutonDuration(Crouton crouton)
        {
            long croutonDuration = crouton.GetConfiguration().DurationInMilliseconds;
            croutonDuration += crouton.GetInAnimation().Duration;
            croutonDuration += crouton.GetOutAnimation().Duration;
            return croutonDuration;
        }

        /**
   * Sends a {@link Crouton} within a {@link Message}.
   *
   * @param crouton
   *     The {@link Crouton} that should be sent.
   * @param messageId
   *     The {@link Message} id.
   */

        private void sendMessage(Crouton crouton, int messageId)
        {
            Message message = ObtainMessage(messageId);
            message.Obj = crouton;
            SendMessage(message);
        }

        /**
   * Sends a {@link Crouton} within a delayed {@link Message}.
   *
   * @param crouton
   *     The {@link Crouton} that should be sent.
   * @param messageId
   *     The {@link Message} id.
   * @param delay
   *     The delay in milliseconds.
   */

        private void SendMessageDelayed(Crouton crouton, Int64 messageId, long delay)
        {
            Message message = ObtainMessage((int)messageId);
            message.Obj = crouton;
            SendMessageDelayed(message, delay);
        }

        /*
   * (non-Javadoc)
   *
   * @see android.os.Handler#handleMessage(android.os.Message)
   */

        public override void HandleMessage(Message message)
        {
            var crouton = (Crouton)message.Obj;
            if (null == crouton)
            {
                return;
            }
            switch (message.What)
            {
                case Messages.DISPLAY_CROUTON:
                    displayCrouton();
                    break;

                case Messages.ADD_CROUTON_TO_VIEW:
                    AddCroutonToView(crouton);
                    break;

                case Messages.REMOVE_CROUTON:
                    RemoveCrouton(crouton);
                    if (null != crouton.GetLifecycleCallback())
                    {
                        crouton.GetLifecycleCallback().OnRemoved();
                    }
                    break;

                default:
                    base.HandleMessage(message);
                    break;
            }
        }

        /**
   * Adds a {@link Crouton} to the {@link ViewParent} of it's {@link Activity}.
   *
   * @param crouton
   *     The {@link Crouton} that should be added.
   */

        private void AddCroutonToView(Crouton crouton)
        {
            // don't add if it is already showing
            if (crouton.IsShowing())
            {
                return;
            }

            View croutonView = crouton.GetView();
            if (null == croutonView.Parent)
            {
                ViewGroup.LayoutParams parameters = croutonView.LayoutParameters;
                if (null == parameters)
                {
                    parameters = new ViewGroup.MarginLayoutParams(ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.WrapContent);
                }
                // display Crouton in ViewGroup is it has been supplied
                if (null != crouton.GetViewGroup())
                {
                    ViewGroup croutonViewGroup = crouton.GetViewGroup();
                    if (ShouldAddViewWithoutPosition(croutonViewGroup))
                    {
                        croutonViewGroup.AddView(croutonView, parameters);
                    }
                    else
                    {
                        croutonViewGroup.AddView(croutonView, 0, parameters);
                    }
                }
                else
                {
                    Activity activity = crouton.GetActivity();
                    if (null == activity || activity.IsFinishing)
                    {
                        return;
                    }
                    HandleTranslucentActionBar((ViewGroup.MarginLayoutParams)parameters, activity);
                    HandleActionBarOverlay((ViewGroup.MarginLayoutParams)parameters, activity);

                    activity.AddContentView(croutonView, parameters);
                }
            }

            croutonView.RequestLayout(); // This is needed so the animation can use the measured with/height
            ViewTreeObserver observer = croutonView.ViewTreeObserver;
            if (null != observer)
            {
                CallOnGlobalLayout(crouton, croutonView);
            }
        }

        [TargetApi(Value = 16)]
        private void CallOnGlobalLayout(Crouton crouton, View croutonView)
        {
            var layoutListener = new GlobalLayoutListener();
            layoutListener.OnGlobalLayout(delegate
            {
                if (Build.VERSION.SdkInt < Build.VERSION_CODES.JellyBean)
                {
                    croutonView.ViewTreeObserver.RemoveGlobalOnLayoutListener(layoutListener);
                }
                else
                {
                    croutonView.ViewTreeObserver.RemoveOnGlobalLayoutListener(layoutListener);
                }

                if (crouton.GetInAnimation() != null)
                {
                    croutonView.StartAnimation(crouton.GetInAnimation());
                    AnnounceForAccessibilityCompat(crouton.GetActivity(), crouton.GetText());
                    if (Configuration.DURATION_INFINITE != crouton.GetConfiguration().DurationInMilliseconds)
                    {
                        SendMessageDelayed(crouton, Messages.REMOVE_CROUTON,
                            crouton.GetConfiguration().DurationInMilliseconds + crouton.GetInAnimation().Duration);
                    }
                }
            });
        }

        private bool ShouldAddViewWithoutPosition(ViewGroup croutonViewGroup)
        {
            return croutonViewGroup is FrameLayout || croutonViewGroup is AdapterView || croutonViewGroup is RelativeLayout;
        }

        [TargetApi(Value = 19)]
        private void HandleTranslucentActionBar(ViewGroup.MarginLayoutParams parameters, Activity activity)
        {
            // Translucent status is only available as of Android 4.4 Kit Kat.
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Kitkat)
            {
                var flags = (int)activity.Window.Attributes.Flags;
                int translucentStatusFlag = (int)WindowManagerFlags.TranslucentStatus;
                if ((flags & translucentStatusFlag) == translucentStatusFlag)
                {
                    SetActionBarMargin(parameters, activity);
                }
            }
        }

        [TargetApi(Value = 11)]
        private void HandleActionBarOverlay(ViewGroup.MarginLayoutParams parameters, Activity activity)
        {
            // ActionBar overlay is only available as of Android 3.0 Honeycomb.
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Honeycomb)
            {
                bool flags = activity.Window.HasFeature(WindowFeatures.ActionBarOverlay);
                if (flags)
                {
                    SetActionBarMargin(parameters, activity);
                }
            }
        }

        private void SetActionBarMargin(ViewGroup.MarginLayoutParams parameters, Activity activity)
        {
            int actionBarContainerId = Resources.System.GetIdentifier("action_bar_container", "id", "android");
            View actionBarContainer = activity.FindViewById(actionBarContainerId);
            // The action bar is present: the app is using a Holo theme.
            if (null != actionBarContainer)
            {
                parameters.TopMargin = actionBarContainer.Bottom;
            }
        }

        /**
   * Removes the {@link Crouton}'s view after it's display
   * DurationInMilliseconds.
   *
   * @param crouton
   *     The {@link Crouton} added to a {@link ViewGroup} and should be
   *     removed.
   */

        public void RemoveCrouton(Crouton crouton)
        {
            View croutonView = crouton.GetView();
            var croutonParentView = (ViewGroup)croutonView.Parent;

            if (null != croutonParentView)
            {
                croutonView.StartAnimation(crouton.GetOutAnimation());

                // Remove the Crouton from the queue.
                var removed = (Crouton)croutonQueue.Poll();

                // Remove the crouton from the view's parent.
                croutonParentView.RemoveView(croutonView);
                if (null != removed)
                {
                    removed.DetachActivity();
                    removed.DetachViewGroup();
                    if (null != removed.GetLifecycleCallback())
                    {
                        removed.GetLifecycleCallback().OnRemoved();
                    }
                    removed.DetachLifecycleCallback();
                }

                // Send a message to display the next crouton but delay it by the out
                // animation duration to Make sure it finishes
                SendMessageDelayed(crouton, Messages.DISPLAY_CROUTON, crouton.GetOutAnimation().Duration);
            }
        }

        /**
   * Removes a {@link Crouton} immediately, even when it's currently being
   * displayed.
   *
   * @param crouton
   *     The {@link Crouton} that should be removed.
   */

        public void RemoveCroutonImmediately(Crouton crouton)
        {
            // if Crouton has already been displayed then it may not be in the queue (because it was popped).
            // This ensures the displayed Crouton is removed from its parent immediately, whether another instance
            // of it exists in the queue or not.
            // Note: crouton.IsShowing() is false here even if it really is showing, as croutonView object in
            // Crouton seems to be out of sync with reality!
            if (null != crouton.GetActivity() && null != crouton.GetView() && null != crouton.GetView().Parent)
            {
                ((ViewGroup)crouton.GetView().Parent).RemoveView(crouton.GetView());

                // remove any messages pending for the crouton
                RemoveAllMessagesForCrouton(crouton);
            }
            // remove any matching croutons from queue
            IIterator croutonIterator = croutonQueue.Iterator();
            while (croutonIterator.HasNext)
            {
                var c = croutonIterator.Next().JavaCast<Crouton>();
                if (c.Equals(crouton) && (null != c.GetActivity()))
                {
                    // remove the crouton from the content view
                    RemoveCroutonFromViewParent(crouton);

                    // remove any messages pending for the crouton
                    RemoveAllMessagesForCrouton(c);

                    // remove the crouton from the queue
                    croutonIterator.Remove();

                    // we have found our crouton so just break
                    break;
                }
            }
        }

        /**
   * Removes all {@link Crouton}s from the queue.
   */

        public void ClearCroutonQueue()
        {
            RemoveAllMessages();

            // remove any views that may already have been added to the activity's
            // content view
            foreach (Crouton crouton in croutonQueue.ToEnumerable())
            {
                RemoveCroutonFromViewParent(crouton);
            }
            croutonQueue.Clear();
        }

        /**
   * Removes all {@link Crouton}s for the provided activity. This will remove
   * crouton from {@link Activity}s content view immediately.
   */

        public void ClearCroutonsForActivity(Activity activity)
        {
            IIterator croutonIterator = croutonQueue.Iterator();
            while (croutonIterator.HasNext)
            {
                var crouton = croutonIterator.Next().JavaCast<Crouton>();
                if ((null != crouton.GetActivity()) && crouton.GetActivity().Equals(activity))
                {
                    // remove the crouton from the content view
                    RemoveCroutonFromViewParent(crouton);

                    RemoveAllMessagesForCrouton(crouton);

                    // remove the crouton from the queue
                    croutonIterator.Remove();
                }
            }
        }

        private void RemoveCroutonFromViewParent(Crouton crouton)
        {
            if (crouton.IsShowing())
            {
                var parent = (ViewGroup)crouton.GetView().Parent;
                if (null != parent)
                {
                    parent.RemoveView(crouton.GetView());
                }
            }
        }

        private void RemoveAllMessages()
        {
            RemoveMessages(Messages.ADD_CROUTON_TO_VIEW);
            RemoveMessages(Messages.DISPLAY_CROUTON);
            RemoveMessages(Messages.REMOVE_CROUTON);
        }

        private void RemoveAllMessagesForCrouton(Crouton crouton)
        {
            RemoveMessages(Messages.ADD_CROUTON_TO_VIEW, crouton);
            RemoveMessages(Messages.DISPLAY_CROUTON, crouton);
            RemoveMessages(Messages.REMOVE_CROUTON, crouton);
        }

        /**
   * Generates and dispatches an SDK-specific spoken announcement.
   * <p>
   * For backwards compatibility, we're constructing an event from scratch
   * using the appropriate event type. If your application only targets SDK
   * 16+, you can just call View.announceForAccessibility(CharSequence).
   * </p>
   * <p/>
   * note: AccessibilityManager is only available from API lvl 4.
   * <p/>
   * Adapted from https://http://eyes-free.googlecode.com/files/accessibility_codelab_demos_v2_src.zip
   * via https://github.com/coreform/android-formidable-validation
   *
   * @param context
   *     Used to get {@link AccessibilityManager}
   * @param text
   *     The text to announce.
   */

        public static void AnnounceForAccessibilityCompat(Context context, String text)
        {
            if ((int)Build.VERSION.SdkInt >= 4)
            {
                AccessibilityManager accessibilityManager = null;
                if (null != context)
                {
                    accessibilityManager = (AccessibilityManager)context.GetSystemService(Context.AccessibilityService);
                }
                if (null == accessibilityManager || !accessibilityManager.IsEnabled)
                {
                    return;
                }

                // Prior to SDK 16, announcements could only be made through FOCUSED
                // events. Jelly Bean (SDK 16) added support for speaking text verbatim
                // using the ANNOUNCEMENT event type.
                EventTypes eventType;
                if ((int)Build.VERSION.SdkInt < 16)
                {
                    eventType = EventTypes.ViewFocused;
                }
                else
                {
                    eventType = EventTypes.Announcement;
                }

                // Construct an accessibility event with the minimum recommended
                // attributes. An event without a class name or package may be dropped.
                AccessibilityEvent ev = AccessibilityEvent.Obtain(eventType);
                var textProxy = new Java.Lang.String(text);
                ev.Text.Add(textProxy);
                ev.ClassName = INSTANCE.GetType().ToString();
                ev.PackageName = context.PackageName;

                // Sends the event directly through the accessibility manager. If your
                // application only targets SDK 14+, you should just call
                // getParent().requestSendAccessibilityEvent(this, event);
                accessibilityManager.SendAccessibilityEvent(ev);
            }
        }

        public override String ToString()
        {
            return "Manager{" +
                   "croutonQueue=" + croutonQueue +
                   '}';
        }
    }
}