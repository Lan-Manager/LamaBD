using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class LocalHelper
    {
        /// <summary>
        /// Retour async des locaux.
        /// Chaque local est garanti d'avoir leur liste de postes chargés, ainsi que l'état de chaque postes.
        /// </summary>
        /// <returns>Une task contenant une list d'objets locaux</returns>
        public async static Task<List<locaux>> SelectAllAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = ctx.locaux
                    .Include(x => x.postes.Select(y => y.etatspostes))
                    .OrderBy(x => x.numero);

                /*var query = from l in ctx.locaux
                            orderby l.numero ascending
                            select l;
                */
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Retour async d'un local par son numéro de local.
        /// La liste Postes associés sont garantis d'êtres présent avec leur état.
        /// </summary>
        /// <param name="numero">Numédo du local ie: (D125)</param>
        /// <returns>Une task contenant un objet locaux</returns>
        public static async Task<locaux> SelectByNumeroAsync(string numero)
        {
            using (var ctx = new Connexion420())
            {
                var local = ctx.locaux
                    .Where(x => x.numero == numero)
                    .Include(x => x.postes.Select(y => y.etatspostes));
                    //.Include(x => x.tournoislocaux.Select(y => y.tournois))
                    //.Include(x => x.tournoislocaux.Select(y => y.comptes))
                /*
                var query = from l in ctx.locaux
                            where l.numero == numero
                            select l;
                */
                locaux obj = await local.SingleOrDefaultAsync();
                return obj;
            }
        }


        /// <summary>
        /// Méthode pour mettre a jour le volontaire associé à un local.
        /// </summary>
        /// <param name="numero">Le numéro du local</param>
        /// <param name="nomUtilisateur">Le nom d'utilisateur du volontaire.</param>
        /// <returns></returns>
        public static async Task<bool> UpdateAsync(string numero, string nomUtilisateur)
        {
            using (var ctx = new Connexion420())
            {
                var tournoi = from tl in ctx.tournoislocaux
                              join l in ctx.locaux on tl.idLocal equals l.idLocal
                              where l.numero == numero
                              select tl;
                tournoislocaux toulo = await tournoi.SingleOrDefaultAsync();
                var nouvVol = from c in ctx.comptes
                              where c.nomUtilisateur == nomUtilisateur
                              select c.idCompte;
                int compt = await nouvVol.SingleOrDefaultAsync();

                toulo.idCompte = compt;

                await ctx.SaveChangesAsync();
                return true;
            }
            //Erreur possible
            return false;
        }


        /// <summary>
        /// Postes, compte ayant fait la dernière modif et l'états des postes garanti
        /// </summary>
        /// <returns></returns>
        public async static Task<List<locaux>> SelectLocauxTournoiAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from l in ctx.locaux.Include(x => x.postes.Select(y => y.etatspostes))
                            orderby l.numero ascending
                            join tl in ctx.tournoislocaux on l.idLocal equals tl.idLocal
                            join t in ctx.tournois on tl.idTournoi equals t.idTournoi
                            where t.enCours == true
                            select l;

                var locaux = ctx.locaux
                    .Where(x => query.Contains(x))
                    .Include(x => x.postes.Select(y => y.etatspostes))
                    .Include(x => x.postes.Select(y => y.comptes));

                return await locaux.ToListAsync();
            }
        }
    }
}
