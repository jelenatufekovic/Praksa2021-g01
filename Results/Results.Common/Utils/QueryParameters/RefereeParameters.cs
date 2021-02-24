namespace Results.Common.Utils.QueryParameters
{
    public class RefereeParameters : PersonParameters
    {
        public int? Rating { get; set; }

        public override bool IsValid()
        {
            return (base.IsValid()
                &&
                Rating == null ?
                    true :
                    Rating > 0 ? Rating <= 10 : false);
        }
    }
}