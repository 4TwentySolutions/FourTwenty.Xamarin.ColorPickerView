using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using Com.Skydoves.Colorpickerview;
using Com.Skydoves.Colorpickerview.Listeners;
using Xamarin.Essentials;
using ActionMode = Com.Skydoves.Colorpickerview.ActionMode;

namespace FourTwenty.Xamarin.ColorPickerView.Sample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IColorListener, IDialogInterfaceOnClickListener
    {
        private View _colorDisplayView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            InitColorPickers();
        }

        private void InitColorPickers()
        {
            //Setting view to display selected colors
            _colorDisplayView = FindViewById<View>(Resource.Id.colorDisplay);
            //Set listener for view in XML
            var pickerView = FindViewById<ColorPickerViewDotNet>(Resource.Id.colorPickerView);
            pickerView.SetColorListener(this);
            //Setting up button to show dialog with color selector
            var showDialogBtn = FindViewById<Button>(Resource.Id.showButton);
            showDialogBtn.Click += ShowDialogBtnOnClick;
        }

        /// <summary>
        /// Building dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDialogBtnOnClick(object sender, EventArgs e)
        {

            var builder = new ColorPickerDialog.Builder(this);
            builder.SetTitle("ColorPicker Dialog");
            builder.SetPreferenceName("MyColorPicker");
            builder.SetPositiveButton("Select", this);
            builder.SetNegativeButton("Cancel", this)
            .Show();
        }

        /// <summary>
        /// Creating picker from code
        /// </summary>
        /// <returns></returns>
        private ColorPickerViewDotNet BuildUsingBuilder()
        {

            ColorPickerViewDotNet colorPickerView = new ColorPickerViewDotNet.Builder(this)
                .SetColorListener(this)
                .SetPreferenceName("MyColorPicker")
                .SetActionMode(ActionMode.Last)
                .SetPaletteDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.palette))
                .SetSelectorDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.wheel))
                .Build();

            return colorPickerView;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Handle colors selection
        /// </summary>
        /// <param name="rawColor"></param>
        /// <param name="fromUser"></param>
        public void OnColorSelected(int rawColor, bool fromUser)
        {
            if (!fromUser) return;
            var color = new Android.Graphics.Color(rawColor);
            _colorDisplayView.SetBackgroundColor(color);
        }

        /// <summary>
        /// Dismiss color picker dialog
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="which"></param>
        public void OnClick(IDialogInterface dialog, int which)
        {
            dialog.Dismiss();
        }
    }
}