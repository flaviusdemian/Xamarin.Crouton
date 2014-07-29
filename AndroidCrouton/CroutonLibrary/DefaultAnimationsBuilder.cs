using Android.Views;
using Android.Views.Animations;

namespace CroutonLibrary
{
    public class DefaultAnimationsBuilder
    {
        private static long DURATION = 400;
        private static Animation slideInDownAnimation, slideOutUpAnimation;
        private static int lastInAnimationHeight, lastOutAnimationHeight;

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

        public static Animation buildDefaultSlideInDownAnimation(View croutonView)
        {
            if (!areLastMeasuredInAnimationHeightAndCurrentEqual(croutonView) || (null == slideInDownAnimation))
            {
                slideInDownAnimation = new TranslateAnimation(
                    0, 0, // X: from, to
                    -croutonView.MeasuredHeight, 0 // Y: from, to
                    );
                slideInDownAnimation.Duration = DURATION;
                setLastInAnimationHeight(croutonView.MeasuredHeight);
            }
            return slideInDownAnimation;
        }

        /**
   * @param croutonView
   *   The croutonView which gets animated.
   *
   * @return The default Animation for a hiding {@link Crouton}.
   */

        public static Animation buildDefaultSlideOutUpAnimation(View croutonView)
        {
            if (!areLastMeasuredOutAnimationHeightAndCurrentEqual(croutonView) || (null == slideOutUpAnimation))
            {
                slideOutUpAnimation = new TranslateAnimation(
                    0, 0, // X: from, to
                    0, -croutonView.MeasuredHeight // Y: from, to
                    );
                slideOutUpAnimation.Duration = DURATION;
                setLastOutAnimationHeight(croutonView.MeasuredHeight);
            }
            return slideOutUpAnimation;
        }

        private static bool areLastMeasuredInAnimationHeightAndCurrentEqual(View croutonView)
        {
            return areLastMeasuredAnimationHeightAndCurrentEqual(lastInAnimationHeight, croutonView);
        }

        private static bool areLastMeasuredOutAnimationHeightAndCurrentEqual(View croutonView)
        {
            return areLastMeasuredAnimationHeightAndCurrentEqual(lastOutAnimationHeight, croutonView);
        }

        private static bool areLastMeasuredAnimationHeightAndCurrentEqual(int lastHeight, View croutonView)
        {
            return lastHeight == croutonView.MeasuredHeight;
        }

        private static void setLastInAnimationHeight(int lastInAnimationHeight)
        {
            DefaultAnimationsBuilder.lastInAnimationHeight = lastInAnimationHeight;
        }

        private static void setLastOutAnimationHeight(int lastOutAnimationHeight)
        {
            DefaultAnimationsBuilder.lastOutAnimationHeight = lastOutAnimationHeight;
        }
    }
}