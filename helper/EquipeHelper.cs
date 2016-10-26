using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class EquipeHelper
    {
        /// <summary>
        /// Retour async des equipes.
        /// Chaque equipe garanti d'avoir leur liste de participants.
        /// </summary>
        /// <returns>Une task contenant une list d'objets equipes</returns>
        public static async Task<List<equipes>> SelectAll()
        {
            using (var ctx = new Connexion420())
            {
                var query = ctx.equipes
                    .Include(x => x.equipesparticipants.Select(y => y.participants))
                    .OrderBy(x => x.nom);

                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Retour async d'une equipe selon son nom.
        /// L'equipe garanti d'avoir sa liste de participants.
        /// </summary>
        /// <returns>Une task contenant un objet equipes</returns>
        public static async Task<equipes> Select(string nom)
        {
            using (var ctx = new Connexion420())
            {
                var equipe = ctx.equipes
                    .Where(x => x.nom == nom)
                    .Include(x => x.equipesparticipants.Select(y => y.participants));

                equipes obj = await equipe.SingleOrDefaultAsync();
                return obj;
            }
        }

    }
}
