using Android.Views;
using Android.Views.Animations;

namespace CroutonLibrary
{
    public class DefaultAnimationsBuilder
    {
        private static long DURATION = 400;
        private static Animation SlideInDownAnimation, SlideOutUpAnimation;
        private static int LastInAnimationHeight, LastOutAnimationHeight;

        private DefaultAnimationsBuilder()
        {
            /* no-op */
        }

        /**
       * @param croutonView
       *   The croutonView which gets animated.
       *
       * @return The default Animation for a showing {@link Crouton}.
       */

        public static Animation BuildDefaultSlideInDownAnimation(View croutonView)
        {
            if (!AreLastMeasuredInAnimationHeightAndCurrentEqual(croutonView) || (null == SlideInDownAnimation))
            {
                SlideInDownAnimation = new TranslateAnimation(
                    0, 0, // X: from, to
                    -croutonView.MeasuredHeight, 0 // Y: from, to
                    );
                SlideInDownAnimation.Duration = DURATION;
                SetLastInAnimationHeight(croutonView.MeasuredHeight);
            }
            return SlideInDownAnimation;
        }

        /**
       * @param croutonView
       *   The croutonView which gets animated.
       *
       * @return The default Animation for a hiding {@link Crouton}.
       */

        public static Animation BuildDefaultSlideOutUpAnimation(View croutonView)
        {
            if (!AreLastMeasuredOutAnimationHeightAndCurrentEqual(croutonView) || (null == SlideOutUpAnimation))
            {
                SlideOutUpAnimation = new TranslateAnimation(
                    0, 0, // X: from, to
                    0, -croutonView.MeasuredHeight // Y: from, to
                    );
                SlideOutUpAnimation.Duration = DURATION;
                SetLastOutAnimationHeight(croutonView.MeasuredHeight);
            }
            return SlideOutUpAnimation;
        }

        private static bool AreLastMeasuredInAnimationHeightAndCurrentEqual(View croutonView)
        {
            return AreLastMeasuredAnimationHeightAndCurrentEqual(LastInAnimationHeight, croutonView);
        }

        private static bool AreLastMeasuredOutAnimationHeightAndCurrentEqual(View croutonView)
        {
            return AreLastMeasuredAnimationHeightAndCurrentEqual(LastOutAnimationHeight, croutonView);
        }

        private static bool AreLastMeasuredAnimationHeightAndCurrentEqual(int lastHeight, View croutonView)
        {
            return lastHeight == croutonView.MeasuredHeight;
        }

        private static void SetLastInAnimationHeight(int lastInAnimationHeight)
        {
            LastInAnimationHeight = lastInAnimationHeight;
        }

        private static void SetLastOutAnimationHeight(int lastOutAnimationHeight)
        {
            LastOutAnimationHeight = lastOutAnimationHeight;
        }
    }
}