using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Test.Suitebuilder;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;
using Java.Interop;
using Java.Lang;
using Java.Util;
using Java.Util.Concurrent;
using String = Java.Lang.String;

namespace CroutonLibrary
{

    /**
     * Manages the lifecycle of {@link Crouton}s.
     */

    public class Manager : Handler
    {
        private static Manager INSTANCE;
        private IQueue croutonQueue;

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
            if (croutonQueue.IsEmpty == true)
            {
                return;
            }

            // First peek whether the Crouton has an activity.
            Crouton currentCrouton = JavaObjectExtensions.JavaCast<Crouton>(croutonQueue.Peek());

            // If the activity is null we poll the Crouton off the queue.
            if (null == currentCrouton.getActivity())
            {
                croutonQueue.Poll();
            }

            if (!currentCrouton.isShowing())
            {
                // Display the Crouton
                sendMessage(currentCrouton, Convert.ToInt32(Messages.ADD_CROUTON_TO_VIEW));
                if (null != currentCrouton.getLifecycleCallback())
                {
                    currentCrouton.getLifecycleCallback().onDisplayed();
                }
            }
            else
            {
                sendMessageDelayed(currentCrouton, (int)Messages.DISPLAY_CROUTON,
                    calculateCroutonDuration(currentCrouton));
            }
        }

        private long calculateCroutonDuration(Crouton crouton)
        {
            long croutonDuration = crouton.getConfiguration().durationInMilliseconds;
            croutonDuration += crouton.getInAnimation().Duration;
            croutonDuration += crouton.getOutAnimation().Duration;
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

        private void sendMessageDelayed(Crouton crouton, Int64 messageId, long delay)
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
            Crouton crouton = (Crouton)message.Obj;
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
                    addCroutonToView(crouton);
                    break;

                case Messages.REMOVE_CROUTON:
                    removeCrouton(crouton);
                    if (null != crouton.getLifecycleCallback())
                    {
                        crouton.getLifecycleCallback().onRemoved();
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

        private void addCroutonToView(Crouton crouton)
        {
            // don't add if it is already showing
            if (crouton.isShowing())
            {
                return;
            }

            View croutonView = crouton.getView();
            if (null == croutonView.Parent)
            {
                ViewGroup.LayoutParams parameters = croutonView.LayoutParameters;
                if (null == parameters)
                {
                    parameters = new ViewGroup.MarginLayoutParams(ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.WrapContent);
                }
                // display Crouton in ViewGroup is it has been supplied
                if (null != crouton.getViewGroup())
                {
                    ViewGroup croutonViewGroup = crouton.getViewGroup();
                    if (shouldAddViewWithoutPosition(croutonViewGroup))
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
                    Activity activity = crouton.getActivity();
                    if (null == activity || activity.IsFinishing)
                    {
                        return;
                    }
                    handleTranslucentActionBar((ViewGroup.MarginLayoutParams)parameters, activity);
                    handleActionBarOverlay((ViewGroup.MarginLayoutParams)parameters, activity);

                    activity.AddContentView(croutonView, parameters);
                }
            }

            croutonView.RequestLayout(); // This is needed so the animation can use the measured with/height
            ViewTreeObserver observer = croutonView.ViewTreeObserver;
            if (null != observer)
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

                        if (crouton.getInAnimation() != null)
                        {
                            croutonView.StartAnimation(crouton.getInAnimation());
                            announceForAccessibilityCompat(crouton.getActivity(), crouton.getText());
                            if (Configuration.DURATION_INFINITE != crouton.getConfiguration().durationInMilliseconds)
                            {
                                sendMessageDelayed(crouton, Messages.REMOVE_CROUTON,
                                    crouton.getConfiguration().durationInMilliseconds + crouton.getInAnimation().Duration);
                            }
                        }
                    });
            }
        }

        private bool shouldAddViewWithoutPosition(ViewGroup croutonViewGroup)
        {
            return croutonViewGroup is FrameLayout || croutonViewGroup is AdapterView ||
                   croutonViewGroup is RelativeLayout;
        }

        //@TargetApi(19)
        private void handleTranslucentActionBar(ViewGroup.MarginLayoutParams parameters, Activity activity)
        {
            // Translucent status is only available as of Android 4.4 Kit Kat.
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Kitkat)
            {
                int flags = (int)activity.Window.Attributes.Flags;
                int translucentStatusFlag = 0;
                //TODO: FIX
                //int translucentStatusFlag = (int)  fl WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS;
                if ((flags & translucentStatusFlag) == translucentStatusFlag)
                {
                    setActionBarMargin(parameters, activity);
                }
            }
        }

        //@TargetApi(11)
        private void handleActionBarOverlay(ViewGroup.MarginLayoutParams parameters, Activity activity)
        {
            // ActionBar overlay is only available as of Android 3.0 Honeycomb.
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Honeycomb)
            {
                bool flags = activity.Window.HasFeature(WindowFeatures.ActionBarOverlay);
                if (flags)
                {
                    setActionBarMargin(parameters, activity);
                }
            }
        }

        private void setActionBarMargin(ViewGroup.MarginLayoutParams parameters, Activity activity)
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
   * durationInMilliseconds.
   *
   * @param crouton
   *     The {@link Crouton} added to a {@link ViewGroup} and should be
   *     removed.
   */

        public void removeCrouton(Crouton crouton)
        {
            View croutonView = crouton.getView();
            ViewGroup croutonParentView = (ViewGroup)croutonView.Parent;

            if (null != croutonParentView)
            {
                croutonView.StartAnimation(crouton.getOutAnimation());

                // Remove the Crouton from the queue.
                Crouton removed = (Crouton)croutonQueue.Poll();

                // Remove the crouton from the view's parent.
                croutonParentView.RemoveView(croutonView);
                if (null != removed)
                {
                    removed.detachActivity();
                    removed.detachViewGroup();
                    if (null != removed.getLifecycleCallback())
                    {
                        removed.getLifecycleCallback().onRemoved();
                    }
                    removed.detachLifecycleCallback();
                }

                // Send a message to display the next crouton but delay it by the out
                // animation duration to make sure it finishes
                sendMessageDelayed(crouton, Messages.DISPLAY_CROUTON, crouton.getOutAnimation().Duration);
            }
        }

        /**
   * Removes a {@link Crouton} immediately, even when it's currently being
   * displayed.
   *
   * @param crouton
   *     The {@link Crouton} that should be removed.
   */

        public void removeCroutonImmediately(Crouton crouton)
        {
            // if Crouton has already been displayed then it may not be in the queue (because it was popped).
            // This ensures the displayed Crouton is removed from its parent immediately, whether another instance
            // of it exists in the queue or not.
            // Note: crouton.isShowing() is false here even if it really is showing, as croutonView object in
            // Crouton seems to be out of sync with reality!
            if (null != crouton.getActivity() && null != crouton.getView() && null != crouton.getView().Parent)
            {
                ((ViewGroup)crouton.getView().Parent).RemoveView(crouton.getView());

                // remove any messages pending for the crouton
                removeAllMessagesForCrouton(crouton);
            }
            // remove any matching croutons from queue
            IIterator croutonIterator = croutonQueue.Iterator();
            while (croutonIterator.HasNext == true)
            {
                Crouton c = JavaObjectExtensions.JavaCast<Crouton>(croutonIterator.Next());
                if (c.Equals(crouton) && (null != c.getActivity()))
                {
                    // remove the crouton from the content view
                    removeCroutonFromViewParent(crouton);

                    // remove any messages pending for the crouton
                    removeAllMessagesForCrouton(c);

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

        public void clearCroutonQueue()
        {
            removeAllMessages();

            // remove any views that may already have been added to the activity's
            // content view
            foreach (Crouton crouton in croutonQueue.ToEnumerable())
            {
                removeCroutonFromViewParent(crouton);
            }
            croutonQueue.Clear();
        }

        /**
   * Removes all {@link Crouton}s for the provided activity. This will remove
   * crouton from {@link Activity}s content view immediately.
   */

        public void clearCroutonsForActivity(Activity activity)
        {
            IIterator croutonIterator = croutonQueue.Iterator();
            while (croutonIterator.HasNext == true)
            {
                Crouton crouton = JavaObjectExtensions.JavaCast<Crouton>(croutonIterator.Next());
                if ((null != crouton.getActivity()) && crouton.getActivity().Equals(activity))
                {
                    // remove the crouton from the content view
                    removeCroutonFromViewParent(crouton);

                    removeAllMessagesForCrouton(crouton);

                    // remove the crouton from the queue
                    croutonIterator.Remove();
                }
            }
        }

        private void removeCroutonFromViewParent(Crouton crouton)
        {
            if (crouton.isShowing())
            {
                ViewGroup parent = (ViewGroup)crouton.getView().Parent;
                if (null != parent)
                {
                    parent.RemoveView(crouton.getView());
                }
            }
        }

        private void removeAllMessages()
        {
            RemoveMessages(Messages.ADD_CROUTON_TO_VIEW);
            RemoveMessages(Messages.DISPLAY_CROUTON);
            RemoveMessages(Messages.REMOVE_CROUTON);
        }

        private void removeAllMessagesForCrouton(Crouton crouton)
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

        public static void announceForAccessibilityCompat(Context context, System.String text)
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
                Java.Lang.String textProxy = new Java.Lang.String(text);
                ev.Text.Add(textProxy);
                ev.ClassName = INSTANCE.GetType().ToString();
                ev.PackageName = context.PackageName;

                // Sends the event directly through the accessibility manager. If your
                // application only targets SDK 14+, you should just call
                // getParent().requestSendAccessibilityEvent(this, event);
                accessibilityManager.SendAccessibilityEvent(ev);
            }
        }

        public override System.String ToString()
        {
            return "Manager{" +
                   "croutonQueue=" + croutonQueue +
                   '}';
        }
    }
}
