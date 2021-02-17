using Results.Model.Common;

namespace Results.Model
{
    public class Referee : Person, IReferee
    {
        public int Rating { get; set; }
    }
}
