using System;

namespace CroutonLibrary
{
    public class Configuration
    {
        /**
         * Display a {@link Crouton} for an infinite amount of time or
         * until {@link de.keyboardsurfer.android.widget.crouton.Crouton#Cancel()} has been called.
         */
        public static int DURATION_INFINITE = -1;
        /** The default long display duration of a {@link Crouton}. */
        public static int DURATION_LONG = 5000;
        /** The default short display duration of a {@link Crouton}. */
        public static int DURATION_SHORT = 3000;
        /** The default {@link Configuration} of a {@link Crouton}. */
        public static Configuration DEFAULT;

        /** The DurationInMilliseconds the {@link Crouton} will be displayed in milliseconds. */
        public readonly int DurationInMilliseconds;
        /** The resource id for the in animation. */
        public readonly int InAnimationResId;
        /** The resource id for the out animation. */
        public readonly int OutAnimationResId;

        static Configuration()
        {
            DEFAULT = new ConfigurationBuilder().SetDuration(DURATION_SHORT).Build();
        }

        public Configuration(ConfigurationBuilder builder)
        {
            DurationInMilliseconds = builder.DurationInMilliseconds;
            InAnimationResId = builder.InAnimationResId;
            OutAnimationResId = builder.OutAnimationResId;
        }

        public override String ToString()
        {
            return "Configuration{" +
                   "DurationInMilliseconds=" + DurationInMilliseconds +
                   ", InAnimationResId=" + InAnimationResId +
                   ", OutAnimationResId=" + OutAnimationResId +
                   '}';
        }
    }
}