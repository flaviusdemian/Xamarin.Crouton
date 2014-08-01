namespace CroutonLibrary
{
    public class ConfigurationBuilder
    {
        /** Creates a {@link Builder} to build a {@link Configuration} upon. */

        public int DurationInMilliseconds = Configuration.DURATION_SHORT;
        public int InAnimationResId = 0;
        public int OutAnimationResId = 0;

        /**
         * Set the DurationInMilliseconds option of the {@link Crouton}.
         *
         * @param duration
         *   The DurationInMilliseconds the crouton will be displayed
         *   {@link Crouton} in milliseconds.
         *
         * @return the {@link Builder}.
         */

        public ConfigurationBuilder SetDuration(int duration)
        {
            DurationInMilliseconds = duration;

            return this;
        }

        /**
         * The resource id for the in animation.
         *
         * @param InAnimationResId
         *   The resource identifier for the animation that's being shown
         *   when the {@link Crouton} is going to be displayed.
         *
         * @return the {@link Builder}.
         */

        public ConfigurationBuilder SetInAnimation(int inAnimationResId)
        {
            InAnimationResId = inAnimationResId;

            return this;
        }

        /**
         * The resource id for the out animation
         *
         * @param OutAnimationResId
         *   The resource identifier for the animation that's being shown
         *   when the {@link Crouton} is going to be removed.
         *
         * @return the {@link Builder}.
         */

        public ConfigurationBuilder SetOutAnimation(int outAnimationResId)
        {
            OutAnimationResId = outAnimationResId;

            return this;
        }

        /**
         * Builds the {@link Configuration}.
         *
         * @return The built {@link Configuration}.
         */

        public Configuration Build()
        {
            return new Configuration(this);
        }
    }
}