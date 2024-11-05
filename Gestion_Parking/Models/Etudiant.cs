namespace Gestion_Parking.Models
{
    public class Etudiant :Personne
    {
        public int groupe_id { get; set; }
        public Groupe Groupe { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
