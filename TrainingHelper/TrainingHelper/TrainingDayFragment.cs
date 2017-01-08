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

namespace TrainingHelper
{
    public class TrainingDayFragment : Android.Support.V4.App.Fragment
    {

        ActivitiCommunicator _activitiCommunicator;

        public TrainingDayFragment() { }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            _activitiCommunicator = (ActivitiCommunicator)context;

        }

        public static TrainingDayFragment newInstance(String excercise, String description)
        {
            TrainingDayFragment fragment = new TrainingDayFragment();
            Bundle args = new Bundle();
            args.PutString("training_excercise", excercise);
            args.PutString("training_summary", description);
            fragment.Arguments = args;

            return fragment;
        }
        public override View OnCreateView(
            LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            string excercise = Arguments.GetString("training_excercise", "");
            string description = Arguments.GetString("training_summary", "");

            View view = inflater.Inflate(Resource.Layout.TrainingDayLayout, container, false);
            TextView trainingType = (TextView)view.FindViewById(Resource.Id.training_day_type);
            EditText treningSummary = (EditText)view.FindViewById(Resource.Id.training_day_summary);

            trainingType.Text = excercise;
            treningSummary.Text = description;

            treningSummary.KeyPress += (object sender, View.KeyEventArgs e) => 
            {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    Toast.MakeText(Activity.ApplicationContext, treningSummary.Text, ToastLength.Short).Show();
                    e.Handled = true;
                }
            };

            Button saveButton = view.FindViewById<Button>(Resource.Id.save_button);
            saveButton.Click += (object sender, EventArgs e) =>
            {
                EditText text = view.FindViewById<EditText>(Resource.Id.training_day_summary);
                _activitiCommunicator.PassValue(text.Text);
            };

            return view;
        }
    }
}