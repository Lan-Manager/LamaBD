using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class TourHelper
    {
        /// <summary>
        /// Retour async d'une liste contenant toutes les parties d'un tour.
        /// </summary>
        /// <param name="tour">Objet tour auquel appartient les parties.</param>
        /// <returns></returns>
        public async static Task<List<parties>> SelectAllPartieAsync(tours tour)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.parties
                            where p.idTour == tour.idTour
                            orderby p.numPartie ascending
                            select p;
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Retour async d'une liste contenant toutes les parties d'un tour.
        /// </summary>
        /// <param name="idTour">id du tour auquel appartient les parties.</param>
        /// <returns></returns>
        public async static Task<List<parties>> SelectAllPartieAsync(int idTour)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.parties
                            where p.idTour == idTour
                            orderby p.numPartie ascending
                            select p;
                return await query.ToListAsync();
            }
        }
    }
}
