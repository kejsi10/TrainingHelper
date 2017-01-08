using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;
using System;

namespace TrainingHelper
{
    [Activity(Label = "TrainingHelper", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity, ActivitiCommunicator
    {
        WordTableReader _tableReader;
        public void PassValue(string value)
        {
            var t = value;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            _tableReader = new WordTableReader("/storage/emulated/legacy/Download/Krzysztof Samson.docx");
            List<TrainingDay> trainingDays = _tableReader.ChangeTextInCell();
            var adapter = new TrainingDayPagerAdapter(SupportFragmentManager, trainingDays);
            viewPager.Adapter = adapter;
            var index = trainingDays.FindIndex(t => t.ExcercisesDay == DateTime.Today);
            viewPager.SetCurrentItem(index, true);

        }
    }
}

