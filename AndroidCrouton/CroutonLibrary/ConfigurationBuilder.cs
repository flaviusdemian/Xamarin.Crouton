using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CroutonLibrary
{
    public class ConfigurationBuilder
    {

        /** Creates a {@link Builder} to build a {@link Configuration} upon. */

        public int durationInMilliseconds = Configuration.DURATION_SHORT;
        public int inAnimationResId = 0;
        public int outAnimationResId = 0;

        /**
         * Set the durationInMilliseconds option of the {@link Crouton}.
         *
         * @param duration
         *   The durationInMilliseconds the crouton will be displayed
         *   {@link Crouton} in milliseconds.
         *
         * @return the {@link Builder}.
         */
        public ConfigurationBuilder setDuration(int duration)
        {
            this.durationInMilliseconds = duration;

            return this;
        }

        /**
         * The resource id for the in animation.
         *
         * @param inAnimationResId
         *   The resource identifier for the animation that's being shown
         *   when the {@link Crouton} is going to be displayed.
         *
         * @return the {@link Builder}.
         */
        public ConfigurationBuilder setInAnimation(int inAnimationResId)
        {
            this.inAnimationResId = inAnimationResId;

            return this;
        }

        /**
         * The resource id for the out animation
         *
         * @param outAnimationResId
         *   The resource identifier for the animation that's being shown
         *   when the {@link Crouton} is going to be removed.
         *
         * @return the {@link Builder}.
         */
        public ConfigurationBuilder setOutAnimation(int outAnimationResId)
        {
            this.outAnimationResId = outAnimationResId;

            return this;
        }

        /**
         * Builds the {@link Configuration}.
         *
         * @return The built {@link Configuration}.
         */
        public Configuration build()
        {
            return new Configuration(this);
        }
    }
}