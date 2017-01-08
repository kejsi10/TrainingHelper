using System;
using Android.App;
using Android.Runtime;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Java.Lang;
using System.Collections.Generic;

namespace TrainingHelper
{
    public class TrainingDayPagerAdapter : FragmentPagerAdapter
    {
        List<TrainingDay> _trainingDays;
        Android.Support.V4.App.FragmentManager _fm;

        public TrainingDayPagerAdapter(Android.Support.V4.App.FragmentManager fm, List<TrainingDay> trainingDays) : base(fm)
        {
            _trainingDays = trainingDays;
            _fm = fm;
        }

        public override int Count
        {
            get { return _trainingDays.Count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return (Android.Support.V4.App.Fragment)
               TrainingDayFragment.newInstance(_trainingDays[position].Exercise, 
                   _trainingDays[position].Description);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_trainingDays[position].ExcercisesDay.ToLongDateString());
        }

        public void test()
        {
            var frag = _fm.FindFragmentById(Resource.Id.training_day_summary);
            EditText edit = (EditText)frag.View;
            var text = edit.Text; 
        }

    }
}