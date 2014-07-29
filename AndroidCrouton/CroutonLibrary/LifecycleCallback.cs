namespace CroutonLibrary
{
    public interface LifecycleCallback
    {
        /** Will be called when your Crouton has been displayed. */
        void onDisplayed();

        /** Will be called when your {@link Crouton} has been removed. */
        void onRemoved();

        //public void onCeasarDressing();
    }
}