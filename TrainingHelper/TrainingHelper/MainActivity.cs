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
        private WordTableReader _tableReader;
        private int _position;
        List<TrainingDay> _trainingDays;

        public void PassValue(string value)
        {
            _trainingDays[_position].Description = value;
            _tableReader.SaveChangedDocument(_trainingDays);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            _tableReader = new WordTableReader("/storage/emulated/legacy/Download/Krzysztof Samson.docx");
            _trainingDays = _tableReader.ChangeTextInCell();
            var adapter = new TrainingDayPagerAdapter(SupportFragmentManager, _trainingDays);
            viewPager.Adapter = adapter;
            var index = _trainingDays.FindIndex(t => t.ExcercisesDay == DateTime.Today);
            if (index >= 0)
            {
                viewPager.SetCurrentItem(index, true);
            }
            viewPager.PageSelected += (object sender, ViewPager.PageSelectedEventArgs e) => {
                _position = e.Position;        
            };
        }  
    }
}

