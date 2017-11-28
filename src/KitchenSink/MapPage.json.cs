using System.Linq;
using Starcounter;

namespace KitchenSink
{
    [Database]
    public class MapCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    partial class MapPage : Json
    {
        //Stockholm coordinates
        public readonly double DefaultLatitude = 59.3319913;
        public readonly double DefaultLongitude = 18.0765409;

        public void Init()
        {
            Position.Data = Db.SQL<MapCoordinates>("SELECT gp FROM MapCoordinates gp").FirstOrDefault()
                            ?? new MapCoordinates
                            {
                                Latitude = DefaultLatitude,
                                Longitude = DefaultLongitude
                            };
        }
    }

    [MapPage_json.Position]
    partial class MapPagePosition : Json, IBound<MapCoordinates>
    {
        static MapPagePosition() {
            DefaultTemplate.Latitude.InstanceType = typeof(double);
            DefaultTemplate.Longitude.InstanceType = typeof(double);
        }

        public void Handle(Input.ResetTrigger action)
        {
            var mapPageParent = (MapPage) Parent;
            Latitude = mapPageParent.DefaultLatitude;
            Longitude = mapPageParent.DefaultLongitude;
            PushChanges();
        }

        public void Handle(Input.Latitude action)
        {
            Latitude = action.Value;
            PushChanges();
        }

        public void Handle(Input.Longitude action)
        {
            Longitude = action.Value;
            PushChanges();
        }

        protected void PushChanges()
        {
            Transaction.Commit();
            Session.RunTaskForAll((s, sessionId) =>
            {
                var master = s.Store[nameof(MasterPage)] as MasterPage;
                if (!(master?.CurrentPage is MapPage)) return;
                s.CalculatePatchAndPushOnWebSocket();
            });
        }
    }
}