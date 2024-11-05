namespace Gestion_Parking.Models
{
    public class Reservation
    {
        public int id { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly heureDebut { get; set; }
        public TimeOnly heureFin { get; set; }
        public string lieu { get ; set; }
        public int etudiant_id { get; set; }
        public int personnel_id { get; set; }
        public int placeParking_id { get; set; }

        public Etudiant Etudiant { get; set; }
        public Personnel Personnel { get; set; }
        public PlaceParking PlaceParking { get; set; }

    }
}
