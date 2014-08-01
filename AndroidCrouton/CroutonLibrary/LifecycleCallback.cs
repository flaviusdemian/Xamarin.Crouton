namespace CroutonLibrary
{
    public interface LifecycleCallback
    {
        /** Will be called when your Crouton has been displayed. */
        void OnDisplayed();

        /** Will be called when your {@link Crouton} has been removed. */
        void OnRemoved();

        //public void onCeasarDressing();
    }
}