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

        public static async Task<bool> CreationEquipe(List<equipes> list)
        {
            using (var ctx = new Connexion420())
            {
                //http://stackoverflow.com/questions/5943394/why-is-inserting-entities-in-ef-4-1-so-slow-compared-to-objectcontext/5943699#5943699
                ctx.Configuration.AutoDetectChangesEnabled = false;

                foreach (equipes entity in list)
                {
                    ctx.equipes.Add(entity);
                }
                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (Exception exct)
                {
                    Console.WriteLine(exct.Message);
                    return false;
                }

                return true;

            }
        }

    }
}
