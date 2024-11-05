namespace Gestion_Parking.Models
{
    public class Personne
    {
        internal object groupe_id;
        internal object role;

        public int id { get; set; }
        public string nom {  get; set; }
        public string prenom {  get; set; }

        public string email { get; set; }
        public string motdepasse { get; set; }
    }
}
