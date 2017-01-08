using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainingHelper
{
    public class TrainingDay
    {

        public TrainingDay(DateTime execrisesDay, string excercise, string description)
        {
            ExcercisesDay = execrisesDay;
            Exercise = excercise;
            Description = description;
        }

        public DateTime ExcercisesDay { get; }
        public string Exercise { get; }
        public string Description { get; set; }
    }
}
