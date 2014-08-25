Xamarin.Crouton - this project is the Xamarin.Android implementation of <a href='https://github.com/keyboardsurfer/Crouton'> https://github.com/keyboardsurfer/Crouton </a>
===============

<article class="markdown-body entry-content" itemprop="mainContentOfPage"><h1>
<a name="user-content-crouton" class="anchor" href="#crouton" aria-hidden="true"><span class="octicon octicon-link"></span></a>Crouton</h1>

<p><a href="https://camo.githubusercontent.com/01dfc06abb8d9c1f8fec83e723caf8e2fa804fd7/68747470733a2f2f7261772e6769746875622e636f6d2f6b6579626f6172647375726665722f43726f75746f6e2f6d61737465722f73616d706c652f7372632f6d61696e2f7265732f6472617761626c652d78686470692f69635f6c61756e636865722e706e67" target="_blank"><img src="https://camo.githubusercontent.com/01dfc06abb8d9c1f8fec83e723caf8e2fa804fd7/68747470733a2f2f7261772e6769746875622e636f6d2f6b6579626f6172647375726665722f43726f75746f6e2f6d61737465722f73616d706c652f7372632f6d61696e2f7265732f6472617761626c652d78686470692f69635f6c61756e636865722e706e67" alt="Crouton" title="Crouton logo" data-canonical-src="https://raw.github.com/keyboardsurfer/Crouton/master/sample/src/main/res/drawable-xhdpi/ic_launcher.png" style="max-width:100%;"></a></p>

<p>Context sensitive notifications for Android</p>

<h2>
<a name="user-content-overview" class="anchor" href="#overview" aria-hidden="true"><span class="octicon octicon-link"></span></a>Overview</h2>

<p><strong>Crouton</strong> is a class that can be used by Android developers that feel the need for an <strong>alternative to the Context insensitive <a href="http://developer.android.com/reference/android/widget/Toast.html">Toast</a></strong>.</p>

<p>A Crouton will be displayed at the position the developer decides.
Standard will be the top of an application window.
You can line up multiple Croutons for display, that will be shown one after another.</p>

<p>You can check some features in the Crouton Demo.</p>

<p><a href="http://play.google.com/store/apps/details?id=de.keyboardsurfer.app.demo.crouton">
  <img alt="Crouton Demo on Google Play" src="https://camo.githubusercontent.com/9e1a34e84a09c0f95303da060457aea4a8899f85/687474703a2f2f646576656c6f7065722e616e64726f69642e636f6d2f696d616765732f6272616e642f656e5f67656e657269635f7267625f776f5f36302e706e67" data-canonical-src="http://developer.android.com/images/brand/en_generic_rgb_wo_60.png" style="max-width:100%;"></a></p>

<p>If you're already using Crouton and just want to download the latest version of the library, follow <a href="http://search.maven.org/#search%7Cga%7C1%7Cg%3A%22de.keyboardsurfer.android.widget%22">this link</a>.</p>

<h2>
<a name="user-content-usage" class="anchor" href="#usage" aria-hidden="true"><span class="octicon octicon-link"></span></a>Usage</h2>

<p>The API is kept as simple as the Toast API:</p>

<p>Create a Crouton for any CharSequence:</p>

<pre><code>Crouton.makeText(Activity, CharSequence, Style).show();
</code></pre>

<p>Create a Crouton with a String from your application's resources:</p>

<pre><code>Crouton.makeText(Activity, int, Style).show();
</code></pre>

<p>Further you can attach a Crouton to any ViewGroup like this:</p>

<pre><code>Crouton.makeText(Activity, int, Style, int).show();

Crouton.makeText(Activity, int, Style, ViewGroup).show();
</code></pre>

<p>Also <code>Crouton.show(...)</code> methods are available for convenient fire and forget display of Croutons. </p>

<p>If you would like a more graphical introduction to Crouton check out <a href="https://speakerdeck.com/keyboardsurfer/crouton-devfest-berlin-2012">this presentation</a>.</p>

<h2>
<a name="user-content-important" class="anchor" href="#important" aria-hidden="true"><span class="octicon octicon-link"></span></a>Important!</h2>

<p>In your Activity.onDestroy() make sure to call</p>

<pre><code>Crouton.cancelAllCroutons();
</code></pre>

<p>to cancel cancel all scheduled Croutons.</p>

<p>This is a workaround and further description is available in <a href="https://github.com/keyboardsurfer/Crouton/issues/24">issue #24</a>.</p>

<h2>
<a name="user-content-basic-examples" class="anchor" href="#basic-examples" aria-hidden="true"><span class="octicon octicon-link"></span></a>Basic Examples</h2>

<p>Currently you can use the three different Style attributes displayed below out of the box:</p>

<p><a href="https://github.com/keyboardsurfer/Crouton/raw/master/res/Alert.png" target="_blank"><img src="https://github.com/keyboardsurfer/Crouton/raw/master/res/Alert.png" alt="Alert" title="Example of Style.ALERT" style="max-width:100%;"></a></p>

<p><a href="https://github.com/keyboardsurfer/Crouton/raw/master/res/Confirm.png" target="_blank"><img src="https://github.com/keyboardsurfer/Crouton/raw/master/res/Confirm.png" alt="Confirm" title="Example of Style.CONFIRM" style="max-width:100%;"></a></p>

<p><a href="https://github.com/keyboardsurfer/Crouton/raw/master/res/Info.png" target="_blank"><img src="https://github.com/keyboardsurfer/Crouton/raw/master/res/Info.png" alt="Info" title="Example of Style.INFO" style="max-width:100%;"></a></p>

<h2>
<a name="user-content-extension-and-modification" class="anchor" href="#extension-and-modification" aria-hidden="true"><span class="octicon octicon-link"></span></a>Extension and Modification</h2>

<p>The whole design of a Crouton is defined by  <a href="https://github.com/slown1/Xamarin.Crouton/blob/master/AndroidCrouton/CroutonLibrary/Style.cs">Style</a>.</p>

<p>You can use one of the styles Crouton ships with: <strong>Style.ALERT</strong>, <strong>Style.CONFIRM</strong> and <strong>Style.INFO</strong>. Or you can create your own Style.</p>

<p>In general you can modify</p>

<ul class="task-list">
<li>display duration</li>
<li>dimension settings</li>
<li>options for the text to display</li>
<li>custom Views</li>
<li>appearance &amp; disappearance Animation</li>
<li>displayed Image</li>
</ul><p>Since <a href="https://github.com/keyboardsurfer/Crouton/blob/master/library/src/de/keyboardsurfer/android/widget/crouton/Style.java">Style</a> is the general entry point for tweaking Croutons, go and see for yourself what can be done with it.</p>

<h2>
<a name="user-content-contribution" class="anchor" href="#contribution" aria-hidden="true"><span class="octicon octicon-link"></span></a>Contribution</h2>

<h3>
<a name="user-content-questions" class="anchor" href="#questions" aria-hidden="true"><span class="octicon octicon-link"></span></a>Questions</h3>

<p>Questions regarding Crouton can be asked on <a href="http://stackoverflow.com/questions/tagged/crouton">StackOverflow, using the crouton tag</a>.</p>

<h3>
<a name="user-content-pull-requests-welcome" class="anchor" href="#pull-requests-welcome" aria-hidden="true"><span class="octicon octicon-link"></span></a>Pull requests welcome</h3>

<p>Feel free to contribute to Crouton.</p>

<p>Either you found a bug or have created a new and awesome feature, just create a pull request.</p>

<p>If you want to start to create a new feature or have any other questions regarding Crouton, <a href="https://github.com/slown1/Xamarin.Crouton/issues/new">file an issue</a>.
I'll try to answer as soon as I find the time.</p>

<p>Please note, if you're working on a pull request, make sure to use the <a href="https://github.com/keyboardsurfer/Crouton/tree/develop">develop branch</a> as your base.</p>

<h3>
<a name="user-content-formatting" class="anchor" href="#formatting" aria-hidden="true"><span class="octicon octicon-link"></span></a>Formatting</h3>

<p>For contributors using Eclipse there's a formatter available at the <a href="https://github.com/downloads/keyboardsurfer/Crouton/Crouton_Eclipseformatter.xml">download section</a>.</p>

<p>In order to reduce merging pains on my end, please use this formatter or format your commit in a way similar to it's example.</p>

<p>If you're using IDEA, the Eclipse Formatter plugin should allow you to use the formatter as well.</p>

<h2>
<a name="user-content-license" class="anchor" href="#license" aria-hidden="true"><span class="octicon octicon-link"></span></a>License</h2>

<ul class="task-list">
<li><a href="http://www.apache.org/licenses/LICENSE-2.0.html">Apache Version 2.0</a></li>
</ul><h2>
<a name="user-content-attributions" class="anchor" href="#attributions" aria-hidden="true"><span class="octicon octicon-link"></span></a>Attributions</h2>

<p>The initial version was written by  <a href="https://plus.google.com/u/0/117509657298845443204?rel=author">Benjamin Weiss</a>.
The name and the idea of <a href="https://github.com/keyboardsurfer/Crouton/blob/master/library/src/de/keyboardsurfer/android/widget/crouton/Crouton.java">Crouton</a> originates in a <a href="http://android.cyrilmottier.com/?p=773">blog article</a> by Cyril Mottier.</p>

<p>The Crouton logo has been created by <a href="http://marie-schweiz.de">Marie Schweiz</a>.</p></article>
