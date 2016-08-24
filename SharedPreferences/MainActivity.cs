using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SharedPreferences
{
    [Activity(Label = "SharedPreferences", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText edtName;
        EditText edtEmail;
        Button btnDelete;
        Button btnSave;
        ISharedPreferences pref;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            edtName = FindViewById<EditText>(Resource.Id.edtName);
            edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            pref = Application.Context.GetSharedPreferences("UserCred", FileCreationMode.Private);
            Reload();

            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
            ISharedPreferencesEditor editor = pref.Edit();
            editor.PutString("Username", edtName.Text.Trim());
            editor.PutString("Email", edtEmail.Text.Trim());
            editor.Apply();

            Reload();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ISharedPreferencesEditor editor = pref.Edit();
            editor.Remove("Username");
            editor.Remove("Email");
            editor.Apply();

            Reload();
        }

        private void Reload()
        {
            string name = pref.GetString("Username", String.Empty);
            string email = pref.GetString("Email", String.Empty);
            edtName.Text = name;
            edtEmail.Text = email;
        }
    }
}

