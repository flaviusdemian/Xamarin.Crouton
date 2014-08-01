using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Android.OS;
using CroutonLibrary;

namespace AndroidCrouton
{
    [Activity(Label = "Crouton Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class DemoActivity : Activity, AdapterView.IOnItemSelectedListener, View.IOnClickListener
    {

        private static Style INFINITE = new StyleBuilder().SetBackgroundColor(Color.DarkCyan).Build();
        private static Configuration CONFIGURATION_INFINITE = new ConfigurationBuilder().SetDuration(Configuration.DURATION_INFINITE).Build();

        private CheckBox displayOnTop;
        private Spinner styleSpinner;
        private EditText croutonTextEdit;
        private EditText croutonDurationEdit;
        private Crouton infiniteCrouton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.crouton_demo);

            FindViewById<Button>(Resource.Id.button_show).SetOnClickListener(this);
            croutonTextEdit = FindViewById<EditText>(Resource.Id.edit_text_text);
            croutonDurationEdit = FindViewById<EditText>(Resource.Id.edit_text_duration);
            styleSpinner = FindViewById<Spinner>(Resource.Id.spinner_style);
            styleSpinner.OnItemSelectedListener = this;
            displayOnTop = FindViewById<CheckBox>(Resource.Id.display_on_top);
        }

        public void OnItemSelected(AdapterView parent, View view, int position, long id)
        {
            switch ((int)id)
            {

                case 4:
                    { // Custom Style
                        croutonTextEdit.Visibility = ViewStates.Visible;
                        croutonDurationEdit.Visibility = ViewStates.Visible;
                        break;
                    }

                case 5:
                    { // Custom View
                        croutonTextEdit.Visibility = ViewStates.Gone;
                        croutonDurationEdit.Visibility = ViewStates.Gone;
                        break;
                    }

                default:
                    {
                        croutonTextEdit.Visibility = ViewStates.Visible;
                        croutonDurationEdit.Visibility = ViewStates.Gone;
                        break;
                    }
            }
        }

        public void OnNothingSelected(AdapterView parent)
        {
            //no-op
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.button_show:
                    {
                        ShowCrouton();
                        break;
                    }

                default:
                    {
                        if (infiniteCrouton != null)
                        {
                            Crouton.Hide(infiniteCrouton);
                            infiniteCrouton = null;
                        }
                        break;
                    }
            }
        }

        private void ShowCrouton()
        {
            Style croutonStyle = GetSelectedStyleFromSpinner();

            if (croutonStyle != null)
            {
                ShowBuiltInCrouton(croutonStyle);
            }
            else
            {
                ShowAdvancedCrouton();
            }
        }

        private Style GetSelectedStyleFromSpinner()
        {
            switch ((int)styleSpinner.SelectedItemId)
            {
                case 0:
                    {
                        return Style.ALERT;
                    }

                case 1:
                    {
                        return Style.CONFIRM;
                    }

                case 2:
                    {
                        return Style.INFO;
                    }

                case 3:
                    {
                        return INFINITE;
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        private String GetCroutonText()
        {
            String croutonText = croutonTextEdit.Text.ToString().Trim();

            if (TextUtils.IsEmpty(croutonText))
            {
                croutonText = GetString(Resource.String.text_demo);
            }
            return croutonText;
        }

        private void ShowBuiltInCrouton(Style croutonStyle)
        {
            String croutonText = GetCroutonText();
            ShowCrouton(croutonText, croutonStyle, Configuration.DEFAULT);
        }

        private void ShowAdvancedCrouton()
        {
            switch (styleSpinner.SelectedItemPosition)
            {
                case 4:
                    {
                        ShowCustomCrouton();
                        break;
                    }

                case 5:
                    {
                        ShowCustomViewCrouton();
                        break;
                    }
            }
        }

        private void ShowCustomCrouton()
        {
            String croutonDurationString = GetCroutonDurationString();

            if (TextUtils.IsEmpty(croutonDurationString))
            {
                ShowCrouton(GetString(Resource.String.warning_duration), Style.ALERT, Configuration.DEFAULT);
                return;
            }

            int croutonDuration = int.Parse(croutonDurationString);
            Style croutonStyle = new StyleBuilder().Build();
            Configuration croutonConfiguration = new ConfigurationBuilder().SetDuration(croutonDuration).Build();

            String croutonText = GetCroutonText();

            ShowCrouton(croutonText, croutonStyle, croutonConfiguration);
        }

        private void ShowCustomViewCrouton()
        {
            View view = LayoutInflater.Inflate(Resource.Layout.crouton_custom_view, null);
            Crouton crouton;
            if (displayOnTop.Checked)
            {
                crouton = Crouton.Make(this, view);
            }
            else
            {
                crouton = Crouton.Make(this, view, Resource.Id.alternate_view_group);
            }
            crouton.Show();
        }

        private String GetCroutonDurationString()
        {
            return croutonDurationEdit.Text.ToString().Trim();
        }

        private void ShowCrouton(String croutonText, Style croutonStyle, Configuration configuration)
        {
            bool infinite = INFINITE == croutonStyle;

            if (infinite)
            {
                croutonText = GetString(Resource.String.infinity_text);
            }

            Crouton crouton;
            if (displayOnTop.Checked)
            {
                crouton = Crouton.MakeText(this, croutonText, croutonStyle);
            }
            else
            {
                crouton = Crouton.MakeText(this, croutonText, croutonStyle, Resource.Id.alternate_view_group);
            }
            if (infinite)
            {
                infiniteCrouton = crouton;
            }
            crouton.SetOnClickListener(this).SetConfiguration(infinite ? CONFIGURATION_INFINITE : configuration).Show();
        }
    }
}

