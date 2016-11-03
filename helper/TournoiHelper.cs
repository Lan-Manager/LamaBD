using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class TournoiHelper
    {
        /// <summary>
        /// Retour async de tout les tournois en bd.
        /// </summary>
        /// <returns></returns>
        public async static Task<List<tournois>> SelectAllAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from t in ctx.tournois
                            orderby t.dateCreation descending
                            select t;
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Retour async du dernier tournois créé qui est toujours en cours.
        /// </summary>
        /// <returns></returns>
        public static async Task<tournois> SelectLast()
        {
            using (var ctx = new Connexion420())
            {
                var query = from t in ctx.tournois
                            where t.dateCreation == (ctx.tournois.Select(x => x.dateCreation).Max())
                            && t.enCours == true
                            select t;
                tournois obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }


        /// <summary>
        /// Retour async de l'équipe gagnante du dernier tournois.
        /// </summary>
        /// <returns></returns>
        public static async Task<equipes> SelectGagnantLast()
        {
            using (var ctx = new Connexion420())
            {
                var query = from t in ctx.tournois
                            join e in ctx.equipes on t.idEquipe_gagnante equals e.idEquipe
                            where t.dateCreation == (ctx.tournois.Select(x => x.dateCreation).Max())
                            select e;
                equipes obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }

        /// <summary>
        /// Retour async de tout les tours du dernier tournois.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<tours>> SelectToursTournois()
        {
            using (var ctx = new Connexion420())
            {
                var query = from t in ctx.tournois
                            join tour in ctx.tours on t.idTournoi equals tour.idTournoi
                            where t.dateCreation == (ctx.tournois.Select(x => x.dateCreation).Max())
                            select tour;
                return await query.ToListAsync();
            }
        }

        public static async Task<bool> CreationTournoi(tournois obj)
        {
            using (var ctx = new Connexion420())
            {
                ctx.tournois.Add(obj);
                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (DbEntityValidationException exc)
                {
                    foreach (var eve in exc.EntityValidationErrors)
                    {
                        Console.WriteLine(eve.ValidationErrors);
                        Console.WriteLine(eve.Entry);
                    }
                    return false;

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
